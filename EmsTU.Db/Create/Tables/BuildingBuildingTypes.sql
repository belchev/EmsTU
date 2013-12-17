print 'BuildingBuildingTypes'
GO 

CREATE TABLE [dbo].[BuildingBuildingTypes](
	[BuildingBuildingTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[BuildingTypeId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingBuildingTypes] PRIMARY KEY CLUSTERED 
(
	[BuildingBuildingTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingBuildingTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingBuildingTypes_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingBuildingTypes] CHECK CONSTRAINT [FK_BuildingBuildingTypes_Buildings]
GO

ALTER TABLE [dbo].[BuildingBuildingTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingBuildingTypes_BuildingTypes] FOREIGN KEY([BuildingTypeId])
REFERENCES [dbo].[BuildingTypes] ([BuildingTypeId])
GO
ALTER TABLE [dbo].[BuildingBuildingTypes] CHECK CONSTRAINT [FK_BuildingBuildingTypes_BuildingTypes]
GO

GO