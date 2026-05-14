CREATE DATABASE BookStoreDB;

USE BookStoreDB;

CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    PublishedYear INT
);

GO

CREATE PROCEDURE sp_AddBook
    @Title NVARCHAR(100),
    @Author NVARCHAR(100),
    @Price DECIMAL(10,2),
    @PublishedYear INT
AS
BEGIN
    INSERT INTO Books (Title, Author, Price, PublishedYear)
    VALUES (@Title, @Author, @Price, @PublishedYear)
END
GO

SELECT DB_NAME();

EXEC sp_AddBook 
    @Title = 'Clean Code',
    @Author = 'Robert C. Martin',
    @Price = 499.00,
    @PublishedYear = 2008;

SELECT * FROM Books;