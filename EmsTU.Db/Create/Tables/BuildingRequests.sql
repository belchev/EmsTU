print 'BuildingRequests'
GO 

CREATE TABLE [dbo].[BuildingRequests](
	[BuildingRequestId] [int] IDENTITY(1,1) NOT NULL,
	[BuildingName] [nvarchar](100) NULL,
	[ContactName] [nvarchar](100) NULL,
	[UserName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[WebSite] [nvarchar](50) NULL,
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_BuildingRequests] PRIMARY KEY CLUSTERED 
(
	[BuildingRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO