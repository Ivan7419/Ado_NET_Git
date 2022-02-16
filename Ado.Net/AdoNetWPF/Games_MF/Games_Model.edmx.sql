
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/15/2022 18:22:13
-- Generated from EDMX file: C:\Users\37206\source\repos\Ado_NET_Git\Ado.Net\AdoNetWPF\Games_MF\Games_Model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Onischenko];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GamesSet'
CREATE TABLE [dbo].[GamesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ReleaseDate] datetime  NOT NULL,
    [Distributor] nvarchar(max)  NOT NULL,
    [Platform] nvarchar(max)  NOT NULL,
    [Developer_Id] int  NOT NULL
);
GO

-- Creating table 'DeveloperSet'
CREATE TABLE [dbo].[DeveloperSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'GamesSet'
ALTER TABLE [dbo].[GamesSet]
ADD CONSTRAINT [PK_GamesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'DeveloperSet'
ALTER TABLE [dbo].[DeveloperSet]
ADD CONSTRAINT [PK_DeveloperSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Developer_Id] in table 'GamesSet'
ALTER TABLE [dbo].[GamesSet]
ADD CONSTRAINT [FK_DeveloperGames]
    FOREIGN KEY ([Developer_Id])
    REFERENCES [dbo].[DeveloperSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeveloperGames'
CREATE INDEX [IX_FK_DeveloperGames]
ON [dbo].[GamesSet]
    ([Developer_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------