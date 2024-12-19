/*1.Триггер, не позволяющий зарегестрировать одно и тоже агенство несколько раз */
CREATE TRIGGER PreventDuplicateAgencyRegistration
ON Agencys
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Agencys A
        JOIN inserted I ON A.NameAgency = I.NameAgency
        WHERE A.AgencyID <> I.AgencyID -- Исключаем из поиска вновь добавленное агентство
    )
    BEGIN
        RAISERROR('Это агенство уже зарегестрировано!', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO
DROP TRIGGER PreventDuplicateAgencyRegistration;
GO
/*2.Триггер,проверяющий тип недвижимости. */
CREATE TRIGGER CheckPropertyType
ON Propertys
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE Class NOT IN ('Дом', 'Квартира', 'Техническое помещение')
    )
    BEGIN
        RAISERROR('Неправильное указание типа недвижимости. Допустимые типы: "Дом", "Квартира", или "Техническое помещение".', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO
/*3.Триггер для автоматической блокировки удаления недвижимости с активными сделками.*/
CREATE TRIGGER PreventPropertyDeletion
ON Propertys
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted d JOIN Transactions t ON d.PropertyID = t.PropertyID)
    BEGIN
        RAISERROR ('Нельзя удалить недвижимость, так как она участвует в сделке!', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        DELETE FROM Propertys WHERE PropertyID IN (SELECT PropertyID FROM deleted);
    END
END;
DROP TRIGGER PreventPropertyDeletion;