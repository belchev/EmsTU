print 'AlbumPhotos'
GO 

CREATE TABLE [dbo].[AlbumPhotos](
	[AlbumPhotoId] [int] IDENTITY(1,1) NOT NULL,
	[AlbumId] [int] NOT NULL,
	[Image] [image] NOT NULL,
	[ImageThumb] [image] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[AlbumPhotoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[AlbumPhotos]  WITH CHECK ADD  CONSTRAINT [FK_Photos_Albums] FOREIGN KEY([AlbumId])
REFERENCES [dbo].[Albums] ([AlbumId])
GO
ALTER TABLE [dbo].[AlbumPhotos] CHECK CONSTRAINT [FK_Photos_Albums]
GO
GO