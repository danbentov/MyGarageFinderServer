﻿Use master
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

CREATE TABLE UserStatus (
    StatusID INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR (50)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(20) NOT NULL,
    LastName NVARCHAR(20) NOT NULL,
    Email NVARCHAR(50) NOT NULL,
    LicenseNumber NVARCHAR(50) NOT NULL,
    UserPassword NVARCHAR(15),
    UserStatusID INT,
    GarageLicense INT,
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(StatusID)
);

CREATE TABLE Vehicle (
    LicensePlate INT PRIMARY KEY,
    Model NVARCHAR(100) NOT NULL,
    VehicleYear INT NOT NULL,
    FuelType NVARCHAR(50) NOT NULL,
    Color NVARCHAR(20),
    Manufacturer NVARCHAR(50) NOT NULL,
    CurrentMileage INT NOT NULL,
    ImageURL NVARCHAR(200)
);

CREATE TABLE VehicleUser (
    VehicleUserID INT PRIMARY KEY IDENTITY,
    VehicleID INT,
    UserID INT,
    FOREIGN KEY (VehicleID) REFERENCES Vehicle(LicensePlate),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
);

CREATE TABLE Garage (
    GarageID INT PRIMARY KEY IDENTITY,
    GarageNumber INT,
    GarageName NVARCHAR(100),
    TypeCode NVARCHAR(50),
    GarageType NVARCHAR(50),
    GarageAddress NVARCHAR(50),
    City VARCHAR(30),
    Phone INT,
    ZipCode INT,
    SpecializationCode INT,
    Specialization NVARCHAR(50),
    GarageManager NVARCHAR(50),
    GarageLicense INT NOT NULL,
    TestTime DATE,
    WorkingHours NVARCHAR(50)
);

CREATE TABLE MessageSender (
    MessageSenderID INT PRIMARY KEY IDENTITY,
    MessageSenderType NVARCHAR(50)
);

CREATE TABLE ChatMessage (
    MessageID INT PRIMARY KEY IDENTITY,
    MessageText TEXT NOT NULL,
    UserID INT,
    GarageID INT,
    MessageSenderID INT,
    MessageTimestamp DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID),
    FOREIGN KEY (MessageSenderID) REFERENCES MessageSender(MessageSenderID)
);

CREATE TABLE Review (
    ReviewID INT PRIMARY KEY IDENTITY,
    Rating INT NOT NULL,
    ReviewDescription TEXT NOT NULL,
    UserID INT,
    GarageID INT,
    ReviewTimestamp DATETIME,
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID)
);

CREATE TABLE GarageImage (
    GarageImageID INT PRIMARY KEY IDENTITY,
    GarageID INT,
    ImageURL NVARCHAR(200) NOT NULL,
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID)
);

CREATE TABLE AppointmentStatus (
    StatusID INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(20)
);

CREATE TABLE Appointment (
    AppointmentID INT PRIMARY KEY IDENTITY,
    GarageID INT,
    VehicleUserID INT,
    AppointmentDate DATETIME,
    StatusID INT,
    AppoitmentDescription TEXT,
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID),
    FOREIGN KEY (VehicleUserID) REFERENCES VehicleUser(VehicleUserID),
    FOREIGN KEY (StatusID) REFERENCES AppointmentStatus(StatusID)
);
Select * from UserStatus

INSERT INTO UserStatus VALUES('User')
INSERT INTO UserStatus VALUES('Manager')

INSERT INTO MessageSender VALUES('User')
INSERT INTO MessageSender VALUES('GarageManager')

INSERT INTO AppointmentStatus VALUES('pending')
INSERT INTO AppointmentStatus VALUES('available')
INSERT INTO AppointmentStatus VALUES('taken')





-- Create a login for the admin user
CREATE LOGIN [MyGarageFinderAdminLogin] WITH PASSWORD = 'DAN1706';
Go

-- Create a user in the MyGarageFinderDB database for the login
CREATE USER [MyGarageFinderAdminUser] FOR LOGIN [MyGarageFinderAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [MyGarageFinderAdminUser];
Go

-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=MyGarageFinderDB;User ID=MyGarageFinderAdminUser;Password=DAN1706;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context MyGarageFinderDbContext -DataAnnotations –force

