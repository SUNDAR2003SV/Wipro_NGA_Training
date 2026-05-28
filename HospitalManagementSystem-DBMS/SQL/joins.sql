-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (JOINS SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 6: JOINS
-- ============================================================
 
-- INNER JOIN: Patient and appointments
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    A.AppointmentDate,
    A.Status
FROM PATIENT P
INNER JOIN APPOINTMENT A ON P.PatientID = A.PatientID;
 
-- INNER JOIN: Doctor with specialization
SELECT
    D.FirstName + ' ' + D.LastName AS DoctorName,
    S.SpecName,
    S.Department
FROM DOCTOR D
INNER JOIN SPECIALIZATION S ON D.SpecializationID = S.SpecializationID;
 
-- INNER JOIN: Full appointment details
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    D.FirstName + ' ' + D.LastName AS DoctorName,
    S.SpecName,
    A.AppointmentDate,
    A.Status
FROM APPOINTMENT A
INNER JOIN PATIENT        P ON A.PatientID        = P.PatientID
INNER JOIN DOCTOR         D ON A.DoctorID         = D.DoctorID
INNER JOIN SPECIALIZATION S ON D.SpecializationID = S.SpecializationID;
 
-- LEFT JOIN: All patients including those with no appointments
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    A.AppointmentDate,
    A.Status
FROM PATIENT P
LEFT JOIN APPOINTMENT A ON P.PatientID = A.PatientID;
 
-- RIGHT JOIN: All doctors including those with no appointments
SELECT
    D.FirstName + ' ' + D.LastName AS DoctorName,
    S.SpecName,
    A.AppointmentDate
FROM APPOINTMENT A
RIGHT JOIN DOCTOR         D ON A.DoctorID         = D.DoctorID
RIGHT JOIN SPECIALIZATION S ON D.SpecializationID = S.SpecializationID;
 
-- FULL OUTER JOIN: All patients and appointments
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    A.AppointmentDate,
    A.Status
FROM PATIENT P
FULL OUTER JOIN APPOINTMENT A ON P.PatientID = A.PatientID;