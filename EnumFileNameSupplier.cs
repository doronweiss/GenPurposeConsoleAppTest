using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using Mono.CSharp;

namespace GenPurposeConsoleAppTest {
  // NOTE: extension includes the dot (.), "xls" should be ".xls"
  class SortedFileDesc {
    public string prefix = "";
    public int ordinal = -1;

    public static SortedFileDesc FromString(string fname, string prefix) {
      SortedFileDesc sfd = new SortedFileDesc();
      int pfsz = prefix.Length;
      sfd.prefix = prefix;
      string tstr = Path.GetFileNameWithoutExtension(fname);
      tstr = tstr.Substring(pfsz);
      if (!int.TryParse(tstr, out sfd.ordinal))
        return null;
      return sfd;
    }

    public override string ToString() => $"{prefix}{ordinal}";
  }

  class EnumFileNameSupplier {
    public static (bool, string) GetNextFileName(string folder, string prefix, string extension) {
      List<string> currFiles;
      try {
        string filter = $"{prefix}*{extension}";
        string[] cfa = Directory.GetFiles(folder, filter);
        currFiles = cfa.ToList();
      } catch (Exception ex) {
        return (false, ex.Message);
      }
      if (currFiles.Count == 0)
        return (true, $"{prefix}1");
      int lastFile = 0;
      int pfsz = prefix.Length;
      foreach (string fname in currFiles) {
        SortedFileDesc sfd = SortedFileDesc.FromString(fname, prefix);
        if (sfd == null)
          continue;
        lastFile = sfd.ordinal > lastFile ? sfd.ordinal : lastFile;
      }
      return (true, $"{prefix}{lastFile+1}");
    }

    public static string GetLastFile(string folder, string prefix, string extension) {
      List<string> fnames = GetSortedFilesList(folder, prefix, extension);
      return fnames?.Count > 0 ? fnames.Last() : null;
    }

    public static List<string> GetSortedFilesList(string folder, string prefix, string extension, bool ascending = true) {
      if (!Directory.Exists(folder)) {
        System.Diagnostics.Debug.WriteLine("Folder does not exist");
        return null;
      }
      List<string> currFiles;
      try {
        string filter = $"{prefix}*{extension}";
        string[] cfa = Directory.GetFiles(folder, filter);
        currFiles = cfa.ToList();
      } catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine(ex.Message);
        return null;
      }
      List<SortedFileDesc> sfds = currFiles.Select(x => SortedFileDesc.FromString(x, prefix)).ToList();
      sfds = ascending ? sfds.OrderBy(x => x.ordinal).ToList() : sfds.OrderByDescending(x => x.ordinal).ToList();
      currFiles = sfds.Select(x => x.ToString()).ToList();
      return currFiles;
    }

    public static void Run() {
    (bool b, string fn) = GetNextFileName(@"c:\temp", "batata", ".txt");
    Console.WriteLine($"Next file name: {b} / {fn}");
    List<string> fnames = GetSortedFilesList(@"c:\temp", "batata", ".txt");
    Console.WriteLine($"GetSortedFilesList => \n{string.Join(Environment.NewLine, fnames)}");
    Console.WriteLine($"GetLastFile => {GetLastFile(@"c:\temp", "batata", ".txt")}");
  }
}
}
