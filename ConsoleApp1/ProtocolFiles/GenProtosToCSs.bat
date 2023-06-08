@echo off

set "PROTOC_EXE=%cd%\ProtocTool\protoc.exe"
set "WORK_DIR=%cd%\Protocols"
set "CS_OUT_PATH=%cd%\CSProtocols"
::if not exist %CS_OUT_PATH% md %CS_OUT_PATH%

for /f "delims=" %%i in ('dir /b Protocols "Protocols/*.proto"') do (
   echo gen Protocols/%%i...
   %PROTOC_EXE%  --proto_path="%WORK_DIR%" --csharp_out="%CS_OUT_PATH%" "%WORK_DIR%\%%i"
   )
echo finish... 

pause
