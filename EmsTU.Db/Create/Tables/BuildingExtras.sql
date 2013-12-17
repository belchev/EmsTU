print 'BuildingExtras'
GO 

CREATE TABLE [dbo].[BuildingExtras](
	[BuildingExtraId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[ExtraId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingExtras] PRIMARY KEY CLUSTERED 
(
	[BuildingExtraId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingExtras]  WITH CHECK ADD  CONSTRAINT [FK_BuildingExtras_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingExtras] CHECK CONSTRAINT [FK_BuildingExtras_Buildings]
GO

ALTER TABLE [dbo].[BuildingExtras]  WITH CHECK ADD  CONSTRAINT [FK_BuildingExtras_Extras] FOREIGN KEY([ExtraId])
REFERENCES [dbo].[Extras] ([ExtraId])
GO
ALTER TABLE [dbo].[BuildingExtras] CHECK CONSTRAINT [FK_BuildingExtras_Extras]
GO

GO