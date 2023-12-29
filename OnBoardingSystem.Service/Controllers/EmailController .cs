//-----------------------------------------------------------------------
// <copyright file="EmailController.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

namespace OnBoardingSystem.Service.Controllers
{
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using Serilog;
    /// <summary>
    /// EmailController .
    /// </summary>
    public class EmailController : Controller
    {
        private readonly IMailServiceDirector mailService;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmailController(IMailServiceDirector mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status500InternalServerError)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> PostAsync(string email, string requestNo, string statusReq, string statusDetail, string Cordmail, string cordName)
        {
            try
            {
                var response = await mailService.sendRequestStatusEmailAsync(email, requestNo, statusReq, statusDetail, Cordmail, cordName, default).ConfigureAwait(false);
                string status;
                if (response == true)
                {
                    return status = "\"Success\"";
                }
                else
                {
                    return status = "\"Try Again\"";
                }
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
