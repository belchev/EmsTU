print 'AlbumPhotos'
GO 

CREATE TABLE [dbo].[AlbumPhotos](
	[AlbumPhotoId] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[ImageThumbPath] [nvarchar](100) NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[AlbumPhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[AlbumPhotos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Albums] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Albums] ([AlbumId])
GO
ALTER TABLE [dbo].[AlbumPhotos] CHECK CONSTRAINT [FK_Photos_Albums]
GO

exec spDescTable  N'AlbumPhotos', N'Снимки.'
exec spDescColumn N'AlbumPhotos', N'AlbumPhotoId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'AlbumPhotos', N'AlbumId', N'Уникален системно генериран идентификатор на албума.'
exec spDescColumn N'AlbumPhotos', N'ImagePath', N'Физически път до снимката.'
exec spDescColumn N'AlbumPhotos', N'ImageThumbPath', N'Физически път до thumbnail на снимката.'
exec spDescColumn N'AlbumPhotos', N'Version', N'Версия.'
GO
