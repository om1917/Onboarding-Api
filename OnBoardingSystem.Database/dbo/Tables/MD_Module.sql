﻿CREATE TABLE [dbo].[MD_Module] (
    [ModuleId]        VARCHAR (10)   NOT NULL,
    [Heading]         VARCHAR (100)  NULL,
    [SubHeading]      VARCHAR (100)  NULL,
    [Url]             VARCHAR (500)  NULL,
    [Path]            VARCHAR (500)  NULL,
    [Parent]          VARCHAR (10)   NULL,
    [MainModule]      VARCHAR (100)  NULL,
    [IsActive]        CHAR (2)       NULL,
    [DisplayPriority] INT            NULL,
    [Remarks]         NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_MD_Module] PRIMARY KEY CLUSTERED ([ModuleId] ASC)
);





