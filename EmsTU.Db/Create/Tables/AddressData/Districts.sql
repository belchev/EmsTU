print 'Districts'
GO 

CREATE TABLE Districts
(
    DistrictId		INT            IDENTITY (1, 1) NOT NULL,
	Code				NVARCHAR (10) NOT NULL,
	Code2				NVARCHAR (10) NOT NULL,
	SecondLevelRegionCode nvarchar(10) NULL,
    Name				NVARCHAR (200) NOT NULL,
	MainSettlementCode  nvarchar(10) NULL,
	Alias				NVARCHAR (200) NULL,
	Description			NVARCHAR (MAX) NULL,
    IsActive		BIT            NOT NULL,
    Version				ROWVERSION     NOT NULL,
    CONSTRAINT PK_Districts PRIMARY KEY CLUSTERED (DistrictId),
)
GO 

exec spDescTable  N'Districts', N'Номенклатура области'
exec spDescColumn N'Districts', N'DistrictId', N'Уникален системно генериран идентификатор.'

