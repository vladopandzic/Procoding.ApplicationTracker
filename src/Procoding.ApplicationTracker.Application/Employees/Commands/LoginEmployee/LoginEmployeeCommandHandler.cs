using LanguageExt.Common;
using Microsoft.AspNetCore.Identity;
using Procoding.ApplicationTracker.Application.Authentication;
using Procoding.ApplicationTracker.Application.Authentication.Claims;
using Procoding.ApplicationTracker.Application.Authentication.JwtTokens;
using Procoding.ApplicationTracker.Application.Core.Abstractions.Messaging;
using Procoding.ApplicationTracker.Application.Employees.Commands.InsertEmployee;
using Procoding.ApplicationTracker.Domain.Abstractions;
using Procoding.ApplicationTracker.Domain.Entities;
using Procoding.ApplicationTracker.Domain.Exceptions;
using Procoding.ApplicationTracker.Domain.Repositories;
using Procoding.ApplicationTracker.DTOs.Response.Employees;
using System;

namespace Procoding.ApplicationTracker.Application.Employees.Commands.LoginEmployee;

internal class LoginEmployeeCommandHandler : ICommandHandler<LoginEmployeeCommand, EmployeeLoginResponseDTO>
{
    readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly UserManager<Employee> _userManager;
    private readonly TimeProvider _timeProvider;
    private readonly IJwtTokenCreator<Employee> _jwtTokenCreator;
    private readonly JwtTokenOptions<Employee> _jwtTokenOptions;
    private readonly IUnitOfWork _unitOfWork;

    public LoginEmployeeCommandHandler(IRefreshTokenRepository refreshTokenRepository,
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

    public async Task<Result<EmployeeLoginResponseDTO>> Handle(LoginEmployeeCommand request, CancellationToken cancellationToken)
    {
        Employee? employee = await _userManager.FindByEmailAsync(request.Email);

        if (employee is null || employee.DeletedOnUtc is not null || await _userManager.CheckPasswordAsync(employee, request.Password) == false)
        {
            return new Result<EmployeeLoginResponseDTO>(new Unauthorized401Exception("Invalid username or password"));
        }

        var claims = ClaimsFactory.CreateClaims(userEmail: employee.Email.ToString()!,
                                                userId: employee.Id.ToString(),
                                                name: employee.Name,
                                                surname: employee.Surname);

        claims.AddRange(ClaimsFactory.CreateEmployeeClaims());

        var expiryDate = _timeProvider.GetLocalNow().AddMonths(6);

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
}
