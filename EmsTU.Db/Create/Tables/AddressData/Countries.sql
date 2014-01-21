print 'Countries'
GO 

CREATE TABLE Countries
(
    CountryId		INT            IDENTITY (1, 1) NOT NULL,
	Code				NVARCHAR (10) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NULL,
	Description			NVARCHAR (MAX) NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_Countries PRIMARY KEY CLUSTERED (CountryId),
)
GO 

exec spDescTable  N'Countries', N'Номенклатура държави'
exec spDescColumn N'Countries', N'CountryId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Countries', N'Code', N'Код на държава.'
exec spDescColumn N'Countries', N'Name', N'Име на държава.'
exec spDescColumn N'Countries', N'Alias', N'Псевдоним на държава.'
exec spDescColumn N'Countries', N'Description', N'Описание на държава.'
exec spDescColumn N'Countries', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Countries', N'Version', N'Версия.'
GO