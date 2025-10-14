USE TelerikAcademy
GO

SELECT
    FirstName,
    MiddleName,
    LastName
FROM
    Employees
WHERE
    LastName LIKE '%ei%';