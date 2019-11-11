@echo off

jumplist %1 > .jmp
SET /P CD_TARGET= < .jmp
del .jmp
CD /D %CD_TARGET%
echo %CD_TARGET%
SET CD_TARGET=