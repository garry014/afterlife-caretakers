CREATE TABLE [dbo].[Users] (
    [Id]                INT           IDENTITY (1, 1) NOT NULL,
    [name]              VARCHAR (500) NOT NULL,
    [email]             VARCHAR (254) NOT NULL,
    [phoneno]           VARCHAR (8)   NOT NULL,
    [password]          VARCHAR (100) NOT NULL,
    [gender]            VARCHAR (10)  NOT NULL,
    [usertype]          VARCHAR (100) NOT NULL,
    [NRIC]              VARCHAR (9)   NOT NULL,
    [willformID]        VARCHAR (100) NULL,
    [NRIC_upload]       VARCHAR (100) NULL,
    [activation_status] VARCHAR (200) NULL,
    [deathcert_upload]  VARCHAR (100) NULL,
    [deathdate_setting] DATETIME NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

