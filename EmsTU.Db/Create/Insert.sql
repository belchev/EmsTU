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

:r "Insert\Tables\BuildingTypes.sql"
:r "Insert\Tables\Extras.sql"
:r "Insert\Tables\KitchenTypes.sql"
:r "Insert\Tables\MusicTypes.sql"
:r "Insert\Tables\OccasionTypes.sql"

:r "Insert\Tables\ConfigFinalize.sql"

---------------------------------------------------------------
--Insert Test Data
---------------------------------------------------------------


SET NOCOUNT OFF;
GO
