CREATE TABLE [dbo].[App_ProjectActivity] (
    [Id]                  INT          IDENTITY (1, 1) NOT NULL,
    [ActivityParentRefId] VARCHAR (20) NOT NULL,
    [ActivityId]          INT          NOT NULL,
    [Status]              VARCHAR (2)  NOT NULL,
    [SubmitTime]          DATETIME     NOT NULL,
    [IpAddress]           VARCHAR (50) NULL,
    CONSTRAINT [PK_App_ProjectActivity] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'RequestNo or Project Code etc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App_ProjectActivity', @level2type = N'COLUMN', @level2name = N'ActivityParentRefId';

