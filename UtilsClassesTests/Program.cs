// See https://aka.ms/new-console-template for more information
using UtilsClassesTests;

PLCMultiBool16 cbv = new PLCMultiBool16();
Console.WriteLine($"value: {(bool)cbv}");
for (int idx = 0; idx < 16; idx++) {
  cbv.Set(idx, true);
  Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
  cbv.Set(idx, true);
  Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
}
for (int idx = 15; idx >= 0; idx--) {
  cbv.Set(idx, false);
  Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
  cbv.Set(idx, false);
  Console.WriteLine($"Idx: {idx}, changed: {cbv.Changed},  value: {(bool)cbv}");
}
