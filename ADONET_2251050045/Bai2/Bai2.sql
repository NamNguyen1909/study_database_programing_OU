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

-- Thêm dữ liệu mới vào bảng Categories
INSERT INTO Categories (CategoryName) VALUES 
    (N'Phụ kiện'),
    (N'Đồ gia dụng'),
    (N'Quần áo');
GO

-- Thêm dữ liệu mới vào bảng Suppliers
INSERT INTO Suppliers (CompanyName) VALUES 
    (N'Công ty D'),
    (N'Công ty E'),
    (N'Công ty F');
GO

-- Thêm dữ liệu mới vào bảng Products (QuantityPerUnit là số nguyên)
INSERT INTO Products (ProductName, UnitPrice, QuantityPerUnit, CategoryID, SupplierID) VALUES 
    (N'Ốp lưng điện thoại', 120000, 200, 4, 4), -- CategoryID=4: Phụ kiện, SupplierID=4: Công ty D
    (N'Máy xay sinh tố', 750000, 30, 5, 5),    -- CategoryID=5: Đồ gia dụng, SupplierID=5: Công ty E
    (N'Áo thun', 150000, 80, 6, 6);           -- CategoryID=6: Quần áo, SupplierID=6: Công ty F
GO