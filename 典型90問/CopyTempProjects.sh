# 解答用プロジェクトを作成
mkdir $1
cd $1
dotnet new console
cp ../tmp/Program.cs Program.cs
cd ..
dotnet sln add $1

# テスト用プロジェクトを作成
mkdir $1Test
cd $1Test
dotnet new mstest
dotnet add reference ../$1
cp ../tmpTest/UnitTest1.cs UnitTest1.cs
mkdir SampleInOut
cd ..

dotnet sln add $1Test