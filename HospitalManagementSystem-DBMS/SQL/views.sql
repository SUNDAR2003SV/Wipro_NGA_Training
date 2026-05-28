-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (VIEWS SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 8: VIEWS
-- ============================================================

GO
CREATE VIEW vw_PatientAppointments AS
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    A.AppointmentDate,
    A.Status,
    D.FirstName + ' ' + D.LastName AS DoctorName,
    S.SpecName
FROM PATIENT P
INNER JOIN APPOINTMENT    A ON P.PatientID        = A.PatientID
INNER JOIN DOCTOR         D ON A.DoctorID         = D.DoctorID
INNER JOIN SPECIALIZATION S ON D.SpecializationID = S.SpecializationID;

GO
CREATE VIEW vw_BillingSummary AS
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    B.TotalAmount,
    B.PaidAmount,
    B.TotalAmount - B.PaidAmount AS BalanceDue,
    B.PaymentStatus,
    B.PaymentMethod,
    B.BillDate
FROM BILLING B
INNER JOIN PATIENT P ON B.PatientID = P.PatientID;

GO
CREATE VIEW vw_PendingPayments AS
SELECT
    P.FirstName + ' ' + P.LastName AS PatientName,
    P.Phone,
    B.TotalAmount,
    B.PaidAmount,
    B.TotalAmount - B.PaidAmount AS BalanceDue,
    B.BillDate
FROM BILLING B
INNER JOIN PATIENT P ON B.PatientID = P.PatientID
WHERE B.PaymentStatus IN ('Pending','Partial');

GO
-- Query all views
SELECT * FROM vw_PatientAppointments;
SELECT * FROM vw_BillingSummary;
SELECT * FROM vw_PendingPayments;