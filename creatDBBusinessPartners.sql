CREATE DATABASE BusinessPartners;

CREATE TABLE UserTbl (
    ID INT IDENTITY PRIMARY KEY,
    FullName NVARCHAR(1024),
    UserName NVARCHAR(254) UNIQUE,
    Password NVARCHAR(MAX),
    Active BIT DEFAULT 1
);

INSERT INTO UserTbl (FullName, UserName, Password, Active)
VALUES (1, 'U1', 'P1', 1),
  (2, 'U2', 'P2', 1);

  -- Create BPType table
CREATE TABLE BPType (
    TypeCode NVARCHAR(1) PRIMARY KEY,
    TypeName NVARCHAR(20)
);

-- Insert seed data
INSERT INTO BPType (TypeCode, TypeName)
VALUES ('C', 'Customer'),
       ('V', 'Vendor');

-- Create BP table
CREATE TABLE BP (
    BPCode NVARCHAR(128) PRIMARY KEY,
    BPName NVARCHAR(254),
    BPType NVARCHAR(1),
    Active BIT DEFAULT 1,
    FOREIGN KEY (BPType) REFERENCES BPType(TypeCode)
);

-- Insert seed data
INSERT INTO BP (BPCode, BPName, BPType, Active)
VALUES ('C0001', 'Customer 1', 'C', 1),
       ('C0002', 'Customer 2', 'C', 0),
       ('V0001', 'Vendor 1', 'V', 1),
       ('V0002', 'Vendor 2', 'V', 0);

-- Create Items table
CREATE TABLE Items (
    ItemCode NVARCHAR(128) PRIMARY KEY,
    ItemName NVARCHAR(254),
    Active BIT DEFAULT 1
);

-- Insert seed data
INSERT INTO Items (ItemCode, ItemName, Active)
VALUES ('Itm1', 'Item 1', 1),
       ('Itm2', 'Item 2', 1),
       ('Itm3', 'Item 3', 0);

CREATE TABLE SaleOrders (
    ID INT IDENTITY PRIMARY KEY,
    BPCode NVARCHAR(128),
    CreateDate DATETIME,
    LastUpdateDate DATETIME,
    CreatedBy INT,
    LastUpdatedBy INT,
    FOREIGN KEY (BPCode) REFERENCES BP(BPCode),
    FOREIGN KEY (CreatedBy) REFERENCES UserTbl(ID),
    FOREIGN KEY (LastUpdatedBy) REFERENCES UserTbl(ID)
);

-- Create SaleOrdersLines table
CREATE TABLE SaleOrdersLines (
    LineID INT IDENTITY PRIMARY KEY,
    DocID INT,
    ItemCode NVARCHAR(128),
    Quantity DECIMAL(38, 18),
    CreateDate DATETIME,
    LastUpdateDate DATETIME,
    CreatedBy INT,
    LastUpdatedBy INT,
    FOREIGN KEY (DocID) REFERENCES SaleOrders(ID),
    FOREIGN KEY (ItemCode) REFERENCES Items(ItemCode),
    FOREIGN KEY (CreatedBy) REFERENCES UserTbl(ID),
    FOREIGN KEY (LastUpdatedBy) REFERENCES UserTbl(ID)
);

-- Create SaleOrdersLinesComments table
CREATE TABLE SaleOrdersLinesComments (
    CommentLineID INT IDENTITY PRIMARY KEY,
    DocID INT,
    LineID INT,
    Comment NVARCHAR(MAX),
    FOREIGN KEY (DocID) REFERENCES SaleOrders(ID),
    FOREIGN KEY (LineID) REFERENCES SaleOrdersLines(LineID)
);

-- Create PurchaseOrders table
CREATE TABLE PurchaseOrders (
    ID INT IDENTITY PRIMARY KEY,
    BPCode NVARCHAR(128),
    CreateDate DATETIME,
    LastUpdateDate DATETIME,
    CreatedBy INT,
    LastUpdatedBy INT,
    FOREIGN KEY (BPCode) REFERENCES BP(BPCode),
    FOREIGN KEY (CreatedBy) REFERENCES UserTbl(ID),
    FOREIGN KEY (LastUpdatedBy) REFERENCES UserTbl(ID)
);
CREATE TABLE PurchaseOrdersLines (
    LineID INT IDENTITY PRIMARY KEY,
    DocID INT,
    ItemCode NVARCHAR(128),
    Quantity DECIMAL(38, 18),
    CreateDate DATETIME,
    LastUpdateDate DATETIME,
    CreatedBy INT,
    LastUpdatedBy INT,
    FOREIGN KEY (DocID) REFERENCES PurchaseOrders(ID),
    FOREIGN KEY (ItemCode) REFERENCES Items(ItemCode),
    FOREIGN KEY (CreatedBy) REFERENCES UserTbl(ID),
    FOREIGN KEY (LastUpdatedBy) REFERENCES UserTbl(ID)
);
select *
from UserTbl

select *
from BPType

select *
from BP

select *
from Items