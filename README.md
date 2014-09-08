# Conz

Conz is a micro framework for formatting console output

[![Conz.Core on NuGet](http://img.shields.io/nuget/v/Conz.Core.svg?style=flat)](https://www.nuget.org/packages/Conz.Core)
[![Conz.Core license](http://img.shields.io/badge/license-mit-blue.svg?style=flat)](https://raw.githubusercontent.com/asipe/Conz/master/LICENSE)

### Install

nuget package (Conz.Core): https://nuget.org/packages/Conz.Core/

install via package manager: Install-Package Conz.Core

### Usage

```csharp
var conz = new Conzole(new StyleSheet(new Class("default")));
conz.WriteLine("Default style sheet which uses the current Console default values");

conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow)));
conz.WriteLine("Default style sheet which has a yellow background");

conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black)));
conz.WriteLine("Default style sheet which has a yellow background and a black foreground");

conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black, 5)));
conz.WriteLine("Default style sheet which has a yellow background and a black foreground and an indent of 5 spaces");

conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black),
                                  new Class("error", ConzoleColor.Red)));
conz.WriteLine("|error|error style| defined which has a red background");

conz = new Conzole(new StyleSheet(new Class("default", ConzoleColor.Yellow, ConzoleColor.Black),
                                  new Class("error", ConzoleColor.Red),
                                  new Class("notice", ConzoleColor.Default, ConzoleColor.DarkCyan)));
conz.WriteLine("|notice|notice style| defined which has a cyan foreground");
```

src\Samples contains some additional samples

### License

Conz is licensed under the MIT license

     The MIT License (MIT)

     Copyright (c) 2014 Andy Sipe

     Permission is hereby granted, free of charge, to any person obtaining a copy
     of this software and associated documentation files (the "Software"), to deal
     in the Software without restriction, including without limitation the rights
     to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
     copies of the Software, and to permit persons to whom the Software is
     furnished to do so, subject to the following conditions:

     The above copyright notice and this permission notice shall be included in all
     copies or substantial portions of the Software.

     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
     LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
     OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
     SOFTWARE.
