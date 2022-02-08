CREATE TABLE [dbo].[VideoMemo]
(
	[Id] INT NOT NULL IDENTITY(1,1) PRIMARY KEY, 
    [videoLink] VARCHAR(200) NOT NULL, 
    [releasePeriod] SMALLINT NOT NULL, 
    [writtenMemo] VARCHAR(MAX) NULL, 
    [willMakerID] INT NOT NULL
)
