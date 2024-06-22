using LiveChat.Application.Features.MessageFeatures.Query;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LiveChat.API.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        [Authorize]
        [Route("[action]")]
        [HttpGet]
        public async Task<IResult> GetAllMessages(ISender sender)
        {
            return Results.Ok(await sender.Send(new GetAllMessagesQuery()));
        }
    }
}
