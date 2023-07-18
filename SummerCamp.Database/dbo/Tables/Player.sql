CREATE TABLE [dbo].[Player] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [FullName]    VARCHAR (255) NULL,
    [BirthDate]   DATE          NULL,
    [Adress]      VARCHAR (255) NULL,
    [Position]    INT           NULL,
    [ShirtNumber] INT           NULL,
    [TeamId]      INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [UQ_Team_Name] UNIQUE NONCLUSTERED ([TeamId] ASC, [ShirtNumber] ASC)
);

