CREATE TABLE [dbo].[Fiance] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [NAME]    NVARCHAR (25)  NOT NULL,
    [NRIC]    NVARCHAR (MAX) NOT NULL,
    [Gender]  NVARCHAR (1)   NOT NULL,
    [PhoneNo] NVARCHAR (8)   NOT NULL,
    [Mstatus] NVARCHAR (30)  NOT NULL,
    [Email]   NVARCHAR (30)  NOT NULL,
    [OWNERID] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

