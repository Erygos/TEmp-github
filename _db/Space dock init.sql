USE master;
GO

--Delete the TestData database if it exists.
IF EXISTS(SELECT * from sys.databases WHERE name='SpaceDockDatabase')
BEGIN
    DROP DATABASE SpaceDockDatabase;
END

--Create a new database called TestData.
CREATE DATABASE SpaceDockDatabase;

USE SpaceDockDatabase

CREATE TABLE docks
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
Location VARCHAR(50),
Capacity INT,
);

CREATE TABLE commodity_type
	(
	ID INT IDENTITY(1,1) Primary key (ID),
Category VARCHAR(50) UNIQUE,
);

CREATE TABLE commodity
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
Category int,
FOREIGN KEY (category) REFERENCES commodity_type(ID)ON DELETE SET NULL,
);

CREATE TABLE ship_class
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
Designation VARCHAR(50)
);

CREATE TABLE faction
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
Policy VARCHAR(50),
);

CREATE TABLE ship
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
CargoSpace INT,
ClassID INT,
FOREIGN KEY (ClassID) REFERENCES ship_class(ID) ON DELETE SET NULL,
);

CREATE TABLE pilot
(
ID INT IDENTITY(1,1) Primary key (ID),
Name VARCHAR(50) UNIQUE,
Birthday DATE,
ShipID INT,
FactionID INT,
OnHandCredits BIGINT,
FOREIGN KEY (FactionID) REFERENCES faction(ID) ON DELETE SET NULL,
FOREIGN KEY (ShipID) REFERENCES ship(ID) ON DELETE SET NULL,
);

CREATE TABLE prices
(
CommodityID INT,
DockID INT,
UnitPrice INT,
FOREIGN KEY (DockID) REFERENCES docks(ID) ON DELETE SET NULL,
FOREIGN KEY (CommodityID) REFERENCES commodity(ID) ON DELETE SET NULL,
);

CREATE TABLE cargo_card
(
CommodityID INT,
ShipID INT,
NumberOfUnits INT,
AcquiredAtDockID INT,
LiquifedAtDockID INT,
FOREIGN KEY (ShipID) REFERENCES ship(ID) ON DELETE SET NULL,
FOREIGN KEY (CommodityID) REFERENCES commodity(ID) ON DELETE SET NULL,
FOREIGN KEY (AcquiredAtDockID) REFERENCES docks(ID) ON DELETE SET NULL,
FOREIGN KEY (LiquifedAtDockID) REFERENCES docks(ID) ON DELETE SET NULL,
);

CREATE TABLE docking_certificate
(
ValidFrom DATE,
ValidThrough DATE,
ExitedAt DATE,
DockID INT,
ShipID INT,
FOREIGN KEY (DockID) REFERENCES docks(ID) ON DELETE SET NULL,
FOREIGN KEY (ShipID) REFERENCES ship(ID) ON DELETE SET NULL,
);

CREATE TABLE pilot_competencies	--connects ship class to pilot
(
Ship_classID INT,
PilotID INT,
FOREIGN KEY (ship_classID) REFERENCES ship_class(ID) ON DELETE SET NULL,
FOREIGN KEY (PilotID) REFERENCES pilot(ID) ON DELETE SET NULL,
) 


