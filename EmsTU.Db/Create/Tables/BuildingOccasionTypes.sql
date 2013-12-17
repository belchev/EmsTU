print 'BuildingOccasionTypes'
GO 

CREATE TABLE [dbo].[BuildingOccasionTypes](
	[BuildingOccasionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[OccasionTypeId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingOccasionTypes] PRIMARY KEY CLUSTERED 
(
	[BuildingOccasionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingOccasionTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingOccasionTypes_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingOccasionTypes] CHECK CONSTRAINT [FK_BuildingOccasionTypes_Buildings]
GO

ALTER TABLE [dbo].[BuildingOccasionTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingOccasionTypes_OccasionTypes] FOREIGN KEY([OccasionTypeId])
REFERENCES [dbo].[OccasionTypes] ([OccasionTypeId])
GO
ALTER TABLE [dbo].[BuildingOccasionTypes] CHECK CONSTRAINT [FK_BuildingOccasionTypes_OccasionTypes]
GO

GO