@echo off

FOR /F "tokens=*" %%a in ('jumplist %1') do SET JUMP_PATH=%%a
CD /D %JUMP_PATH%
SET JUMP_PATH=