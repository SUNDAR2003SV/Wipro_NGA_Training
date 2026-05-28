-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (SUBQUERIES SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 7: SUBQUERIES
-- ============================================================
 
-- Patients who have appointments
SELECT FirstName, LastName FROM PATIENT
WHERE PatientID IN (SELECT DISTINCT PatientID FROM APPOINTMENT);
 
-- Bills above average amount
SELECT BillID, PatientID, TotalAmount FROM BILLING
WHERE TotalAmount > (SELECT AVG(TotalAmount) FROM BILLING);
 
-- Patients with pending bills
SELECT FirstName, LastName FROM PATIENT
WHERE PatientID IN (SELECT PatientID FROM BILLING WHERE PaymentStatus = 'Pending');