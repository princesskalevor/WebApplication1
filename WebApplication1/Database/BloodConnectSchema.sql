-- BloodConnect Database Schema
-- This script creates all tables according to your specified schema

USE [BLOODCONNECT]
GO

-- Create DONOR table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='DONOR' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[DONOR](
        [DONORID] [int] IDENTITY(1,1) NOT NULL,
        [FIRSTNAME] [varchar](100) NOT NULL,
        [LASTNAME] [varchar](100) NOT NULL,
        [PHONENUMBER] [varchar](15) NOT NULL,
        [EMAILADDRESS] [varchar](100) NULL,
        [BLOODTYPE] [char](5) NOT NULL,
        [MEDICALHISTORY] [text] NOT NULL,
        [QRCODE] [varchar](300) NULL,
        CONSTRAINT [PK_DONOR] PRIMARY KEY CLUSTERED ([DONORID] ASC)
    )
END
GO

-- Create RECEIPIENT table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='RECEIPIENT' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[RECEIPIENT](
        [RECEIPIENTID] [int] IDENTITY(1,1) NOT NULL,
        [FIRSTNAME] [varchar](100) NOT NULL,
        [LASTNAME] [varchar](100) NOT NULL,
        [EMAIL] [varchar](100) NOT NULL,
        [PHONENUMBER] [int] NULL,
        [BLOODTYPE] [varchar](5) NOT NULL,
        [QUANTITYREQUESTED] [int] NULL,
        CONSTRAINT [PK_RECEIPIENT] PRIMARY KEY CLUSTERED ([RECEIPIENTID] ASC)
    )
END
GO

-- Create BLOODREQUEST table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BLOODREQUEST' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[BLOODREQUEST](
        [REQUESTID] [int] IDENTITY(1,1) NOT NULL,
        [BLOODTYPEREQUIRED] [varchar](5) NOT NULL,
        [QUANTITYREQUESTED] [int] NULL,
        [REQUESTEDDATE] [datetime] NOT NULL,
        [STATUS] [varchar](50) NOT NULL,
        [RECEIPIENTID] [int] NOT NULL,
        CONSTRAINT [PK_BLOODREQUEST] PRIMARY KEY CLUSTERED ([REQUESTID] ASC),
        CONSTRAINT [FK_BLOODREQUEST_RECEIPIENT] FOREIGN KEY([RECEIPIENTID]) REFERENCES [dbo].[RECEIPIENT] ([RECEIPIENTID])
    )
END
GO

-- Create BLOODINVENTORY table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BLOODINVENTORY' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[BLOODINVENTORY](
        [UNITID] [int] IDENTITY(1,1) NOT NULL,
        [BLOODTYPE] [varchar](5) NOT NULL,
        [QUANTITY] [int] NULL,
        [EXPIRATIONDATE] [datetime] NOT NULL,
        [STORAGELOCATION] [varchar](100) NULL,
        [STATUS] [varchar](50) NULL,
        CONSTRAINT [PK_BLOODINVENTORY] PRIMARY KEY CLUSTERED ([UNITID] ASC)
    )
END
GO

-- Create BLOODDONATION table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='BLOODDONATION' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[BLOODDONATION](
        [DONATIONID] [int] IDENTITY(1,1) NOT NULL,
        [BLOODTYPE] [varchar](5) NOT NULL,
        [QUANTITY] [int] NOT NULL,
        [DONATIONDATE] [datetime] NULL,
        [DONORID] [int] NOT NULL,
        CONSTRAINT [PK_BLOODDONATION] PRIMARY KEY CLUSTERED ([DONATIONID] ASC),
        CONSTRAINT [FK_BLOODDONATION_DONOR] FOREIGN KEY([DONORID]) REFERENCES [dbo].[DONOR] ([DONORID])
    )
END
GO

-- Create EMAILNOTIFICATION table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='EMAILNOTIFICATION' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[EMAILNOTIFICATION](
        [NOTIFICATIONID] [int] IDENTITY(1,1) NOT NULL,
        [SUBJECT] [varchar](100) NULL,
        [MESSAGE] [text] NULL,
        [DATESENT] [datetime] NOT NULL,
        [RECEIPIENTID] [int] NOT NULL,
        CONSTRAINT [PK_EMAILNOTIFICATION] PRIMARY KEY CLUSTERED ([NOTIFICATIONID] ASC),
        CONSTRAINT [FK_EMAILNOTIFICATION_RECEIPIENT] FOREIGN KEY([RECEIPIENTID]) REFERENCES [dbo].[RECEIPIENT] ([RECEIPIENTID])
    )
END
GO

-- Insert sample data
-- Sample Donors
INSERT INTO [dbo].[DONOR] ([FIRSTNAME], [LASTNAME], [PHONENUMBER], [EMAILADDRESS], [BLOODTYPE], [MEDICALHISTORY], [QRCODE])
VALUES 
('John', 'Doe', '0241234567', 'john.doe@email.com', 'A+', 'No known allergies', 'QR001'),
('Jane', 'Smith', '0241234568', 'jane.smith@email.com', 'B+', 'No medical conditions', 'QR002'),
('Mike', 'Johnson', '0241234569', 'mike.johnson@email.com', 'O+', 'No known issues', 'QR003'),
('Sarah', 'Wilson', '0241234570', 'sarah.wilson@email.com', 'AB+', 'No medical history', 'QR004'),
('David', 'Brown', '0241234571', 'david.brown@email.com', 'A-', 'No allergies', 'QR005')
GO

-- Sample Recipients
INSERT INTO [dbo].[RECEIPIENT] ([FIRSTNAME], [LASTNAME], [EMAIL], [PHONENUMBER], [BLOODTYPE], [QUANTITYREQUESTED])
VALUES 
('Alice', 'Cooper', 'alice.cooper@email.com', 0241234572, 'A+', 2),
('Bob', 'Taylor', 'bob.taylor@email.com', 0241234573, 'B+', 1),
('Carol', 'Davis', 'carol.davis@email.com', 0241234574, 'O+', 3),
('Eve', 'Miller', 'eve.miller@email.com', 0241234575, 'AB+', 2),
('Frank', 'Garcia', 'frank.garcia@email.com', 0241234576, 'A-', 1)
GO

-- Sample Blood Requests
INSERT INTO [dbo].[BLOODREQUEST] ([BLOODTYPEREQUIRED], [QUANTITYREQUESTED], [REQUESTEDDATE], [STATUS], [RECEIPIENTID])
VALUES 
('A+', 2, '2024-01-15 10:00:00', 'Pending', 1),
('B+', 1, '2024-01-16 14:30:00', 'Approved', 2),
('O+', 3, '2024-01-17 09:15:00', 'Pending', 3),
('AB+', 2, '2024-01-18 16:45:00', 'Completed', 4),
('A-', 1, '2024-01-19 11:20:00', 'Pending', 5)
GO

-- Sample Blood Inventory
INSERT INTO [dbo].[BLOODINVENTORY] ([BLOODTYPE], [QUANTITY], [EXPIRATIONDATE], [STORAGELOCATION], [STATUS])
VALUES 
('A+', 10, '2024-02-15', 'Refrigerator A', 'Available'),
('B+', 8, '2024-02-20', 'Refrigerator B', 'Available'),
('O+', 15, '2024-02-25', 'Refrigerator A', 'Available'),
('AB+', 5, '2024-02-18', 'Refrigerator C', 'Available'),
('A-', 7, '2024-02-22', 'Refrigerator B', 'Available')
GO

-- Sample Blood Donations
INSERT INTO [dbo].[BLOODDONATION] ([BLOODTYPE], [QUANTITY], [DONATIONDATE], [DONORID])
VALUES 
('A+', 1, '2024-01-10 10:00:00', 1),
('B+', 1, '2024-01-11 14:30:00', 2),
('O+', 1, '2024-01-12 09:15:00', 3),
('AB+', 1, '2024-01-13 16:45:00', 4),
('A-', 1, '2024-01-14 11:20:00', 5)
GO

-- Sample Email Notifications
INSERT INTO [dbo].[EMAILNOTIFICATION] ([SUBJECT], [MESSAGE], [DATESENT], [RECEIPIENTID])
VALUES 
('Blood Request Approved', 'Your blood request has been approved. Please contact us for further details.', '2024-01-16 15:00:00', 2),
('Blood Request Status Update', 'Your blood request is being processed. We will notify you once completed.', '2024-01-15 11:00:00', 1),
('Blood Request Completed', 'Your blood request has been completed successfully. Thank you for using our service.', '2024-01-18 17:00:00', 4),
('Urgent Blood Request', 'We have an urgent blood request that matches your blood type. Please contact us if you can help.', '2024-01-19 12:00:00', 5),
('Thank You', 'Thank you for your blood donation. Your contribution helps save lives.', '2024-01-14 12:00:00', 5)
GO

PRINT 'BloodConnect database schema created successfully with sample data!'
