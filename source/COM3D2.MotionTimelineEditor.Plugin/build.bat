@echo off
setlocal enabledelayedexpansion

cd /d %~dp0

set PLUGIN_NAME=COM3D2.MotionTimelineEditor.Plugin
set CSC_PATH="C:\Windows\Microsoft.NET\Framework\v3.5\csc"
set LIB_PATHS="/lib:..\..\..\..\COM3D2x64_Data\Managed" "/lib:..\..\.." "/lib:..\..\..\lib" "/lib:..\..\..\UnityInjector"
set REFERENCES="/r:UnityEngine.dll" "/r:UnityInjector.dll" "/r:Assembly-CSharp.dll" "/r:Assembly-CSharp-firstpass.dll" "/r:Assembly-UnityScript-firstpass.dll" "/r:COM3D2.SceneCapture.Plugin.dll"
set RESOURCES="/resource:..\..\UnityProject\Assets\Bundles\mte_bundle"
set SOURCE_DIR=%~dp0
set SOURCE_DIR2=%~dp0\..\..\UnityProject\Assets\Scripts
set MAIN_FILE=%SOURCE_DIR%%PLUGIN_NAME%.cs

set DEBUG_OPTION=
set DEFINES="/define:COM3D2"
IF "%~1"=="debug" (
    set DEBUG_OPTION="/debug+"
    set DEFINES=!DEFINES! "/define:DEBUG"
)

set SOURCES="%MAIN_FILE%"

for /R %SOURCE_DIR% %%f in (*.cs) do (
    echo Add: %%f
    set "SOURCE_FILE=%%f"
    if not "%%f"=="%MAIN_FILE%" set SOURCES=!SOURCES! "!SOURCE_FILE:%SOURCE_DIR%=.\!"
)

for /R %SOURCE_DIR2% %%f in (*.cs) do (
    echo Add: %%f
    set "SOURCE_FILE=%%f"
    set SOURCES=!SOURCES! "!SOURCE_FILE:%SOURCE_DIR%=.\!"
)

echo %CSC_PATH% /t:library %LIB_PATHS% %REFERENCES% %DEBUG_OPTION% %DEFINES% %SOURCES% %RESOURCES%
%CSC_PATH% /t:library %LIB_PATHS% %REFERENCES% %DEBUG_OPTION% %DEFINES% %SOURCES% %RESOURCES%
if %ERRORLEVEL% neq 0 (
    echo Failed build
    exit /b 1
)

copy *.dll ..\..\..\UnityInjector
if %ERRORLEVEL% neq 0 (
    echo Failed to copy dlls
    exit /b 1
)

move *.dll ..\..\UnityInjector
del *.pdb
