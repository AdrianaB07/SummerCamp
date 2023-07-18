CREATE TABLE [dbo].[CompetitionMatch]
(
	[Id] INT NOT NULL PRIMARY KEY Identity(1,1), 
    [CompetitionId] INT NOT NULL, 
    [HomeTeamId] INT NOT NULL, 
    [AwayTeamId] INT NOT NULL, 
    [HomeTeamGoals] INT NOT NULL, 
    [AwayTeamGoals] INT NOT NULL,
    FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]),
    FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team] ([Id]),
    FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team] ([Id])
)
