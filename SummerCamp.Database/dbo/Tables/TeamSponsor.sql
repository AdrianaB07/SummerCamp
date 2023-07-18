CREATE TABLE [dbo].[TeamSponsor] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [TeamId]    INT NULL,
    [SponsorId] INT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SponsorId]) REFERENCES [dbo].[Sponsor] ([Id]),
    FOREIGN KEY ([TeamId]) REFERENCES [dbo].[Team] ([Id]),
    CONSTRAINT [UQ_Team_Sponsor] UNIQUE NONCLUSTERED ([TeamId] ASC, [SponsorId] ASC)
);

