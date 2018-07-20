@echo off 

set srcPath=%cd%

cd %cd%\..

set distGoPath=Server\src\usercmd
 
set binPath=Tools\bin
 
%binPath%\protoc --gogofaster_out=%distGoPath% Proto\MyDemo.proto
echo "ok"
pause