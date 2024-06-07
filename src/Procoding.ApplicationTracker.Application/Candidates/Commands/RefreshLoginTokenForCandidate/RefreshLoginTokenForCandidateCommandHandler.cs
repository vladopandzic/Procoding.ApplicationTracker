using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Authentication.Claims;
using Procoding.ApplicationTracker.Application.Authentication.JwtTokens;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Response.Candidates;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System.IdentityModel.Tokens.Jwt;

namespace Procoding.ApplicationTracker.Application.Candidates.Commands.RefreshLoginTokenForCandidate;

internal class RefreshLoginTokenForCandidateCommandHandler : ICommandHandler<RefreshLoginTokenForCandidateCommand, CandidateLoginResponseDTO>
{
    readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<Candidate> _userManager;
    private readonly TimeProvider _timeProvider;
    private readonly IJwtTokenCreator<Candidate> _jwtTokenCreator;
    private readonly JwtTokenOptions<Candidate> _jwtTokenOptions;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshLoginTokenForCandidateCommandHandler(IRefreshTokenRepository refreshTokenRepository,
                                                       UserManager<Candidate> userManager,
                                                       TimeProvider timeProvider,
                                                       IJwtTokenCreator<Candidate> jwtTokenCreator,
                                                       JwtTokenOptions<Candidate> jwtTokenOptions,
                                                       IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _timeProvider = timeProvider;
        _jwtTokenCreator = jwtTokenCreator;
        _jwtTokenOptions = jwtTokenOptions;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<CandidateLoginResponseDTO>> Handle(RefreshLoginTokenForCandidateCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByToken(request.RefreshToken);

        if (refreshToken is null || refreshToken.HasExpired(_timeProvider) || refreshToken.Invalidated)
        {
            return new Result<CandidateLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }
        string userIdFromToken = GetUserIdFromAccessToken(request.AccessToken);

        var employee = (await _userManager.FindByIdAsync(userIdFromToken))!;

        if (employee is null)
        {
            return new Result<CandidateLoginResponseDTO>(new ValidationException("Not his refresh token!"));
        }

        if (userIdFromToken != refreshToken.EmployeeId.ToString())
        {
            return new Result<CandidateLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }
        if (employee.DeletedOnUtc != null)
        {
            return new Result<CandidateLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }

        await _refreshTokenRepository.MarkAsUsed(refreshToken);

        var expiryDate = _timeProvider.GetLocalNow().AddMonths(6);

        var claims = ClaimsFactory.CreateClaims(userEmail: employee.Email.ToString()!,
                                              userId: employee.Id.ToString(),
                                              name: employee.Name,
                                              surname: employee.Surname);

        claims.AddRange(ClaimsFactory.CreateCandidateClaims());

        var tokenResponse = new CandidateLoginResponseDTO()
        {
            AccessToken = _jwtTokenCreator.CreateJwtToken(claims),
            RefreshToken = GenerateRefreshToken(),
            ExpiresIn = _jwtTokenOptions.ExpiresInSeconds,
            TokenType = "Bearer"
        };

        await _refreshTokenRepository.Insert(new Domain.Auth.RefreshToken(expiryDate: expiryDate,
                                                                          accessToken: tokenResponse.AccessToken,
                                                                          refreshToken: tokenResponse.RefreshToken,
                                                                          employeeId: employee.Id));

        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return tokenResponse;
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    private static string GetUserIdFromAccessToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(accessToken);
        var userId = token.Subject;
        return userId;
    }
}
