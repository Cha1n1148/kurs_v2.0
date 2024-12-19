CREATE DATABASE Kurs;
GO
USE Kurs;
GO
--Таблица "Недвижимость"
CREATE TABLE Propertys(
PropertyID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, --номер недвижимости
AddressProperty NVARCHAR(200) NOT NULL, --адрес недвижимости
Class NVARCHAR(50) NOT NULL, -- тип недвижимости
Price MONEY NOT NULL, -- цена
Condition NVARCHAR(50) NOT NULL -- состояние
);
-- Таблица "Клиенты"
CREATE TABLE Clients(
ClientsID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id клиента
FirstName NVARCHAR(100) NOT NULL, -- имя
MiddleName NVARCHAR(100) NOT NULL, -- отчество
LastName NVARCHAR(100) NOT NULL, -- фамилия
PhoneNumber CHAR(10) NOT NULL, -- номер телефона клиента
Email VARCHAR(255) NULL, -- электронная почта
 
CONSTRAINT [CK_Clients_PhoneNumber] 
		CHECK ([PhoneNumber] LIKE '9[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
-- Таблица "Агенства недвижимости"
CREATE TABLE Agencys(
AgencyID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id агенства
NameAgency NVARCHAR(100) NOT NULL, -- название агенства
AddressAgency NVARCHAR(200) NOT NULL, -- адрес агенства
PhoneAgency CHAR(10) NOT NULL -- номер телефона агенства

CONSTRAINT [CK_Agency_PhoneAgency] 
		CHECK ([PhoneAgency] LIKE '9[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]')
);
-- Таблица "Сделки"
CREATE TABLE Transactions(
TransactionsID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, --id сделки
PropertyID INT FOREIGN KEY REFERENCES Propertys(PropertyID) NOT NULL,
ClientsID INT FOREIGN KEY REFERENCES Clients(ClientsID) NOT NULL,
AgencyID INT FOREIGN KEY REFERENCES Agencys(AgencyID) NOT NULL,
TransactionDate DATE NOT NULL, --дата сделки
AmountTransaction MONEY NOT NULL -- сумма сделки
);
--Таблица "Тип сделок"
CREATE TABLE TypeTransactions(
TransactionTypeID INT IDENTITY(1,1) PRIMARY KEY NOT NULL, -- id типа сделки
TransactionsID INT FOREIGN KEY REFERENCES Transactions(TransactionsID) NOT NULL,
TypeName NVARCHAR(50) NOT NULL -- тип сделки
);

