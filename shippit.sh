rm -rf ./pub-linux
rm -rf ./pub-windows
rm -rf ./pub-mac
dotnet publish ./src/ROTRBIFOS/ROTRBIFOS.vbproj -o ./pub-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/ROTRBIFOS/ROTRBIFOS.vbproj -o ./pub-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/ROTRBIFOS/ROTRBIFOS.vbproj -o ./pub-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
rm ./pub-linux/*.pdb
rm ./pub-windows/*.pdb
rm ./pub-mac/*.pdb
butler push pub-windows thegrumpygamedev/rotrbif-of-splorr:windows
butler push pub-linux thegrumpygamedev/rotrbif-of-splorr:linux
butler push pub-mac thegrumpygamedev/rotrbif-of-splorr:mac
