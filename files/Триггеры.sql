/*1.�������, �� ����������� ���������������� ���� � ���� �������� ��������� ��� */
CREATE TRIGGER PreventDuplicateAgencyRegistration
ON Agencys
AFTER INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Agencys A
        JOIN inserted I ON A.NameAgency = I.NameAgency
        WHERE A.AgencyID <> I.AgencyID -- ��������� �� ������ ����� ����������� ���������
    )
    BEGIN
        RAISERROR('��� �������� ��� ����������������!', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO
DROP TRIGGER PreventDuplicateAgencyRegistration;
GO
/*2.�������,����������� ��� ������������. */
CREATE TRIGGER CheckPropertyType
ON Propertys
AFTER INSERT, UPDATE
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE Class NOT IN ('���', '��������', '����������� ���������')
    )
    BEGIN
        RAISERROR('������������ �������� ���� ������������. ���������� ����: "���", "��������", ��� "����������� ���������".', 16, 1);
        ROLLBACK TRANSACTION;
    END;
END;
GO
/*3.������� ��� �������������� ���������� �������� ������������ � ��������� ��������.*/
CREATE TRIGGER PreventPropertyDeletion
ON Propertys
INSTEAD OF DELETE
AS
BEGIN
    IF EXISTS (SELECT 1 FROM deleted d JOIN Transactions t ON d.PropertyID = t.PropertyID)
    BEGIN
        RAISERROR ('������ ������� ������������, ��� ��� ��� ��������� � ������!', 16, 1);
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        DELETE FROM Propertys WHERE PropertyID IN (SELECT PropertyID FROM deleted);
    END
END;
DROP TRIGGER PreventPropertyDeletion;