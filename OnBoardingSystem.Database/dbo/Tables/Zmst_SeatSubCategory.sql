﻿CREATE TABLE [dbo].[Zmst_SeatSubCategory] (
    [seatSubCategoryId] VARCHAR (2)   NOT NULL,
    [description]       VARCHAR (200) NOT NULL,
    [alternatenames]    VARCHAR (500) NULL,
    CONSTRAINT [PK_Zmst_SeatSubCategory] PRIMARY KEY CLUSTERED ([seatSubCategoryId] ASC)
);



