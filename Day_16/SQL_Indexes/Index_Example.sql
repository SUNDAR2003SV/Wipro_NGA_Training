USE Employee;

CREATE TABLE New_Employee(
	EmpID INT PRIMARY KEY,
	Name VARCHAR(50),
	Salary INT,
	Dept VARCHAR(30)
);

INSERT INTO New_Employee (EmpID, Name, Salary, Dept)
VALUES (1, 'John Snow', 5000, 'IT'),
	   (2, 'Tyrion', 3000, 'HR'),
	   (3, 'Arya', 4500, 'FINANCE'),
	   (4, 'Arya', 4500, 'IT'),
	   (5, 'Sansa', 6000, 'HR')

SET SHOWPLAN_ALL ON;
GO

SELECT * FROM New_Employee WHERE Dept = 'IT';

GO
SET SHOWPLAN_ALL OFF;

CREATE INDEX idx_dept
ON New_Employee(Dept);

SET SHOWPLAN_ALL ON;
GO

SELECT * FROM New_Employee WHERE Dept = 'IT';

GO
SET SHOWPLAN_ALL OFF;

SET STATISTICS TIME ON;

SET STATISTICS IO ON;

SET STATISTICS PROFILE ON;
