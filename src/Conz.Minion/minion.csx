using System.Diagnostics;
using Conz.Core;

var conzole = new Conzole(Conz.Core.BuiltInStyles.ForegroundColorOnly._Instance);

var config = Require<FitterBuilder>().Build(new {
  RootDir = Directory.GetCurrentDirectory(),
  DebugDir = @"<rootdir>\debug",
  SourceDir = @"<rootdir>\src",
  ThirdpartyDir = @"<rootdir>\thirdparty",
  PackagesDir = @"<thirdpartydir>\packages",
  NugetExePath = @"<thirdpartydir>\nuget\nuget.exe",
  NugetWorkingDir = @"<rootdir>\nugetworking",
  NunitConsoleExePath = @"<thirdpartydir>\packages\common\Nunit.Runners\tools\nunit-console.exe"
});
  
void TryDelete(string dir) {
  if (Directory.Exists(dir))
    Directory.Delete(dir, true);
}

void Clean() {
  TryDelete(config["DebugDir"]);
}

void CleanAll() {
  Clean();
  TryDelete(config["PackagesDir"]);
}

void Echo(string message, params string[] args) {
  conzole.WriteLine("");
  conzole.WriteLine(message, args);
}

void Bootstrap() {
  Run(config["NugetExePath"], @"install .\src\Conz.Nuget.Packages\common\packages.config -OutputDirectory .\thirdparty\packages\common -ExcludeVersion");
  Run(config["NugetExePath"], @"install .\src\Conz.Nuget.Packages\net-4.5\packages.config -OutputDirectory .\thirdparty\packages\net-4.5 -ExcludeVersion");
}

void Run(string exePath, string args) {
  var info = new ProcessStartInfo {
    FileName = exePath,
    Arguments = args,
    UseShellExecute = false
  };
  
  using (var p = Process.Start(info)) {
    p.WaitForExit();                               
  }
}

void BuildAll() {
  Run(@"C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe", @".\src\Conz.Build\Conz.proj /ds /maxcpucount:6");
}

void BuildNugetPackages() {
  TryDelete(config["NugetWorkingDir"]);
  
  var dir = Path.Combine(config["NugetWorkingDir"], @"Conz.Core\lib\net45");
  Directory.CreateDirectory(dir);
  File.Copy(Path.Combine(config["DebugDir"], @"net-4.5\Conz.Core\Conz.Core.dll"),
            Path.Combine(dir, "Conz.Core.dll")); 
  File.Copy(Path.Combine(config["SourceDir"], @"Conz.Nuget.Specs\Conz.Core.dll.nuspec"),
            Path.Combine(config["NugetWorkingDir"], @"Conz.Core\Conz.Core.dll.nuspec"));
  Run(config["NugetExePath"], @"pack .\nugetworking\Conz.Core\Conz.Core.dll.nuspec -OutputDirectory .\nugetworking\Conz.Core");
}

void RunTests(string name, string assembly, string framework) {
  conzole.WriteLine("|dc|------------------------------------------|");
  conzole.WriteLine("|y|----------- {0} Tests {1} -----------|", name, framework);
  conzole.WriteLine("|y|-- {0}|", assembly);
  Run(config["NunitConsoleExePath"], string.Format(@"{0} /nologo /framework:{1}", assembly, framework));
  conzole.WriteLine("|dc|------------------------------------------");
}

void RunUnitTestsVS() {
  RunTests("VS Unit", @".\src\Conz.UnitTests\bin\debug\Conz.UnitTests.dll", "net-4.5");
}

void RunUnitTestsDebug() {
  RunTests("Debug Unit", Path.Combine(config["DebugDir"], @"net-4.5\Conz.UnitTests\Conz.UnitTests.dll"), "net-4.5");
}

void RunAllTests() {
  RunUnitTestsVS();
  RunUnitTestsDebug();
}

void PushNugetPackages() {
  conzole.WriteLine("|dc|------------------------------------------|");
  conzole.WriteLine("|y|Push Nuget Packages!!|");
  conzole.WriteLine("|y|Are You Sure?|  Enter YES to Continue");
  if (conzole.ReadLine() == "YES") {
    Run(config["NugetExePath"], @"push .\nugetworking\Conz.Core\Conz.Core.0.0.0.3.nupkg");
  }
  else 
    conzole.WriteLine("|r|Operation Cancelled...|");
  conzole.WriteLine("|dc|------------------------------------------|");
}

string[] GetCommands(string[] commands) {
  return ((commands != null) && commands.Any())
    ? commands 
    : conzole
      .ReadLine()
      .Split(',')
      .Select(s => s.Trim())
      .ToArray();
}

void ProcessCommands(params string[] commands) {
  var exiting = false;
  
  while (!exiting) {
    conzole.WriteLine("");
    conzole.Write("Waiting: ");
    
    try {    
      commands = GetCommands(commands); 
        
      foreach (var command in commands) {
        switch (command) {
          case ("exit"):
          case ("x"):
            exiting = true;
            Echo("Goodbye");
            break;
          case ("clean"):
            Clean();
            break;
          case ("clean.all"):
            CleanAll();
            break;
          case ("bootstrap"):
            Bootstrap();
            break;
          case ("build.all"):
            BuildAll();
            break;  
          case ("build.nuget.packages"):
            BuildNugetPackages();
            break;  
          case ("run.unit.tests.vs"):
            RunUnitTestsVS();
            break;  
          case ("run.unit.tests.debug"):
            RunUnitTestsDebug();
            break;           
          case ("run.all.tests"):
            RunAllTests();
            break;
          case ("push.nuget.packages"):
            PushNugetPackages();
            break;
          default: 
            Echo("|r|Unknown Command: '{0}'|", command);
            break;
        }
      }
    } catch (Exception e) {
      conzole.WriteLine("");
      conzole.WriteLine("|r|{0}|", e);
    }
    commands = null;
  }
}

ProcessCommands(Env.ScriptArgs.ToArray());  