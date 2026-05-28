-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (DML Scripts)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 5: SELECT QUERIES (SELECT, WHERE, GROUP BY, HAVING)
-- ============================================================
 
-- All patients
SELECT * FROM PATIENT;
 
-- Completed appointments
SELECT * FROM APPOINTMENT WHERE Status = 'Completed';
 
-- Pending bills
SELECT * FROM BILLING WHERE PaymentStatus = 'Pending';
 
-- Count appointments by status
SELECT Status, COUNT(*) AS Total
FROM APPOINTMENT
GROUP BY Status;
 
-- Total billed per patient
SELECT PatientID, SUM(TotalAmount) AS TotalBilled
FROM BILLING
GROUP BY PatientID
HAVING SUM(TotalAmount) > 1000
ORDER BY TotalBilled DESC;