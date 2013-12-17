SET QUOTED_IDENTIFIER ON
GO

USE [$(DatabaseName)]
GO

SET NOCOUNT ON;
GO

---------------------------------------------------------------
--Insert
---------------------------------------------------------------

--AddressData
:r "Insert\Tables\AddressData\Countries.sql"
:r "Insert\Tables\AddressData\Districts.sql"
:r "Insert\Tables\AddressData\Municipalities.sql"
:r "Insert\Tables\AddressData\Settlements.sql"
 

---------------------------------------------------------------
--Insert Test Data
---------------------------------------------------------------


SET NOCOUNT OFF;
GO
