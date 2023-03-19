#!/bin/bash

cd /Users/tsunenorimotomura/GitHub/AtCoder

# コンテストフォルダ作成
mkdir AtCoderBeginnerContestXXX
cd AtCoderBeginnerContestXXX

dotnet new sln --name KyoPro

# 問題名の定義
problemNames=("A" "B" "C" "D" "E")

# 問題ごとに処理を繰り返す
for i in {0..4}; do
    problemName=${problemNames[$i]}

    # 解答用プロジェクトを作成
    mkdir $problemName
    cd $problemName
    dotnet new console
    cp ../../_tmp/KyoPro/Program.cs .
    cd ..
    dotnet sln add $problemName

    # テスト用プロジェクトを作成
    mkdir ${problemName}Test
    cd ${problemName}Test
    dotnet new mstest
    dotnet add reference ../$problemName
    cp ../../_tmp/Test/UnitTest1.cs .
    mkdir SampleInOut
    cd ..

    dotnet sln add ${problemName}Test
done