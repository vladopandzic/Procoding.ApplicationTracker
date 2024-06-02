using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Application.Authentication.Claims;

public static class ClaimsFactory
{
    public static List<Claim> CreateClaims(string userEmail, string userId, string name, string surname)
    {
        var claimValues = new (string Type, string Value)[]
        {
            (JwtRegisteredClaimNames.Email, userEmail ?? ""),
            (JwtRegisteredClaimNames.Sub, userId),
            (JwtRegisteredClaimNames.GivenName, name),
            (JwtRegisteredClaimNames.FamilyName, surname)
        };

        return claimValues
            .Select(cv => new Claim(cv.Type, cv.Value))
            .ToList();
    }
}