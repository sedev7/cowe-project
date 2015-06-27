-- Create_Packets_Database_Script.sql
--
-- J. Savage  05-05-2015
--
-- COWE Thesis/Research Project (CISE)
--

CREATE DATABASE Packets
ON
PRIMARY
( NAME = PacketsData,
  FILENAME = 'c:\sql_data\Packets_Data.mdf',
  SIZE = 20MB,
  MAXSIZE = 200,
  FILEGROWTH = 20)
LOG ON
( NAME = PacketsLog,
  FILENAME = 'c:\sql_data\Packets_Log.ldf',
  SIZE = 5MB,
  MAXSIZE = 200,
  FILEGROWTH = 20)
