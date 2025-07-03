DECLARE @Default_DB_Path as VARCHAR(64)  
SET @Default_DB_Path = N'C:\SourceCode\DataBase\'

USE [master]

/* Drop database if already exists */
IF  EXISTS (SELECT name FROM sys.databases WHERE name = 'Mad201')
	DROP DATABASE Mad201


/* DataBase Creation */
	                              
DECLARE @sql nvarchar(500)

SET @sql = 
  N'CREATE DATABASE [Mad201] 
    ON PRIMARY ( NAME = Mad201, FILENAME = "' + @Default_DB_Path + N'Mad201.mdf")
    LOG ON ( NAME = Mad201_log, FILENAME = "' + @Default_DB_Path + N'Mad201_log.ldf")'

EXEC(@sql)
PRINT N'Database [Mad201] created.'
GO
