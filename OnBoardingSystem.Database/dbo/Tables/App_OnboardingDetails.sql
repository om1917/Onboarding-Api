﻿CREATE TABLE [dbo].[App_OnboardingDetails] (
    [RequestNo]                           VARCHAR (32)  NOT NULL,
    [Website]                             VARCHAR (64)  NULL,
    [YearOfFirstTimeAffilitionSession]    VARCHAR (16)  NULL,
    [ExamLastSessionConductedIn]          VARCHAR (16)  NULL,
    [ExamLastSessionTechSupportBy]        VARCHAR (200) NULL,
    [ExamLastSessionDescription]          VARCHAR (MAX) NULL,
    [CounsLastSessionConductedIn]         VARCHAR (16)  NULL,
    [CounsLastSessionTechSupportBy]       VARCHAR (200) NULL,
    [CounsLastSessionDescription]         VARCHAR (MAX) NULL,
    [ExamExpectedApplicant]               INT           NULL,
    [ExamCourseList]                      VARCHAR (MAX) NULL,
    [ExamTotalCourse]                     INT           NULL,
    [ExamTentativeScheduleStart]          DATETIME      NULL,
    [ExamTentativeScheduleEnd]            DATETIME      NULL,
    [ExamDissimilarityOfSchedule]         BIT           NULL,
    [CounsExpectedApplicant]              INT           NULL,
    [CounsExpectedSeat]                   INT           NULL,
    [CounsCourseList]                     VARCHAR (MAX) NULL,
    [CounsTotalCourse]                    INT           NULL,
    [CounsExpectedRound]                  INT           NULL,
    [CounsExpectedSpotRound]              INT           NULL,
    [CounsExpectedParticipatingInstitute] INT           NULL,
    [CounsTentativeScheduleStart]         DATETIME      NULL,
    [CounsTentativeScheduleEnd]           DATETIME      NULL,
    [CounsDissimilarityOfSchedule]        BIT           NULL,
    [SubmitTime]                          DATETIME      NULL,
    [IPAddress]                           VARCHAR (32)  NULL,
    [Status]                              VARCHAR (2)   NULL,
    [Remarks]                             VARCHAR (MAX) NULL,
    [IsActive]                            VARCHAR (2)   NULL
);



