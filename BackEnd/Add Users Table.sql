USE [master]
GO

alter database Northwind set single_user with rollback immediate
alter database Northwind set MULTI_USER

Use Northwind
GO

DROP TABLE IF EXISTS Users
GO

CREATE TABLE [Users] (
	userid uniqueidentifier NOT NULL,
	username nvarchar(30) NOT NULL,
	email nvarchar(30) NOT NULL,
	hashpassword nvarchar(28) NOT NULL,
	salt nvarchar(24) NOT NULL

	PRIMARY KEY(userid),

	UNIQUE(username),
	UNIQUE(email)
)