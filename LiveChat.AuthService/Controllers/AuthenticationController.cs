using LiveChat.AuthService.Data;
using LiveChat.AuthService.Models;
using LiveChat.AuthService.Models.Dtos.Requests;
using LiveChat.AuthService.Models.Dtos.Responses;
using LiveChat.AuthService.Services;
using LiveChat.AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace LiveChat.AuthService.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        public readonly ApplicationDbContext _applicationDbContext;
        public readonly UserManager<ApplicationUser> _userManager;
        public readonly ITokenService _tokenService;

        public AuthenticationController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager, ITokenService tokenService) 
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IResult> Registration(RegisterRequest request)
        {
            IdentityResult result;
            ApplicationUser user = new ApplicationUser(){ UserName = request.Email, Email = request.Email };

            result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest(result);
            }

            List<Claim> claims = new() { new Claim("Id", user.Id) };

            var response = new LoginResponse() 
            {   
                AccessToken = _tokenService.GenerateAccessToken(user, claims),
                RefreshToken = _tokenService.GenerateRefreshToken(user, claims) 
            };

            return Results.Ok(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IResult> Login(LoginRequest request)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return Results.NotFound();
            }

            var passwordValidatingResult = await _userManager.CheckPasswordAsync(user, request.Password);


            if (!passwordValidatingResult)
            {
                return Results.Forbid();
            }

            List<Claim> claims = new() { new Claim("Id", user.Id) };

            string refreshToken = _tokenService.GenerateRefreshToken(user, claims);

            user.RefreshToken = refreshToken;
            await _userManager.UpdateAsync(user);

            var response = new LoginResponse()
            {
                AccessToken = _tokenService.GenerateAccessToken(user, claims),
                RefreshToken = refreshToken
            };

            HttpContext.Response.Cookies.Append("RefreshToken", refreshToken);

            return Results.Ok(response);
        }

        [Authorize]
        [Route("[action]")]
        [HttpPost]
        public async Task<IResult> Logout()
        {
            string accessToken = HttpContext.Request.Headers["Authorization"].First()!.Split(" ").Last();
            string? refreshToken = HttpContext.Request.Cookies["RefreshToken"];

            if (refreshToken != null) 
            {
                Console.WriteLine("RefreshToken: " + refreshToken);
                HttpContext.Response.Cookies.Delete("RefreshToken");
            }


            IEnumerable<Claim> claims = _tokenService.GetClaims(accessToken);

            string id = claims.Single(p => p.Type == "Id").Value;
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Results.BadRequest();
            }
            _applicationDbContext.RevokedTokens.Add(new RevokedToken() { Token = user.RefreshToken, RevokedTime = DateTime.UtcNow });
            await _applicationDbContext.SaveChangesAsync();

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Results.Ok();
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IResult> Refresh()
        {
            HttpContext.Request.Cookies.TryGetValue("RefreshToken", out string? refreshToken);
            if (refreshToken == null)
            {
                return Results.BadRequest("There is no RefreshToken in Cookies");
            }

            IEnumerable<Claim> claims = _tokenService.GetClaims(refreshToken);

            string id = claims.Single(p => p.Type == "Id").Value;
            ApplicationUser? user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Results.BadRequest($"Unable to refresh {id}");
            }

            string newAccessToken = _tokenService.GenerateAccessToken(user, claims.ToList());
            string newRefreshToken = _tokenService.GenerateRefreshToken(user, claims.ToList());

            var loginResponse = new LoginResponse()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };

            HttpContext.Response.Cookies.Append("RefreshToken", newRefreshToken);


            return Results.Ok(loginResponse);
        }

    }
}
