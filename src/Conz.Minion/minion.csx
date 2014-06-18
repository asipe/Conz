using System.Diagnostics;

var root = Directory.GetCurrentDirectory();
var thirdparty = Path.Combine(root, "thirdparty");
var config =  new {
    Paths = new {
      RootDir = root,
      DebugDir = Path.Combine(root, "debug"),
      ThirdpartyDir = thirdparty,
      PackagesDir = Path.Combine(thirdparty, "packages"),
      NugetExePath = Path.Combine(thirdparty, @"nuget\nuget.exe"),
    }
  };
  
void TryDelete(string dir) {
  if (Directory.Exists(dir))
    Directory.Delete(dir, true);
}

void Clean() {
  TryDelete(config.Paths.DebugDir);
}

void CleanAll() {
  Clean();
  TryDelete(config.Paths.PackagesDir);
}

void Echo(string message) {
  Console.WriteLine("");
  Console.WriteLine(message);
}

void Bootstrap() {
  Echo("NOT YET IMPLEMENTED");
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
