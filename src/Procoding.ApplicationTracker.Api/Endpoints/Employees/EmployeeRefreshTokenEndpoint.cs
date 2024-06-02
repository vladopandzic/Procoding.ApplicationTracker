//using Azure;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Procoding.ApplicationTracker.Application.Authentication;
//using System.Globalization;

//namespace Procoding.ApplicationTracker.Api.Endpoints.Employees;

//public class EmployeeRefreshTokenEndpoint
//{
//    [HttpPost("employee/refresh")]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<List<AlecsReportResponseDto>>))]
//    [AllowAnonymous]
//    [ApiExplorerSettings(IgnoreApi = true)]
//    public async Task<IActionResult> Refresh([FromBody] TokenModel tokenModel)
//    {
//        var refreshToken = await _refreshTokenService.GetByToken(tokenModel.RefreshToken);

//        if (refreshToken == null || refreshToken.HasExpired() || refreshToken.Invalidated)
//        {
//            return BadRequest("Invalid refresh token");
//        }
//        string userIdFromToken = GetUserIdFromAccessToken(tokenModel);
//        var user = (await _userManager.FindByIdAsync(userIdFromToken))!;

//        if (userIdFromToken != refreshToken.UserId.ToString(CultureInfo.InvariantCulture))
//        {
//            return BadRequest("Invalid refresh token");
//        }
//        if (user.DeletedAt != null)
//        {
//            return BadRequest("Invalid refresh token!");
//        }

//        await _refreshTokenService.MarkAsUsed(refreshToken);

//        TokenResponse tokenResponse = await _authService.GenerateTokenFromUser(user);

//        return Ok(tokenResponse);
//    }

//}
