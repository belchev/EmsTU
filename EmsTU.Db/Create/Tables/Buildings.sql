print 'Buildings'
GO 

CREATE TABLE [dbo].[Buildings](
	[BuildingId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Slogan] [nvarchar](50) NULL,
	[WebSite] [nvarchar](100) NULL,
	[ModifyDate] [datetime] NULL,
	[DistrictId] [int] NULL,
	[MunicipalityId] [int] NULL,
	[SettlementId] [int] NULL,
	[Address] [nvarchar](max) NULL,
	[ContactName] [nvarchar](100) NULL,
	[ContactPhone] [nvarchar](100) NULL,
	[Info] [nvarchar](max) NULL,
	[BuildingPhone] [nvarchar](50) NULL,
	[Price] [int] NULL,
	[SeatsInside] [int] NULL,
	[SeatsOutside] [int] NULL,
	[IsActive] [bit] NOT NULL,
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
