@echo off

setlocal enabledelayedexpansion

rem コンテストフォルダ作成
mkdir AtCoderBeginnerContestXXX
cd AtCoderBeginnerContestXXX

rem .vscodeフォルダ以下をコピーしてくる
xcopy ..\_tmp\.vscode\ .vscode\

rem 問題名の定義
set problemNames[0]=A
set problemNames[1]=B
set problemNames[2]=C
set problemNames[3]=D

rem 問題ごとに処理を繰り返す
for /l %%i in (0, 1, 3) do (
	set problemName=!problemNames[%%i]!
	
	rem 解答用プロジェクトを作成
	mkdir !problemName!
	cd !problemName!
	dotnet new console
	copy ..\..\_tmp\AtCoder\Program.cs
	cd ..

	rem テスト用プロジェクトを作成
	mkdir !problemName!Test
	cd !problemName!Test
	dotnet new mstest
	dotnet add reference ../!problemName!
	copy ..\..\_tmp\Test\UnitTest1.cs

	mkdir "SampleInOut/1"
	type nul > SampleInOut/1/In.txt
	type nul > SampleInOut/1/Out.txt

	cd ..
)

endlocal
