print 'Albums'
GO 

CREATE TABLE [dbo].[Albums](
	[AlbumId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Name] [nvarchar](150) NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Albums] PRIMARY KEY CLUSTERED 
(
	[AlbumId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Albums]  WITH CHECK ADD  CONSTRAINT [FK_Albums_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Albums] CHECK CONSTRAINT [FK_Albums_Buildings]
GO

exec spDescTable  N'Albums', N'Албуми.'
exec spDescColumn N'Albums', N'AlbumId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Albums', N'BuildingId', N'Уникален системно генериран идентификатор на заведение.'
exec spDescColumn N'Albums', N'Name', N'Име на албума.'
exec spDescColumn N'Albums', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Albums', N'Version', N'Версия.'
GO
