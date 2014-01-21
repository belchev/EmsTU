print 'Comments'
GO 

CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Name][nvarchar](50) NULL,
	[Comment] [nvarchar](1000) NULL,
	[Date] [datetime] NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Buildings]
GO

exec spDescTable  N'Comments', N'Коментари.'
exec spDescColumn N'Comments', N'CommentId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Comments', N'BuildingId', N'Уникален системно генериран идентификатор на заведение.'
exec spDescColumn N'Comments', N'Name', N'Име на коментиращия.'
exec spDescColumn N'Comments', N'Comment', N'Коментар.'
exec spDescColumn N'Comments', N'Date', N'Дата на коментара.'
exec spDescColumn N'Comments', N'Version', N'Версия.'
GO