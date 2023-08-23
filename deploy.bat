@echo off
for /f %%x in ('powershell -command "Get-Date -Format 'yyyy-MM-dd'"') do set "current_date=%%x"
set "commit_message=Automated update - %current_date%"
git add .
git commit -m "%commit_message%"
git push origin main
pause