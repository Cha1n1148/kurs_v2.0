/*1.Процедура "Добавить новую сделку". Эта процедура позволяет добавить новую сделку в базу данных.*/
CREATE PROCEDURE AddNewClient
    @FirstName NVARCHAR(100),
    @MiddleName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @PhoneNumber CHAR(10),
    @Email VARCHAR(255)
AS
BEGIN
    INSERT INTO Clients (FirstName, MiddleName, LastName, PhoneNumber, Email)
    VALUES (@FirstName, @MiddleName, @LastName, @PhoneNumber, @Email);
END;
GO
EXEC AddNewClient
@FirstName = 'Серегей',
@MiddleName = 'Борисович',
@LastName = 'Комаров',
@PhoneNumber = '9551734590',
@Email = 'zxc@yandex.ru';
GO

/*2.Процедура "Удалить клиента". Эта процедура удаляет клиента из базы данных по его идентификатору.*/
CREATE PROCEDURE DeleteClient
    @ClientID INT
AS
BEGIN
    DELETE FROM Clients WHERE ClientsID = @ClientID
END;
GO
EXEC DeleteClient
@ClientID = '52';
GO

/*3.Процедура "Получить информацию о сделке по её идентификатору". Эта процедура возвращает информацию о сделке по её идентификатору.*/
CREATE PROCEDURE GetTransactionDetails
    @TransactionsID INT
AS
BEGIN
    SELECT *
    FROM Transactions
    WHERE TransactionsID = @TransactionsID
END;
GO
EXEC GetTransactionDetails
@TransactionsID = '15'
GO

/*4.Процедура "Получить список сделок по типу недвижимости". Эта процедура возвращает список всех сделок для определенного типа недвижимости.*/
CREATE PROCEDURE GetTransactionsByPropertyType
    @PropertyType NVARCHAR(50)
AS
BEGIN
    SELECT *
    FROM Transactions AS T
    JOIN Propertys AS P ON T.PropertyID = P.PropertyID
    WHERE P.Class = @PropertyType;
END;
GO
EXEC GetTransactionsByPropertyType
@PropertyType = 'Дом'
GO

/*5.Процедура "Получить список всех агентств и количество сделок, совершенных каждым из них": Эта процедура возвращает список всех агентств и количество сделок, совершенных каждым из них.*/
CREATE PROCEDURE GetAgencyTransactionCount
AS
BEGIN
    SELECT 
        A.NameAgency,
        COUNT(T.TransactionsID) AS TransactionCount
    FROM Agencys AS A
    LEFT JOIN Transactions AS T ON A.AgencyID = T.AgencyID
    GROUP BY A.NameAgency;
END;
EXEC GetAgencyTransactionCount
GO