USE TelerikAcademy
GO

SELECT
    FirstName,
    MiddleName,
    LastName,
    Salary
FROM
    Employees
WHERE
    Salary BETWEEN 20000 AND 30000;