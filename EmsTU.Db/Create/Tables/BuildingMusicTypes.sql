print 'BuildingMusicTypes'
GO 

CREATE TABLE [dbo].[BuildingMusicTypes] (
    [BuildingId]     INT            NOT NULL,
    [MusicTypeId]     INT            NOT NULL,
    CONSTRAINT [PK_BuildingMusicTypes] PRIMARY KEY CLUSTERED ([BuildingId] ASC, [MusicTypeId] ASC),
    CONSTRAINT [FK_BuildingMusicTypes_Buildings] FOREIGN KEY ([BuildingId]) REFERENCES [dbo].[Buildings] ([BuildingId]),
    CONSTRAINT [FK_BuildingMusicTypes_MusicTypes] FOREIGN KEY ([MusicTypeId]) REFERENCES [dbo].[MusicTypes] ([MusicTypeId])
);
GO