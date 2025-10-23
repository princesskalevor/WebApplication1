@echo off
echo Fixing Identity Columns in BLOODCONNECT Database...
echo WARNING: This will drop and recreate all tables. Any existing data will be lost!
echo.
set /p confirm="Are you sure you want to continue? (y/N): "
if /i not "%confirm%"=="y" (
    echo Operation cancelled.
    pause
    exit /b
)

echo.
echo Dropping and recreating tables with proper IDENTITY columns...
echo.

sqlcmd -S localhost\SQLEXPRESS -E -i "Database\FixIdentityColumns.sql"

echo.
echo Tables have been recreated with proper IDENTITY columns.
echo You can now test creating new records.
echo.
pause
