USE TelerikAcademy
GO

SELECT TOP (5)
    *
FROM
    Employees
WHERE
    Salary > 50000
ORDER BY Salary DESC;