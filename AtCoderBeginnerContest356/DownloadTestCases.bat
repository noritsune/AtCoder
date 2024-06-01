@echo off
setlocal

set "contest=%~1"
if "%contest%"=="" (
    echo You need to specify the contest number. e.g. 021
    exit /b 1
)

for %%c in (A B C D E F) do (
    pushd "%%cTest"
    oj d -f "%%i%%e.txt" "https://atcoder.jp/contests/abc%contest%/tasks/abc%contest%_%%c"
    popd
)

echo All processes are done.
endlocal
