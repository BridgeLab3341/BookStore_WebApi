-- ============================================================================
-- Author        : Somashekar
-- Create date    : 06-11-2023
-- Description    : BookStoreApplication Database,AdminUser Table,Customer Tables.
-- ==============================================================================
Create Database BookStoreApplication;

Create Table AdminUser
(AdminId BigInt Identity(1,1) Primary key,
FirstName Varchar(100) NOT NUll,
LastName varchar(100) NOT NULL,
Email varchar(100) UNIQUE NOT NULL,
Password varchar(100) NOT NULL);

drop Table AdminUser;
drop Table ProductTable;
Create Table ProductTable
(ProductId BigInt Identity(1,1) Primary key,
BookName Varchar(max),
Author Varchar(max),
Language varchar(100),
Image varchar(max),
Descrption Varchar(max),
Quantity Int,
Status varchar(100),
Discountprice Decimal,
Price Decimal,
AdminId BigInt,
--Foreign Key (AdminId) REFERENCES AdminUser(AdminId));

Create Table CustomerTable
(CustomerId BigInt Identity(1,1) Primary key,
CustomerName varchar(100) NOT NULL,
PhoneNumber varchar(10) UNIQUE NOT NULL,
Email varchar (100)UNIQUE NOT NULL,
Password varchar(100) NOT NULL
);

Create Table CustomerDetailsTable
(
CustomerDetailId BigInt Identity(1,1) primary key,
AddressType varchar(100),
AreaBuilding varchar(500),
City varchar(100),
State varchar(100),
PinCode varchar(6),
PhoneNumber varchar(10),
CustomerId BigInt,
Foreign key (CustomerId) REFERENCES CustomerTable(CustomerId),
CONSTRAINT CK_AddressType CHECK (AddressType IN ('Home','Office','Other'))
);

Create Table CartTable(
CartId BigInt Identity(1,1) Primary key,
CustomerId BigInt,
ProductId BigInt,
Foreign key (CustomerId) REFERENCES CustomerTable(CustomerId),
Foreign key (ProductId) REFERENCES ProductTable(ProductId)
);

drop table OrderTable;

Create Table OrderTable(
OrderId BigInt Identity(1,1) Primary key,
CustomerDetailId BigInt,
OrderTime DateTime,
ProductId BigInt,
Foreign key (CustomerDetailId) REFERENCES CustomerDetailsTable(CustomerDetailId),
Foreign key (ProductId) REFERENCES ProductTable(ProductId)
);
