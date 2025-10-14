USE TelerikAcademy
GO

SELECT
    E.FirstName,
    E.LastName,
    E.HireDate,
    D.Name
FROM
    Employees AS E
INNER JOIN
    Departments AS D 
    ON E.DepartmentID = D.DepartmentID
WHERE
    D.Name IN ('Sales', 'Finance')
    AND DATEPART(year, E.HireDate) BETWEEN 1995 AND 2005