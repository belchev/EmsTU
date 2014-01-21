print 'Ratings'
GO 

CREATE TABLE [dbo].[Ratings](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Rating] [int] NULL,
	[Ip] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Ratings] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Ratings]  WITH CHECK ADD  CONSTRAINT [FK_Ratings_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Ratings] CHECK CONSTRAINT [FK_Ratings_Buildings]
GO

exec spDescTable  N'Ratings', N'Рейтинги.'
exec spDescColumn N'Ratings', N'RatingId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Ratings', N'BuildingId', N'Уникален системно генериран идентификатор на заведение.'
exec spDescColumn N'Ratings', N'Rating', N'Рейтинг.'
exec spDescColumn N'Ratings', N'Ip', N'Ip адрес.'
exec spDescColumn N'Ratings', N'Version', N'Версия.'
GO