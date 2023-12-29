CREATE TABLE [dbo].[MD_Status] (
    [StatusId]   VARCHAR (5)   NOT NULL,
    [ActivityId] VARCHAR (5)   NOT NULL,
    [Status]     VARCHAR (100) NULL,
    [IsActive]   BIT           NULL,
    CONSTRAINT [PK_MD_Status] PRIMARY KEY CLUSTERED ([StatusId] ASC, [ActivityId] ASC)
);

