print 'Municipalities'
GO 

CREATE TABLE Municipalities
(
    MunicipalityId		INT            IDENTITY (1, 1) NOT NULL,
	DistrictId			INT            NOT NULL,
	Code				NVARCHAR (10) NOT NULL,
    Name				NVARCHAR (200) NOT NULL,
	Alias				NVARCHAR (200) NULL,
	Description			NVARCHAR (MAX) NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_Municipalities PRIMARY KEY CLUSTERED (MunicipalityId),
	CONSTRAINT [FK_Municipalities_Districts] FOREIGN KEY (DistrictId) REFERENCES [dbo].Districts (DistrictId),
)
GO 

exec spDescTable  N'Municipalities', N'Номенклатура община'
exec spDescColumn N'Municipalities', N'MunicipalityId', N'Уникален системно генериран идентификатор.'
exec spDescColumn N'Municipalities', N'DistrictId', N'Уникален системно генериран идентификатор за област.'
exec spDescColumn N'Municipalities', N'Code', N'Код на община.'
exec spDescColumn N'Municipalities', N'Name', N'Име на община.'
exec spDescColumn N'Municipalities', N'Alias', N'Псевдоним на община.'
exec spDescColumn N'Municipalities', N'Description', N'Описание на община.'
exec spDescColumn N'Municipalities', N'IsActive', N'Маркер за активност.'
exec spDescColumn N'Municipalities', N'Version', N'Версия.'