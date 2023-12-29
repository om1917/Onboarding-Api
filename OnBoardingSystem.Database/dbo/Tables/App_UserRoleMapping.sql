CREATE TABLE [dbo].[App_UserRoleMapping] (
    [UserID]     VARCHAR (50) NOT NULL,
    [RoleID]     VARCHAR (50) NOT NULL,
    [IsReadOnly] CHAR (2)     CONSTRAINT [DF_App_UserRoleMapping_IsReadOnly] DEFAULT ('Y') NULL,
    [IsActive]   CHAR (2)     CONSTRAINT [DF_App_UserRoleMapping_IsActive] DEFAULT ('Y') NULL,
    CONSTRAINT [PK_App_UserRoleMapping] PRIMARY KEY CLUSTERED ([UserID] ASC, [RoleID] ASC)
);



