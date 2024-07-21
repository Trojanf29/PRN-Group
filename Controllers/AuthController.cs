using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;
using CloudinaryDotNet.Actions;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authentication.Google;
using System.Data;

namespace Bloggie_Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    public IActionResult OAuth()
    {
        return Challenge(
            new AuthenticationProperties() { RedirectUri = $"/api/auth/signin-google" },
            GoogleDefaults.AuthenticationScheme
        );
    }

    [HttpGet("signin-google")]
    public async Task<IActionResult> OAuthCallback()
    {
        var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!authenticateResult.Succeeded)
            return RedirectToPage("/Index");

        var externalClaims = authenticateResult.Principal.Identities
            .FirstOrDefault()?.Claims;

        var emailClaim = externalClaims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var nameClaim = externalClaims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        var user = await _userManager.FindByEmailAsync(emailClaim);
        if (user == null)
        {
            user = new IdentityUser { UserName = emailClaim, Email = emailClaim };
            await _userManager.CreateAsync(user);
            await _userManager.AddLoginAsync(user, new UserLoginInfo("Google", authenticateResult.Principal.FindFirstValue(ClaimTypes.NameIdentifier), "Google"));
        }

        await _signInManager.SignInAsync(user, false);

        return Redirect("/");
    }
}
