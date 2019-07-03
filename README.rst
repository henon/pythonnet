pythonnet - Python for .NET Standard
===========================

This fork of the original [pythonnet/pythonnet](https://github.com/pythonnet/pythonnet) targets the portable .NET Standard instead of the .NET Framework. This readme contains only infos specific to pythonnet_netstandard. For the original documentation please check out their readme and wiki. 

## Python.Rundime.NetStandard[.Mono|.OSX]

We release Python.Rundime.NetStandard.dll on Nuget for every Python version that is required by Projects depending on it. Note that the first two digits of the version is the Python version. Currently the following are available on Nuget: 

### Targeting Python 3.5
* [Python.Rundime.NetStandard](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.5.0) v3.5 (Windows)

### Targeting Python 3.6
* [Python.Rundime.NetStandard](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.6.0) v3.6 (Windows)
* [Python.Rundime.Mono](https://www.nuget.org/packages/Python.Runtime.Mono/) v3.6 (Linux) 
* [Python.Rundime.OSX.dll](https://www.nuget.org/packages/Python.Runtime.OSX/3.6.0) v3.6 (MacOS X)

### Targeting Python 3.7
* [Python.Rundime.NetStandard.dll](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.7.0) v3.7 (Windows)
* [Python.Rundime.OSX.dll](https://www.nuget.org/packages/Python.Runtime.OSX/3.7.0) v3.7 (MacOS X)


## Projects using Python.Runtime.NetStandard
* [Python.Included](https://github.com/henon/Python.Included)
* [Numpy.NET](https://github.com/SciSharp/Numpy.NET)
* [Keras.NET](https://github.com/SciSharp/Keras.NET)
* [Torch.NET](https://github.com/SciSharp/Torch.NET)
