CREATE TABLE [dbo].[ConfigurationEnvironment] (
    [ApplicationId]           INT             NOT NULL,
    [environmentMode]         VARCHAR (2)     NULL,
    [environmentModeDesc]     VARCHAR (50)    NULL,
    [captchaMode]             VARCHAR (2)     NULL,
    [captchaModeDesc]         VARCHAR (10)    NULL,
    [captchaValue]            VARCHAR (10)    NULL,
    [isOfflineEnabled]        CHAR (1)        NULL,
    [offlineModeMessage]      NVARCHAR (1000) NULL,
    [isOfflineEnabledAdmin]   CHAR (1)        NULL,
    [offlineModeMessageAdmin] NVARCHAR (1000) NULL,
    [isDataCached]            CHAR (1)        NULL,
    [adminKey]                VARCHAR (200)   NULL,
    [agencyKey]               VARCHAR (200)   NULL,
    [AuthMode]                VARCHAR (10)    NULL,
    [otpMedium]               VARCHAR (10)    NULL,
    CONSTRAINT [PK_ConfigurationEnvironment] PRIMARY KEY CLUSTERED ([ApplicationId] ASC)
);

