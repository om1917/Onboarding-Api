//-----------------------------------------------------------------------
// <copyright file="AgencyController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using OnBoardingSystem.Data.Abstractions.Exceptions;
using OnBoardingSystem.Data.Business.Behaviors;

namespace OnBoardingSystem.Service.Controllers
{
    using System.Text.Json;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
    using Serilog;

    /// <summary>
    /// AppOnboardingDetailsController.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController:ControllerBase
    {
        private readonly IAgencyDirector iagency;

        /// <summary>
        /// Initializes a new instance of the <see cref="AgencyController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingdetail">iAppOnboardingdetail.</param>

        public AgencyController(IAgencyDirector _iagency)
        {
            this.iagency = _iagency;
        }

        /// <summary>
        /// Agency List.
        /// </summary>
        /// <returns>GetAll.</returns>
        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.MdAgency), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetAll")]
        public async Task<ActionResult<List<AbsModels.MdAgency>>> GetAsync()
        {
            try
            {
                return await iagency.GetAllAsync(default).ConfigureAwait(false);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            catch (EntityFoundException entityFoundEx)
            {
                Serilog.Log.Information(entityFoundEx.Message);
                return new StatusCodeResult(StatusCodes.Status409Conflict);
            }
            catch (System.Exception ex)
            {
                Serilog.Log.Information(ex.Message);
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
       }
}
