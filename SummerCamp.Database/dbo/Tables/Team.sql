CREATE TABLE [dbo].[Team] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [NickName] VARCHAR (255) NULL,
    [Name]     VARCHAR (255) NULL,
    [CoachId]  INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([CoachId]) REFERENCES [dbo].[Coach] ([Id])
);

