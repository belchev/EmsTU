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
