print 'ActionErrorLogs'
GO 

CREATE TABLE [dbo].[ActionErrorLogs](
	[ActionErrorLogId] [int] IDENTITY(1,1) NOT NULL,
	[RequestId] [uniqueidentifier] NULL,
	[RequestInfo] [nvarchar](max) NULL,
	[ErrorInfo] [nvarchar](max) NULL,
	[ActionErrorDate] [datetime] NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_ActionErrorLogs] PRIMARY KEY CLUSTERED 
(
	[ActionErrorLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

exec spDescTable  N'ActionErrorLogs', N'Лог на заявките с грешки.'
exec spDescColumn N'ActionErrorLogs', N'ActionErrorLogId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'ActionErrorLogs', N'RequestId', N'Уникален системно генериран идентификатор за номера на заявката.'
exec spDescColumn N'ActionErrorLogs', N'RequestInfo', N'Допълнителна информация на заявката.'
exec spDescColumn N'ActionErrorLogs', N'ErrorInfo', N'Допълнителна информация на грешката.'
exec spDescColumn N'ActionErrorLogs', N'ActionErrorDate', N'Дата на грешката.'
exec spDescColumn N'ActionErrorLogs', N'Version', N'Версия.'
GO
