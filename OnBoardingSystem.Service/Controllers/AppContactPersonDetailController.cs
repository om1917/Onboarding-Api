//-----------------------------------------------------------------------
// <copyright file="AppOnboardingDetailsController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

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

    /// <summary>
    /// AppOnboardingDetailsController.
    /// </summary>
    public class AppContactPersonDetailController : ControllerBase
    {
        private readonly IAppContactPersonDetailDirector contactPersonDetail;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppOnboardingDetailsController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iAppOnboardingdetail">iAppOnboardingdetail.</param>

        public AppContactPersonDetailController(IAppContactPersonDetailDirector _contactPersonDetail)
        {
            this.contactPersonDetail = _contactPersonDetail;
        }

        /// <summary>
        /// Get Ministry List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppContactPersonDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppContactPersonDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppContactPersonDetails>>> GetAllAsync()
        {
            try
            {
                return await contactPersonDetail.GetAllAsync(default).ConfigureAwait(false);
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

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppContactPersonDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppContactPersonDetails), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.AppContactPersonDetails>>> GetByRequestIdAsync(string requestId)
        {
            try
            {
                var response = await contactPersonDetail.GetByRequestIdAsync(requestId, default).ConfigureAwait(false);
                return response == null ? Created(string.Empty, response) : Ok(response);
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