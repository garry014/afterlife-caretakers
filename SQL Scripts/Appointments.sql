CREATE TABLE [dbo].[Appointments] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [ApptType]    VARCHAR (11) NULL,
    [CustName]    VARCHAR (20) NULL,
    [CustId]      INT          NULL,
    [ConsultName] VARCHAR (20) NULL,
    [ConsultRate] INT          NULL,
    [ConsultId]   INT          NULL,
    [Date]        DATE         NOT NULL,
    [StartTime]   INT          NOT NULL,
    [Duration]    INT          NOT NULL,
    [ApptStatus]  BIT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

