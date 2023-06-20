@echo off

setlocal enabledelayedexpansion

echo Input problem name.
set /p problemName=

rem 回答用プロジェクトを作成
mkdir %problemName%
cd %problemName%
dotnet new console
copy ..\..\_tmp\KyoPro\Program.cs
cd ..
dotnet sln add %problemName%

rem テスト用プロジェクトを作成
mkdir %problemName%Test
cd %problemName%Test
dotnet new mstest
dotnet add reference ../%problemName%
copy ..\..\_tmp\Test\UnitTest1.cs
mkdir SampleInOut
cd ..

dotnet sln add %problemName%Test

endlocal
