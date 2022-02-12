CREATE TABLE [dbo].[BVideoPermission] (
	[Id]          INT IDENTITY (1, 1) NOT NULL,
    [bene_id] VARCHAR(350) NOT NULL,
    [video_id]  INT NOT NULL
);