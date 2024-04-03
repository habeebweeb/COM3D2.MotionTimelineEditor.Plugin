@echo off
setlocal enabledelayedexpansion
set CSC_PATH="C:\Windows\Microsoft.NET\Framework\v3.5\csc"
set LIB_PATHS="/lib:..\..\COM3D2x64_Data\Managed" "/lib:.." "/lib:..\lib"
set REFERENCES="/r:UnityEngine.dll" "/r:UnityInjector.dll" "/r:Assembly-CSharp.dll" "/r:Assembly-CSharp-firstpass.dll" "/r:ExIni.dll"
set SOURCE_DIR=source
set MAIN_FILE=%SOURCE_DIR%\COM3D2.MotionTimelineEditor.Plugin.cs

set DEBUG_OPTION=
IF "%~1"=="debug" (
    set DEBUG_OPTION="/debug+"
    set DEBUG_OPTION=!DEBUG_OPTION! /define:DEBUG
)

set SOURCES="%MAIN_FILE%"

for %%f in (%SOURCE_DIR%\*.cs) do (
    echo Add: %%f
    if not "%%f"=="%MAIN_FILE%" set SOURCES=!SOURCES! "%%f"
)

echo %CSC_PATH% /t:library %LIB_PATHS% %REFERENCES% %DEBUG_OPTION% %SOURCES%
%CSC_PATH% /t:library %LIB_PATHS% %REFERENCES% %DEBUG_OPTION% %SOURCES%

copy *.dll ..\UnityInjector
move *.dll UnityInjector
del *.pdb
