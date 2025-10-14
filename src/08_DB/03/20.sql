USE TelerikAcademy
GO

SELECT
    E.FirstName AS EmployeeFirstName,
    E.LastName AS EmployeeLastName,
    M.FirstName AS ManagerFirstName,
    M.LastName AS ManagerLastName
FROM
    Employees AS E
LEFT JOIN
    Employees AS M
ON
    E.ManagerID = M.EmployeeID;
