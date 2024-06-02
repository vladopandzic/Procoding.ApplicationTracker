using FluentValidation;
using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Authentication.Claims;
using Procoding.ApplicationTracker.Application.Authentication.JwtTokens;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.RefreshLoginTokenForEmployee;

internal class RefreshLoginTokenForEmployeeCommandHandler : ICommandHandler<RefreshLoginTokenForEmployeeCommand, EmployeeLoginResponseDTO>
{
    readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<Employee> _userManager;
    private readonly TimeProvider _timeProvider;
    private readonly IJwtTokenCreator<Employee> _jwtTokenCreator;
    private readonly JwtTokenOptions<Employee> _jwtTokenOptions;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshLoginTokenForEmployeeCommandHandler(IRefreshTokenRepository refreshTokenRepository,
                                       UserManager<Employee> userManager,
                                       TimeProvider timeProvider,
                                       IJwtTokenCreator<Employee> jwtTokenCreator,
                                       JwtTokenOptions<Employee> jwtTokenOptions,
                                       IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userManager = userManager;
        _timeProvider = timeProvider;
        _jwtTokenCreator = jwtTokenCreator;
        _jwtTokenOptions = jwtTokenOptions;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmployeeLoginResponseDTO>> Handle(RefreshLoginTokenForEmployeeCommand request, CancellationToken cancellationToken)
    {
        var refreshToken = await _refreshTokenRepository.GetByToken(request.RefreshToken);

        if (refreshToken is null || refreshToken.HasExpired(_timeProvider) || refreshToken.Invalidated)
        {
            return new Result<EmployeeLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }
        string userIdFromToken = GetUserIdFromAccessToken(request.AccessToken);

        var employee = (await _userManager.FindByIdAsync(userIdFromToken))!;

        if (userIdFromToken != refreshToken.EmployeeId.ToString())
        {
            return new Result<EmployeeLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }
        if (employee.DeletedOnUtc != null)
        {
            return new Result<EmployeeLoginResponseDTO>(new ValidationException("Invalid refresh token"));
        }

        await _refreshTokenRepository.MarkAsUsed(refreshToken);

        var expiryDate = _timeProvider.GetLocalNow().AddMonths(6);

        var claims = ClaimsFactory.CreateClaims(userEmail: employee.Email.ToString()!,
                                              userId: employee.Id.ToString(),
                                              name: employee.Name,
                                              surname: employee.Surname);

        var tokenResponse = new EmployeeLoginResponseDTO()
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
