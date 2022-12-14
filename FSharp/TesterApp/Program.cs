using System.Diagnostics;
using Microsoft.FSharp.Collections;


static int MySum2(List<int> lst) => lst[0] + lst[1];

// // See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
double d = FSFirstLib.FSFirstLib.MyMax(1.2, 3.4);
Console.WriteLine($"Max = {d}");

List<int> lst = new List<int>() { 1, 2, 3, 4, 5 };

int loops = 1000000;
Stopwatch st = new Stopwatch();
Console.WriteLine("Starting");
st.Start();
int str = 0;
for (int idx = 0; idx < loops; idx++)
  str = MySum2(lst);
st.Stop();
long cpt = st.ElapsedMilliseconds;
long cptt = st.ElapsedTicks;
st.Reset();
st.Start();
for (int idx = 0; idx < loops; idx++)
  str = FSFirstLib.FSFirstLib.SumF2(ListModule.OfSeq(lst));
st.Stop();
long fpt = st.ElapsedMilliseconds;
long fptt = st.ElapsedTicks;
Console.WriteLine($"Elapssed: {cpt}/{fpt}");
Console.WriteLine($"Elapssed ticks: {cptt}/{fptt}");


//Console.WriteLine($"2 sum = {FSFirstLib.FSFirstLib.SumF2(ListModule.OfSeq(lst))}");
