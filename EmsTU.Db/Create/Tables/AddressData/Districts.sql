print 'Districts'
GO 

CREATE TABLE Districts
(
    DistrictId		INT            IDENTITY (1, 1) NOT NULL,
	Code				NVARCHAR (10) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NULL,
	Description			NVARCHAR (MAX) NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_Districts PRIMARY KEY CLUSTERED (DistrictId),
)
GO 

exec spDescTable  N'Districts', N'Номенклатура области'
exec spDescColumn N'Districts', N'DistrictId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Districts', N'Code', N'Код на област.'
exec spDescColumn N'Districts', N'Name', N'Име на област.'
exec spDescColumn N'Districts', N'Alias', N'Псевдоним на област.'
exec spDescColumn N'Districts', N'Description', N'Описание на област.'
exec spDescColumn N'Districts', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Districts', N'Version', N'Версия.'

