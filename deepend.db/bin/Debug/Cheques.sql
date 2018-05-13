﻿CREATE TABLE [dbo].[Cheques]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PersonName] NVARCHAR(50) NOT NULL, 
    [DateTime] DATETIME NOT NULL, 
    [Amount] DECIMAL(18, 2) NOT NULL
)
