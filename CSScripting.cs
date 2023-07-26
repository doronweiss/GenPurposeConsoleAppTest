using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSScriptLibrary;
using DynamicExpresso;
//using ExpressionEvaluator;
using Neo.IronLua;
using System.Threading;
using System.Diagnostics;

namespace GenPurposeConsoleAppTest {
  class CSScripting {
    public static void Run() {
      Stopwatch sw = new Stopwatch();
      // create lua script engine
      int count = 10;
      // create an environment, that is associated to the lua scripts
      try {
        #region dynamicexpresso
        long sum = 0;
        var interpreter = new Interpreter();
        // C# interpreter - dynamicexpresso (https://github.com/davideicardi/DynamicExpresso)
        sw.Start();
        for (int idx = 0; idx < count; idx++) {
          interpreter.SetVariable("a", idx, typeof(int));
          interpreter.SetVariable("b", 3, typeof(int));
          int lr = (int)interpreter.Eval("a*b");
          sum = sum + lr;
          //Console.WriteLine("Lr = {0}", lr.Values[0]);
        }
        sw.Stop();
        Console.WriteLine("DExpresso\t\tcount: {0}, , Sum: {2},  time : {1}, op-time: {3}", count, sw.ElapsedMilliseconds, sum, sw.ElapsedMilliseconds * 1.0 / count);
        sw.Reset();
        #endregion
        #region LUA
        sum = 0;
        // lua
        using (var l = new Lua()) {
          dynamic g = l.CreateEnvironment<LuaGlobal>();
          sw.Start();
          for (int idx = 0; idx < count; idx++) {
            g["a"] = idx;
            g["b"] = 3;
            LuaResult lr = g.dochunk(" return a*b");
            sum = sum + (int)lr.Values[0];
            //Console.WriteLine("Lr = {0}", lr.Values[0]);
          }
          sw.Stop();
        }
        Console.WriteLine("LUA\t\t\tcount: {0}, , Sum: {2},  time : {1}, op-time: {3}", count, sw.ElapsedMilliseconds, sum, sw.ElapsedMilliseconds * 1.0 / count);
        sw.Reset();
        #endregion
//        #region expression evaluator
//        sum = 0;
//        // expression evaluator (https://csharpeval.codeplex.com/)
//        sw.Reset();
//        sum = 0;
//        sw.Start();
//        for (int idx = 0; idx < count; idx++) {
//          Int32 ae = idx;
//          Int32 be = 3;
//          var t = new TypeRegistry();
//          t.RegisterSymbol("a", ae);
//          t.RegisterSymbol("b", be);
//          var p = new CompiledExpression("a*b");
//          p.TypeRegistry = t;
//          p.ExpressionType = CompiledExpressionType.Expression;
//          var res = p.Eval();
//          sum = sum + (int)res;
//        }
//        sw.Stop();
//        Console.WriteLine("ExpEval\t\t\tcount: {0}, , Sum: {2},  time : {1}, op-time: {3}", count, sw.ElapsedMilliseconds, sum, sw.ElapsedMilliseconds * 1.0 / count);
//        sw.Reset();
//        #endregion
        #region CSScript  // https://github.com/oleg-shilo/cs-script
        sum = 0;
        sw.Reset();
        sw.Start();
        var adder = CSScript.CreateFunc<int>(
          @"int Addit (int a, int b) {
              return a*b;
            }");
        for (int idx = 0; idx < count; idx++) {
          int a = idx;
          int b = 3;
          sum = sum + adder(a, b);
        }
        sw.Stop();
        Console.WriteLine("CSScript\t\tcount: {0}, , Sum: {2},  time : {1}, op-time: {3}", count, sw.ElapsedMilliseconds, sum, sw.ElapsedMilliseconds * 1.0 / count);
        sw.Reset();
        #endregion
        #region .NET
        sum = 0;
        sw.Start();
        for (int idx = 0; idx < count; idx++) {
          int a = idx;
          int b = 3;
          int c = a * b;
          sum = sum + c;
        }
        sw.Stop();
        Console.WriteLine("CS\t\t\tcount: {0}, , Sum: {2},  time : {1}, op-time: {3}", count, sw.ElapsedMilliseconds, sum, sw.ElapsedMilliseconds * 1.0 / count);
        #endregion
      } catch (Exception e) {
        Console.WriteLine($"Expception: {e.Message}");
        var d = LuaExceptionData.GetData(e); // get stack trace
        Console.WriteLine("StackTrace: {0}", d.FormatStackTrace(0, false));
      }
    }
  }
}
