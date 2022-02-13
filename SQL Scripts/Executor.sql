CREATE TABLE [dbo].[Executor] (
    [Id]      INT            IDENTITY (1, 1) NOT NULL,
    [NAME]    NVARCHAR (25)  NOT NULL,
    [NRIC]    NVARCHAR (MAX) NOT NULL,
    [Email]   NVARCHAR (MAX) NOT NULL,
    [PhoneNo] NVARCHAR (8)   NOT NULL,
    [OWNERID] INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

