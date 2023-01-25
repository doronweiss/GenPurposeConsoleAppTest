// See https://aka.ms/new-console-template for more information
using System;
using UtilsClassesTests;

// PLCMultiBool16 cbv = new PLCMultiBool16();
// Console.WriteLine($"value: {(bool)cbv}");
// for (int idx = 0; idx < 16; idx++) {
//   cbv.Set(idx, true);
//   Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
//   cbv.Set(idx, true);
//   Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
// }
// for (int idx = 15; idx >= 0; idx--) {
//   cbv.Set(idx, false);
//   Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
//   cbv.Set(idx, false);
//   Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
// }

enum EN { t1, t2, t3, t4 };


namespace MatLib {
  class Program {
    static void Main(string[] args) {
      PLCMultiBoolE<EN> pm = new PLCMultiBoolE<EN>(false);
      pm.Set(EN.t1, true);
      Console.WriteLine($"Pm:{(bool)pm} rose:{pm.WhoRose()},fell: {pm.WhoFell()} ");
      pm.Set(EN.t1, true);
      pm.Set(EN.t4, true);
      Console.WriteLine($"Pm:{(bool)pm} rose:{pm.WhoRose()},fell: {pm.WhoFell()} ");
      pm.Set(EN.t4, true);
      pm.Set(EN.t1, false);
      Console.WriteLine($"Pm:{(bool)pm} rose:{pm.WhoRose()},fell: {pm.WhoFell()} ");
      pm.Set(EN.t1, false);
      pm.Set(EN.t4, false);
      Console.WriteLine($"Pm:{(bool)pm} rose:{pm.WhoRose()},fell: {pm.WhoFell()} ");
      pm.Set(EN.t4, false);
      Console.WriteLine($"Pm:{(bool)pm} rose:{pm.WhoRose()},fell: {pm.WhoFell()} ");
    }
  }
}
