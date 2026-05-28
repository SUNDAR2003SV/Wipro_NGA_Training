-- ============================================================
--   HOSPITAL MANAGEMENT SYSTEM
--   Sprint 2: Database Implementation (DML Scripts)
-- ============================================================

USE HospitalManagementSystem;

-- ============================================================
-- SECTION 3: INSERT DATA 
-- ============================================================
 
INSERT INTO SPECIALIZATION VALUES
(1, 'Cardiology',  'Heart & Vascular'),
(2, 'Neurology',   'Brain & Spine'),
(3, 'Orthopedics', 'Bone & Joint'),
(4, 'Dermatology', 'Skin & Hair'),
(5, 'Pediatrics',  'Child Care');
 
INSERT INTO PATIENT VALUES
(1,  'James',    'Carter',   '1985-03-12', 'M', 'O+',  '9876543201', 'james.carter@email.com',    '12 Oak Street, New York'),
(2,  'Emily',    'Davis',    '1990-07-25', 'F', 'A+',  '9876543202', 'emily.davis@email.com',     '34 Maple Ave, Chicago'),
(3,  'Michael',  'Brown',    '1978-11-08', 'M', 'B+',  '9876543203', 'michael.brown@email.com',   '56 Pine Road, Houston'),
(4,  'Sophia',   'Wilson',   '1995-05-19', 'F', 'AB+', '9876543204', 'sophia.wilson@email.com',   '78 Cedar Lane, Phoenix'),
(5,  'William',  'Taylor',   '1982-09-30', 'M', 'A-',  '9876543205', 'william.taylor@email.com',  '90 Birch Blvd, Dallas'),
(6,  'Olivia',   'Anderson', '1998-01-14', 'F', 'O-',  '9876543206', 'olivia.anderson@email.com', '22 Elm Street, San Jose'),
(7,  'Benjamin', 'Thomas',   '1975-06-22', 'M', 'B-',  '9876543207', 'benjamin.thomas@email.com', '44 Walnut Dr, Austin'),
(8,  'Charlotte','Jackson',  '1988-12-05', 'F', 'AB-', '9876543208', 'charlotte.j@email.com',     '66 Spruce Way, Seattle'),
(9,  'Henry',    'White',    '2001-04-17', 'M', 'O+',  '9876543209', 'henry.white@email.com',     '88 Poplar Ct, Denver'),
(10, 'Amelia',   'Harris',   '1993-08-29', 'F', 'A+',  '9876543210', 'amelia.harris@email.com',   '100 Ash Ave, Boston');
 
INSERT INTO DOCTOR VALUES
(1, 'John',   'Smith',   1, '9876501001', 'john.smith@hospital.com'),
(2, 'Sarah',  'Connor',  1, '9876501002', 'sarah.connor@hospital.com'),
(3, 'Robert', 'Miles',   2, '9876501003', 'robert.miles@hospital.com'),
(4, 'Alan',   'Grant',   3, '9876501004', 'alan.grant@hospital.com'),
(5, 'Laura',  'Hayes',   4, '9876501005', 'laura.hayes@hospital.com');
 
INSERT INTO APPOINTMENT VALUES
(1,  1, 1, '2024-01-10 10:00', 'Completed', 'Regular checkup'),
(2,  2, 2, '2024-01-11 11:00', 'Completed', 'Chest pain'),
(3,  3, 3, '2024-01-12 09:00', 'Completed', 'Headache'),
(4,  4, 4, '2024-01-13 14:00', 'Completed', 'Knee pain'),
(5,  5, 5, '2024-01-14 15:00', 'Completed', 'Skin rash'),
(6,  6, 1, '2024-02-10 10:00', 'Completed', 'Follow up'),
(7,  7, 2, '2024-02-15 11:30', 'Cancelled', 'No show'),
(8,  8, 3, '2024-02-20 09:30', 'Completed', 'Migraine'),
(9,  9, 4, '2024-03-05 14:00', 'Scheduled', 'Shoulder pain'),
(10,10, 5, '2024-03-10 15:30', 'Scheduled', 'Acne treatment');
 
INSERT INTO TREATMENT VALUES
(1, 1, 'Hypertension',    'Amlodipine 5mg',    'Monitor BP daily',        '2024-01-10'),
(2, 2, 'Angina',          'Nitroglycerin',      'Avoid stress',            '2024-01-11'),
(3, 3, 'Migraine',        'Sumatriptan 50mg',   'Rest in dark room',       '2024-01-12'),
(4, 4, 'Osteoarthritis',  'Ibuprofen 400mg',    'Physiotherapy 2x/week',   '2024-01-13'),
(5, 5, 'Eczema',          'Hydrocortisone 1%',  'Apply cream twice daily', '2024-01-14'),
(6, 6, 'Atrial Fibril.',  'Warfarin 5mg',       'Weekly INR test',         '2024-02-10'),
(7, 8, 'Chronic Migraine','Topiramate 25mg',    'Daily for 3 months',      '2024-02-20'),
(8, 9, 'Rotator Cuff',    'Diclofenac 75mg',    'MRI scan required',       '2024-03-05');
 
INSERT INTO BILLING VALUES
(1,  1,  1,  1500.00, 1500.00, 'Paid',    'Card',      '2024-01-10'),
(2,  2,  2,  2000.00, 2000.00, 'Paid',    'Insurance', '2024-01-11'),
(3,  3,  3,  1200.00,  600.00, 'Partial', 'Cash',      '2024-01-12'),
(4,  4,  4,  1800.00, 1800.00, 'Paid',    'Online',    '2024-01-13'),
(5,  5,  5,  1000.00,    0.00, 'Pending', 'Cash',      '2024-01-14'),
(6,  6,  6,  2500.00, 2500.00, 'Paid',    'Card',      '2024-02-10'),
(7,  8,  8,  1300.00, 1300.00, 'Paid',    'Insurance', '2024-02-20'),
(8, 10, 10,   900.00,    0.00, 'Pending', 'Online',    '2024-03-10');
 
INSERT INTO AUDIT_LOG VALUES
(1, 3, 'UPDATE', 'Pending', 'Partial', '2024-01-15 10:00', 'Admin'),
(2, 5, 'UPDATE', 'Pending', 'Pending', '2024-01-20 11:00', 'Admin'),
(3, 6, 'INSERT', NULL,      'Paid',    '2024-02-10 16:00', 'Receptionist');

-- ============================================================
-- SECTION 4: UPDATE AND DELETE
-- ============================================================
 
-- UPDATE: Mark bill as paid
UPDATE BILLING
SET PaidAmount = 1200.00, PaymentStatus = 'Paid'
WHERE BillID = 3;
 
-- UPDATE: Change appointment status
UPDATE APPOINTMENT
SET Status = 'Completed'
WHERE AppointmentID = 9;
 
-- DELETE: Remove cancelled appointment
DELETE FROM APPOINTMENT
WHERE Status = 'Cancelled'
AND AppointmentID = 7;

-- ============================================================
-- SECTION 5: SELECT QUERIES
-- ============================================================
 
-- All patients
SELECT * FROM PATIENT;
 
-- Completed appointments
SELECT * FROM APPOINTMENT WHERE Status = 'Completed';
 
-- Pending bills
SELECT * FROM BILLING WHERE PaymentStatus = 'Pending';
 
-- Count appointments by status
SELECT Status, COUNT(*) AS Total
FROM APPOINTMENT
GROUP BY Status;
 
-- Total billed per patient
SELECT PatientID, SUM(TotalAmount) AS TotalBilled
FROM BILLING
GROUP BY PatientID
HAVING SUM(TotalAmount) > 1000
ORDER BY TotalBilled DESC;
