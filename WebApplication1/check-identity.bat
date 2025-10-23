@echo off
echo Checking Identity Columns in BLOODCONNECT Database...
echo.

sqlcmd -S localhost\SQLEXPRESS -E -i "Database\CheckIdentityColumns.sql"

echo.
echo Check completed. If any IsIdentity column shows 0 (false), the table needs to be recreated.
echo.
pause
