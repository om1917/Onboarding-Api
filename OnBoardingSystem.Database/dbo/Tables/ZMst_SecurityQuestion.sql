CREATE TABLE [dbo].[ZMst_SecurityQuestion] (
    [securityQuesId] CHAR (2)      NOT NULL,
    [securityQues]   VARCHAR (200) NOT NULL,
    CONSTRAINT [PK_ZMst_SecurityQuestion] PRIMARY KEY CLUSTERED ([securityQuesId] ASC)
);

