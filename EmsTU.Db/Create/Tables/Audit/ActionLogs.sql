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

exec spDescTable  N'ActionLogs', N'Лог на заявките.'
exec spDescColumn N'ActionLogs', N'ActionLogId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ActionLogs', N'ActionDate', N'Дата на заявката.'
exec spDescColumn N'ActionLogs', N'IP', N'IP на от който се достъпва.'
exec spDescColumn N'ActionLogs', N'Action', N'Action заявка.'
exec spDescColumn N'ActionLogs', N'ObjectId', N'Обект, който се предава.'
exec spDescColumn N'ActionLogs', N'RawUrl', N'Физическо Url.'
exec spDescColumn N'ActionLogs', N'Form', N'Данни от формата.'
exec spDescColumn N'ActionLogs', N'BrowserInfo', N'Информация за browser-а.'
exec spDescColumn N'ActionLogs', N'SessionId', N'Id на сесия.'
exec spDescColumn N'ActionLogs', N'LoginName', N'Логин име.'
exec spDescColumn N'ActionLogs', N'UserId', N'Уникален системно генериран идентификатор на потребител.'
exec spDescColumn N'ActionLogs', N'RequestId', N'Уникален системно генериран идентификатор за номера на заявката.'
GO
