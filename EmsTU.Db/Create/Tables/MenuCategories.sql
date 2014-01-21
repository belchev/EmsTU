print 'MenuCategories'
GO 

CREATE TABLE [dbo].[MenuCategories](
	[MenuCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_MenuCategories] PRIMARY KEY CLUSTERED 
(
	[MenuCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MenuCategories]  WITH CHECK ADD  CONSTRAINT [FK_MenuCategories_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[MenuCategories] CHECK CONSTRAINT [FK_MenuCategories_Buildings]
GO

exec spDescTable  N'MenuCategories', N'Меню категории.'
exec spDescColumn N'MenuCategories', N'MenuCategoryId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'MenuCategories', N'BuildingId', N'Уникален системно генериран идентификатор на заведение.'
exec spDescColumn N'MenuCategories', N'Name', N'Име на меню категорията.'
exec spDescColumn N'MenuCategories', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'MenuCategories', N'Version', N'Версия.'
GO
