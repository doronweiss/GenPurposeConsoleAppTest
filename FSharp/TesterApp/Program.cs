using System.Diagnostics;
using Microsoft.FSharp.Collections;


static int MySum2(List<int> lst) => lst[0] + lst[1];

// // See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//double d = FSFirstLib.FSFirstLib.MyMax(1.2, 3.4);
//Console.WriteLine($"Max = {d}");

List<int> lst = new List<int>() { 1, 2, 3, 4, 5 };
int str = FSFirstLib.FSFirstLib.SumF2Seq(lst);
// Console.WriteLine($"lst sum =  {FSFirstLib.FSFirstLib.SumF2Seq(lst)}");

//int loops = 1000000, sum=0;
//Stopwatch st = new Stopwatch();
//Console.WriteLine("Starting");
//st.Start();
// for (int idx = 0; idx < loops; idx++)
//  sum = FSFirstLib.FSFirstLib.SumF2Seq(lst);
//st.Stop();
//long fpt = st.ElapsedMilliseconds;
//Console.WriteLine($"SEQ:   Elapssed: {fpt}, per op: {fpt * 1.0 / loops}m sum: {sum}");
//st.Reset();
//st.Start();
// for (int idx = 0; idx < loops; idx++)
//  sum = FSFirstLib.FSFirstLib.SumF2(ListModule.OfSeq(lst));
//st.Stop();
//fpt = st.ElapsedMilliseconds;
//Console.WriteLine($"LIST:   Elapssed: {fpt}, per op: {fpt * 1.0 / loops}m sum: {sum}");

FSFirstLib.FSFirstLib.Person pers = new FSFirstLib.FSFirstLib.Person("moshe", "levi", DateTime.Now);
Console.WriteLine(pers.FullName);




//
// int loops = 1000000;
// Stopwatch st = new Stopwatch();
// Console.WriteLine("Starting");
// st.Start();
// int str = 0;
// for (int idx = 0; idx < loops; idx++)
//   str = MySum2(lst);
// st.Stop();
// long cpt = st.ElapsedMilliseconds;
// long cptt = st.ElapsedTicks;
// st.Reset();
// st.Start();
// for (int idx = 0; idx < loops; idx++)
//   str = FSFirstLib.FSFirstLib.SumF2(ListModule.OfSeq(lst));
// st.Stop();
// long fpt = st.ElapsedMilliseconds;
// long fptt = st.ElapsedTicks;
// Console.WriteLine($"Elapssed: {cpt}/{fpt}");
// Console.WriteLine($"Elapssed ticks: {cptt}/{fptt}");


//Console.WriteLine($"2 sum = {FSFirstLib.FSFirstLib.SumF2(ListModule.OfSeq(lst))}");
