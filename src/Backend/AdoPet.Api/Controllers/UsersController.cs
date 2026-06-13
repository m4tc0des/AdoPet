using AdoPet.Application.UseCases.User.Register;
using AdoPet.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AdoPet.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    public IActionResult Register([FromBody] RequestsRegisterUserJson request, [FromServices] IRegisterUserUseCase useCase)
    {
        useCase.Execute(request);

        return Created();
    }
}
