SET BUILDTOOL="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"

REM Restore NuGet packages first
dotnet restore src\csmatio.csproj

REM Build for specific framework targets
dotnet build src\csmatio.csproj -c Release -f netstandard2.0
dotnet build src\csmatio.csproj -c Release -f netstandard2.1
dotnet build src\csmatio.csproj -c Release -f net6.0
dotnet build src\csmatio.csproj -c Release -f net8.0

REM Create NuGet package with all frameworks
dotnet pack src\csmatio.csproj -c Release --no-build

echo Press any key to continue...
pause > nul
