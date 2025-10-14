USE TelerikAcademy
GO

SELECT
    [Name] AS TownsAndDepartments
FROM
    Departments

UNION

SELECT
    [Name] AS TownsAndDepartments
FROM
    Towns

ORDER BY
    TownsAndDepartments;