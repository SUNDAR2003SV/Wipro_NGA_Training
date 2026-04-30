USE Employee;

CREATE TABLE Employees_func
(
    EmpID INT PRIMARY KEY,         
    FirstName VARCHAR(50),         
    LastName VARCHAR(50),           
    Department VARCHAR(50),         
    Salary INT                      
);

INSERT INTO Employees_func(EmpID, FirstName, LastName, Department, Salary) 
VALUES (1, 'John', 'Snow', 'IT', 50000),
       (2, 'Tyrion', 'Lannister', 'HR', 60000),
       (3, 'Ned', 'Stark', 'IT', 80000),
       (4, 'Arya', 'Stark', 'Finance', 75000);

SELECT * FROM Employees_func;

GO

-- Function to return Full Name
CREATE FUNCTION GETFULLNAME
(
    @FirstName VARCHAR(50),   
    @LastName VARCHAR(50)     
)
RETURNS VARCHAR(101)        
AS
BEGIN
    RETURN @FirstName + ' ' + @LastName;  
END;

GO

-- Calling function to display Full Name
SELECT dbo.GETFULLNAME(FirstName, LastName) AS FullName
FROM Employees_func;

GO

-- Function to calculate Annual Salary
CREATE FUNCTION GetAnnualSalary(@Salary INT)  
RETURNS INT                                   
AS
BEGIN
    RETURN @Salary * 12;  
END;

GO

-- Calling function to display Annual Salary
SELECT EmpID, dbo.GetAnnualSalary(Salary) AS AnnualSalary
FROM Employees_func;

GO

-- Function to return Employee Name if Salary > Given Value
CREATE FUNCTION GetEmployeeNameBySalary
(
    @FirstName VARCHAR(50),   
    @LastName VARCHAR(50),    
    @Salary INT,             
    @InputSalary INT         
)
RETURNS VARCHAR(101)          
AS
BEGIN
    DECLARE @Result VARCHAR(101);  

    IF @Salary > @InputSalary
        SET @Result = @FirstName + ' ' + @LastName;  
    ELSE
        SET @Result = NULL; 

    RETURN @Result;
END;

GO

-- Calling function to filter employees with salary > 60000
SELECT dbo.GetEmployeeNameBySalary(FirstName, LastName, Salary, 60000) AS EmployeeName
FROM Employees_func;

USE Employee;

GO

-- Function to calculate Bonus based on Department
CREATE FUNCTION GetBonus
(
    @Department VARCHAR(50),   
    @Salary INT                
)
RETURNS INT                   
AS
BEGIN
    DECLARE @Bonus INT        

    IF @Department = 'IT'
        SET @Bonus = @Salary * 15 / 100   -- 15% bonus for IT
    ELSE IF @Department = 'HR'
        SET @Bonus = @Salary * 12 / 100   -- 12% bonus for HR
    ELSE
        SET @Bonus = @Salary * 10 / 100   -- 10% bonus for other departments

    RETURN @Bonus   
END;

GO

-- Calling function to display employee details with bonus
SELECT 
    FirstName,                                  
    Department,                                  
    Salary,                                     
    dbo.GetBonus(Department, Salary) AS Bonus   
FROM Employees_func;