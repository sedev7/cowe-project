-- Create_COWE_Schema_Script.sql
--
-- J. Savage  05-05-2015
-- COWE Thesis/Research Project (CISE)
--

USE Packets;
GO	

-- Create schema with owner = dbo (default)
CREATE SCHEMA COWE;
GO

---- Create schema with separate owner
--CREATE SCHEMA <new schema name> AUTHORIZATION [new schema owner];
--GO

-- Allow everyone to view the data in the schema
GRANT SELECT,EXECUTE ON SCHEMA :: COWE TO PUBLIC;
GO


---- Delete a schema
--USE <database>
--GO

--DROP SCHEMA <schema name>;
--GO

