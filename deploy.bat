dotnet build -c Release
pause
dotnet publish -c Release --no-self-contained -r win-x64 --output "c:/coffeedeploystation"
pause