SELECT MAX(tmp.territory) as 'Максимальное колличество территории'
FROM 
(
SELECT EmployeeID as id, COUNT(*) as territory
FROM EmployeeTerritories
GROUP BY EmployeeID
HAVING COUNT(*)>1
) tmp
