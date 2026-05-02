CREATE DATABASE Normalization;

USE Normalization;

--1NF EXAMPLE
CREATE TABLE Student (
    student_id INT PRIMARY KEY,
    student_name VARCHAR(100)
);


--1NF BREAKING TABLES PHONENUMBER 
CREATE TABLE StudentPhone (
    student_id INT,
    phone VARCHAR(20),
    PRIMARY KEY(student_id, phone),
    FOREIGN KEY (student_id) REFERENCES Student(student_id)
);

INSERT INTO Student(student_id, student_name)
VALUES
(1, 'Alice'),
(2, 'Bob')

INSERT INTO StudentPhone(student_id, phone)
VALUES
(1, '123-4567'),
(1, '256-7891'),
(2, '335-5454')

SELECT * FROM Student;

SELECT * FROM StudentPhone;

--- IN THIS OUTPUT WE HAVE EACH SINGLE PEICE OF DATA IN A PARTICULAR COLUMN - HENCE 1NF ACHIEVED.

---2NF EXAMPLE
CREATE TABLE Courses (
    course_id INT PRIMARY KEY,
    course_name VARCHAR(100),
    instructor VARCHAR(100)
);

CREATE TABLE Enrollment (
    student_id INT,
    course_id INT,
    PRIMARY KEY(student_id, course_id),
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);

INSERT INTO Courses (course_id, course_name, instructor)
VALUES
(101, 'Intro to Sql', 'Dr.Smith'),
(102, 'Intro to Python	', 'Dr.James')

INSERT INTO Enrollment (student_id, course_id)
VALUES(1, 101),
      (2, 101),
      (1, 102);

SELECT * FROM Courses;

--- HERE (course_name, instructor) DEPENDS ON (course_id -> PRIMARY KEY) , HENCE PARTIAL DEPENDENCIES IS REMOVED


---3NF EXAMPLE
CREATE TABLE Instructor6 (
    instructor VARCHAR(100) PRIMARY KEY,
    instructor_office  VARCHAR(100)
);

CREATE TABLE Courses6 (
    course_id    INT PRIMARY KEY,
    course_name  VARCHAR(100),
    instructor   VARCHAR(100),
    FOREIGN KEY (instructor) REFERENCES Instructor6(instructor)
);

INSERT INTO Instructor6 (instructor, instructor_office)
VALUES
('Dr.Smith', 'Room 101');

INSERT INTO Courses6 (course_id, course_name, instructor)
VALUES
(101 , 'Into to SQL', 'Dr.Smith');

SELECT * FROM Instructor6;

SELECT * FROM Courses6;
--HERE (course_name -> NON-KEY, instructor -> NON-KEY) THEY BOTH DEPENDS COMPLETELY ON (course_id -> PRIMARY KEY), HENCE TRANSITIVE DEPENDENCIES IS REMOVED.