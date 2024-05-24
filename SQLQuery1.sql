SELECT DISTINCT Products.ProductID AS 'ID', Products.ProductName as 'Название продукта',
Products.UnitPrice as 'Цена', [Order Details].UnitPrice as 'Цена заказа'
FROM Products, [Order Details]
WHERE [Order Details].UnitPrice >50 AND [Order Details].ProductID=Products.ProductID;