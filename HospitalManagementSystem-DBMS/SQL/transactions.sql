-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 3: Advanced SQL and Optimization (TRANSACTIONS SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 4: TRANSACTIONS (TCL)
-- ============================================================

-- Transaction 1: COMMIT (Success)
BEGIN TRANSACTION;
    UPDATE BILLING
    SET PaidAmount = 1000.00, PaymentStatus = 'Paid'
    WHERE BillID = 5;
    PRINT 'Payment updated successfully';
COMMIT TRANSACTION;
PRINT 'COMMIT: Transaction successful';

-- Verify
SELECT BillID, PaidAmount, PaymentStatus FROM BILLING WHERE BillID = 5;

-- ─────────────────────────────────────

-- Transaction 2: ROLLBACK (Failure)
BEGIN TRANSACTION;
    UPDATE BILLING
    SET PaidAmount = 99999.00, PaymentStatus = 'Paid'
    WHERE BillID = 8;

    IF (SELECT PaidAmount FROM BILLING WHERE BillID = 8)
     > (SELECT TotalAmount FROM BILLING WHERE BillID = 8)
    BEGIN
        PRINT 'ERROR: Invalid amount — rolling back';
        ROLLBACK TRANSACTION;
    END
    ELSE
    BEGIN
        COMMIT TRANSACTION;
    END

-- Verify unchanged
SELECT BillID, TotalAmount, PaidAmount, PaymentStatus FROM BILLING WHERE BillID = 8;