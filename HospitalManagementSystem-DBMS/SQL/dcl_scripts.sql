-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 3: Advanced SQL and Optimization (DCL SCRIPTS)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 5: DCL (GRANT and REVOKE)
-- ============================================================

-- Create users
CREATE LOGIN DoctorUser    WITH PASSWORD = 'Doctor@123';
CREATE LOGIN ReceptionUser WITH PASSWORD = 'Reception@123';
CREATE USER  DoctorUser    FOR LOGIN DoctorUser;
CREATE USER  ReceptionUser FOR LOGIN ReceptionUser;

-- Grant permissions
GRANT SELECT ON PATIENT     TO DoctorUser;
GRANT SELECT ON APPOINTMENT TO DoctorUser;
GRANT SELECT ON TREATMENT   TO DoctorUser;

GRANT SELECT, INSERT, UPDATE ON APPOINTMENT TO ReceptionUser;
GRANT SELECT, INSERT, UPDATE ON BILLING     TO ReceptionUser;

-- Verify permissions
SELECT
    pr.name                   AS UserName,
    OBJECT_NAME(dp.major_id)  AS ObjectName,
    dp.permission_name        AS Permission,
    dp.state_desc             AS State
FROM sys.database_permissions dp
INNER JOIN sys.database_principals pr
        ON dp.grantee_principal_id = pr.principal_id
WHERE pr.name IN ('DoctorUser','ReceptionUser')
ORDER BY pr.name;

-- Revoke
REVOKE UPDATE ON BILLING FROM ReceptionUser;

-- Verify after revoke
SELECT
    pr.name                   AS UserName,
    OBJECT_NAME(dp.major_id)  AS ObjectName,
    dp.permission_name        AS Permission,
    dp.state_desc             AS State
FROM sys.database_permissions dp
INNER JOIN sys.database_principals pr
        ON dp.grantee_principal_id = pr.principal_id
WHERE pr.name IN ('DoctorUser','ReceptionUser')
ORDER BY pr.name;

-- ============================================================
-- END OF SPRINT 3
-- ============================================================