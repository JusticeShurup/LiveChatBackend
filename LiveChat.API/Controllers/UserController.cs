﻿using LiveChat.Application.Dtos;
using LiveChat.Application.Features.UserFeatures.Command;
using LiveChat.Application.Features.UserFeatures.Query;
using LiveChat.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LiveChat.API.Controllers
{

    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IHttpContextAccessor _httpContext;

        public UserController(IHttpContextAccessor httpContext) 
        {
            _httpContext = httpContext;
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IResult> CreateUser(ISender sender, CreateUserCommand command)
        {
            await sender.Send(command);

            return Results.Created();
        }

        [Authorize]
        [Route("[action]")]
        [HttpPut]
        public async Task<IResult> UpdateUserProfile(ISender sender, [FromBody] UpdateUserProfileCommand command)
        {
            await sender.Send(command);

            return Results.Ok();
        }

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<IResult> GetUserImageBase64(ISender sender)
        {
            var response = await sender.Send(new GetImageQuery());

            return Results.Ok(response);
        }

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<IResult> GetAllUsers(ISender sender, [FromQuery] GetAllUsersQuery query)
        {
            var response = await sender.Send(query);

            return Results.Ok(response);
        }

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<IResult> GetUser(ISender sender)
        {
            var response = await sender.Send(new GetUserQuery());

            return Results.Ok(response);
        }

    }
}
