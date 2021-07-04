@echo off

setlocal enabledelayedexpansion

rem コンテストフォルダ作成
mkdir AtCoderBeginnerContestXXX
cd AtCoderBeginnerContestXXX

rem 問題名の定義
set problemNames[0]=A
set problemNames[1]=B
set problemNames[2]=C
set problemNames[3]=D

rem 問題ごとに処理を繰り返す
for /l %%i in (0, 1, 3) do (
	rem 問題名のフォルダを作って移動
	set currentDir=!problemNames[%%i]!
	mkdir !currentDir!
	cd !currentDir!

	rem プロジェクト初期化
	dotnet new console

	rem util.csをコピー
	copy ..\..\_tmp\util\util.cs Program.cs

	cd ..
)

rem VSCodeを開く
code .

endlocal
