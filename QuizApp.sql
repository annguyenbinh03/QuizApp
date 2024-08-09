 GO
 IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'QuizApp')
 DROP DATABASE [QuizApp]
 GO
 CREATE DATABASE [QuizApp]
 GO
 GO
 USE [QuizApp]
 GO

 
CREATE TABLE [dbo].[User](
	[userId] INT IDENTITY(1,1) PRIMARY KEY,
	[fullName] NVARCHAR(30)
)

CREATE TABLE [dbo].[Deck](
	[deckId] INT IDENTITY(1,1) PRIMARY KEY,
	[name] NVARCHAR(50),
	[color] NVARCHAR(20),
	[creatorId] INT,
	CONSTRAINT FK_Deck_User FOREIGN KEY ([creatorId]) REFERENCES [dbo].[User] ([userId])
)

CREATE TABLE [dbo].[Card](
	[cardId] INT IDENTITY(1,1) PRIMARY KEY,
	[question] NVARCHAR(500) NOT NULL,
	[answer] NVARCHAR(500) NOT NULL,
	[deckId] INT,
	CONSTRAINT FK_Card_Deck FOREIGN KEY ([deckId]) REFERENCES [dbo].[Deck] ([deckId])
)


