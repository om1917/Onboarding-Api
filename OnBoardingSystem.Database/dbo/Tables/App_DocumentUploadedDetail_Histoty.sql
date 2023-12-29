CREATE TABLE [dbo].[App_DocumentUploadedDetail_Histoty] (
    [id]             INT            IDENTITY (1, 1) NOT NULL,
    [documentId]     INT            NOT NULL,
    [activityid]     VARCHAR (5)    NULL,
    [ModuleRefId]    VARCHAR (100)  NOT NULL,
    [docType]        VARCHAR (50)   NOT NULL,
    [docId]          VARCHAR (50)   NULL,
    [docSubject]     VARCHAR (500)  NULL,
    [docContent]     VARCHAR (MAX)  NULL,
    [docContentType] VARCHAR (10)   NULL,
    [docFileName]    VARCHAR (200)  NULL,
    [objectId]       VARCHAR (50)   NULL,
    [objectUrl]      VARCHAR (1500) NULL,
    [docNatureId]    CHAR (1)       NULL,
    [ipAddress]      VARCHAR (20)   NULL,
    [subTime]        DATETIME       NULL,
    [createdBy]      VARCHAR (100)  NULL,
    [VersionNo]      INT            CONSTRAINT [DF_App_DocumentUploadedDetail_Histoty_VersionNo] DEFAULT ((1)) NULL,
    CONSTRAINT [PK_App_DocumentUploadedDetail_Histoty] PRIMARY KEY CLUSTERED ([id] ASC)
);

