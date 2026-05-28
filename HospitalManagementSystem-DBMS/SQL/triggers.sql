-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 3: Advanced SQL and Optimization (FUNCTIONS SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 3: TRIGGERS
-- ============================================================

-- Trigger 1: AFTER INSERT on BILLING
GO
CREATE TRIGGER trg_Billing_AfterInsert
ON BILLING
AFTER INSERT
AS
BEGIN
    INSERT INTO AUDIT_LOG
    (LogID, BillID, ActionType, OldStatus, NewStatus, ChangedAt, ChangedBy)
    SELECT
        (SELECT ISNULL(MAX(LogID), 0) + 1 FROM AUDIT_LOG),
        i.BillID,
        'INSERT',
        NULL,
        i.PaymentStatus,
        GETDATE(),
        'System'
    FROM inserted i
    PRINT 'Trigger fired: New bill logged'
END
GO

-- Test
INSERT INTO BILLING VALUES
(9, 7, 9, 1600.00, 0.00, 'Pending', 'Cash', '2024-03-15');

SELECT * FROM AUDIT_LOG ORDER BY LogID DESC;

-- ─────────────────────────────────────

-- Trigger 2: AFTER UPDATE on BILLING
GO
CREATE TRIGGER trg_Billing_AfterUpdate
ON BILLING
AFTER UPDATE
AS
BEGIN
    IF UPDATE(PaymentStatus)
    BEGIN
        INSERT INTO AUDIT_LOG
        (LogID, BillID, ActionType, OldStatus, NewStatus, ChangedAt, ChangedBy)
        SELECT
            (SELECT ISNULL(MAX(LogID), 0) + 1 FROM AUDIT_LOG),
            i.BillID,
            'UPDATE',
            d.PaymentStatus,
            i.PaymentStatus,
            GETDATE(),
            'System'
        FROM inserted i
        INNER JOIN deleted d ON i.BillID = d.BillID
        PRINT 'Trigger fired: Bill status change logged'
    END
END
GO

-- Test
UPDATE BILLING
SET PaidAmount = 1600.00, PaymentStatus = 'Paid'
WHERE BillID = 9;

SELECT * FROM AUDIT_LOG ORDER BY LogID DESC;

-- ─────────────────────────────────────

-- Trigger 3: AFTER DELETE on APPOINTMENT
GO
CREATE TRIGGER trg_Appointment_AfterDelete
ON APPOINTMENT
AFTER DELETE
AS
BEGIN
    PRINT 'Trigger fired: Appointment deleted'
END
GO

-- Test
DELETE FROM APPOINTMENT
WHERE AppointmentID = 7;
