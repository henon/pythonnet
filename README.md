# pythonnet_netstandard - Python for .NET Standard

This fork of the original [pythonnet/pythonnet](https://github.com/pythonnet/pythonnet) targets the portable .NET Standard instead of the .NET Framework. This readme contains only infos specific to pythonnet_netstandard. For the original documentation please check out their readme and wiki. 

## Nuget

We release pythonnet (Python.Runtime.dll) on Nuget for different Python versions (2.7, 3.5, 3.6, 3.7, 3.8) and operating systems (win, linux, osx) and .net frameworks (net40 and netstandard2.0). All these different parameters affect the assembly via conditional build variables in pythonnet, so for every combination of parameters we compile and release an assembly on nuget:

### Targeting netstandard2.0
* Nuget Package Name: **pythonnet_netstandard_py(27|35|36|37|38)_(win|linux|osx)**
* Assembly Name: Python.Runtime.dll

So if you want python3.7 on windows for .NET Standard you should get the package `pythonnet_netstandard_py37_win`

### Targeting net40
* Nuget Package Name: **pythonnet_py(27|35|36|37|38)_(win|linux|osx)**
* Assembly Name: Python.Runtime.dll

So if you want python3.7 on windows for .NET Framework you should get the package `pythonnet_py37_win`

### Historic package names on Nuget
Older packages were named differently and the assembly name was different too. The package version reflected the python version i.e. v3.5.0 -> python3.5

* Old package names: Python.Runtime.NetStandard, Python.Runtime.Mono, Python.Runtime.OSX
* Old assembly name: Python.Runtime.NetStandard.dll

Please switch to the newer `pythonnet` and `pythonnet_netstandard` packages above.

## Projects using Python.Runtime.NetStandard
* [Python.Included](https://github.com/henon/Python.Included)
* [Numpy.NET](https://github.com/SciSharp/Numpy.NET)
* [Keras.NET](https://github.com/SciSharp/Keras.NET)
* [Torch.NET](https://github.com/SciSharp/Torch.NET)
