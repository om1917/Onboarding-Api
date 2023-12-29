//-----------------------------------------------------------------------
// <copyright file="MdDocumentTypeController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Azure.Core;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using Serilog;

    [Route("api/[controller]")]
    [ApiController]
    public class MDModuleController : ControllerBase
    {
        private readonly IMDModuleDirector iMDModule;
        /// <summary>
        /// Initializes a new instance of the <see cref="MinistryController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="iMdMinistry">IMdMinistry.</param>
        public MDModuleController(IMDModuleDirector _iMDModule)
        {
            this.iMDModule = _iMDModule;
        }

        /// <summary>
        /// Get Ministry List.
        /// </summary>
        /// <returns>GetAll.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MDModule), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Data.Abstractions.Models.MDModule), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [Route("GetByUserId")]
        public async Task<ActionResult<Data.Abstractions.Models.MDModule>> GetByUserIdAsync(string userId)
        {
            try
            {
                var response = await iMDModule.GetByUserIdAsync(userId, default).ConfigureAwait(false);
                return response == null ? Created(string.Empty, response) : Ok(response);
            }
            catch (Abp.Domain.Entities.EntityNotFoundException ex)
            {
                Serilog.Log.Information(ex.Message);
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
