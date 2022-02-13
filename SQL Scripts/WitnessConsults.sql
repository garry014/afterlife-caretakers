CREATE TABLE [dbo].[WitnessConsults] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [ServiceType]   VARCHAR (11)  NULL,
    [ServiceStatus] BIT           NOT NULL,
    [ConsultName]   VARCHAR (20)  NOT NULL,
    [Experience]    INT           NOT NULL,
    [ProfileImg]    VARCHAR (MAX) NULL,
    [PublishStatus] BIT           NULL,
    [UserId]        INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);