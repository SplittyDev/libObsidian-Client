# libObsidian-Client
Obsidian client library for Microsoft .Net and Mono

## What's Obsidian?
Obsidian is a new screencap- and file-sharing service.  
The API is currently under construction.

## How to build this library
First, you'll need to clone the repository.   
This is the same for Microsoft Windows, MacOS X, Linux and other Unixoids:
```
$ git clone https://github.com/SplittyDev/libObsidian-Client.git
```
Now you'll probably want to build the release configuration of the client library.

### Microsoft Windows (.Net)
Use MSBuild:
```
$ cd libObsidian-Client\src
$ msbuild Obsidian.sln /p:Configuration=Release
```

### MacOS X, Linux and other Unixoids (Mono)
Use xbuild:
```
$ cd libObsidian-Client/src
$ xbuild /p:Configuration=Release Obsidian.sln
```
