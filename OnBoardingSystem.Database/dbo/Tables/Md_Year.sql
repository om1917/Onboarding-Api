CREATE TABLE [dbo].[Md_Year] (
    [YearId]      VARCHAR (4)  NOT NULL,
    [Description] VARCHAR (20) NULL,
    [Abbrivation] VARCHAR (20) NULL,
    [YearGroup]   VARCHAR (50) NULL,
    [IsActive]    VARCHAR (2)  NULL,
    PRIMARY KEY CLUSTERED ([YearId] ASC)
);

