-- Create_Test_Schema_Script.sql
--
-- J. Savage  01-28-2016
-- COWE Thesis/Research Project (CISE)
--

USE PacketsTest;
GO	

-- Create schema with owner = dbo (default)
CREATE SCHEMA Test;
GO

---- Create schema with separate owner
--CREATE SCHEMA <new schema name> AUTHORIZATION [new schema owner];
--GO

-- Allow everyone to view the data in the schema
GRANT SELECT,EXECUTE ON SCHEMA :: Test TO PUBLIC;
GO


---- Delete a schema
--USE <database>
--GO

--DROP SCHEMA <schema name>;
--GO

