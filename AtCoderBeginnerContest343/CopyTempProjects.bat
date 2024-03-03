rem 解答用プロジェクトを作成
mkdir %1
cd %1
dotnet new console
copy ..\E\Program.cs
cd ..
dotnet sln add %1

rem テスト用プロジェクトを作成
mkdir %1Test
cd %1Test
dotnet new mstest
dotnet add reference ../%1
copy ..\ETest\UnitTest1.cs
mkdir SampleInOut
cd ..

dotnet sln add %1Test