@echo off
pushd "%~dp0"
setlocal
set ARGS=%*
if "%~1"=="" SET ARGS=Benchmarks
dotnet run -c Release --project tests\Benchmarks\ParseFive.Benchmarks.csproj -- %ARGS%
popd
