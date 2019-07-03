# pythonnet - Python for .NET Standard

This fork of the original [pythonnet/pythonnet](https://github.com/pythonnet/pythonnet) targets the portable .NET Standard instead of the .NET Framework. This readme contains only infos specific to pythonnet_netstandard. For the original documentation please check out their readme and wiki. 

## Python.Runtime.NetStandard.dll on Nuget

We release Python.Rundime.NetStandard.dll on Nuget for different Python versions and Operating Systems. Note that the first two digits of the version is the Python version and not the pythonnet version. Currently the following are available on Nuget: 

### Targeting Python 3.5
* [Python.Rundime.NetStandard](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.5.0) (Windows)

### Targeting Python 3.6
* [Python.Rundime.NetStandard](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.6.0) (Windows)
* [Python.Rundime.Mono](https://www.nuget.org/packages/Python.Runtime.Mono/) (Linux) 
* [Python.Rundime.OSX](https://www.nuget.org/packages/Python.Runtime.OSX/3.6.0) (MacOS X)

### Targeting Python 3.7
* [Python.Rundime.NetStandard](https://www.nuget.org/packages/Python.Runtime.NETStandard/3.7.0) (Windows)
* [Python.Rundime.OSX](https://www.nuget.org/packages/Python.Runtime.OSX/3.7.0) (MacOS X)


## Projects using Python.Runtime.NetStandard
* [Python.Included](https://github.com/henon/Python.Included)
* [Numpy.NET](https://github.com/SciSharp/Numpy.NET)
* [Keras.NET](https://github.com/SciSharp/Keras.NET)
* [Torch.NET](https://github.com/SciSharp/Torch.NET)
