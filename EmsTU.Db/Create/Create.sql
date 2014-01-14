SET QUOTED_IDENTIFIER ON
GO

USE [master]
GO

:r "Create\CreateDB.sql"

USE [$(DatabaseName)]
GO

---------------------------------------------------------------
--Tools
---------------------------------------------------------------
:r "Create\Tools\Tool_ScriptDiagram2008.sql"
:r "Create\Tools\spDesc.sql"
:r "Create\Tools\sp_generate_inserts.sql"

---------------------------------------------------------------
--Tables
---------------------------------------------------------------

--Audit
:r "Create\Tables\Audit\ActionLogs.sql"
:r "Create\Tables\Audit\ActionErrorLogs.sql"

--Address
:r "Create\Tables\AddressData\Countries.sql"
:r "Create\Tables\AddressData\Districts.sql"
:r "Create\Tables\AddressData\Municipalities.sql"
:r "Create\Tables\AddressData\Settlements.sql"

--Users
:r "Create\Tables\Roles.sql"
:r "Create\Tables\Users.sql"

--
:r "Create\Tables\Buildings.sql"
:r "Create\Tables\BuildingUsers.sql"

:r "Create\Tables\NomTypes.sql"
:r "Create\Tables\Noms.sql"
:r "Create\Tables\BuildingNoms.sql"

:r "Create\Tables\Visitors.sql"
:r "Create\Tables\Ratings.sql"
:r "Create\Tables\Offers.sql"
:r "Create\Tables\Events.sql"
:r "Create\Tables\MenuCategories.sql"
:r "Create\Tables\Menus.sql"
:r "Create\Tables\Comments.sql"
:r "Create\Tables\Albums.sql"
:r "Create\Tables\AlbumPhotos.sql"
:r "Create\Tables\BuildingRequests.sql"


---------------------------------------------------------------
-- Functions
---------------------------------------------------------------

---------------------------------------------------------------
-- Procedures
---------------------------------------------------------------

---------------------------------------------------------------
-- Diagram
---------------------------------------------------------------

---------------------------------------------------------------
-- Views
---------------------------------------------------------------

