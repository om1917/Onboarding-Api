CREATE PROCEDURE [dbo].[USP_SaveOnboardingDetail]        
(        
 @InputJson Varchar(MAX),      
 @IsError BIT output     
 --@Message Varchar(100) output        
 -- @Mode varchar(20)=null    
)        
AS        
/*        
DECLARE @InputJson Varchar(MAX), @IsError bit     
 --@Message varchar(100)    
--SET @Mode='FinalSubmit'    
Set @InputJson ='{"RequestNo":"SA20241197","Website":"www.nic.in","YearOfFirstTimeAffilitionSession":"2005","ExamLastSessionConductedIn":"2011","ExamLastSessionTechSupportBy":"Vimal","ExamLastSessionDescription":"Brief Description","CounsLastSessionConductedIn":"NA","CounsLastSessionTechSupportBy":null,"CounsLastSessionDescription":null,"ExamExpectedApplicant":250,"ExamCourseList":"ABXC","ExamTotalCourse":44,"ExamTentativeScheduleStart":"2023-12-19T00:00:00","ExamTentativeScheduleEnd":"2023-12-20T00:00:00","ExamDissimilarityOfSchedule":true,"CounsExpectedApplicant":null,"CounsExpectedSeat":null,"CounsCourseList":null,"CounsTotalCourse":null,"CounsExpectedRound":null,"CounsExpectedSpotRound":0,"CounsExpectedParticipatingInstitute":null,"CounsTentativeScheduleStart":null,"CounsTentativeScheduleEnd":null,"CounsDissimilarityOfSchedule":false,"SubmitTime":"2023-12-18T00:00:00","Ipaddress":"::1","Status":"","Remarks":"","IsActive":"","Mode":"FinalSubmit","CoordinatorMail":"mohan@gmail.com","HodMail":"shiv@gmail.com","AttachFilecontent":null,"contactdetails":[{"Id":0,"RequestNo":"SA20241197","DepartmentId":"1","RoleId":"12","Name":"Deputy Coordinator","Designation":"ddd","MobileNo":"ZnisEzMHDs1L2a3S4h4rpXrLe0ExAg==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"1","RoleId":"13","Name":"Program Manager Unit optional","Designation":"ddd","MobileNo":"HJbeuIikrYBsKzy71yGS6Q==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"2","RoleId":"21","Name":"Nodal Officer","Designation":"ddd","MobileNo":"A\u002BMepsFRNKEtSZZY9\u002BQ4bw==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"2","RoleId":"22","Name":"Authorized Signatory","Designation":"dd","MobileNo":"BnlsX5o82ys6k4SvZZ1sJA==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"3","RoleId":"31","Name":"System Admin","Designation":"ddd","MobileNo":"uHWVE\u002BFrFama2iaUO1PaFw==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"3","RoleId":"32","Name":"Database Admin","Designation":"dd","MobileNo":"OZ2BgOJdn2gZGT4IErSGrA==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="},{"Id":0,"RequestNo":"SA20241197","DepartmentId":"3","RoleId":"33","Name":"Web Information Manager","Designation":"ddd","MobileNo":"sgNdNZLeNWDESX\u002Bus1L2a3S4hAzL0w==","EmailId":"DMerwYG5MXlhkFvKFuYkqg=="}]}
'        
EXEC USP_SaveOnboardingDetail @InputJson      
, @IsError output-- ,  @Message output        
select @IsError ,  @Message        
*/   

BEGIN    

 SET @IsError=0    
 --SET @Message ='Saved'        
      
 SELECT *  INTO #TT  FROM OpenJson(@InputJson)      
 WITH (RequestNo VARCHAR(50) '$.RequestNo',      
 Website varchar(64) '$.Website',       
 YearOfFirstTimeAffilitionSession varchar(16) '$.YearOfFirstTimeAffilitionSession',      
 ExamLastSessionConductedIn varchar(16) '$.ExamLastSessionConductedIn',      
 ExamLastSessionTechSupportBy varchar(200) '$.ExamLastSessionTechSupportBy',      
 ExamLastSessionDescription varchar(max) '$.ExamLastSessionDescription',      
 CounsLastSessionConductedIn varchar(16) '$.CounsLastSessionConductedIn',      
 CounsLastSessionTechSupportBy varchar(200) '$.CounsLastSessionTechSupportBy',      
 CounsLastSessionDescription varchar(max) '$.CounsLastSessionDescription',    
 ExamExpectedApplicant int '$.ExamExpectedApplicant',      
 ExamCourseList varchar(max) '$.ExamCourseList',      
 ExamTotalCourse INT '$.ExamTotalCourse',      
 ExamTentativeScheduleStart datetime '$.ExamTentativeScheduleStart',      
 ExamTentativeScheduleEnd datetime '$.ExamTentativeScheduleEnd',      
 ExamDissimilarityOfSchedule bit '$.ExamDissimilarityOfSchedule',      
 CounsExpectedApplicant INT '$.CounsExpectedApplicant',      
 CounsExpectedSeat INT '$.CounsExpectedSeat',      
 CounsCourseList varchar(max) '$.CounsCourseList',      
 CounsTotalCourse INT '$.CounsTotalCourse',     
 CounsExpectedRound INT '$.CounsExpectedRound',      
 CounsExpectedSpotRound INT '$.CounsExpectedSpotRound',     
 CounsExpectedParticipatingInstitute INT '$.CounsExpectedParticipatingInstitute',      
 CounsTentativeScheduleStart datetime '$.CounsTentativeScheduleStart',      
 CounsTentativeScheduleEnd datetime '$.CounsTentativeScheduleEnd',      
 CounsDissimilarityOfSchedule bit '$.CounsDissimilarityOfSchedule',      
 SubmitTime datetime '$.SubmitTime',      
 IPAddress varchar(20) '$.Ipaddress',      
 Status varchar(20) '$.Status',      
 Remarks varchar(max) '$.Remarks',      
 IsActive varchar(2) '$.IsActive',      
 Mode varchar(20) '$.Mode',    
 Contactdetails NVARCHAR(MAX) '$.contactdetails' AS JSON      
 )       

 ----------------------------Contactdetails Json----------------------      
    
 Declare @ContactInputJson Varchar(MAX) , @RequestNo varchar(50),@Mode varchar(20),@Status varchar(2)    
 Set     @ContactInputJson=(Select #TT.Contactdetails from #TT)      
 Set @RequestNo=(Select #TT.RequestNo from #TT)      
 SET @Mode=(Select #TT.Mode from #TT)    
 SELECT *  INTO #TTCdetails  FROM OpenJson(@ContactInputJson)      
 --CROSS APPLY OPENJSON(B.Contactdetails)      
 WITH (RequestNo VARCHAR(50) '$.RequestNo',      
 DepartmentId varchar(50) '$.DepartmentId',      
 RoleId varchar(50) '$.RoleId',      
 Name varchar(50) '$.Name',      
 Designation varchar(50) '$.Designation',      
 MobileNo varchar(300) '$.MobileNo',      
 EmailId varchar(300) '$.EmailId'      
 )      
  if(@Mode='FinalSubmit')    
  Begin    
      
  set @Status='DP'    
  Update App_OnboardingRequest Set CurrentStage='Details form submitted' Where RequestNo=@RequestNo    
  End    
     
  if(@Mode='SaveDraft')    
  Begin    
     
  set @Status='DD'    
  End    
    
  if exists(Select A.RequestNo  from App_OnboardingDetails A Where A.RequestNo=@RequestNo)    
 Begin     
  UPDATE D SET     
  D.RequestNo        =T.RequestNo       ,     
  D.Website        =T.Website        ,    
  D.YearOfFirstTimeAffilitionSession  =T.YearOfFirstTimeAffilitionSession  ,    
  D.ExamLastSessionConductedIn   =T.ExamLastSessionConductedIn   ,    
  D.ExamLastSessionTechSupportBy   =T.ExamLastSessionTechSupportBy   ,    
  D.ExamLastSessionDescription   =T.ExamLastSessionDescription   ,    
  D.CounsLastSessionConductedIn   =T.CounsLastSessionConductedIn   ,    
  D.CounsLastSessionTechSupportBy   =T.CounsLastSessionTechSupportBy  ,     
  D.CounsLastSessionDescription   =T.CounsLastSessionDescription   ,    
  D.ExamExpectedApplicant     =T.ExamExpectedApplicant    ,     
  D.ExamCourseList      =T.ExamCourseList      ,    
  D.ExamTotalCourse      =T.ExamTotalCourse      ,    
  D.ExamTentativeScheduleStart   =T.ExamTentativeScheduleStart   ,    
  D.ExamTentativeScheduleEnd    =T.ExamTentativeScheduleEnd    ,    
  D.ExamDissimilarityOfSchedule   =T.ExamDissimilarityOfSchedule   ,    
  D.CounsExpectedApplicant    =T.CounsExpectedApplicant    ,    
  D.CounsExpectedSeat      =T.CounsExpectedSeat     ,     
  D.CounsCourseList      =T.CounsCourseList      ,    
  D.CounsTotalCourse      =T.CounsTotalCourse      ,    
  D.CounsExpectedRound     =T.CounsExpectedRound     ,    
  D.CounsExpectedSpotRound    =T.CounsExpectedSpotRound    ,    
  D.CounsExpectedParticipatingInstitute =T.CounsExpectedParticipatingInstitute ,    
  D.CounsTentativeScheduleStart   =T.CounsTentativeScheduleStart   ,    
  D.CounsTentativeScheduleEnd    =T.CounsTentativeScheduleEnd   ,     
  D.CounsDissimilarityOfSchedule   =T.CounsDissimilarityOfSchedule   ,      
  D.SubmitTime=T.SubmitTime,D.IPAddress=T.IPAddress,     
  D.Status=@Status,D.Remarks=T.Remarks,D.IsActive=T.IsActive     
  FROM App_OnboardingDetails D     
  INNER JOIN #TT T on D.RequestNo=D.RequestNo     
  where D.RequestNo=@RequestNo    
    
  SET @IsError = 1    
 End    
    
 if not exists(Select A.RequestNo  from App_OnboardingDetails A Where A.RequestNo=@RequestNo)    
 Begin    
     
  ---------------------------Insert App_OnboardingDetails------------------     
  Insert into App_OnboardingDetails([RequestNo], [Website], [YearOfFirstTimeAffilitionSession],     
  [ExamLastSessionConductedIn], [ExamLastSessionTechSupportBy], [ExamLastSessionDescription],     
  [CounsLastSessionConductedIn], [CounsLastSessionTechSupportBy], [CounsLastSessionDescription],     
  [ExamExpectedApplicant], [ExamCourseList], [ExamTotalCourse], [ExamTentativeScheduleStart], [ExamTentativeScheduleEnd], [ExamDissimilarityOfSchedule],     
  [CounsExpectedApplicant], [CounsExpectedSeat], [CounsCourseList], [CounsTotalCourse], [CounsExpectedRound], [CounsExpectedSpotRound],     
  [CounsExpectedParticipatingInstitute], [CounsTentativeScheduleStart], [CounsTentativeScheduleEnd], [CounsDissimilarityOfSchedule],     
  [SubmitTime], [IPAddress], [Status], [Remarks], [IsActive])      
  Select T.[RequestNo], T.[Website], T.[YearOfFirstTimeAffilitionSession],     
  T.[ExamLastSessionConductedIn], T.[ExamLastSessionTechSupportBy], T.[ExamLastSessionDescription],     
  T.[CounsLastSessionConductedIn], T.[CounsLastSessionTechSupportBy], T.[CounsLastSessionDescription],     
  T.[ExamExpectedApplicant], T.[ExamCourseList], T.[ExamTotalCourse], T.[ExamTentativeScheduleStart], T.[ExamTentativeScheduleEnd], T.[ExamDissimilarityOfSchedule],     
  T.[CounsExpectedApplicant], T.[CounsExpectedSeat], T.[CounsCourseList], T.[CounsTotalCourse], T.[CounsExpectedRound], T.[CounsExpectedSpotRound],     
  T.[CounsExpectedParticipatingInstitute], T.[CounsTentativeScheduleStart], T.[CounsTentativeScheduleEnd], T.[CounsDissimilarityOfSchedule],    
  T.SubmitTime,T.IPAddress,@Status,T.Remarks,T.IsActive     
  from #TT T      
      
  SET @IsError = 1    
 End   
 
	if exists(Select * from App_ContactPersonDetails D INNER JOIN #TTCdetails TT On D.RequestNo=TT.RequestNo AND D.DepartmentId=TT.DepartmentId and D.RoleId=TT.RoleId)
	BEGIN
		UPDATE CPD     
		SET CPD.MobileNo=T.MobileNo,CPD.Name=T.Name,CPD.Designation=T.Designation,CPD.EmailId=T.EmailId     
		from     
		App_ContactPersonDetails CPD     
		INNER JOIN #TTCdetails T on CPD.RequestNo=T.RequestNo and CPD.DepartmentId=T.DepartmentId and CPD.RoleId=T.RoleId       
	END

	Insert into  App_ContactPersonDetails (RequestNo,DepartmentId,RoleId,Name,Designation,MobileNo,EmailId)    
	Select RequestNo,T.DepartmentId,T.RoleId,T.Name,T.Designation,T.MobileNo,T.EmailId  
	from  #TTCdetails T 
	EXCEPT
	Select RequestNo,DepartmentId,RoleId,Name,Designation,MobileNo,EmailId   
	FROM App_ContactPersonDetails  
	WHERE RequestNo=@RequestNo     

	--Select RequestNo,DepartmentId,RoleId,Name,Designation,MobileNo,EmailId   
	--FROM App_ContactPersonDetails  
	--WHERE RequestNo=@RequestNo   
END