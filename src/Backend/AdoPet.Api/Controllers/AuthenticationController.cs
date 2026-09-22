using AdoPet.Application.UseCases.Login.LoginWithEmailAndPassword;
using AdoPet.Communication.Requests;
using AdoPet.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace AdoPet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> LoginWithEmail([FromBody] RequestLoginJson request, [FromServices] ILoginWithEmailAndPasswordUseCase useCase)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
