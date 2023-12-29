using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnBoardingSystem.Data.Abstractions.Behaviors;
using OnBoardingSystem.Data.Abstractions.Exceptions;
using OnBoardingSystem.Data.Abstractions.Models;
using OnBoardingSystem.Data.Business.Behaviors;
using Serilog;

namespace OnBoardingSystem.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationAPISecureKeyController : ControllerBase
    {
        private readonly IConfigurationAPISecureKeyDirector configurationAPISecureKeyDirector;
        private readonly ILogger<ConfigurationAPISecureKeyController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationAPISecureKeyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="configurationAPISecureKeyDirector">ConfigurationAPISecureKeyDirector.</param>
        /// <param name="logger">logger.</param>
        public ConfigurationAPISecureKeyController(IConfigurationAPISecureKeyDirector _configurationAPISecureKeyDirector, ILogger<ConfigurationAPISecureKeyController> logger)
        {
            this.configurationAPISecureKeyDirector = _configurationAPISecureKeyDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ConfigurationAPISecureKey List.
        /// </summary>
        /// <returns>Get All ConfigurationAPISecureKey List.</returns>
        [HttpGet]
        [Route("GetAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ConfigurationApisecureKey), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ConfigurationApisecureKey), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ConfigurationApisecureKey>>> GetAllAsync()
        {
            try
            {
                return await configurationAPISecureKeyDirector.GetAllAsync(default).ConfigureAwait(false);
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
