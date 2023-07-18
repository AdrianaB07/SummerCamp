CREATE TABLE [dbo].[CompetitionTeam] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [TeamId]        INT NULL,
    [CompetitionId] INT NULL,
    [TotalPoints] INT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id])
);

