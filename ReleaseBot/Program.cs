using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using HtmlAgilityPack;

namespace ReleaseBot
{
    class Program
    {
        private const int V = 1; // <--- pythonnet_netstandard version!
        private const string ProjectPath = "../../../src/runtime";
        private const string ProjectName = "Python.Runtime.csproj";
        private const string Description = "Pythonnet compiled against .NetStandard 2.0 and CPython ";
        private const string Tags = "Python, pythonnet, interop";

        static void Main(string[] args)
        {
            var win_id = "Python.Runtime.NETStandard";
            var linux_id = "Python.Runtime.Mono";
            var osx_id = "Python.Runtime.OSX";
            var specs = new ReleaseSpec[]
            {
                // linux                
                new ReleaseSpec() { Version = "2.7."+V, Description = Description + "2.7 (linux)", PackageId = linux_id, PackageTags = Tags, Constants = "TRACE;PYTHON2;PYTHON27;UCS4;MONO_LINUX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.5."+V, Description = Description + "3.5 (linux)", PackageId = linux_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON35;UCS4;MONO_LINUX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.6."+V, Description = Description + "3.6 (linux)", PackageId = linux_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON36;UCS4;MONO_LINUX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.7."+V, Description = Description + "3.7 (linux)", PackageId = linux_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON37;UCS4;MONO_LINUX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                // mac
                new ReleaseSpec() { Version = "2.7."+V, Description = Description + "2.7 (osx)", PackageId = osx_id, PackageTags = Tags, Constants = "TRACE;PYTHON2;PYTHON27;UCS2;MONO_OSX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.5."+V, Description = Description + "3.5 (osx)", PackageId = osx_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON35;UCS2;MONO_OSX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.6."+V, Description = Description + "3.6 (osx)", PackageId = osx_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON36;UCS2;MONO_OSX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.7."+V, Description = Description + "3.7 (osx)", PackageId = osx_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON37;UCS2;MONO_OSX;PYTHON_WITH_PYMALLOC", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                // win
                new ReleaseSpec() { Version = "2.7."+V, Description = Description + "2.7 (win64)", PackageId = win_id, PackageTags = Tags, Constants = "TRACE;PYTHON2;PYTHON27;UCS2", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.5."+V, Description = Description + "3.5 (win64)", PackageId = win_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON35;UCS2", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.6."+V, Description = Description + "3.6 (win64)", PackageId = win_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON36;UCS2", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },
                new ReleaseSpec() { Version = "3.7."+V, Description = Description + "3.7 (win64)", PackageId = win_id, PackageTags = Tags, Constants = "TRACE;PYTHON3;PYTHON37;UCS2", RelativeProjectPath = ProjectPath, ProjectName = ProjectName },

            };
            foreach (var spec in specs)
            {
                //try
                {
                    spec.Process();
                }
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //    Console.WriteLine(e.StackTrace);
                //}
            }

            var key = File.ReadAllText("../../nuget.key").Trim();
            foreach (var nuget in Directory.GetFiles(Path.Combine(ProjectPath, "bin", "Release"), "*.nupkg"))
            {
                Console.WriteLine("Push " + nuget);
                var arg = $"push -Source https://api.nuget.org/v3/index.json -ApiKey {key} {nuget}";                
                var p = new Process() { StartInfo = new ProcessStartInfo("nuget.exe", arg) { RedirectStandardOutput = true, RedirectStandardError = true, UseShellExecute = false} };
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

        public void Process()
        {
            if (!File.Exists(FullProjectPath))
                throw new InvalidOperationException("Project not found at: "+FullProjectPath);
            // modify csproj
            var doc=new HtmlDocument(){ OptionOutputOriginalCase = true, OptionWriteEmptyNodes = true};
            doc.Load(FullProjectPath);
            var group0 = doc.DocumentNode.Descendants("propertygroup").FirstOrDefault();
            SetInnerText(group0.Element("version"), Version);
            Console.WriteLine("Version: " + group0.Element("version").InnerText);
            SetInnerText(group0.Element("description"), Description);
            Console.WriteLine("Description: " + group0.Element("description").InnerText);
            SetInnerText(group0.Element("packageid"), PackageId);
            foreach (var group in doc.DocumentNode.Descendants("propertygroup"))
            {
                var configuration = group.Attributes["condition"]?.Value;
                if (configuration==null)
                    continue;
                var is_release = configuration.Contains("Release");
                var constants = group.Element("defineconstants");
                SetInnerText(constants, (is_release ? "" : "DEBUG;") + Constants);
                Console.WriteLine("Constants: " + constants.InnerText);
            }
            doc.Save(FullProjectPath);
            // now build in release mode
            Console.WriteLine("Build " + Description);
            var p=new Process(){ StartInfo = new ProcessStartInfo("dotnet", "build -c Release") { WorkingDirectory = Path.GetFullPath(RelativeProjectPath) } };
            p.Start();
            p.WaitForExit();
        }

        private void SetInnerText(HtmlNode node, string text)
        {
            node.ReplaceChild(HtmlTextNode.CreateNode(text), node.FirstChild);
        }
    }
}
