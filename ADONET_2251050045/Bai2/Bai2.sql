CREATE DATABASE QLSP
GO

USE QLSP;
GO

-- Tạo bảng Categories
CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL
);
GO

-- Tạo bảng Suppliers
CREATE TABLE Suppliers (
    SupplierID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName NVARCHAR(100) NOT NULL
);
GO

-- Tạo bảng Products
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    QuantityPerUnit NVARCHAR(50) NOT NULL,
    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);
GO

-- Chèn dữ liệu mẫu để kiểm tra
INSERT INTO Categories (CategoryName) VALUES 
    (N'Đồ uống'),
    (N'Thực phẩm'),
    (N'Đồ điện tử');
GO

INSERT INTO Suppliers (CompanyName) VALUES 
    (N'Công ty A'),
    (N'Công ty B'),
    (N'Công ty C');
GO

INSERT INTO Products (ProductName, UnitPrice, QuantityPerUnit, CategoryID, SupplierID) VALUES 
    (N'Nước ngọt', 15000, N'1 chai', 1, 1),
    (N'Bánh mì', 20000, N'1 ổ', 2, 2),
    (N'Tai nghe', 500000, N'1 cái', 3, 3);
GO

DROP TABLE Products;
GO

-- Tạo lại bảng Products với QuantityPerUnit là INT
CREATE TABLE Products (
    ProductID INT PRIMARY KEY IDENTITY(1,1),
    ProductName NVARCHAR(100) NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    QuantityPerUnit INT NOT NULL,
    CategoryID INT NOT NULL,
    SupplierID INT NOT NULL,
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (SupplierID) REFERENCES Suppliers(SupplierID)
);
GO

-- Chèn dữ liệu mẫu mới (QuantityPerUnit là số nguyên)
INSERT INTO Products (ProductName, UnitPrice, QuantityPerUnit, CategoryID, SupplierID) VALUES 
    (N'Nước ngọt', 15000, 100, 1, 1), -- Ví dụ: 100 chai
    (N'Bánh mì', 20000, 50, 2, 2),   -- Ví dụ: 50 ổ
    (N'Tai nghe', 500000, 10, 3, 3); -- Ví dụ: 10 cái
GO