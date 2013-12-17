print 'BuildingKitchenTypes'
GO 

CREATE TABLE [dbo].[BuildingKitchenTypes](
	[BuildingKitchenTypeId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[KitchenTypeId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingKitchenTypes] PRIMARY KEY CLUSTERED 
(
	[BuildingKitchenTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingKitchenTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingKitchenTypes_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingKitchenTypes] CHECK CONSTRAINT [FK_BuildingKitchenTypes_Buildings]
GO

ALTER TABLE [dbo].[BuildingKitchenTypes]  WITH CHECK ADD  CONSTRAINT [FK_BuildingKitchenTypes_KitchenTypes] FOREIGN KEY([KitchenTypeId])
REFERENCES [dbo].[KitchenTypes] ([KitchenTypeId])
GO
ALTER TABLE [dbo].[BuildingKitchenTypes] CHECK CONSTRAINT [FK_BuildingKitchenTypes_KitchenTypes]
GO


GO