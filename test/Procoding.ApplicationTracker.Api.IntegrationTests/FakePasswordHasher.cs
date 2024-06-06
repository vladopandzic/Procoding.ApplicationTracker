using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Procoding.ApplicationTracker.Api.IntegrationTests;

internal class FakePasswordHasher<T> : IPasswordHasher<T> where T : class
{
    public string HashPassword(T user, string password)
    {
        return password;
    }

    public PasswordVerificationResult VerifyHashedPassword(T user, string hashedPassword, string providedPassword)
    {
        return PasswordVerificationResult.Success;
    }
}
