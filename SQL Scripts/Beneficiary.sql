CREATE TABLE [dbo].[Beneficiary] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [NAME]         NVARCHAR (25)  NOT NULL,
    [NRIC]         NVARCHAR (MAX) NOT NULL,
    [BirthDate]    DATETIME       NOT NULL,
    [Relationship] VARCHAR (15)   NOT NULL,
    [PhoneNo]      NVARCHAR (8)   NOT NULL,
    [OWNERID]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

