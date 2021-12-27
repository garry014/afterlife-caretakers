CREATE TABLE [dbo].[casketCatalouge]
(
	[casketID] INT NOT NULL PRIMARY KEY, 
    [name] VARCHAR(100) NOT NULL, 
    [category] VARCHAR(20) NOT NULL, 
    [imageLink] VARCHAR(100) NOT NULL, 
    [price] FLOAT NOT NULL, 
    [selectedTimes] SMALLINT NOT NULL, 
    [isDeleted] BIT NOT NULL
)
