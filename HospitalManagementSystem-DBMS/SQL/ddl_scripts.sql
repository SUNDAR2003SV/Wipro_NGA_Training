-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (DDL SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 1: CREATE TABLES WITH CONSTRAINTS 
-- ============================================================
 
CREATE TABLE SPECIALIZATION (
    SpecializationID INT          PRIMARY KEY,
    SpecName         VARCHAR(100) NOT NULL UNIQUE,
    Department       VARCHAR(100) NOT NULL
);
 
CREATE TABLE PATIENT (
    PatientID   INT          PRIMARY KEY,
    FirstName   VARCHAR(50)  NOT NULL,
    LastName    VARCHAR(50)  NOT NULL,
    DateOfBirth DATE         NOT NULL,
    Gender      CHAR(1)      NOT NULL CHECK (Gender IN ('M','F','O')),
    BloodGroup  VARCHAR(5)   NOT NULL CHECK (BloodGroup IN ('A+','A-','B+','B-','AB+','AB-','O+','O-')),
    Phone       VARCHAR(15)  NOT NULL UNIQUE,
    Email       VARCHAR(100) NOT NULL UNIQUE,
    Address     VARCHAR(255) NOT NULL
);
 
CREATE TABLE DOCTOR (
    DoctorID         INT          PRIMARY KEY,
    FirstName        VARCHAR(50)  NOT NULL,
    LastName         VARCHAR(50)  NOT NULL,
    SpecializationID INT          NOT NULL,
    Phone            VARCHAR(15)  NOT NULL UNIQUE,
    Email            VARCHAR(100) NOT NULL UNIQUE,
    FOREIGN KEY (SpecializationID) REFERENCES SPECIALIZATION(SpecializationID)
);
 
CREATE TABLE APPOINTMENT (
    AppointmentID   INT         PRIMARY KEY,
    PatientID       INT         NOT NULL,
    DoctorID        INT         NOT NULL,
    AppointmentDate DATETIME    NOT NULL,
    Status          VARCHAR(20) NOT NULL CHECK (Status IN ('Scheduled','Completed','Cancelled')),
    Notes           VARCHAR(255),
    FOREIGN KEY (PatientID) REFERENCES PATIENT(PatientID),
    FOREIGN KEY (DoctorID)  REFERENCES DOCTOR(DoctorID)
);
 
CREATE TABLE TREATMENT (
    TreatmentID      INT          PRIMARY KEY,
    AppointmentID    INT          NOT NULL UNIQUE,
    Diagnosis        VARCHAR(255) NOT NULL,
    Prescription     VARCHAR(255) NOT NULL,
    TreatmentDetails VARCHAR(500),
    TreatmentDate    DATE         NOT NULL,
    FOREIGN KEY (AppointmentID) REFERENCES APPOINTMENT(AppointmentID)
);
 
CREATE TABLE BILLING (
    BillID        INT           PRIMARY KEY,
    PatientID     INT           NOT NULL,
    AppointmentID INT           NOT NULL UNIQUE,
    TotalAmount   DECIMAL(10,2) NOT NULL CHECK (TotalAmount > 0),
    PaidAmount    DECIMAL(10,2) NOT NULL DEFAULT 0,
    PaymentStatus VARCHAR(20)   NOT NULL CHECK (PaymentStatus IN ('Paid','Pending','Partial')),
    PaymentMethod VARCHAR(20)   NOT NULL CHECK (PaymentMethod IN ('Cash','Card','Insurance','Online')),
    BillDate      DATE          NOT NULL,
    FOREIGN KEY (PatientID)     REFERENCES PATIENT(PatientID),
    FOREIGN KEY (AppointmentID) REFERENCES APPOINTMENT(AppointmentID)
);
 
CREATE TABLE AUDIT_LOG (
    LogID      INT         PRIMARY KEY,
    BillID     INT         NOT NULL,
    ActionType VARCHAR(20) NOT NULL CHECK (ActionType IN ('INSERT','UPDATE','DELETE')),
    OldStatus  VARCHAR(20),
    NewStatus  VARCHAR(20),
    ChangedAt  DATETIME    NOT NULL DEFAULT GETDATE(),
    ChangedBy  VARCHAR(50) NOT NULL,
    FOREIGN KEY (BillID) REFERENCES BILLING(BillID)
);

-- ============================================================
-- SECTION 2: ALTER TABLE
-- ============================================================
 
-- Add column
ALTER TABLE PATIENT ADD IsActive BIT NOT NULL DEFAULT 1;
 
-- Modify column
ALTER TABLE TREATMENT ALTER COLUMN Diagnosis VARCHAR(500);
 
-- Drop column
ALTER TABLE PATIENT DROP COLUMN IsActive;