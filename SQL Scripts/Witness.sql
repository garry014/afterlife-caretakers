CREATE TABLE [dbo].[Witness] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Relationship] NVARCHAR (15)  NOT NULL,
    [NAME]         NVARCHAR (25)  NOT NULL,
    [NRIC]         NVARCHAR (MAX) NOT NULL,
    [BirthDate]    DATETIME       NOT NULL,
    [PhoneNo]      NVARCHAR (8)   NOT NULL,
    [Email]        NVARCHAR (30)  NOT NULL,
    [OWNERID]      INT            NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

