print 'BuildingUserRoles'
GO 

CREATE TABLE [dbo].[BuildingUserRoles](
	[BuildingUserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_BuildingUserRoles] PRIMARY KEY CLUSTERED 
(
	[BuildingUserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[BuildingUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_BuildingUserRoles_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[BuildingUserRoles] CHECK CONSTRAINT [FK_BuildingUserRoles_Buildings]
GO

ALTER TABLE [dbo].[BuildingUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_BuildingUserRoles_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[BuildingUserRoles] CHECK CONSTRAINT [FK_BuildingUserRoles_Roles]
GO

ALTER TABLE [dbo].[BuildingUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_BuildingUserRoles_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[BuildingUserRoles] CHECK CONSTRAINT [FK_BuildingUserRoles_Users]
GO