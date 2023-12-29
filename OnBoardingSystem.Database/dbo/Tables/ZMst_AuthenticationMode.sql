CREATE TABLE [dbo].[ZMst_AuthenticationMode] (
    [AuthCode] CHAR (1)     NOT NULL,
    [Authmode] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ZMst_AuthenticationMode] PRIMARY KEY CLUSTERED ([AuthCode] ASC)
);

