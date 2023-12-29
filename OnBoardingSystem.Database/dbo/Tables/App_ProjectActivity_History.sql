CREATE TABLE [dbo].[App_ProjectActivity_History] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [ActivityParentRefId] VARCHAR (20) NOT NULL,
    [ActivityId]          INT          NOT NULL,
    [Status]              VARCHAR (2)  NOT NULL,
    [SubmitTime]          DATETIME     NOT NULL,
    [IpAddress]           VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_App_ProjectActivity_Histoty] PRIMARY KEY CLUSTERED ([Id] ASC)
);

