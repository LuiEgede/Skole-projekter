-- 1NF: Each table stores one type of data, and each column contains only one value.

-- 2NF: Each non-key column depends on the full primary key, not just part of it.

-- 3NF: Data is separated into different tables so information is stored only once and linked with foreign keys.


-- 1NF: Customers, Cars, WorkOrders, Parts, and WorkOrderParts all store one type of data, and each column contains only one value.

-- 2NF: WorkOrderParts uses a combined key, and Quantity depends on the full combination of WorkOrderId and PartId.

-- 3NF: Customer, car, work order, and part data are stored in separate tables and linked with foreign keys to avoid duplicate information.


PRAGMA foreign_keys = ON;

CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerName TEXT NOT NULL,
    Phone TEXT NOT NULL,
    Email TEXT UNIQUE
);

CREATE TABLE Cars (
    CarId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    LicensePlate TEXT NOT NULL UNIQUE,
    Brand TEXT NOT NULL,
    Model TEXT NOT NULL,
    ManufactureYear INTEGER NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

CREATE TABLE WorkOrders (
    WorkOrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CarId INTEGER NOT NULL,
    StartDate TEXT NOT NULL,
    EndDate TEXT,
    WorkDescription TEXT NOT NULL,
    WorkStatus TEXT NOT NULL,
    FOREIGN KEY (CarId) REFERENCES Cars(CarId)
);

CREATE TABLE Parts (
    PartId INTEGER PRIMARY KEY AUTOINCREMENT,
    PartName TEXT NOT NULL,
    Price REAL NOT NULL
);

CREATE TABLE WorkOrderParts (
    WorkOrderId INTEGER NOT NULL,
    PartId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    PRIMARY KEY (WorkOrderId, PartId),
    FOREIGN KEY (WorkOrderId) REFERENCES WorkOrders(WorkOrderId),
    FOREIGN KEY (PartId) REFERENCES Parts(PartId)
);