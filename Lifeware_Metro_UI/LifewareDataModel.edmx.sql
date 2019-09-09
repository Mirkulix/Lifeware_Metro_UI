
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/09/2019 12:45:47
-- Generated from EDMX file: C:\Users\Alfa\Desktop\MeinRepro\Lifeware_Metro_UI\Lifeware_Metro_UI\LifewareDataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ePatientDBlocal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_LoginPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Patients] DROP CONSTRAINT [FK_LoginPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_Pat_Medi_DataAllergien]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Allergiens] DROP CONSTRAINT [FK_Pat_Medi_DataAllergien];
GO
IF OBJECT_ID(N'[dbo].[FK_Pat_Medi_DataDokumente]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Dokumentes] DROP CONSTRAINT [FK_Pat_Medi_DataDokumente];
GO
IF OBJECT_ID(N'[dbo].[FK_Pat_Medi_DataErkrankungen]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Erkrankungens] DROP CONSTRAINT [FK_Pat_Medi_DataErkrankungen];
GO
IF OBJECT_ID(N'[dbo].[FK_Pat_Medi_DataMedikation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Medikations] DROP CONSTRAINT [FK_Pat_Medi_DataMedikation];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientArzt]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Arzts] DROP CONSTRAINT [FK_PatientArzt];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientNotfallaktion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notfallaktions] DROP CONSTRAINT [FK_PatientNotfallaktion];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientPat_Medi_Data]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Pat_Medi_Datas] DROP CONSTRAINT [FK_PatientPat_Medi_Data];
GO
IF OBJECT_ID(N'[dbo].[FK_PatientsNotfallkontakte]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Notfallkontakte] DROP CONSTRAINT [FK_PatientsNotfallkontakte];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Allergiens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Allergiens];
GO
IF OBJECT_ID(N'[dbo].[Arzts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Arzts];
GO
IF OBJECT_ID(N'[dbo].[Dokumentes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Dokumentes];
GO
IF OBJECT_ID(N'[dbo].[Erkrankungens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Erkrankungens];
GO
IF OBJECT_ID(N'[dbo].[Logins]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Logins];
GO
IF OBJECT_ID(N'[dbo].[Medikations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Medikations];
GO
IF OBJECT_ID(N'[dbo].[Notfallaktions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notfallaktions];
GO
IF OBJECT_ID(N'[dbo].[Notfallkontakte]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Notfallkontakte];
GO
IF OBJECT_ID(N'[dbo].[Pat_Medi_Datas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Pat_Medi_Datas];
GO
IF OBJECT_ID(N'[dbo].[Patients]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patients];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Allergiens'
CREATE TABLE [dbo].[Allergiens] (
    [Id_Allergien] int IDENTITY(1,1) NOT NULL,
    [Allergie] nvarchar(max)  NULL,
    [Wann] datetime  NULL,
    [Reaktion] nvarchar(max)  NULL,
    [Besonderheit] nvarchar(max)  NULL,
    [Pat_Medi_DataId_Pat_Medi_Data] int  NOT NULL
);
GO

-- Creating table 'Arzts'
CREATE TABLE [dbo].[Arzts] (
    [Id_Artz] int IDENTITY(1,1) NOT NULL,
    [Anrede] nvarchar(max)  NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Nachname] nvarchar(max)  NOT NULL,
    [Straße] nvarchar(max)  NULL,
    [Nr] smallint  NULL,
    [PLZ] smallint  NULL,
    [Ort] nvarchar(max)  NULL,
    [Telefon] nvarchar(max)  NOT NULL,
    [PatientId_Patient] int  NOT NULL
);
GO

-- Creating table 'Dokumentes'
CREATE TABLE [dbo].[Dokumentes] (
    [Id_Dokumente] int IDENTITY(1,1) NOT NULL,
    [Was] nvarchar(max)  NULL,
    [Date] datetime  NULL,
    [Wo] nvarchar(max)  NULL,
    [Datei] varbinary(max)  NULL,
    [Pat_Medi_DataId_Pat_Medi_Data] int  NOT NULL
);
GO

-- Creating table 'Erkrankungens'
CREATE TABLE [dbo].[Erkrankungens] (
    [Id_Erkrankung] int IDENTITY(1,1) NOT NULL,
    [Erkrankung] nvarchar(max)  NULL,
    [Wann] datetime  NULL,
    [Chronisch] nvarchar(max)  NULL,
    [Pat_Medi_DataId_Pat_Medi_Data] int  NOT NULL
);
GO

-- Creating table 'Logins'
CREATE TABLE [dbo].[Logins] (
    [Id_Login] int IDENTITY(1,1) NOT NULL,
    [Username] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [EMail] nvarchar(max)  NULL,
    [AES_ID] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Medikations'
CREATE TABLE [dbo].[Medikations] (
    [Id_Medikation] int IDENTITY(1,1) NOT NULL,
    [Medikament] nvarchar(max)  NULL,
    [Art] nvarchar(max)  NULL,
    [Dosis] nvarchar(max)  NULL,
    [Einnahmeregel] nvarchar(max)  NULL,
    [Besonderheit] nvarchar(max)  NULL,
    [Wann] datetime  NULL,
    [Pat_Medi_DataId_Pat_Medi_Data] int  NOT NULL
);
GO

-- Creating table 'Notfallaktions'
CREATE TABLE [dbo].[Notfallaktions] (
    [Id_Nottaktion] int IDENTITY(1,1) NOT NULL,
    [Aktion] nvarchar(max)  NULL,
    [Vorname] nvarchar(max)  NULL,
    [Nachname] nvarchar(max)  NULL,
    [Straße] nvarchar(max)  NULL,
    [Nr] smallint  NULL,
    [PLZ] smallint  NULL,
    [Ort] nvarchar(max)  NULL,
    [Telefon] nvarchar(max)  NULL,
    [Besonderheit] nvarchar(max)  NULL,
    [PatientId_Patient] int  NOT NULL
);
GO

-- Creating table 'Notfallkontakte'
CREATE TABLE [dbo].[Notfallkontakte] (
    [Id_Notfallkontakte] int IDENTITY(1,1) NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Telefonnummer] nvarchar(max)  NOT NULL,
    [Strasse] nvarchar(max)  NOT NULL,
    [PLZ] nvarchar(max)  NOT NULL,
    [Ort] nvarchar(max)  NOT NULL,
    [PatientsId_Patient] int  NOT NULL
);
GO

-- Creating table 'Pat_Medi_Datas'
CREATE TABLE [dbo].[Pat_Medi_Datas] (
    [Id_Pat_Medi_Data] int IDENTITY(1,1) NOT NULL,
    [Organspende] bit  NULL,
    [Patfafü] bit  NULL,
    [Betreuungsverfügung] bit  NULL,
    [PatientId_Patient] int  NOT NULL,
    [Sorgerechtsverfügung] bit  NOT NULL
);
GO

-- Creating table 'Patients'
CREATE TABLE [dbo].[Patients] (
    [Id_Patient] int IDENTITY(1,1) NOT NULL,
    [Anrede] nvarchar(max)  NOT NULL,
    [Vorname] nvarchar(max)  NOT NULL,
    [Nachname] nvarchar(max)  NOT NULL,
    [Straße] nvarchar(max)  NULL,
    [Nr] smallint  NULL,
    [PLZ] smallint  NULL,
    [Ort] nvarchar(max)  NULL,
    [geb] nvarchar(max)  NOT NULL,
    [ort_geb] nvarchar(max)  NOT NULL,
    [Telefon] nvarchar(max)  NOT NULL,
    [Krankenkasse] nvarchar(max)  NULL,
    [KrankenkassenNR] nvarchar(max)  NULL,
    [PersonalausweisNR] nvarchar(max)  NOT NULL,
    [FührerscheinNR] nvarchar(max)  NOT NULL,
    [Image] varbinary(max)  NULL,
    [LoginId_Login] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Allergien] in table 'Allergiens'
ALTER TABLE [dbo].[Allergiens]
ADD CONSTRAINT [PK_Allergiens]
    PRIMARY KEY CLUSTERED ([Id_Allergien] ASC);
GO

-- Creating primary key on [Id_Artz] in table 'Arzts'
ALTER TABLE [dbo].[Arzts]
ADD CONSTRAINT [PK_Arzts]
    PRIMARY KEY CLUSTERED ([Id_Artz] ASC);
GO

-- Creating primary key on [Id_Dokumente] in table 'Dokumentes'
ALTER TABLE [dbo].[Dokumentes]
ADD CONSTRAINT [PK_Dokumentes]
    PRIMARY KEY CLUSTERED ([Id_Dokumente] ASC);
GO

-- Creating primary key on [Id_Erkrankung] in table 'Erkrankungens'
ALTER TABLE [dbo].[Erkrankungens]
ADD CONSTRAINT [PK_Erkrankungens]
    PRIMARY KEY CLUSTERED ([Id_Erkrankung] ASC);
GO

-- Creating primary key on [Id_Login] in table 'Logins'
ALTER TABLE [dbo].[Logins]
ADD CONSTRAINT [PK_Logins]
    PRIMARY KEY CLUSTERED ([Id_Login] ASC);
GO

-- Creating primary key on [Id_Medikation] in table 'Medikations'
ALTER TABLE [dbo].[Medikations]
ADD CONSTRAINT [PK_Medikations]
    PRIMARY KEY CLUSTERED ([Id_Medikation] ASC);
GO

-- Creating primary key on [Id_Nottaktion] in table 'Notfallaktions'
ALTER TABLE [dbo].[Notfallaktions]
ADD CONSTRAINT [PK_Notfallaktions]
    PRIMARY KEY CLUSTERED ([Id_Nottaktion] ASC);
GO

-- Creating primary key on [Id_Notfallkontakte] in table 'Notfallkontakte'
ALTER TABLE [dbo].[Notfallkontakte]
ADD CONSTRAINT [PK_Notfallkontakte]
    PRIMARY KEY CLUSTERED ([Id_Notfallkontakte] ASC);
GO

-- Creating primary key on [Id_Pat_Medi_Data] in table 'Pat_Medi_Datas'
ALTER TABLE [dbo].[Pat_Medi_Datas]
ADD CONSTRAINT [PK_Pat_Medi_Datas]
    PRIMARY KEY CLUSTERED ([Id_Pat_Medi_Data] ASC);
GO

-- Creating primary key on [Id_Patient] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [PK_Patients]
    PRIMARY KEY CLUSTERED ([Id_Patient] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Pat_Medi_DataId_Pat_Medi_Data] in table 'Allergiens'
ALTER TABLE [dbo].[Allergiens]
ADD CONSTRAINT [FK_Pat_Medi_DataAllergien]
    FOREIGN KEY ([Pat_Medi_DataId_Pat_Medi_Data])
    REFERENCES [dbo].[Pat_Medi_Datas]
        ([Id_Pat_Medi_Data])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pat_Medi_DataAllergien'
CREATE INDEX [IX_FK_Pat_Medi_DataAllergien]
ON [dbo].[Allergiens]
    ([Pat_Medi_DataId_Pat_Medi_Data]);
GO

-- Creating foreign key on [PatientId_Patient] in table 'Arzts'
ALTER TABLE [dbo].[Arzts]
ADD CONSTRAINT [FK_PatientArzt]
    FOREIGN KEY ([PatientId_Patient])
    REFERENCES [dbo].[Patients]
        ([Id_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientArzt'
CREATE INDEX [IX_FK_PatientArzt]
ON [dbo].[Arzts]
    ([PatientId_Patient]);
GO

-- Creating foreign key on [Pat_Medi_DataId_Pat_Medi_Data] in table 'Dokumentes'
ALTER TABLE [dbo].[Dokumentes]
ADD CONSTRAINT [FK_Pat_Medi_DataDokumente]
    FOREIGN KEY ([Pat_Medi_DataId_Pat_Medi_Data])
    REFERENCES [dbo].[Pat_Medi_Datas]
        ([Id_Pat_Medi_Data])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pat_Medi_DataDokumente'
CREATE INDEX [IX_FK_Pat_Medi_DataDokumente]
ON [dbo].[Dokumentes]
    ([Pat_Medi_DataId_Pat_Medi_Data]);
GO

-- Creating foreign key on [Pat_Medi_DataId_Pat_Medi_Data] in table 'Erkrankungens'
ALTER TABLE [dbo].[Erkrankungens]
ADD CONSTRAINT [FK_Pat_Medi_DataErkrankungen]
    FOREIGN KEY ([Pat_Medi_DataId_Pat_Medi_Data])
    REFERENCES [dbo].[Pat_Medi_Datas]
        ([Id_Pat_Medi_Data])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pat_Medi_DataErkrankungen'
CREATE INDEX [IX_FK_Pat_Medi_DataErkrankungen]
ON [dbo].[Erkrankungens]
    ([Pat_Medi_DataId_Pat_Medi_Data]);
GO

-- Creating foreign key on [LoginId_Login] in table 'Patients'
ALTER TABLE [dbo].[Patients]
ADD CONSTRAINT [FK_LoginPatient]
    FOREIGN KEY ([LoginId_Login])
    REFERENCES [dbo].[Logins]
        ([Id_Login])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LoginPatient'
CREATE INDEX [IX_FK_LoginPatient]
ON [dbo].[Patients]
    ([LoginId_Login]);
GO

-- Creating foreign key on [Pat_Medi_DataId_Pat_Medi_Data] in table 'Medikations'
ALTER TABLE [dbo].[Medikations]
ADD CONSTRAINT [FK_Pat_Medi_DataMedikation]
    FOREIGN KEY ([Pat_Medi_DataId_Pat_Medi_Data])
    REFERENCES [dbo].[Pat_Medi_Datas]
        ([Id_Pat_Medi_Data])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Pat_Medi_DataMedikation'
CREATE INDEX [IX_FK_Pat_Medi_DataMedikation]
ON [dbo].[Medikations]
    ([Pat_Medi_DataId_Pat_Medi_Data]);
GO

-- Creating foreign key on [PatientId_Patient] in table 'Notfallaktions'
ALTER TABLE [dbo].[Notfallaktions]
ADD CONSTRAINT [FK_PatientNotfallaktion]
    FOREIGN KEY ([PatientId_Patient])
    REFERENCES [dbo].[Patients]
        ([Id_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientNotfallaktion'
CREATE INDEX [IX_FK_PatientNotfallaktion]
ON [dbo].[Notfallaktions]
    ([PatientId_Patient]);
GO

-- Creating foreign key on [PatientsId_Patient] in table 'Notfallkontakte'
ALTER TABLE [dbo].[Notfallkontakte]
ADD CONSTRAINT [FK_PatientsNotfallkontakte]
    FOREIGN KEY ([PatientsId_Patient])
    REFERENCES [dbo].[Patients]
        ([Id_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientsNotfallkontakte'
CREATE INDEX [IX_FK_PatientsNotfallkontakte]
ON [dbo].[Notfallkontakte]
    ([PatientsId_Patient]);
GO

-- Creating foreign key on [PatientId_Patient] in table 'Pat_Medi_Datas'
ALTER TABLE [dbo].[Pat_Medi_Datas]
ADD CONSTRAINT [FK_PatientPat_Medi_Data]
    FOREIGN KEY ([PatientId_Patient])
    REFERENCES [dbo].[Patients]
        ([Id_Patient])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PatientPat_Medi_Data'
CREATE INDEX [IX_FK_PatientPat_Medi_Data]
ON [dbo].[Pat_Medi_Datas]
    ([PatientId_Patient]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------