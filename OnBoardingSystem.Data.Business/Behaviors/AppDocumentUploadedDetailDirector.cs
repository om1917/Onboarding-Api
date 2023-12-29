//-----------------------------------------------------------------------
// <copyright file="AppDocumentUploadedDetailDirector.cs" company="NIC">
// Copyright (c) NIC. All rights reserved.
// </copyright>
//-------------------------------------------------------------------

using AutoMapper;

namespace OnBoardingSystem.Data.Business.Behaviors
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Drawing;
    using System.Linq;
    using System.Reflection.Metadata;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using Abp.Extensions;
    using Abp.Json;
    using AutoMapper;
    using Azure.Core;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;
    using OnBoardingSystem.Common.enums;
    using OnBoardingSystem.Data.Abstractions.Behaviors;
    using OnBoardingSystem.Data.Abstractions.Exceptions;
    using OnBoardingSystem.Data.Abstractions.Models;
    using OnBoardingSystem.Data.Business.Services;
    using OnBoardingSystem.Data.EF.Models;
    using OnBoardingSystem.Data.Interfaces;
    using Abs = Abstractions.Models;

    public class AppDocumentUploadedDetailDirector : IAppDocumentUploadedDetailDirector
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly EncryptionDecryptionService decryptionService;
        private readonly UtilityService utilityService;
        private readonly SMSService sMSService;
        private readonly IHttpContextAccessor httpContextAccessor;
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDocumentUploadedDetailDirector"/> class.
        /// </summary>
        /// <param name="mapper">Automapper.</param>
        /// <param name="unitOfWork">Unit of Work.</param>
        public AppDocumentUploadedDetailDirector(IHttpContextAccessor _httpContextAccessor, IMapper mapper, SMSService _sMSService, IUnitOfWork unitOfWork, EncryptionDecryptionService _encryptionDecryptionService, UtilityService _utilityService)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.decryptionService = _encryptionDecryptionService;
            this.sMSService = _sMSService;
            this.utilityService = _utilityService;
            this.httpContextAccessor = _httpContextAccessor;
        }

        public virtual async Task<bool> Save(List<Abs.AppDocumentUploadedDetail> appDocumentUploadedDetail, CancellationToken cancellationToken)
        {
            string Createdby = appDocumentUploadedDetail[0].CreatedBy;
            var userType = await this.unitOfWork.AppUserRoleMappingRepository.FindByAsync(x => x.UserId == Createdby, cancellationToken).ConfigureAwait(false); ;
            string requestno = appDocumentUploadedDetail[0].RequestNo;
            string doctype = appDocumentUploadedDetail[0].DocType;
            appDocumentUploadedDetail[0].SubTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            var documents = await this.unitOfWork.MdDocumentTypeRepository.FindByAsync(x => x.Id == doctype, cancellationToken).ConfigureAwait(false); ;
            string document = documents.Title;
            var userCredentials = await this.unitOfWork.AppContactPersonDetailRepository.FindByAsync(x => x.RequestNo == requestno && x.RoleId == "11", cancellationToken).ConfigureAwait(false);
            var smsTemplatePmuToUser = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "S0007", cancellationToken).ConfigureAwait(false);
            if (userCredentials!=null)
            {
                string userEmail = decryptionService.Decryption(userCredentials.EmailId);
                string mobile = decryptionService.Decryption(userCredentials.MobileNo);

                string coordinatorName = userCredentials.Name;
                if (userType.RoleId == "USER")
                {

                    var emailbyRoleId = await this.unitOfWork.EmailRecipientRepository.FindAllByAsync(x => x.RoleId == '1', cancellationToken).ConfigureAwait(false);
                    MailRequest mailContentPMU = new MailRequest();
                    var emaiTemplatePMU = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0015", cancellationToken).ConfigureAwait(false);
                    string BodyUser = emaiTemplatePMU.MessageTemplate;
                    BodyUser = BodyUser.Replace("#USER#", "Admin");
                    BodyUser = BodyUser.Replace("#UploadBy#", coordinatorName);
                    BodyUser = BodyUser.Replace("#Document#", document);
                    mailContentPMU.Body = BodyUser;
                    mailContentPMU.Subject = emaiTemplatePMU.MessageSubject;
                    foreach (var pmumail in emailbyRoleId)
                    {
                        mailContentPMU.ToEmail = pmumail.Email;
                        var mail = utilityService.SendEmailAsync(mailContentPMU);
                    }

                }
                else if (userType.RoleId == "PMUADMIN")
                {
                    MailRequest mailCoodinator = new MailRequest();
                    var emailUpload = await this.unitOfWork.MdSmsEmailTemplateRepository.FindByAsync(x => x.TemplateId == "E0015", cancellationToken).ConfigureAwait(false);
                    string Body = emailUpload.MessageTemplate;
                    Body = Body.Replace("#USER#", coordinatorName);
                    Body = Body.Replace("#UploadBy#", "PMUAdmin");
                    Body = Body.Replace("#Document#", document);
                    mailCoodinator.Body = Body;
                    mailCoodinator.Subject = emailUpload.MessageSubject;
                    mailCoodinator.ToEmail = userEmail;
                    string isdCode = "91";
                    string messagRPH = "";
                    string TemplateId = smsTemplatePmuToUser.RegisteredTemplateId;
                    string message = smsTemplatePmuToUser.MessageTemplate;
                    messagRPH = message.Replace("#USER#", coordinatorName);
                    messagRPH = messagRPH.Replace("#DOCTYPE#", document);
                    messagRPH = messagRPH.Replace("#REQNO#", requestno);
                }

            }
            var EfappDocUploadedDetails = this.mapper.Map<List<Data.EF.Models.AppDocumentUploadedDetail>>(appDocumentUploadedDetail);
            await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(EfappDocUploadedDetails, cancellationToken).ConfigureAwait(false);
            var effectedRows = await unitOfWork.CommitAsync(cancellationToken);
            if (effectedRows > 0)
            {
                return true;
            }
            return false;
        }
        public virtual async Task<bool> InsertAndUpdateActivityStatus(Abs.AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken)
        {
            using (var transaction = this.unitOfWork.OBSDBContext.Database.BeginTransaction())
            {
                try
                {
                    var AppProjectActivity=await this.unitOfWork.AppProjectActivityRepository.FindByAsync(x=>x.ActivityId.ToString() == appDocumentUploadedDetail.Activityid && x.ActivityParentRefId== appDocumentUploadedDetail.ModuleRefId, cancellationToken).ConfigureAwait(false);
                    var EfappDocUploadedDetails = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(appDocumentUploadedDetail);
                    if (AppProjectActivity != null)
                    {
                        AppProjectActivity.Status = MdStatusEnum.Completed.Value.ToString();
                        await this.unitOfWork.AppProjectActivityRepository.UpdateAsync(AppProjectActivity, cancellationToken).ConfigureAwait(false);
                    }
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(EfappDocUploadedDetails, cancellationToken).ConfigureAwait(false);
                    var effectedRows = await unitOfWork.CommitAsync(cancellationToken);
                    if (effectedRows > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback(); 
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public virtual async Task<List<AppDocumentUploadAndDocumentType>> GetByRequestId(AppDocumentFilter appDocFilter, CancellationToken cancellationToken)
        {
            try
            {
                var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(re => re.ModuleRefId == appDocFilter.ModuleRefId && re.Activityid==appDocFilter.ActivityId,cancellationToken).ConfigureAwait(false);
                var documentTypeList = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
                var query = from documentDetails in appUploadedDetails
                            join documentType in documentTypeList on documentDetails.DocType equals documentType.Id
                            where documentDetails.ModuleRefId == appDocFilter.ModuleRefId
                            select new AppDocumentUploadAndDocumentType
                            {
                                DocumentId = documentDetails.DocumentId,
                                DocType = documentDetails.DocType,
                                DocTitle = documentType.Title,
                                DocSubject= documentDetails.DocSubject,
                                DocContent = "",
                                createdby = documentDetails.CreatedBy,
                                submit = documentDetails.SubTime,
                            };

                return query.ToList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<List<AppDocumentUploadAndDocumentType>> GetByProjectDetailId(int id, CancellationToken cancellationToken)
        {
            try
            {
                var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllByAsync(re=>re.Activityid == "601", cancellationToken).ConfigureAwait(false);
				var documentTypeList = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
				var zmstprojectslist =await this.unitOfWork.ZmstProjectRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
				var mdDocumentType = await this.unitOfWork.MdDocumentTypeRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);
				var appProjectDetailsList = await this.unitOfWork.AppProjectDetailsRepository.FindAllByAsync(re => re.Id == id, cancellationToken).ConfigureAwait(false);
                try
                {
                    var query=from projectdetail in  appProjectDetailsList join
                               appdoc in appUploadedDetails on projectdetail.Id.ToString() equals appdoc.ModuleRefId
                               join mdDocument in mdDocumentType on appdoc.DocType equals mdDocument.Id
                               select new AppDocumentUploadAndDocumentType
                               {
                                   DocumentId = appdoc.DocumentId,
                                   DocType = appdoc.DocType,
                                   DocTitle = mdDocument.Title,
                                   DocContent = "",
                                   createdby = appdoc.CreatedBy,
                                   projectNo = projectdetail.ProjectCode,
                                   projectName = projectdetail.ProjectName,
                                   submit = appdoc.SubTime,
                               };
                    
                    return query.ToList();

                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<Abstractions.Models.AppDocumentUploadedDetail> GetDocumentByRequestId(AppDocActivity appdoc, CancellationToken cancellationToken)
        {
            try
            {
                var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(re => re.ModuleRefId == appdoc.Id && re.Activityid == appdoc.ActivityId, cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<Abstractions.Models.AppDocumentUploadedDetail>(appUploadedDetails);
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //ModuleRefId
        public virtual async Task<List<AppDocumentUploadAndDocumentType>> UserMenuByRequestId(string requestId, CancellationToken cancellationToken)
        {
            try
            {
                var result = new List<AppDocumentUploadAndDocumentType>();
                var appUploadedDetails = this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllBy(re => re.ModuleRefId == requestId);
                var documentTypeList = this.unitOfWork.MdDocumentTypeRepository.GetAll();
                var documentUserMapping = this.unitOfWork.AppDocumentTypeRoleMapping.GetAll();
                var query = from documentDetails in appUploadedDetails
                            join documentType in documentTypeList on documentDetails.DocType equals documentType.Id into tempappuploaddetails
                            from documentType in tempappuploaddetails.DefaultIfEmpty()
                            join docUserMapping in documentUserMapping on documentType.Id equals docUserMapping.DocumentTypeId into tempdocUserMapping
                            from docUserMapping in tempdocUserMapping.DefaultIfEmpty()
                            select new AppDocumentUploadAndDocumentType
                            {
                                IsVisible = docUserMapping.IsVisible.ToString(),
                                DocumentId = documentDetails.DocumentId,
                                DocType = documentDetails.DocType,
                                DocTitle = documentType.Title == null ? "NA" : documentType.Title,
                                DocContent = "",
                                submit = documentDetails.SubTime,
                                createdby = documentDetails.CreatedBy
                            };

                result = query.ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<List<AppDocumentUploadAndDocumentType>> ModuleRefId(string ModuleRefId, CancellationToken cancellationToken)
        {
            try
            {
                var result = new List<AppDocumentUploadAndDocumentType>();
                var appUploadedDetails = this.unitOfWork.AppDocumentUploadedDetailRepository.FindAllBy(re => re.ModuleRefId == ModuleRefId);
                var documentTypeList = this.unitOfWork.MdDocumentTypeRepository.GetAll();
                var query = from appUploaded in appUploadedDetails
                            join documentType in documentTypeList on appUploaded.DocType equals documentType.Id
                            select new AppDocumentUploadAndDocumentType
                            {
                                DocType = documentType.Title,
                                DocumentId= appUploaded.DocumentId,
                                projectName = appUploaded.DocSubject,
                                createdby = appUploaded.CreatedBy,
                                docContentType= appUploaded.DocContentType,
                            };

                result = query.ToList();
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<int> Saveprofilephoto(Abs.AppDocumentUploadedDetail appDocumentUploadedDetail, CancellationToken cancellationToken)
        {
            try
            {
                int result = 0;
                Data.EF.Models.AppDocumentUploadedDetail efdoc = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(x => x.ModuleRefId == appDocumentUploadedDetail.ModuleRefId, cancellationToken).ConfigureAwait(false);
                if (efdoc != null)
                {
                    efdoc.Activityid = ((int)Enumactivity.ProfileDetails).ToString();
                    efdoc.DocContent = appDocumentUploadedDetail.DocContent;
                    efdoc.IpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    efdoc.SubTime = DateTime.Now;
                    efdoc.DocType = ((int)EnumDocType.Photo).ToString();
                    efdoc.CreatedBy = appDocumentUploadedDetail.CreatedBy;
                    efdoc.DocFileName = appDocumentUploadedDetail.DocFileName;
                    efdoc.DocContentType = appDocumentUploadedDetail.DocContentType;
                    efdoc.ModuleRefId = appDocumentUploadedDetail.ModuleRefId;
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.UpdateAsync(efdoc, cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    Abstractions.Models.AppDocumentUploadedDetail appdoc = new Abstractions.Models.AppDocumentUploadedDetail();
                    appdoc.Activityid = ((int)Enumactivity.ProfileDetails).ToString();
                    appdoc.ModuleRefId = appDocumentUploadedDetail.ModuleRefId;
                    appdoc.DocContent = appDocumentUploadedDetail.DocContent;
                    appdoc.IpAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    appdoc.SubTime = DateTime.Now;
                    appdoc.DocType = ((int)EnumDocType.Photo).ToString();
                    efdoc.DocFileName = appDocumentUploadedDetail.DocFileName;
                    efdoc.DocContentType = appDocumentUploadedDetail.DocContentType;
                    appdoc.CreatedBy = appDocumentUploadedDetail.CreatedBy;
                    var AppDocuments = this.mapper.Map<Data.EF.Models.AppDocumentUploadedDetail>(appdoc);
                    await this.unitOfWork.AppDocumentUploadedDetailRepository.InsertAsync(AppDocuments, cancellationToken).ConfigureAwait(false);
                }
                result = await this.unitOfWork.CommitAsync(cancellationToken).ConfigureAwait(false);
                return result;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public virtual async Task<Abstractions.Models.AppDocumentUploadedDetail> GetById(int id, CancellationToken cancellationToken)
        {
            var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(re => re.DocumentId == id, cancellationToken).ConfigureAwait(false);
            var result = this.mapper.Map<Abstractions.Models.AppDocumentUploadedDetail>(appUploadedDetails);
            return result;
        }

        public virtual async Task<Abstractions.Models.AppDocumentUploadedDetail> GetDocumentByDocType(AppDocumentFilter employeeDocDetails, CancellationToken cancellationToken)
        {
            if (employeeDocDetails.DocType == "0" || employeeDocDetails.DocType == "")
            {
                var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(re => re.ModuleRefId == employeeDocDetails.ModuleRefId && (re.DocType == "0" || re.DocType == "") && re.Activityid == employeeDocDetails.ActivityId, cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<Abstractions.Models.AppDocumentUploadedDetail>(appUploadedDetails);
                return result;
            }
            else
            {
                var appUploadedDetails = await this.unitOfWork.AppDocumentUploadedDetailRepository.FindByAsync(re => re.ModuleRefId == employeeDocDetails.ModuleRefId && re.DocType == employeeDocDetails.DocType && re.Activityid == employeeDocDetails.ActivityId, cancellationToken).ConfigureAwait(false);
                var result = this.mapper.Map<Abstractions.Models.AppDocumentUploadedDetail>(appUploadedDetails);
                return result;
            }
        }
    }
}