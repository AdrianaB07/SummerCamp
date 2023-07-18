CREATE TABLE [dbo].[Competition] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (255) NULL,
    [NumberOfTeams] INT           NULL,
    [Adress]        VARCHAR(255)           NULL,
    [StartDate]     DATE          NULL,
    [EndDate]       DATE          NULL,
    [SponsorId]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([SponsorId]) REFERENCES [dbo].[Sponsor] ([Id])
);

