﻿/*
Deployment script for SummerCamp

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SummerCamp"
:setvar DefaultFilePrefix "SummerCamp"
:setvar DefaultDataPath "D:\Microsoft SQL Express\MSSQL16.SQLEXPRESS01\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Microsoft SQL Express\MSSQL16.SQLEXPRESS01\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The type for column Adress in table [dbo].[Competition] is currently  VARCHAR (50) NULL but is being changed to  INT NULL. Data loss could occur and deployment may fail if the column contains data that is incompatible with type  INT NULL.
*/

IF EXISTS (select top 1 1 from [dbo].[Competition])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[Competition]...';


GO
ALTER TABLE [dbo].[Competition] DROP CONSTRAINT [FK__Competiti__Spons__59063A47];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[CompetitionTeam]...';


GO
ALTER TABLE [dbo].[CompetitionTeam] DROP CONSTRAINT [FK__Competiti__Compe__60A75C0F];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] DROP CONSTRAINT [FK__Competiti__Compe__70DDC3D8];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] DROP CONSTRAINT [FK__Competiti__HomeT__71D1E811];


GO
PRINT N'Dropping Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] DROP CONSTRAINT [FK__Competiti__AwayT__72C60C4A];


GO
PRINT N'Dropping Unique Constraint [dbo].[UQ_PlayerId_TeamId]...';


GO
ALTER TABLE [dbo].[Player] DROP CONSTRAINT [UQ_PlayerId_TeamId];


GO
PRINT N'Starting rebuilding table [dbo].[Competition]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Competition] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (255) NULL,
    [NumberOfTeams] INT           NULL,
    [Adress]        INT           NULL,
    [StartDate]     DATE          NULL,
    [EndDate]       DATE          NULL,
    [SponsorId]     INT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Competition])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Competition] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Competition] ([Id], [Name], [NumberOfTeams], [StartDate], [EndDate], [SponsorId], [Adress])
        SELECT   [Id],
                 [Name],
                 [NumberOfTeams],
                 [StartDate],
                 [EndDate],
                 [SponsorId],
                 [Adress]
        FROM     [dbo].[Competition]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Competition] OFF;
    END

DROP TABLE [dbo].[Competition];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Competition]', N'Competition';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[CompetitionMatch]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_CompetitionMatch] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
    [CompetitionId] INT NOT NULL,
    [HomeTeamId]    INT NOT NULL,
    [AwayTeamId]    INT NOT NULL,
    [HomeTeamGoals] INT NOT NULL,
    [AwayTeamGoals] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[CompetitionMatch])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CompetitionMatch] ON;
        INSERT INTO [dbo].[tmp_ms_xx_CompetitionMatch] ([Id], [CompetitionId], [HomeTeamId], [AwayTeamId], [HomeTeamGoals], [AwayTeamGoals])
        SELECT   [Id],
                 [CompetitionId],
                 [HomeTeamId],
                 [AwayTeamId],
                 [HomeTeamGoals],
                 [AwayTeamGoals]
        FROM     [dbo].[CompetitionMatch]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_CompetitionMatch] OFF;
    END

DROP TABLE [dbo].[CompetitionMatch];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_CompetitionMatch]', N'CompetitionMatch';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Unique Constraint [dbo].[UQ_Team_Name]...';


GO
ALTER TABLE [dbo].[Player]
    ADD CONSTRAINT [UQ_Team_Name] UNIQUE NONCLUSTERED ([TeamId] ASC, [ShirtNumber] ASC);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Competition]...';


GO
ALTER TABLE [dbo].[Competition] WITH NOCHECK
    ADD FOREIGN KEY ([SponsorId]) REFERENCES [dbo].[Sponsor] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[CompetitionTeam]...';


GO
ALTER TABLE [dbo].[CompetitionTeam] WITH NOCHECK
    ADD FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] WITH NOCHECK
    ADD FOREIGN KEY ([CompetitionId]) REFERENCES [dbo].[Competition] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] WITH NOCHECK
    ADD FOREIGN KEY ([HomeTeamId]) REFERENCES [dbo].[Team] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[CompetitionMatch]...';


GO
ALTER TABLE [dbo].[CompetitionMatch] WITH NOCHECK
    ADD FOREIGN KEY ([AwayTeamId]) REFERENCES [dbo].[Team] ([Id]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Competition'), OBJECT_ID(N'dbo.CompetitionTeam'), OBJECT_ID(N'dbo.CompetitionMatch'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
