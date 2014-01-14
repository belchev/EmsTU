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
 
:r "Insert\Tables\Roles.sql"
:r "Insert\Tables\Users.sql"

:r "Insert\Tables\NomTypes.sql"
:r "Insert\Tables\Noms.sql"

:r "Insert\Tables\ConfigFinalize.sql"

---------------------------------------------------------------
--Insert Test Data
---------------------------------------------------------------

:r "Insert\Tables\Buildings.sql"


SET NOCOUNT OFF;
GO
