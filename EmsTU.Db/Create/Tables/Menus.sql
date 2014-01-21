print 'Menus'
GO 

CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Info] [nvarchar](200) NULL,
	[Size] [int] NULL,
	[Price] [decimal](15, 3) NULL,
	[ImagePath] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_MenuCategories] FOREIGN KEY([MenuCategoryId])
REFERENCES [dbo].[MenuCategories] ([MenuCategoryId])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_MenuCategories]
GO

exec spDescTable  N'Menus', N'Менюта.'
exec spDescColumn N'Menus', N'MenuId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Menus', N'MenuCategoryId', N'Уникален системно генериран идентификатор на типа категория.'
exec spDescColumn N'Menus', N'Name', N'Име на менюто.'
exec spDescColumn N'Menus', N'Info', N'Допълнителни данни за менюто.'
exec spDescColumn N'Menus', N'Price', N'Цена за менюто.'
exec spDescColumn N'Menus', N'Size', N'Грамаж на менюто.'
exec spDescColumn N'Menus', N'ImagePath', N'Физически път до снимката на менюто.'
exec spDescColumn N'Menus', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Menus', N'Version', N'Версия.'
GO
