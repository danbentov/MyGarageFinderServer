Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'MyGarageFinderDB')
BEGIN
    DROP DATABASE MyGarageFinderDB;
END
Go
Create Database MyGarageFinderDB
Go
Use MyGarageFinderDB
Go
Create Table AppUsers
(
	Id int Primary Key Identity,
	UserName nvarchar(50) Not Null,
	UserLastName nvarchar(50) Not Null,
	UserEmail nvarchar(50) Unique Not Null,
	UserPassword nvarchar(50) Not Null,
	IsManager bit Not Null Default 0
)
Insert Into AppUsers Values('admin', 'admin', 'kuku@kuku.com', '1234', 1)
Go
-- Create a login for the admin user
CREATE LOGIN [MyGarageFinderAdminLogin] WITH PASSWORD = 'DAN1706';
Go

-- Create a user in the MyGarageFinderDB database for the login
CREATE USER [MyGarageFinderAdminLogin] FOR LOGIN [MyGarageFinderAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [MyGarageFinderAdminLogin];
Go

-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=MyGarageFinderDB;User ID=MyGarageFinderAdminLogin;Password=DAN1706;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context MyGarageFinderDbContext -DataAnnotations –force

