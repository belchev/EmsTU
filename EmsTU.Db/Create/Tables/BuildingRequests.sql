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
	[HasRegisteredUser] [bit],
	[HasRegisteredBuilding] [bit],
	[Version] [timestamp] NULL,
 CONSTRAINT [PK_BuildingRequests] PRIMARY KEY CLUSTERED 
(
	[BuildingRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

exec spDescTable  N'BuildingRequests', N'Заявки за регистрация на заведение.'
exec spDescColumn N'BuildingRequests', N'BuildingRequestId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'BuildingRequests', N'BuildingName', N'Желано име за заведение.'
exec spDescColumn N'BuildingRequests', N'ContactName', N'Име за контакт.'
exec spDescColumn N'BuildingRequests', N'UserName', N'Желано име за потребител.'
exec spDescColumn N'BuildingRequests', N'Phone', N'Телефон на контакта.'
exec spDescColumn N'BuildingRequests', N'Email', N'Email на контакта.'
exec spDescColumn N'BuildingRequests', N'WebSite', N'Уеб сайт на контакта.'
exec spDescColumn N'BuildingRequests', N'HasRegisteredUser', N'Маркер за регистриран потребител.'
exec spDescColumn N'BuildingRequests', N'HasRegisteredBuilding', N'Маркер за създадено заведение.'
exec spDescColumn N'BuildingRequests', N'Version', N'Версия.'
GO