using LiveChat.Application.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace LiveChat.API.Middlewares
{
    public class UserMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Headers["Authorization"].FirstOrDefault() == "Bearer null")
            {
                await next.Invoke(context);
                return;
            }

            if (context.Request.Headers["Authorization"].FirstOrDefault() == null)
            {
                await next.Invoke(context);
                return;
            }

            string tokenString = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;

            JwtSecurityToken token;
            token = new JwtSecurityToken(tokenString);

            string userId = (string)token.Payload.GetValueOrDefault("Id");

            context.User.Claims.Append(new System.Security.Claims.Claim("Id", userId));

            await next.Invoke(context);
            return;
        }
    }
}
