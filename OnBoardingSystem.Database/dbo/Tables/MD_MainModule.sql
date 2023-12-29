CREATE TABLE [dbo].[MD_MainModule] (
    [MainModuleId]    VARCHAR (10)   NOT NULL,
    [Heading]         VARCHAR (100)  NULL,
    [SubHeading]      VARCHAR (100)  NULL,
    [Path]            VARCHAR (500)  NULL,
    [IsActive]        CHAR (2)       NULL,
    [DisplayPriority] INT            NULL,
    [Remarks]         NVARCHAR (MAX) NULL,
    [Icon]            NVARCHAR (100) NULL,
    [CssClass]        NVARCHAR (100) NULL,
    CONSTRAINT [PK_MD_MainModule] PRIMARY KEY CLUSTERED ([MainModuleId] ASC)
);

