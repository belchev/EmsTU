print 'BuildingUsers'
GO 

CREATE TABLE [dbo].[BuildingUsers](
	[BuildingUserId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingUsers] PRIMARY KEY CLUSTERED 
(
	[BuildingUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingUsers]  WITH CHECK ADD  CONSTRAINT [FK_BuildingUsers_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingUsers] CHECK CONSTRAINT [FK_BuildingUsers_Buildings]
GO

ALTER TABLE [dbo].[BuildingUsers]  WITH CHECK ADD  CONSTRAINT [FK_BuildingUsers_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BuildingUsers] CHECK CONSTRAINT [FK_BuildingUsers_Users]
GO