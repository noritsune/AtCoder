@echo off

setlocal enabledelayedexpansion

rem コンテストフォルダ作成
mkdir AtCoderBeginnerContestXXX
cd AtCoderBeginnerContestXXX

dotnet new sln --name KyoPro

rem 問題名の定義
set problemNames[0]=A
set problemNames[1]=B
set problemNames[2]=C
set problemNames[3]=D
set problemNames[4]=E

rem 問題ごとに処理を繰り返す
for /l %%i in (0, 1, 4) do (
	set problemName=!problemNames[%%i]!
	
	rem 解答用プロジェクトを作成
	mkdir !problemName!
	cd !problemName!
	dotnet new console
	copy ..\..\_tmp\KyoPro\Program.cs
	cd ..
	dotnet sln add !problemName!

	rem テスト用プロジェクトを作成
	mkdir !problemName!Test
	cd !problemName!Test
	dotnet new mstest
	dotnet add reference ../!problemName!
	copy ..\..\_tmp\Test\UnitTest1.cs
	mkdir SampleInOut
	cd ..

	dotnet sln add !problemName!Test
)

@REM rem Rider起動とソリューション読み込みまで済ませる。しかしcmdを閉じるとRiderが閉じる
rider KyoPro.sln

@REM rem Rider起動までを行う。ソリューション読み込みは手動でやる
@REM cd D:\Program Files\JetBrains\JetBrains Rider 2021.1.3\bin
@REM rider64.exe

endlocal
