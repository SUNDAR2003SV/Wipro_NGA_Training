# Hospital Management System — DBMS

## Project Overview
A fully normalized relational database system for managing
hospital operations including patient records, doctor scheduling,
appointments, treatments and billing.

| Field        | Details                  |
|--------------|--------------------------|
| Domain       | Healthcare               |
| Database     | Microsoft SQL Server     |
| Language     | T-SQL                    |
| Tool         | SSMS                     |
| Normal Form  | 3NF                      |

---

## Database Design

### 7 Tables
| Table          | Purpose                              |
|----------------|--------------------------------------|
| SPECIALIZATION | Medical departments and specializations |
| DOCTOR         | Doctor details linked to specialization |
| PATIENT        | Patient personal and contact details |
| APPOINTMENT    | Appointment bookings between patient and doctor |
| TREATMENT      | Diagnosis and prescription records  |
| BILLING        | Bill and payment details            |
| AUDIT_LOG      | Tracks every billing change         |

### Relationships
- SPECIALIZATION → DOCTOR     (categorizes)
- PATIENT → APPOINTMENT       (books)
- DOCTOR → APPOINTMENT        (attends)
- APPOINTMENT → TREATMENT     (leads to)
- APPOINTMENT → BILLING       (generates)
- PATIENT → BILLING           (receives)
- BILLING → AUDIT_LOG         (tracked by)

---

## Features Implemented

### Sprint 1 — Database Design
- ER Diagram with 7 tables and 7 relationships
- Normalization up to 3NF
- Entity-Relationship justification

### Sprint 2 — Database Implementation
- DDL: CREATE, ALTER, DROP, TRUNCATE
- Constraints: PK, FK, NOT NULL, CHECK, UNIQUE
- DML: INSERT, UPDATE, DELETE
- SELECT queries with WHERE, GROUP BY, HAVING
- INNER, LEFT, RIGHT, FULL OUTER Joins
- Subqueries: Scalar, Correlated, Nested
- Views: vw_PatientAppointments, vw_BillingSummary, vw_PendingPayments
- Indexes: Non-clustered indexes with performance proof

### Sprint 3 — Advanced SQL
- Scalar Functions: fn_GetPatientAge, fn_GetBalanceDue
- Table-Valued Function: fn_GetDoctorAppointments
- Triggers: AFTER INSERT, AFTER UPDATE, AFTER DELETE
- Transactions: COMMIT and ROLLBACK demo
- DCL: GRANT and REVOKE with role simulation

---

## How to Execute

### Step 1 — Create Database
```sql
CREATE DATABASE HospitalManagementSystem;
USE HospitalManagementSystem;
```

### Step 2 — Run Scripts in Order

SQL/ddl_scripts.sql     → Create tables with constraints
SQL/dml_scripts.sql     → Insert sample data
SQL/queries.sql         → SELECT, JOIN, Subqueries
SQL/views.sql           → Create views
SQL/indexes.sql         → Create indexes
SQL/triggers.sql        → Create triggers
SQL/functions.sql       → Create functions

### Step 3 — Verify
```sql
SELECT name FROM sys.tables;
SELECT name FROM sys.views;
SELECT name FROM sys.indexes;
```

---

## Sample Queries

### Patient Appointment Summary
```sql
SELECT * FROM vw_PatientAppointments;
```

### Pending Payments
```sql
SELECT * FROM vw_PendingPayments;
```

### Doctor Appointments (Function)
```sql
SELECT * FROM dbo.fn_GetDoctorAppointments(1);
```

### Patient Age (Function)
```sql
SELECT FirstName, dbo.fn_GetPatientAge(DateOfBirth) AS Age
FROM PATIENT;
```

---

## Normalization

| Normal Form | Rule | Status |
|-------------|------|--------|
| 1NF | Atomic values, no repeating groups | ✅ All 7 tables pass |
| 2NF | No partial dependency | ✅ All 7 tables pass |
| 3NF | No transitive dependency | ✅ All 7 tables pass |

---

## Index Performance Proof

| Index | Table | Without Index | With Index |
|-------|-------|---------------|------------|
| idx_Appointment_Status | APPOINTMENT | Table Scan | Index Seek |
| idx_Billing_PaymentStatus | BILLING | Table Scan | Index Seek |
| idx_Patient_Phone | PATIENT | Table Scan | Index Seek |

---

## Project Structure

HospitalManagementSystem-DBMS/
├── README.md
├── ERD/
│   └── HMS_ER_Diagram.png
├── SQL/
│   ├── ddl_scripts.sql
│   ├── dml_scripts.sql
│   ├── queries.sql
│   ├── subqueries.sql
│   ├── views.sql
│   ├── indexes.sql
│   ├── triggers.sql
│   ├── functions.sql
│   ├── transactions.sql
│   └── dcl_scripts.sql
├── Documentation/
│   └── normalization_report.pdf
└── Output/
└── sample_results.xlsx

