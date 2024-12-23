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
    LicensePlate NVARCHAR(50) PRIMARY KEY,
    Model NVARCHAR(100) NOT NULL,
    VehicleYear NVARCHAR(6) NOT NULL,
    FuelType NVARCHAR(50) NOT NULL,
    Color NVARCHAR(20),
    Manufacturer NVARCHAR(50) NOT NULL,
    CurrentMileage INT NOT NULL,
    ImageURL NVARCHAR(200)
);

CREATE TABLE VehicleUser (
    VehicleUserID INT PRIMARY KEY IDENTITY,
    VehicleID NVARCHAR(50),
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
    GarageAddress NVARCHAR(70),
    City NVARCHAR(30),
    Phone NVARCHAR(50),
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
select * from Garage

INSERT INTO UserStatus VALUES('User')
INSERT INTO UserStatus VALUES('Manager')

INSERT INTO MessageSender VALUES('User')
INSERT INTO MessageSender VALUES('GarageManager')

INSERT INTO AppointmentStatus VALUES('pending')
INSERT INTO AppointmentStatus VALUES('available')
INSERT INTO AppointmentStatus VALUES('taken')


Insert into Users Values('Yali', 'Kriaf', 'Yk@gmail.com', '54321', '12345',1,23)
Insert into Users Values('Dan', 'Ben Tov', 'DB@gmail.com', '123', '456',2,0)

Insert into Vehicle Values('4444', 'Civic', '2013', 'gas', 'black','Honda',500,'')
Insert into Vehicle Values('55555', 'Q3', '2023', 'gas', 'black','Audi',500,'')

Insert into VehicleUser values('4444', 1)
Insert into VehicleUser values('4444', 2)
Insert into VehicleUser values('55555', 1)
Insert into VehicleUser values('55555', 2)

select * from Garage

-- Create a login for the admin user
CREATE LOGIN [MyGarageFinderAdminLogin] WITH PASSWORD = 'DAN1706';
Go

-- Create a user in the MyGarageFinderDB database for the login
CREATE USER [MyGarageFinderAdminUser] FOR LOGIN [MyGarageFinderAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [MyGarageFinderAdminUser];
Go

-- scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=MyGarageFinderDB;User ID=MyGarageFinderAdminLogin;Password=DAN1706;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context MyGarageFinderDbContext -DataAnnotations –force

