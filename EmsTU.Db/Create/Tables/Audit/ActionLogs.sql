print 'ActionLogs'
GO 

CREATE TABLE [dbo].[ActionLogs](
	[ActionLogId] [int] IDENTITY(1,1) NOT NULL,
	[ActionDate] [datetime] NULL,
	[IP] [nvarchar](50) NULL,
	[Action] [nvarchar](200) NULL,
	[ObjectId] [nvarchar](200) NULL,
	[RawUrl] [nvarchar](500) NULL,
	[Form] [nvarchar](500) NULL,
	[BrowserInfo] [nvarchar](200) NULL,
	[SessionId] [nvarchar](50) NULL,
	[LoginName] [nvarchar](200) NULL,
	[UserId] [int] NULL,
	[RequestId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_ActionLogs] PRIMARY KEY CLUSTERED 
(
	[ActionLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

