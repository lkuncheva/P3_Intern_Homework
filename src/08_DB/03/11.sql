USE TelerikAcademy
GO

SELECT
    FirstName,
    MiddleName,
    LastName
FROM
    Employees
WHERE
    FirstName LIKE 'SA%';