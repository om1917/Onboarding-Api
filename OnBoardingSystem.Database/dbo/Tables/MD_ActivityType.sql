CREATE TABLE [dbo].[MD_ActivityType] (
    [ActivityId]    INT           NOT NULL,
    [Activity]      VARCHAR (100) NULL,
    [ActivityGroup] VARCHAR (20)  NULL,
    [IsActive]      BIT           NULL,
    CONSTRAINT [PK_MD_ActivityType] PRIMARY KEY CLUSTERED ([ActivityId] ASC)
);

