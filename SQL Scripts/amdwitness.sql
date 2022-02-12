CREATE TABLE [dbo].[amdwitness] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [name]     NVARCHAR (MAX) NOT NULL,
    [nric]     NVARCHAR (MAX) NOT NULL,
    [address]  NVARCHAR (MAX) NOT NULL,
    [postal]   NVARCHAR (6)   NOT NULL,
    [homeno]   NVARCHAR (MAX) NOT NULL,
    [officeno] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

