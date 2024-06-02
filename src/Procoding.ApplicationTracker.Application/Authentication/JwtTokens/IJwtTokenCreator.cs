using System;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Procoding.ApplicationTracker.Application.Authentication.JwtTokens;

public interface IJwtTokenCreator<T>
{
    string CreateJwtToken(List<Claim> claims);

    SigningCredentials GetDefaultSigningCredentials(string secretkey);
}

