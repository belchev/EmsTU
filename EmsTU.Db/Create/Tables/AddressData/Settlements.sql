print 'Settlements'
GO 

CREATE TABLE Settlements
(
    SettlementId		INT            IDENTITY (1, 1) NOT NULL,
	MunicipalityId		INT            NOT NULL,
	DistrictId			INT            NOT NULL,
	Code				NVARCHAR (10) NOT NULL,
	ManucipalityCode	nvarchar(10) NULL,
	DistrictCode		nvarchar(10) NULL,
	ManucipalityCode2	nvarchar(10) NULL,
	DistrictCode2		nvarchar(10) NULL,
	Name				NVARCHAR (200) NOT NULL,
	TypeName			NVARCHAR (20) NOT NULL,
	SettlementName		NVARCHAR (200) NOT NULL,
	TypeCode			nvarchar(50) NULL,
	MayoraltyCode		nvarchar(50) NULL,
	Category			nvarchar (50) NULL,
	Altitude			nvarchar(50) NULL,
	Alias				NVARCHAR (200) NULL,
	Description			NVARCHAR (MAX) NULL,
	IsDistrict			bit		 NOT NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_Settlements PRIMARY KEY CLUSTERED (SettlementId),
	CONSTRAINT [FK_Settlements_Municipalities] FOREIGN KEY (MunicipalityId) REFERENCES [dbo].Municipalities (MunicipalityId),
	CONSTRAINT [FK_Settlements_Districts] FOREIGN KEY (DistrictId) REFERENCES [dbo].Districts (DistrictId),
)
GO 

exec spDescTable  N'Settlements', N'Номенклатура области'
exec spDescColumn N'Settlements', N'SettlementId', N'Уникален системно генериран идентификатор.'

