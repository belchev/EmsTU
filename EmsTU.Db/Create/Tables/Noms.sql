print 'Noms'
GO 

CREATE TABLE [dbo].[Noms](
	[NomId] [int] IDENTITY(1,1) NOT NULL,
	[NomTypeId] [int] NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Alias] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_Noms] PRIMARY KEY CLUSTERED 
(
	[NomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[Noms]  WITH CHECK ADD  CONSTRAINT [FK_Noms_NomTypes] FOREIGN KEY([NomTypeId])
REFERENCES [dbo].[NomTypes] ([NomTypeId])
GO
ALTER TABLE [dbo].[Noms] CHECK CONSTRAINT [FK_Noms_NomTypes]
GO

exec spDescTable  N'Noms', N'Номенклатури.'
exec spDescColumn N'Noms', N'NomId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Noms', N'NomTypeId', N'Уникален системно генериран идентификатор на типа номенклатура.'
exec spDescColumn N'Noms', N'Name', N'Име на номенклатурата.'
exec spDescColumn N'Noms', N'Alias', N'Псевдоним на номенклатурата.'
exec spDescColumn N'Noms', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Noms', N'Version', N'Версия.'
GO