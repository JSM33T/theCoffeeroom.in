@echo off
set "exclude_folders=wwwroot\assets\vendor wwwroot\assets\img"

for /f %%x in ('powershell -command "Get-Date -Format 'yyyy-MM-dd'"') do set "current_date=%%x"
set "commit_message=Automated update - %current_date%"

for %%f in (%exclude_folders%) do (
    git rm --cached -r "%%f"
)

git commit -m "%commit_message%"
git push origin main
pause
