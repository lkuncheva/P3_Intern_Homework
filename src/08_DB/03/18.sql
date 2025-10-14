USE TelerikAcademy
GO

SELECT
    E.FirstName,
    E.MiddleName,
    E.LastName,
    A.AddressText
FROM
    Employees AS E
INNER JOIN
    Addresses AS A
ON
    E.AddressID = A.AddressID;