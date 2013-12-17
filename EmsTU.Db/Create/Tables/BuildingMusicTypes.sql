print 'BuildingMusicTypes'
GO 

CREATE TABLE [dbo].[BuildingMusicTypes](
	[BuildingMusicTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[MusicTypeId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingMusicTypes] PRIMARY KEY CLUSTERED 
(
	[BuildingMusicTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingMusicTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingMusicTypes_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingMusicTypes] CHECK CONSTRAINT [FK_BuildingMusicTypes_Buildings]
GO

ALTER TABLE [dbo].[BuildingMusicTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingMusicTypes_MusicTypes] FOREIGN KEY([MusicTypeId])
REFERENCES [dbo].[MusicTypes] ([MusicTypeId])
GO
ALTER TABLE [dbo].[BuildingMusicTypes] CHECK CONSTRAINT [FK_BuildingMusicTypes_MusicTypes]
GO

GO