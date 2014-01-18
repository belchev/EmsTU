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

exec spDescTable  N'Municipalities', N'Номенклатура области'
exec spDescColumn N'Municipalities', N'MunicipalityId', N'Уникален системно генериран идентификатор.'
