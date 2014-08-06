using System.Diagnostics;

var config = Require<FitterBuilder>().Build(new {
  RootDir = Directory.GetCurrentDirectory(),
  DebugDir = @"<rootdir>\debug",
  SourceDir = @"<rootdir>\src",
  ThirdpartyDir = @"<rootdir>\thirdparty",
  PackagesDir = @"<thirdpartydir>\packages",
  NugetExePath = @"<thirdpartydir>\nuget\nuget.exe",
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

void Echo(string message) {
  Console.WriteLine("");
  Console.WriteLine(message);
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

void ProcessCommands() {
  var exiting = false;
  
  while (!exiting) {
    Console.WriteLine("");
    Console.Write("Waiting: ");
    
    try {    
      var commands = Console
        .ReadLine()
        .Split(',')
        .Select(s => s.Trim());
        
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
          default: 
            Echo("Unknown Command");
            break;
        }
      }
    } catch (Exception e) {
      Console.WriteLine("");
      Console.WriteLine(e);
    }
  }
}

ProcessCommands();
