-- Create_PacketsTest_Database_Script.sql
--
-- J. Savage  01-28-2016
--
-- COWE Thesis/Research Project (CISE)
--

CREATE DATABASE PacketsTest
ON
PRIMARY
( NAME = PacketsTestData,
  FILENAME = 'c:\sql_data\PacketsTest_Data.mdf',
  SIZE = 20MB,
  MAXSIZE = 200,
  FILEGROWTH = 20)
LOG ON
( NAME = PacketsTestLog,
  FILENAME = 'c:\sql_data\PacketsTest_Log.ldf',
  SIZE = 5MB,
  MAXSIZE = 200,
  FILEGROWTH = 20)
