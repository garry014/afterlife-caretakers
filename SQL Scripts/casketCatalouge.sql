CREATE TABLE [dbo].[Caskets]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [Name] VARCHAR(100) NOT NULL, 
    [Category] VARCHAR(20) NOT NULL, 
    [ImageLink] VARCHAR(100) NOT NULL, 
    [Price] FLOAT NOT NULL, 
    [SelectedTimes] SMALLINT NOT NULL, 
    [IsDeleted] BIT NOT NULL
)
