USE TelerikAcademy
GO

SELECT
    E.FirstName,
    E.MiddleName,
    E.LastName,
    A.AddressText
FROM
    Employees AS E,
    Addresses AS A
WHERE
    E.AddressID = A.AddressID;