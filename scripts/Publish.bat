dotnet publish ..\Ab1Analyzer.sln -c Release --self-contained true -p:PublishSingleFile=true -r win-x64
dotnet publish ..\Ab1Analyzer.sln -c Release --self-contained true -p:PublishSingleFile=true -r linux-x64
dotnet publish ..\Ab1Analyzer.sln -c Release --self-contained true -p:PublishSingleFile=true -r osx-x64
