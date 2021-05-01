using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace release_bot
{
    class Program
    {
        private const string pythonnet_version = "2.5.1"; // <--- pythonnet version


        private static int PackageVersion = 1; // NOTE: only if you need to fix a release increase > 0. reset to 0 if pythonnet version increases!
        public static string V => PackageVersion == 0 ? pythonnet_version : $"{pythonnet_version}.{PackageVersion}";

        private const string Package = "pythonnet";
        private const string PackageNetstd = "pythonnet_netstandard";
        private const string ProjectPath = "../../../../../src/runtime";
        private const string ProjectName = "Python.Runtime.15.csproj";
        private const string ProjectNameNetStandard = "Python.Runtime.15.netstandard.csproj";
        private const string Description = "Python and CLR (.NET and Mono) cross-platform language interop.";
        private const string Tags = "Python, pythonnet, interop, dlr";
        private const string net = " Compiled against .Net 4.0 and CPython ";
        private const string netstd = " Compiled against .NetStandard 2.0 and CPython ";
        private const string COMMON = ";FINALIZER_CHECK;XPLAT";
        private const string MALLOC = ";PYTHON_WITH_PYMALLOC";
        private const string NETSTD = ";NETSTANDARD";

        static void Main(string[] args)
        {
            var specs = new ReleaseSpec[]
            {
                // ====== .NET Framework 4.0 =======
                // linux                
                new ReleaseSpec() { Version = V, Description = Description + net + "2.7 (linux)", PackageId = Package+"_py27_linux", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS4;MONO_LINUX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.5 (linux)", PackageId = Package+"_py35_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS4;MONO_LINUX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.6 (linux)", PackageId = Package+"_py36_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS4;MONO_LINUX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.7 (linux)", PackageId = Package+"_py37_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS4;MONO_LINUX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.8 (linux)", PackageId = Package+"_py38_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS4;MONO_LINUX"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                // mac
                new ReleaseSpec() { Version = V, Description = Description + net + "2.7 (osx)", PackageId = Package+"_py27_osx", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS2;MONO_OSX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.5 (osx)", PackageId = Package+"_py35_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS2;MONO_OSX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.6 (osx)", PackageId = Package+"_py36_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS2;MONO_OSX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.7 (osx)", PackageId = Package+"_py37_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS2;MONO_OSX"+MALLOC+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40"},
                new ReleaseSpec() { Version = V, Description = Description + net + "3.8 (osx)", PackageId = Package+"_py38_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS2;MONO_OSX"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                // win
                new ReleaseSpec() { Version = V, Description = Description + net + "2.7 (win64)", PackageId = Package+"_py27_win", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS2"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.5 (win64)", PackageId = Package+"_py35_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS2"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.6 (win64)", PackageId = Package+"_py36_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS2"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.7 (win64)", PackageId = Package+"_py37_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS2"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },
                new ReleaseSpec() { Version = V, Description = Description + net + "3.8 (win64)", PackageId = Package+"_py38_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS2"+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectName, Framework="net40" },

                // ====== .NET Standard 2.0 =======
                // linux
                new ReleaseSpec() { Version = V, Description = Description + netstd + "2.7 (linux)", PackageId = PackageNetstd+"_py27_linux", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS4;MONO_LINUX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.5 (linux)", PackageId = PackageNetstd+"_py35_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS4;MONO_LINUX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.6 (linux)", PackageId = PackageNetstd+"_py36_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS4;MONO_LINUX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.7 (linux)", PackageId = PackageNetstd+"_py37_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS4;MONO_LINUX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.8 (linux)", PackageId = PackageNetstd+"_py38_linux", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS4;MONO_LINUX"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                // mac
                new ReleaseSpec() { Version = V, Description = Description + netstd + "2.7 (osx)", PackageId = PackageNetstd+"_py27_osx", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS2;MONO_OSX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.5 (osx)", PackageId = PackageNetstd+"_py35_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS2;MONO_OSX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.6 (osx)", PackageId = PackageNetstd+"_py36_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS2;MONO_OSX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.7 (osx)", PackageId = PackageNetstd+"_py37_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS2;MONO_OSX"+MALLOC+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.8 (osx)", PackageId = PackageNetstd+"_py38_osx", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS2;MONO_OSX"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                // win
                new ReleaseSpec() { Version = V, Description = Description + netstd + "2.7 (win64)", PackageId = PackageNetstd+"_py27_win", PackageTags = Tags, Constants = "PYTHON2;PYTHON27;UCS2"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.5 (win64)", PackageId = PackageNetstd+"_py35_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON35;UCS2"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.6 (win64)", PackageId = PackageNetstd+"_py36_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON36;UCS2"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.7 (win64)", PackageId = PackageNetstd+"_py37_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON37;UCS2"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },
                new ReleaseSpec() { Version = V, Description = Description + netstd + "3.8 (win64)", PackageId = PackageNetstd+"_py38_win", PackageTags = Tags, Constants = "PYTHON3;PYTHON38;UCS2"+NETSTD+COMMON, RelativeProjectPath = ProjectPath, ProjectName = ProjectNameNetStandard, Framework="netstandard2.0" },

            };
            foreach (var platform in new[] {"x86", "x64"})
            {
                foreach (var spec in specs)
                {
                    spec.Platform = platform;
                    //try
                    {
                        // ======================
                        //   build 
                        // ======================
                        Console.WriteLine("Build " + Description);
                        var dir = Directory.GetCurrentDirectory();
                        var proj_path = Path.GetFullPath(Path.Combine(dir, spec.RelativeProjectPath));
                        var out_path = Path.GetFullPath(Path.Combine(proj_path, $@"bin\{spec.PackageId}\"));
                        var spec_path = Path.Combine(out_path, spec.PackageId + ".nuspec");
                        if (!File.Exists(Path.Combine(proj_path, spec.ProjectName)))
                            throw new InvalidOperationException("Path to project not right: " + proj_path);
                        // dotnet msbuild -target:Rebuild -property:OutDir=.\bin\win_py27\;Configuration=ReleaseWin;Platform=x64 Python.Runtime.csproj
                        // 
                        var build =
                            $@"msbuild -target:Rebuild -restore -property:OutDir=.\bin\{spec.PackageId}\;Configuration=Release;Platform={platform};DefineConstants=\""{spec.Constants}\"" {spec.ProjectName}";
                        var p = new Process()
                            {StartInfo = new ProcessStartInfo("dotnet", build) {WorkingDirectory = proj_path,}};
                        p.Start();
                        p.WaitForExit();

                        // ======================
                        //   nuget pack
                        // ======================
                        File.WriteAllText(spec_path, $@"<?xml version=""1.0""?>
<package>
  <metadata>
    <id>{spec.PackageId + (spec.Is64Bit ? "" : "_x86")}</id>
    <version>{spec.Version}</version>
    <authors>pythonnet</authors>
    <owners>pythonnet</owners>
    <licenseUrl>https://github.com/pythonnet/pythonnet/blob/master/LICENSE</licenseUrl>
    <projectUrl>https://github.com/pythonnet/pythonnet</projectUrl>
    <iconUrl>https://api.nuget.org/v3-flatcontainer/pythonnet/2.3.0-py35-dotnet/icon</iconUrl>
    <requireLicenseAcceptance>false</requireLicenseAcceptance>
    <description>{spec.Description}</description>
    <releaseNotes>Packaged for Nuget by Henon aka Meinrad Recheis. Please get the release notes from the project site</releaseNotes>
    <copyright>Copyright (c) 2006-2020 the contributors of the 'Python for .NET' project</copyright>
    <tags>python interop dynamic dlr Mono pinvoke</tags>
  </metadata>
  <files>
    <file src=""Python.Runtime.dll"" target =""lib\{spec.Framework}\"" />
    <file src=""Python.Runtime.xml"" target = ""lib\{spec.Framework}\"" />
  </files>
</package>");
                        var pack = $@"pack {spec.PackageId}.nuspec";
                        p = new Process()
                            {StartInfo = new ProcessStartInfo("nuget", pack) {WorkingDirectory = out_path,}};
                        p.Start();
                        p.WaitForExit();

                    }
                    //catch (Exception e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //    Console.WriteLine(e.StackTrace);
                    //}
                }
            }

            // ======================
            //  publish nuget packages
            // ======================
            var key = File.ReadAllText("../../../nuget.key").Trim();
            var packages = Directory.GetFiles(Path.Combine(ProjectPath, "bin"), $"*.{V}.nupkg",
                SearchOption.AllDirectories);
            foreach (var nuget in packages)
            {
                Console.WriteLine("Push " + nuget);
                var push = $"push -Source https://api.nuget.org/v3/index.json -ApiKey {key} {nuget}";
                var p = new Process() { StartInfo = new ProcessStartInfo("nuget", push) { RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false } };
                p.OutputDataReceived += (x, data) => Console.WriteLine(data.Data);
                p.ErrorDataReceived += (x, data) => Console.WriteLine("Error: " + data.Data);
                p.Start();
                p.WaitForExit();
                Console.WriteLine("... pushed");
            }
            Thread.Sleep(3000);
        }
    }

    public class ReleaseSpec
    {
        /// <summary>
        /// The assembly / nuget package version
        /// </summary>
        public string Version;

        /// <summary>
        /// Project description
        /// </summary>
        public string Description;

        /// <summary>
        /// Project description
        /// </summary>
        public string PackageTags;

        /// <summary>
        /// Nuget package id
        /// </summary>
        public string PackageId;

        /// <summary>
        /// Project description
        /// </summary>
        public string Constants;

        /// <summary>
        /// Name of the csproj file
        /// </summary>
        public string ProjectName;

        /// <summary>
        /// Path to the csproj file, relative to the execution directory of ReleaseBot
        /// </summary>
        public string RelativeProjectPath;

        public string FullProjectPath => Path.Combine(RelativeProjectPath, ProjectName);

        /// <summary>
        /// The framework identifyer net40 or netstandard2.0
        /// </summary>
        public string Framework;

        public string Platform { get; set; } = "x64";

        public bool Is64Bit => Platform=="x64";
    }
}
