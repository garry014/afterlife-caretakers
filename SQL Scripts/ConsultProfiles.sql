CREATE TABLE [dbo].[ConsultProfiles] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ServiceType]   VARCHAR (20)  NULL,
    [ServiceStatus] BIT           NOT NULL,
    [ConsultName]   VARCHAR (20)  NOT NULL,
    [Rate]          INT           NOT NULL,
    [Experience]    INT           NOT NULL,
    [ProfileImg]    VARCHAR (MAX) NULL,
    [Description1]  VARCHAR (500) NOT NULL,
    [Description2]  VARCHAR (500) NOT NULL,
    [PublishStatus] BIT           NULL,
    [UserId]        INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

