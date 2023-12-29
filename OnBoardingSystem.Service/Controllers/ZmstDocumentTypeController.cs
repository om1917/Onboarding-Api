
//-----------------------------------------------------------------------
// <copyright file="ZmstDocumentTypeController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using Serilog;
    using AbsModels = OnBoardingSystem.Data.Abstractions.Models;

    /// <summary>
    /// ZmstDocumentTypeController.
    /// </summary>

    public class ZmstDocumentTypeController : ControllerBase
    {
        private readonly IZmstDocumentTypeDirector zmstdocumenttypeDirector;
        private readonly ILogger<ZmstDocumentTypeController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZmstDocumentTypeController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="zmstdocumenttypeDirector">zmstdocumenttypeDirector.</param>
        /// <param name="logger">logger.</param>
        public ZmstDocumentTypeController(IZmstDocumentTypeDirector zmstdocumenttypeDirector, ILogger<ZmstDocumentTypeController> logger)
        {
            this.zmstdocumenttypeDirector = zmstdocumenttypeDirector;
            this.logger = logger;
        }

        /// <summary>
        /// Get ZmstDocumentType List.
        /// </summary>
        /// <returns>Get All ZmstDocumentType List.</returns>        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AbsModels.ZmstDocumentType>>> GetAllAsync()
        {
            try
            {
                return await zmstdocumenttypeDirector.GetAllAsync(default).ConfigureAwait(false);
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

        /// <summary>
        /// Get ZmstDocumentType List By Id.
        /// </summary>
        /// <param name="DocumentTypeId">DocumentTypeId.</param>
        /// <returns>Get by id.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.ZmstDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.ZmstDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.ZmstDocumentType>> GetByIdAsync(string DocumentTypeId)
        {
            try
            {
                var response = await zmstdocumenttypeDirector.GetByIdAsync(DocumentTypeId, default).ConfigureAwait(false);
                return response == null ? Created(string.Empty, response) : Ok(response);
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

        /// <summary>
        /// Insert ZmstDocumentType.
        /// </summary>
        /// <param name="zmstdocumenttype">zmstdocumenttype.</param>
        /// <returns>Effected Row.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> InsertAsync([FromBody] AbsModels.ZmstDocumentType zmstdocumenttype)
        {
            if (zmstdocumenttype == null)
            {
                return BadRequest(zmstdocumenttype);
            }

            try
            {
                var response = await zmstdocumenttypeDirector.InsertAsync(zmstdocumenttype, default).ConfigureAwait(false);
                return response > 0 ? Created(string.Empty, response) : Ok(response);
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

        /// <summary>
        /// Update to ZmstDocumentType.
        /// </summary>
        /// <param name="zmstdocumenttype">zmstdocumenttype.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> UpdateAsync([FromBody] AbsModels.ZmstDocumentType zmstdocumenttype)
        {
            if (zmstdocumenttype == null)
            {
                return BadRequest(nameof(zmstdocumenttype));
            }

            if (zmstdocumenttype.DocumentTypeId == "0")
            {
                return BadRequest(nameof(zmstdocumenttype.DocumentTypeId));
            }

            try
            {
                var response = await zmstdocumenttypeDirector.UpdateAsync(zmstdocumenttype, default).ConfigureAwait(false);
                return response > 0 ? this.Ok(response) : BadRequest();
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

        /// <summary>
        /// Delete ZmstDocumentType.
        /// </summary>
        /// <param name="DocumentTypeId">DocumentTypeId.</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(int), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(int), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> DeleteAsync(string DocumentTypeId)
        {
            string status;
            try
            {
                var response = await zmstdocumenttypeDirector.DeleteAsync(DocumentTypeId, default).ConfigureAwait(false);

                if (response > 0)
                {
                    status = "\"Delete Successfully\"";
                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response > 0 ? Created(string.Empty, status) : Ok(status);
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