USE TelerikAcademy
GO

SELECT
    CONCAT(E.FirstName, '.', E.LastName, '@telerik.com') AS "Full Email Addresses"
FROM
    Employees AS E;