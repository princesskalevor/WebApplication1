@echo off
echo Setting up BloodConnect Database Schema...
echo.

sqlcmd -S "DESKTOP-IF49OJQ\SQLEXPRESS" -i "WebApplication1\Database\BloodConnectSchema.sql"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo ✅ Database schema created successfully!
    echo ✅ Sample data inserted!
    echo.
    echo You can now run your application.
) else (
    echo.
    echo ❌ Error creating database schema.
    echo Please check your SQL Server connection and try again.
)

pause
