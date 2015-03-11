@echo off
pushd %~dp0

set ShouldPublish=""
set NugetKey=""
if "%1"=="publish" set ShouldPublish="nuget"
if not "%2"=="" set NugetKey=%2

if %ShouldPublish%=="nuget" echo Publish to NuGet enabled

if exist Build goto Build
mkdir Build

:Build
%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\msbuild Build.proj /m /nr:false /v:M /fl /p:ShouldPublish=%ShouldPublish%;NugetKey=%NugetKey%
if errorlevel 1 goto BuildFail
goto BuildSuccess

:BuildFail
echo.
echo *** BUILD FAILED ***
goto End

:BuildSuccess
echo.
echo **** BUILD SUCCESSFUL ***
goto end

:End
popd

pause