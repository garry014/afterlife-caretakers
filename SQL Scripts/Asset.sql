CREATE TABLE [dbo].[Asset] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [TYPE]        NVARCHAR (15)  NOT NULL,
    [gift_type]   NVARCHAR (40)  NOT NULL,
    [description] NVARCHAR (MAX) NULL,
    [OWNERID]     INT            NOT NULL,
    [BeneID]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

