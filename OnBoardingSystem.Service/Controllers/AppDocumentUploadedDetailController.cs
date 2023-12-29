//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Http;
	using Microsoft.AspNetCore.Mvc;
	using OnBoardingSystem.Data.Abstractions.Behaviors;
	using OnBoardingSystem.Data.Abstractions.Exceptions;
	using OnBoardingSystem.Data.Abstractions.Models;
	using OnBoardingSystem.Data.EF.Models;
	using Serilog;
	using AbsModels = OnBoardingSystem.Data.Abstractions.Models;
	using System.Text.Json;
	using Microsoft.AspNetCore.Authorization;

    /// <summary>
    /// AppDocumentUploadedDetailController.
    /// </summary>
    public class AppDocumentUploadedDetailController : ControllerBase
    {
        private readonly IAppDocumentUploadedDetailDirector iappDocumentUploadedDetail;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailController"/> class.
        /// Constructor.
        /// </summary>
        /// <param name="_appDocumentUploadedDetail">iAppDocumentUploadedDetail.</param>

        public AppDocumentUploadedDetailController(IHttpContextAccessor httpContextAccessor, IAppDocumentUploadedDetailDirector _appDocumentUploadedDetail)
        {
            this.iappDocumentUploadedDetail = _appDocumentUploadedDetail;
            this._httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="appDocumentUploadedDetail">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAsync([FromBody] List<AbsModels.AppDocumentUploadedDetail> appDocumentUploadedDetail)
        {
            try
            {
                string status;
                appDocumentUploadedDetail[0].IpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                var jsonString = JsonSerializer.Serialize(appDocumentUploadedDetail);
                var response = await iappDocumentUploadedDetail.Save(appDocumentUploadedDetail, default).ConfigureAwait(false);
                if (response == true)
                {
                    status = "\"Data Stored Successfully\"";

                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response == true ? Created(string.Empty, status) : Ok(status);
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

        /// <summary>
        /// Get appDocumentUploadedDetail List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppDocumentUploadAndDocumentType>>> GetByRequestIdAsync([FromBody] AppDocumentFilter appDocFilter)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.GetByRequestId(appDocFilter, default).ConfigureAwait(false);
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

        /// <summary>
        /// Get appDocumentUploadedDetail List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        //[Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppDocumentUploadAndDocumentType>>> GetByProjectDetailIdAsync(int id)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.GetByProjectDetailId(id, default).ConfigureAwait(false);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppDocumentUploadedDetail>> GetDocumentByRequestIdAsync([FromBody] AppDocActivity appDoc)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.GetDocumentByRequestId(appDoc, default).ConfigureAwait(false);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppDocumentUploadedDetail>> GetDocumentByDocTypeAsync([FromBody] AppDocumentFilter employeeDocDetails)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.GetDocumentByDocType(employeeDocDetails, default).ConfigureAwait(false);
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

        /// <summary>
        /// Get appDocumentUploadedDetail List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<AbsModels.AppDocumentUploadedDetail>> GetByIdAsync(int Id)
        {
            var response = await iappDocumentUploadedDetail.GetById(Id, default).ConfigureAwait(false);
            return response == null ? Created(string.Empty, response) : Ok(response);
        }
        /// <summary>
        /// Get appDocumentUploadedDetail List by id.
        /// </summary>
        /// <param name="requestId">appOnboardingRequest List by id.</param>
        /// <returns>get by id.</returns>

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppDocumentUploadAndDocumentType>>> UserMenuByRequestIdAsync(string requestId)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.UserMenuByRequestId(requestId, default).ConfigureAwait(false);
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

        [Authorize]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<AppDocumentUploadAndDocumentType>>> ModuleRefId(string ModuleRefId)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.ModuleRefId(ModuleRefId, default).ConfigureAwait(false);
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

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AppDocumentUploadAndDocumentType), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<int> Saveprofilephoto([FromBody] AbsModels.AppDocumentUploadedDetail appDocumentUploadedDetail)
        {
            try
            {
                var response = await iappDocumentUploadedDetail.Saveprofilephoto(appDocumentUploadedDetail, default).ConfigureAwait(false);
                return response;// == null ? Created(string.Empty, response) : Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Insert By EFCore.
        /// </summary>
        /// <param name="appDocumentUploadedDetail">mdMinistry.</param>
        /// <returns>Effected Row.</returns>

        [Authorize]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(AbsModels.AppDocumentUploadedDetail), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> InsertAndUpdateActivityStatus([FromBody] AbsModels.AppDocumentUploadedDetail appDocumentUploadedDetail)
        {

            string status = "";
            try
            {
                var response = await iappDocumentUploadedDetail.InsertAndUpdateActivityStatus(appDocumentUploadedDetail, default).ConfigureAwait(false);
                if (response == true)
                {
                    status = "\"Data Stored Successfully\"";

                }
                else
                {
                    status = "\"Try Again\"";
                }

                return response == true ? Created(string.Empty, status) : Ok(status);
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
