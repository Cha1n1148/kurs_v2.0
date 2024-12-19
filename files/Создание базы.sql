CREATE DATABASE Kurs;
GO
USE Kurs;
GO
--������� "������������"
CREATE TABLE Propertys(
PropertyID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, --����� ������������
AddressProperty NVARCHAR(200) NOT NULL, --����� ������������
Class NVARCHAR(50) NOT NULL, -- ��� ������������
Price MONEY NOT NULL, -- ����
Condition NVARCHAR(50) NOT NULL -- ���������
);
-- ������� "�������"
CREATE TABLE Clients(
ClientsID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id �������
FirstName NVARCHAR(100) NOT NULL, -- ���
MiddleName NVARCHAR(100) NOT NULL, -- ��������
LastName NVARCHAR(100) NOT NULL, -- �������
PhoneNumber CHAR(10) NOT NULL, -- ����� �������� �������
Email VARCHAR(255) NULL, -- ����������� �����
 
CONSTRAINT [CK_Clients_PhoneNumber] 
		CHECK ([PhoneNumber] LIKE '9[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
-- ������� "�������� ������������"
CREATE TABLE Agencys(
AgencyID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id ��������
NameAgency NVARCHAR(100) NOT NULL, -- �������� ��������
AddressAgency NVARCHAR(200) NOT NULL, -- ����� ��������
PhoneAgency CHAR(10) NOT NULL -- ����� �������� ��������

CONSTRAINT [CK_Agency_PhoneAgency] 
		CHECK ([PhoneAgency] LIKE '9[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
-- ������� "������"
CREATE TABLE Transactions(
TransactionsID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, --id ������
PropertyID INT FOREIGN KEY REFERENCES Propertys(PropertyID) NOT NULL,
ClientsID INT FOREIGN KEY REFERENCES Clients(ClientsID) NOT NULL,
AgencyID INT FOREIGN KEY REFERENCES Agencys(AgencyID) NOT NULL,
TransactionDate DATE NOT NULL, --���� ������
AmountTransaction MONEY NOT NULL -- ����� ������
);
--������� "��� ������"
CREATE TABLE TypeTransactions(
TransactionTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id ���� ������
TransactionsID INT FOREIGN KEY REFERENCES Transactions(TransactionsID) NOT NULL,
TypeName NVARCHAR(50) NOT NULL -- ��� ������
);

