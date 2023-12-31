/****** Script for SelectTopNRows command from SSMS  ******/
/*distinct - מסדר לפי האותיות מלמעלה למלטה ומוריד כפוליותת*/
SELECT distinct CustomerName as name
FROM Customers 


SELECT *
FROM Customers

SELECT *
FROM Customers
where state <> 'NY'

SELECT *
FROM Customers
where state not in('NY','wa')

SELECT *
FROM Customers
where Phone like '9%9'

select *
from Orders
where OrderTotal between 0 and 1000

select c.CustomerID, c.CustomerName, c.Phone , o.OrderID, o.OrderTotal ,o.OrderDate
from Customers c 
right outer join Orders o on c.CustomerID = o.CustomerID
where c.CustomerName like 'T%' /*and o.OrderID IS NULL*/
Order By o.OrderTotal desc




INSERT INTO Orders (CustomerID, OrderDate,OrderID, OrderTotal)
VALUES (6, '2023-12-15',52, 140);

INSERT INTO Customers (CustomerID,CustomerName,Phone,Address,City,State,zip,Country)
VALUES
(7, 'osher odd','123123123','harnof','jeruslame','','111','Israel')
,(8, 'foodes','333444555','bitvagan','jeruslame','','121','Israel');

UPDATE Orders
SET OrderTotal = 590
where OrderID = 51

select sum(OrderTotal)
from Orders
where OrderDate >= DATEADD(month,-1,GETDATE())
group by CustomerID

select *
from Customers
where Address like '%Drive%' and Phone like '%9'

select *
from Customers
where Address LIKE '1[2-4]%' 

select top(3)  o.CustomerID,o.OrderTotal
from Customers c
join Orders o on o.CustomerID = c.CustomerID
order by o.OrderTotal desc
/* ^-start , $-ends , [asd]*/

select  o.CustomerID ,o.OrderTotal, op.CookieID, op.Quantity , o.OrderTotal/op.Quantity as price
from Customers c
join Orders o on o.CustomerID = c.CustomerID
join Order_Product op on op.OrderID=o.OrderID
order by price






