dotnet publish ./src/SwarmsOfSPLORR/SwarmsOfSPLORR.vbproj -o ./pub-linux -c Release --sc -r linux-x64
dotnet publish ./src/SwarmsOfSPLORR/SwarmsOfSPLORR.vbproj -o ./pub-windows -c Release --sc -r win-x64
butler push pub-windows thegrumpygamedev/swarms-of-splorr:windows
butler push pub-linux thegrumpygamedev/swarms-of-splorr:linux
git add -A
git commit -m "shipped it!"