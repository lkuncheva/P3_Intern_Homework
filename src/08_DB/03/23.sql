USE TelerikAcademy
GO

SELECT
    E.EmployeeID,
    E.FirstName AS EmployeeFirstName,
    E.LastName AS EmployeeLastName,
    M.FirstName AS ManagerFirstName,
    M.LastName AS ManagerLastName
FROM
    Employees AS M
RIGHT OUTER JOIN
    Employees AS E
    ON E.ManagerID = M.EmployeeID;

/*
SELECT
    E.EmployeeID,
    E.FirstName AS EmployeeFirstName,
    E.LastName AS EmployeeLastName,
    M.FirstName AS ManagerFirstName,
    M.LastName AS ManagerLastName
FROM
    Employees AS E
LEFT OUTER JOIN
    Employees AS M
    ON E.ManagerID = M.EmployeeID;
*/