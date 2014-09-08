Push-Location -Path .\src\Conz.Minion
scriptcs -install
Pop-Location
scriptcs .\src\Conz.Minion\minion.csx -- clean.all,bootstrap,build.all,run.unit.tests.debug,x
