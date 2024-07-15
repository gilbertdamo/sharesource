-- Customer Table
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY IDENTITY(1,1),
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Phone NVARCHAR(20),
    Address NVARCHAR(255), -- Adding address information
    City NVARCHAR(100),
    State NVARCHAR(100),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(100),
    DateOfBirth DATE, -- Adding date of birth
	FeatureEnabled NVARCHAR(50) NOT NULL DEFAULT '00000000',
    CreatedAt DATETIME2,
    UpdatedAt DATETIME2
);

-- Merchant Table
CREATE TABLE Merchant (
    MerchantID INT PRIMARY KEY IDENTITY(1,1),
    MerchantName NVARCHAR(100) NOT NULL,
    ContactName NVARCHAR(50),
    ContactEmail NVARCHAR(100),
    ContactPhone NVARCHAR(20),
    Address NVARCHAR(255), -- Adding address information
    City NVARCHAR(100),
    State NVARCHAR(100),
    PostalCode NVARCHAR(20),
    Country NVARCHAR(100),
    CreatedAt DATETIME2,
    UpdatedAt DATETIME2
);

-- Product Table
CREATE TABLE Product (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    MerchantID INT,
    ProductName NVARCHAR(100) NOT NULL,
    ProductDescription NVARCHAR(255),
    Price DECIMAL(10, 2) NOT NULL,
    CreatedAt DATETIME2,
    UpdatedAt DATETIME2,
    FOREIGN KEY (MerchantID) REFERENCES Merchant(MerchantID)
);

-- Transaction Table
CREATE TABLE [Transaction] (
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    CustomerID INT,
    MerchantID INT,
    TransactionDate DATETIME2,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    TransactionStatus NVARCHAR(50) NOT NULL, -- e.g., Pending, Completed, Cancelled
	CreatedAt DATETIME2,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID),
    FOREIGN KEY (MerchantID) REFERENCES Merchant(MerchantID)
);

-- TransactionItem Table
CREATE TABLE TransactionItem (
    TransactionItemID INT PRIMARY KEY IDENTITY(1,1),
    TransactionID INT,
    ProductID INT,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    TotalPrice Decimal(10, 2) NOT NULL,
    FOREIGN KEY (TransactionID) REFERENCES [Transaction](TransactionID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Payment Table
CREATE TABLE Payment (
    PaymentID INT PRIMARY KEY IDENTITY(1,1),
    TransactionID INT,
    PaymentDate DATETIME2,
    PaymentAmount DECIMAL(10, 2) NOT NULL,
    PaymentStatus NVARCHAR(50) NOT NULL, -- e.g., Pending, Successful, Failed
    PaymentMethod NVARCHAR(50), -- e.g., Credit Card, PayPal, Bank Transfer
    FailedReason NVARCHAR(255), -- Reason for payment failure
    FOREIGN KEY (TransactionID) REFERENCES [Transaction](TransactionID)
);
