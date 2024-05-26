@echo off

setlocal enabledelayedexpansion

rem コンテストフォルダ作成
mkdir AtCoderBeginnerContestXXX
cd AtCoderBeginnerContestXXX

dotnet new sln --name KyoPro

rem 問題ごとに処理を繰り返す
for %%c in (A B C D E F) do (
	rem 解答用プロジェクトを作成
	mkdir %%c
	cd %%c
	dotnet new console
	copy ..\..\_tmp\KyoPro\Program.cs
	cd ..
	dotnet sln add %%c

	rem テスト用プロジェクトを作成
	mkdir %%cTest
	cd %%cTest
	dotnet new mstest
	dotnet add reference ../%%c
	copy ..\..\_tmp\Test\UnitTest1.cs
	mkdir test
	cd ..

	dotnet sln add %%cTest
)

rem テストケースDL用のbatファイルをコピーしてくる
copy ..\DownloadTestCases.bat


@REM rem Rider起動とソリューション読み込みまで済ませる。しかしcmdを閉じるとRiderが閉じる
@REM rider KyoPro.sln

@REM rem Rider起動までを行う。ソリューション読み込みは手動でやる
@REM cd D:\Program Files\JetBrains\JetBrains Rider 2021.1.3\bin
@REM rider64.exe

endlocal
