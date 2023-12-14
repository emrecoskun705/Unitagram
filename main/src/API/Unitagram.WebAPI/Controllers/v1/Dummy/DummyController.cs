using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Unitagram.WebAPI.Controllers.v1.Dummy;

[ApiVersion("1.0")]
public class DummyController : CustomControllerBase
{
    [HttpPost("")]
    public async Task<ActionResult<Guid>> Dummy(
        CancellationToken cancellationToken)
    {
        var user = User.Identity as ClaimsIdentity;

        long.TryParse(user.FindFirst("exp").Value, out long expirationTimeEpoch);
        //
        // string jwtToken = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJBaWc0Wk9jUzQtSndjMVpKSS1IY2prVHpKSXlKVEdvWW9xWVB5cTRVVVdFIn0.eyJleHAiOjE3MDIxNDgxODEsImlhdCI6MTcwMjE0ODEyMSwianRpIjoiOGNlOTViNDctODA0Ny00MzM5LWI2N2QtOGIyMWI5YzU2NDBmIiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo4MDEwL2F1dGgvcmVhbG1zL1VuaXRhZ3JhbSIsImF1ZCI6ImFjY291bnQiLCJzdWIiOiI3NjY0YWM5MS1iZmY3LTQ3YTQtYTcxNS01NGMzZTMwNjBhMjIiLCJ0eXAiOiJCZWFyZXIiLCJhenAiOiJ1bml0YWdyYW0tYXV0aC1jbGllbnQiLCJzZXNzaW9uX3N0YXRlIjoiYzhkOWViY2YtMGJmMy00M2Y5LWI1MmEtOGE3YjVhZjg3N2EyIiwiYWNyIjoiMSIsInJlYWxtX2FjY2VzcyI6eyJyb2xlcyI6WyJvZmZsaW5lX2FjY2VzcyIsImRlZmF1bHQtcm9sZXMtdW5pdGFncmFtIiwidW1hX2F1dGhvcml6YXRpb24iXX0sInJlc291cmNlX2FjY2VzcyI6eyJhY2NvdW50Ijp7InJvbGVzIjpbIm1hbmFnZS1hY2NvdW50IiwibWFuYWdlLWFjY291bnQtbGlua3MiLCJ2aWV3LXByb2ZpbGUiXX19LCJzY29wZSI6Im9wZW5pZCBlbWFpbCBwcm9maWxlIiwic2lkIjoiYzhkOWViY2YtMGJmMy00M2Y5LWI1MmEtOGE3YjVhZjg3N2EyIiwiZW1haWxfdmVyaWZpZWQiOnRydWUsInByZWZlcnJlZF91c2VybmFtZSI6ImFzZHNkczEiLCJnaXZlbl9uYW1lIjoiIiwiZmFtaWx5X25hbWUiOiIiLCJlbWFpbCI6ImVtcmUxQGVtcmUuY29tIn0.RYws3T7OGsNSPvJISrfnv_qMa42UGDE0dQzLF-raNgm1ESfeiMf7MKuVkR_kKEDg9IfWlLh8IX6pNoHaPlp4X5LFSruC5vkFtT_gXaK3JAPr9Mq2C4vyxSdSIziWYEjGfegzVQOFwre2qiEGEsm91JGcCePZXJMfyUmkdAbVN64j1JV8uoKgKn_uxhxSVsnSpaZZyooq1jfxgpkuU0oGa16pMy72OmozidZOhJcKmiwmNvBLRGP0yKKsEm4C-eclRV10WrzZaXh0SFAs4-Ob4uTP268LMERcTOtBYTRlVzTmWKKcpUTT0dc20iDaVDvYCAU-K1vsVTCxz2zZXoSOWA"; // Replace with the actual JWT token
        //
        // var tokenHandler = new JwtSecurityTokenHandler();
        //
        // try
        // {
        //     // Read and validate the JWT token
        //     var jsonToken = tokenHandler.ReadToken(jwtToken) as JwtSecurityToken;
        //
        //     if (jsonToken != null)
        //     {
        //         // Access the expiration time from the token
        //         DateTime expirationTime = jsonToken.ValidTo;
        //
        //         // Print the expiration date
        //         Console.WriteLine($"Token will expire on: {expirationTime}");
        //     }
        //     else
        //     {
        //         Console.WriteLine("Failed to read or validate the JWT token.");
        //     }
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"An error occurred: {ex.Message}");
        // }
        
        var findFirst = DateTimeOffset.FromUnixTimeSeconds(expirationTimeEpoch).UtcDateTime;
        var findFirst2 = DateTimeOffset.FromUnixTimeSeconds(expirationTimeEpoch).DateTime;
        
        return Ok(DateTime.Now);
    }
}