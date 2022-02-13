CREATE TABLE [dbo].[Admins] (
    [Id]             INT           IDENTITY (1, 1) NOT NULL,
    [name]           VARCHAR (500) NOT NULL,
    [email]          VARCHAR (254) NOT NULL,
    [password]       VARCHAR (100) NOT NULL,
    [gender]         VARCHAR (10)  NOT NULL,
    [admin_role]     VARCHAR (20)    NOT NULL,
    [office_num]     VARCHAR (8)   NOT NULL,
    [specialisation] VARCHAR (MAX) NULL,
    [status]         VARCHAR (100) NULL,
    [clinic_address] VARCHAR (MAX) NULL,
    [creationuser]   VARCHAR (100) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

