﻿print 'Visitors'
GO 

CREATE TABLE [dbo].[Visitors](
	[VisitorId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingId] [int] NOT NULL,
	[Ip] [nvarchar](50) NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Visitors] PRIMARY KEY CLUSTERED 
(
	[VisitorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Visitors]  WITH CHECK ADD  CONSTRAINT [FK_Visitors_Buildings] FOREIGN KEY([BuildingId])
REFERENCES [dbo].[Buildings] ([BuildingId])
GO
ALTER TABLE [dbo].[Visitors] CHECK CONSTRAINT [FK_Visitors_Buildings]
GO

GO