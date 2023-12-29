CREATE TABLE [dbo].[App_ProjectExpenditure] (
    [Id]                   INT             IDENTITY (1, 1) NOT NULL,
    [ProjectId]            INT             NULL,
    [FinancialComponentId] INT             NULL,
    [Amount]               DECIMAL (16, 2) NULL,
    [CreatedBy]            VARCHAR (50)    NULL,
    [CreatedOn]            DATETIME        NULL,
    [ModifiedBy]           VARCHAR (50)    NULL,
    [ModifiedOn]           DATETIME        NULL,
    [IsActive]             BIT             NOT NULL,
    CONSTRAINT [pk_App_ProjectExpenditure] PRIMARY KEY CLUSTERED ([Id] ASC)
);

