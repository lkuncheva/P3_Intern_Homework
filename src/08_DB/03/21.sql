USE TelerikAcademy
GO

SELECT
    E.FirstName AS EmployeeFirstName,
    E.LastName AS EmployeeLastName,
    M.FirstName AS ManagerFirstName,
    M.LastName AS ManagerLastName,
    A.AddressText AS EmployeeAddress
FROM
    Employees AS E
JOIN
    Addresses AS A
    ON E.AddressID = A.AddressID
LEFT JOIN
    Employees AS M
    ON E.ManagerID = M.EmployeeID;
