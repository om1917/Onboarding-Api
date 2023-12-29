﻿CREATE TABLE [dbo].[Zmst_SeatCategory] (
    [seatCategoryId] VARCHAR (2)   NOT NULL,
    [description]    VARCHAR (200) NOT NULL,
    [alternateNames] VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SeatCategory] PRIMARY KEY CLUSTERED ([seatCategoryId] ASC)
);

