@echo off 

set srcPath=%cd%

cd %cd%\..
 
set binPath=Tools\bin
 
%binPath%\protoGen -i:Proto\MyDemo.proto -o:Client\Assets\Scripts\usercmd\usercmd.cs
echo "ok"
pause