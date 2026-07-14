$project = "RedisAnalyzer/RedisAnalyzer.csproj"
$output = "dist"

# Clean
Remove-Item -Recurse -Force $output -ErrorAction Ignore

# Windows
dotnet publish $project -c Release -r win-x64 --self-contained true /p:PublishSingleFile=true -o "$output/win"

# Linux
dotnet publish $project -c Release -r linux-x64 --self-contained true /p:PublishSingleFile=true -o "$output/linux"

# macOS
dotnet publish $project -c Release -r osx-x64 --self-contained true /p:PublishSingleFile=true -o "$output/mac"

Write-Host "Build completed!"

Compress-Archive -Path dist/win/* -DestinationPath redis-analyzer-win.zip
Compress-Archive -Path dist/linux/* -DestinationPath redis-analyzer-linux.zip
Compress-Archive -Path dist/mac/* -DestinationPath redis-analyzer-mac.zip

Write-Host "DONE!"