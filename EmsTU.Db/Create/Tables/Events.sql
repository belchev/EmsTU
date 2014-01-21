print 'Events'
GO 

CREATE TABLE [dbo].[Events](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Date] [datetime] NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Info] [nvarchar](max) NULL,
	[ImagePath] [nvarchar](100) NULL,
	[ImageThumbPath] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Events]  WITH CHECK ADD  CONSTRAINT [FK_Events_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Events] CHECK CONSTRAINT [FK_Events_Buildings]
GO

exec spDescTable  N'Events', N'Събития.'
exec spDescColumn N'Events', N'EventId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Events', N'BuildingId', N'Уникален системно генериран идентификатор на заведение.'
exec spDescColumn N'Events', N'Date', N'Дата на събитието.'
exec spDescColumn N'Events', N'Name', N'Заглавие на събитието.'
exec spDescColumn N'Events', N'Info', N'Допълнителни данни за събитието.'
exec spDescColumn N'Events', N'ImagePath', N'Физически път до снимката на събитието.'
exec spDescColumn N'Events', N'ImageThumbPath', N'Физически път до thumbnail снимката на събитието.'
exec spDescColumn N'Events', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Events', N'Version', N'Версия.'
GO