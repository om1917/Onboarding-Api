CREATE TABLE [dbo].[App_ProjectPaymentDetails] (
    [PaymentId]          INT             IDENTITY (1, 1) NOT NULL,
    [PaymentParentRefId] VARCHAR (50)    NULL,
    [Amount]             DECIMAL (16, 2) NULL,
    [UTRNo]              VARCHAR (50)    NULL,
    [PaymentDate]        DATETIME        NULL,
    [IncomeTax]          DECIMAL (16, 2) NULL,
    [GST]                DECIMAL (16, 2) NULL,
    [TDS]                DECIMAL (16, 2) NULL,
    [Status]             VARCHAR (2)     NULL,
    [IPAddress]          VARCHAR (50)    NULL,
    [SubmitTime]         DATETIME        NULL,
    CONSTRAINT [PK_App_ProjectPaymentDetails] PRIMARY KEY CLUSTERED ([PaymentId] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Request No or any other ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'App_ProjectPaymentDetails', @level2type = N'COLUMN', @level2name = N'PaymentParentRefId';

