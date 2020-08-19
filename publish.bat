set dir="Publish"
rmdir /s /q %dir%
mkdir %dir%

FOR /L %%A IN (1,1,4) DO (
	dotnet publish /p:Configuration=Release SHPA.Blockchain/SHPA.Blockchain.csproj -c Release --force -o %dir%/Node_%%A
	powershell -Command "(gc %dir%/Node_%%A/appsettings.json) -replace 'nd-one', 'nd-%%A' -replace '5000', '500%%A' | Out-File -encoding ASCII %dir%/Node_%%A/appsettings.json"
	start ./%dir%/Node_%%A/SHPA.Blockchain.exe
)