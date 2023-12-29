CREATE TABLE [dbo].[App_RoleModulePermission] (
    [RoleId]     VARCHAR (10) NOT NULL,
    [ModuleId]   VARCHAR (10) NOT NULL,
    [IsReadOnly] CHAR (2)     NULL,
    [IsActive]   CHAR (2)     NULL,
    CONSTRAINT [PK_App_RoleModulePermission] PRIMARY KEY CLUSTERED ([RoleId] ASC, [ModuleId] ASC)
);



