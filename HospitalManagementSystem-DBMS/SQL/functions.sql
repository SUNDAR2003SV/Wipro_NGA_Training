-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 3: Advanced SQL and Optimization (FUNCTIONS SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 1: SCALAR FUNCTIONS
-- ============================================================

-- Function 1: Calculate Patient Age
GO
CREATE FUNCTION fn_GetPatientAge (@DateOfBirth DATE)
RETURNS INT
AS
BEGIN
    RETURN DATEDIFF(YEAR, @DateOfBirth, GETDATE())
END
GO

-- Test
SELECT
    FirstName + ' ' + LastName        AS PatientName,
    DateOfBirth,
    dbo.fn_GetPatientAge(DateOfBirth) AS Age
FROM PATIENT;

-- ─────────────────────────────────────

-- Function 2: Calculate Balance Due
GO
CREATE FUNCTION fn_GetBalanceDue (@BillID INT)
RETURNS DECIMAL(10,2)
AS
BEGIN
    DECLARE @Balance DECIMAL(10,2)
    SELECT @Balance = TotalAmount - PaidAmount
    FROM BILLING WHERE BillID = @BillID
    RETURN @Balance
END
GO

-- Test
SELECT
    BillID,
    TotalAmount,
    PaidAmount,
    dbo.fn_GetBalanceDue(BillID) AS BalanceDue
FROM BILLING;

-- ============================================================
-- SECTION 2: TABLE-VALUED FUNCTION
-- ============================================================

-- Function 3: Get appointments for a doctor
GO
CREATE FUNCTION fn_GetDoctorAppointments (@DoctorID INT)
RETURNS TABLE
AS
RETURN
(
    SELECT
        A.AppointmentID,
        P.FirstName + ' ' + P.LastName AS PatientName,
        A.AppointmentDate,
        A.Status
    FROM APPOINTMENT A
    INNER JOIN PATIENT P ON A.PatientID = P.PatientID
    WHERE A.DoctorID = @DoctorID
)
GO

-- Test
SELECT * FROM dbo.fn_GetDoctorAppointments(1);