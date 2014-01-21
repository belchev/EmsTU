print 'NomTypes'
GO 

CREATE TABLE [dbo].[NomTypes](
	[NomTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Alias] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
 CONSTRAINT [PK_NomTypes] PRIMARY KEY CLUSTERED 
(
	[NomTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

exec spDescTable  N'NomTypes', N'Типове номенклатури.'
exec spDescColumn N'NomTypes', N'NomTypeId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'NomTypes', N'Name', N'Име на типа.'
exec spDescColumn N'NomTypes', N'Alias', N'Псевдоним на типа.'
exec spDescColumn N'NomTypes', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'NomTypes', N'Version', N'Версия.'
GO