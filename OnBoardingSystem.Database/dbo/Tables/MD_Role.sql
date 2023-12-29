﻿CREATE TABLE [dbo].[MD_Role] (
    [roleId]      VARCHAR (20)  NOT NULL,
    [roleName]    VARCHAR (50)  NOT NULL,
    [description] VARCHAR (800) NOT NULL,
    CONSTRAINT [PK_MD_Role] PRIMARY KEY CLUSTERED ([roleId] ASC)
);



