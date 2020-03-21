using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Net;
using Microsoft.CSharp;
using System.CodeDom.Compiler;


namespace GenPurposeConsoleAppTest {
  class CSScriptRT {
    public static void Run() {
      Dictionary<string, string> providerOptions = new Dictionary<string, string> { { "CompilerVersion", "v4.0" } };
      CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);
      CompilerParameters compilerParams = new CompilerParameters {
        GenerateInMemory = true,
        GenerateExecutable = false,
        IncludeDebugInformation = true
      };
      //string source = File.ReadAllText("Script1.cs");
      CompilerResults results = provider.CompileAssemblyFromFile(compilerParams, "Script1.cs");
      if (results.Errors.Count != 0) {
        Console.WriteLine("Mission failed!");
        return;
      }
      object o = results.CompiledAssembly.CreateInstance("Foo.Bar");
      MethodInfo mi = o.GetType().GetMethod("SayHello");
      for (int idx = 0; idx < 100; idx++)
        System.Console.WriteLine(" => " + mi.Invoke(o, new object[] { idx}));
    }
  }
}
