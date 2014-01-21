print 'Buildings'
GO 

CREATE TABLE [dbo].[Buildings](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ImagePath] [nvarchar](100) NULL,
	[Slogan] [nvarchar](50) NULL,
	[WebSite] [nvarchar](100) NULL,
	[ModifyUserId] [int] NULL,
	[ModifyDate] [datetime] NULL,
	[DistrictId] [int] NULL,
	[MunicipalityId] [int] NULL,
	[SettlementId] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[ContactName] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](100) NULL,
	[Info] [nvarchar](max) NULL,
	[WorkingTime] [nvarchar](50) NULL,
	[BuildingPhone] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[SeatsInside] [int] NULL,
	[SeatsOutside] [int] NULL,
	[IsActive] [bit] NOT NULL DEFAULT (1),
	[IsDeleted] [bit] NOT NULL DEFAULT (0),
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Buildings] PRIMARY KEY CLUSTERED 
(
	[BuildingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Districts] FOREIGN KEY([DistrictId])
REFERENCES [dbo].[Districts] ([DistrictId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Districts]
GO

ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Municipalities] FOREIGN KEY([MunicipalityId])
REFERENCES [dbo].[Municipalities] ([MunicipalityId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Municipalities]
GO

ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Settlements] FOREIGN KEY([SettlementId])
REFERENCES [dbo].[Settlements] ([SettlementId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Settlements]
GO

ALTER TABLE [dbo].[Buildings]  WITH CHECK ADD  CONSTRAINT [FK_Buildings_Users] FOREIGN KEY([ModifyUserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Buildings] CHECK CONSTRAINT [FK_Buildings_Users]
GO

exec spDescTable  N'Buildings', N'Заведения.'
exec spDescColumn N'Buildings', N'BuildingId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Buildings', N'Name', N'Име на заведението.'
exec spDescColumn N'Buildings', N'ImagePath', N'Физически път до снимката на заведението.'
exec spDescColumn N'Buildings', N'Slogan', N'Мото на заведението.'
exec spDescColumn N'Buildings', N'WebSite', N'Уеб сайт на заведението.'
exec spDescColumn N'Buildings', N'ModifyUserId', N'Последен потребител променил заведението.'
exec spDescColumn N'Buildings', N'ModifyDate', N'Дата на последна модификация на заведението.'
exec spDescColumn N'Buildings', N'DistrictId', N'Уникален системно генериран идентификатор на област.'
exec spDescColumn N'Buildings', N'MunicipalityId', N'Уникален системно генериран идентификатор на община.'
exec spDescColumn N'Buildings', N'SettlementId', N'Уникален системно генериран идентификатор на населено място.'
exec spDescColumn N'Buildings', N'Address', N'Адрес на заведението.'
exec spDescColumn N'Buildings', N'ContactName', N'Име за контакт.'
exec spDescColumn N'Buildings', N'ContactPhone', N'Телефон за контакт.'
exec spDescColumn N'Buildings', N'Info', N'Допълнителна информация за заведението.'
exec spDescColumn N'Buildings', N'WorkingTime', N'Работно време.'
exec spDescColumn N'Buildings', N'BuildingPhone', N'Телефон за контакт на заведенеито.'
exec spDescColumn N'Buildings', N'Price', N'Ценови клас.'
exec spDescColumn N'Buildings', N'SeatsInside', N'Места вътре.'
exec spDescColumn N'Buildings', N'SeatsOutside', N'Места вън.'
exec spDescColumn N'Buildings', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Buildings', N'IsDeleted', N'Маркер за изтрито заведение.'
exec spDescColumn N'Buildings', N'Version', N'Версия.'
GO
