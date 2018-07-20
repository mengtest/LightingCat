@echo off 

set srcPath=%cd%\
 
set distGoPath=%srcPath%..\Server\src\usercmd
 
set binPath=%srcPath%\bin

set target = %srcPath%..\Proto\MyDemo.proto
 
%binPath%\protoc --gogofaster_out=%distGoPath% %target%
echo "ok"
pause