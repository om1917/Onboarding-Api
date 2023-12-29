﻿CREATE TABLE [dbo].[App_OnboardingResponse] (
    [Id]         INT           NULL,
    [RequestNo]  VARCHAR (32)  NULL,
    [Status]     VARCHAR (2)   NULL,
    [Remarks]    VARCHAR (MAX) NULL,
    [Version]    INT           NULL,
    [UserId]     VARCHAR (16)  NULL,
    [SubmitTime] DATETIME      NULL,
    [IPAddress]  VARCHAR (32)  NULL
);



