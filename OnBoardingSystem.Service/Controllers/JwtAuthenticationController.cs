

namespace OnBoardingSystem.Service.Controllers
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Behaviors;
    using Castle.DynamicProxy.Generators.Emitters.SimpleAST;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using Serilog;

    [Route("api/[controller]")]
    [ApiController]
    public class JwtAuthenticationController : ControllerBase
    {
        private readonly JwtAuthenticationDirector _authenticationDirector;

        public JwtAuthenticationController(JwtAuthenticationDirector authenticationDirector) 
        {
            _authenticationDirector = authenticationDirector;
        }

        [AllowAnonymous]
        [HttpPost("Authorize")]
        public IActionResult AuthUser([FromBody]UserInfo user)
        {
            try
            {
                var token = _authenticationDirector.Authenticate(user.Username, user.Password);
                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            catch (EntityFoundException entityFoundEx)
            {
                Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
