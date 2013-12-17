print 'Menus'
GO 

CREATE TABLE [dbo].[Menus](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuCategoryId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Info] [nvarchar](200) NULL,
	[Size] [int] NULL,
	[Price] [decimal](15, 3) NULL,
	[Image] [image] NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Menus] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Menus]  WITH CHECK ADD  CONSTRAINT [FK_Menus_MenuCategories] FOREIGN KEY([MenuCategoryId])
REFERENCES [dbo].[MenuCategories] ([MenuCategoryId])
GO
ALTER TABLE [dbo].[Menus] CHECK CONSTRAINT [FK_Menus_MenuCategories]
GO

GO