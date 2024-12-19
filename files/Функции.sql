/*1.Функция "Получить среднюю цену недвижимости по типу". Эта функция возвращает среднюю цену недвижимости для заданного типа.*/
CREATE FUNCTION GetAveragePriceByPropertyType
(
    @PropertyType NVARCHAR(50)
)
RETURNS MONEY
AS
BEGIN
    DECLARE @AveragePrice MONEY;

    SELECT @AveragePrice = AVG(Price)
    FROM Propertys
    WHERE Class = @PropertyType;

    RETURN @AveragePrice;
END;
GO
SELECT dbo.GetAveragePriceByPropertyType('Квартира');
GO

/*2.Функция, которая получает информацию о сделке по ее ID.*/
CREATE FUNCTION Function2
(
    @TransactionID INT
)
RETURNS TABLE
AS
RETURN
(
    SELECT 
        T.TransactionsID,
        T.PropertyID,
        T.ClientsID,
        T.AgencyID,
        T.TransactionDate,
        T.AmountTransaction,
        P.AddressProperty,
        P.Class AS PropertyType,
        C.FirstName + ' ' + C.LastName AS ClientName,
        A.NameAgency
    FROM Transactions AS T
    JOIN Propertys AS P ON T.PropertyID = P.PropertyID
    JOIN Clients AS C ON T.ClientsID = C.ClientsID
    JOIN Agencys AS A ON T.AgencyID = A.AgencyID
    WHERE T.TransactionsID = @TransactionID
);
GO
SELECT * FROM dbo.Function2('22');
GO
/*3.Функция "Получить количество сделок по типу недвижимости": Эта функция возвращает количество сделок для заданного типа недвижимости.*/
CREATE FUNCTION dbo.GetTransactionCountByPropertyType
(
    @PropertyType NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
    DECLARE @TransactionCount INT;

    SELECT @TransactionCount = COUNT(*)
    FROM Transactions AS T
    JOIN Propertys AS P ON T.PropertyID = P.PropertyID
    WHERE P.Class = @PropertyType;

    RETURN @TransactionCount;
END;
GO
SELECT dbo.GetTransactionCountByPropertyType('Дом');
GO

/*4.Функция "Получить общую сумму всех сделок в базе данных". Эта функция возвращает общую сумму всех сделок, совершенных в базе данных.*/
CREATE FUNCTION dbo.GetTotalTransactionAmount
()
RETURNS MONEY
AS
BEGIN
    DECLARE @TotalAmount MONEY;

    SELECT @TotalAmount = SUM(AmountTransaction)
    FROM Transactions;

    RETURN @TotalAmount;
END;
GO

SELECT dbo.GetTotalTransactionAmount()
GO

/*5.Функция, которая считает количество недвижимости по заданному типу*/
CREATE FUNCTION dbo.GetPropertyCountByType
(
    @PropertyType NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
    DECLARE @PropertyCount INT;

    SELECT @PropertyCount = COUNT(*)
    FROM Propertys
    WHERE Class = @PropertyType;

    RETURN @PropertyCount;
END;

SELECT dbo.GetPropertyCountByType('Техническое помещение');