print 'BuildingNoms'
GO 

CREATE TABLE [dbo].[BuildingNoms] (
    [BuildingId]     INT            NOT NULL,
    [NomId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingNoms] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [NomId] ASC),
    CONSTRAINT [FK_BuildingNoms_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingNoms_Noms] FOREIGN KEY ([NomId]) REFERENCES [dbo].[Noms] ([NomId])
);
GO

exec spDescTable  N'BuildingNoms', N'Номенклатури за заведение.'
exec spDescColumn N'BuildingNoms', N'NomId', N'Номенклатура.'
exec spDescColumn N'BuildingNoms', N'BuildingId', N'Заведение.'
GO