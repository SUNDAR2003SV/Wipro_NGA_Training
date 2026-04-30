create database Joins_db;

USE Joins_db;

CREATE TABLE Restaurants (
    ID INT PRIMARY KEY,
    Name VARCHAR(50),
    Location VARCHAR(100)
);

INSERT INTO Restaurants (ID, Name, Location)
VALUES
('1','ABC Restaurant','Chennai'),
('2','Red Chillis','Pondy'),
('3','Accord','Bangalore'),
('4','Kamatchi','Mahe')

select * from Restaurants;

CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    restaurant_id INT,
    order_date DATE
);

INSERT INTO Orders (order_id, restaurant_id, order_date)
VALUES
('1','1','2025-01-01'),
('2','1','2025-01-02'),
('3','3','2025-01-02'),
('4','2','2025-01-03')

select * from Orders;

select 
r.Name as Restaurant_name,
o.order_date as Ordered_date
from Restaurants r
JOIN Orders o on r.ID = o.restaurant_id

select 
r.Name as Restaurant_name,
o.order_date as Ordered_date
from Restaurants r
LEFT JOIN Orders o on r.ID = o.restaurant_id

select 
r.Name as Restaurant_name,
o.order_date as Ordered_date
from Restaurants r
RIGHT JOIN Orders o on r.ID = o.restaurant_id
