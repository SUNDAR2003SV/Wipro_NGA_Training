-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (INDEX SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;
-- ============================================================
-- SECTION 9: INDEXES
-- ============================================================
 
CREATE NONCLUSTERED INDEX idx_Appointment_Status
ON APPOINTMENT (Status);
 
CREATE NONCLUSTERED INDEX idx_Billing_PaymentStatus
ON BILLING (PaymentStatus);
 
CREATE NONCLUSTERED INDEX idx_Patient_Phone
ON PATIENT (Phone);

SET STATISTICS IO ON;
SET STATISTICS TIME ON;

-- ============================================================
-- TEST 1: WITHOUT INDEX (Table Scan)
-- ============================================================

-- Drop index if exists
DROP INDEX IF EXISTS idx_Appointment_Status ON APPOINTMENT;
DROP INDEX IF EXISTS idx_Billing_PaymentStatus ON BILLING;
DROP INDEX IF EXISTS idx_Patient_Phone ON PATIENT;

-- Query 1: Filter by Appointment Status
SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status
FROM APPOINTMENT
WHERE Status = 'Completed';

-- Query 2: Filter by Billing Payment Status
SELECT BillID, PatientID, TotalAmount, PaidAmount, PaymentStatus
FROM BILLING
WHERE PaymentStatus = 'Pending';

-- Query 3: Filter by Patient Phone
SELECT PatientID, FirstName, LastName, Phone, Email
FROM PATIENT
WHERE Phone = '9876543205';

-- Turn on execution plan
SET STATISTICS PROFILE ON;

SELECT AppointmentID, PatientID, Status
FROM APPOINTMENT WITH (NOLOCK)
WHERE Status = 'Completed';

SET STATISTICS PROFILE OFF;

-- ============================================================
-- CREATE INDEXES
-- ============================================================

CREATE NONCLUSTERED INDEX idx_Appointment_Status
ON APPOINTMENT (Status);

CREATE NONCLUSTERED INDEX idx_Billing_PaymentStatus
ON BILLING (PaymentStatus);

CREATE NONCLUSTERED INDEX idx_Patient_Phone
ON PATIENT (Phone);

-- ============================================================
-- TEST 2: WITH INDEX (Index Seek)
-- ============================================================

-- Query 1: Filter by Appointment Status
SELECT AppointmentID, PatientID, DoctorID, AppointmentDate, Status
FROM APPOINTMENT
WHERE Status = 'Completed';

-- Query 2: Filter by Billing Payment Status
SELECT BillID, PatientID, TotalAmount, PaidAmount, PaymentStatus
FROM BILLING
WHERE PaymentStatus = 'Pending';

-- Query 3: Filter by Patient Phone
SELECT PatientID, FirstName, LastName, Phone, Email
FROM PATIENT
WHERE Phone = '9876543205';

-- Turn on execution plan
SET STATISTICS PROFILE ON;

SELECT AppointmentID, PatientID, Status
FROM APPOINTMENT WITH (INDEX(idx_Appointment_Status))
WHERE Status = 'Completed';

SET STATISTICS PROFILE OFF;

-- View all indexes on project tables
SELECT
    t.name        AS TableName,
    i.name        AS IndexName,
    i.type_desc   AS IndexType,
    c.name        AS ColumnName
FROM sys.indexes i
INNER JOIN sys.tables       t ON i.object_id  = t.object_id
INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id
                               AND i.index_id  = ic.index_id
INNER JOIN sys.columns      c ON ic.object_id  = c.object_id
                               AND ic.column_id = c.column_id
WHERE t.name IN ('APPOINTMENT','BILLING','PATIENT')
AND   i.name IS NOT NULL
ORDER BY t.name, i.name;
 
-- ============================================================
-- END OF SPRINT 2
-- ============================================================