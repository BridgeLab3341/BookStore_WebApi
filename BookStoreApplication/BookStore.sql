-- ============================================================================
-- Author        : Somashekar
-- Create date    : 06-11-2023
-- Description    : BookStoreApplication Database,AdminUser Table,Customer Tables.
-- ==============================================================================
Create Database BookStoreApplication;

Select * from RegistrationTable;
Select * from ProductTable;
select * from CustomerDetailsTable;
Select * from OrderTable;
Select * from CartTable;

Create Table RegistrationTable
(RegisterId BigInt Identity(1,1) Primary key,
TypeofRegister varchar(10) NOT NULL CHECK(TypeofRegister IN ('Admin','Customer')),
FirstName Varchar(100) NOT NUll,
LastName varchar(100) NOT NULL,
PhoneNumber varchar(100) NOT NULL,
Email varchar(100) UNIQUE NOT NULL,
Password varchar(100) NOT NULL);


Delete from ProductTable where ProductId=1;

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
RegisterId BigInt,
Foreign Key (RegisterId) REFERENCES RegistrationTable(RegisterId));


Create Table CustomerDetailsTable
(
CustomerDetailId BigInt Identity(1,1) primary key,
AddressType varchar(100),
AreaBuilding varchar(500),
City varchar(100),
State varchar(100),
PinCode varchar(6),
PhoneNumber varchar(10),
RegisterId BigInt,
Foreign key (RegisterId) REFERENCES RegistrationTable(RegisterId),
CONSTRAINT CK_AddressType CHECK (AddressType IN ('Home','Office','Other'))
);

Create Table CartTable(
CartId BigInt Identity(1,1) Primary key,
RegisterId BigInt,
ProductId BigInt,
Foreign key (RegisterId) REFERENCES RegistrationTable(RegisterId),
Foreign key (ProductId) REFERENCES ProductTable(ProductId)
);

select * from OrderTable;

Create Table OrderTable(
OrderId BigInt Identity(1,1) Primary key,
OrderTime DateTime,
Quantity Int,
Amount Decimal,
CustomerDetailId BigInt,
ProductId BigInt,
RegisterId BigInt,
Foreign key (CustomerDetailId) REFERENCES CustomerDetailsTable(CustomerDetailId),
Foreign key (ProductId) REFERENCES ProductTable(ProductId),
Foreign key (RegisterId) REFERENCES RegistrationTable(RegisterId),
);

