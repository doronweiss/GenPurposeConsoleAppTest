using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Gorillas;
internal class OrbCommRegex {
  enum IBMessageState {
    RxComplete=2,
    RXRetrieved
  }

  class InBoundMsgDesc {
    public string name;
    public int msgNumNumber;
    public int msgNumSequence;
    public int priority;
    public int sin;
    public IBMessageState state;
    public int length;
    public int retrieved;

    public static InBoundMsgDesc FromString(string str) {
      string[] toks = str.Split(',', StringSplitOptions.RemoveEmptyEntries);
      InBoundMsgDesc ib = new InBoundMsgDesc();
      ib.name = toks[0].Trim('\"');
      int dotIdx = toks[1].IndexOf('.');
      ib.msgNumNumber = int.Parse(toks[1][0..dotIdx]);
      dotIdx++;
      ib.msgNumSequence = int.Parse(toks[1][dotIdx..]);
      ib.priority = int.Parse(toks[2]);
      ib.sin = int.Parse(toks[3]);
      ib.state = (IBMessageState)int.Parse(toks[4]);
      ib.length = int.Parse(toks[5]);
      ib.retrieved = int.Parse(toks[6]);
      return ib;
    }
  }

  public static void Run() {
    List< InBoundMsgDesc> ibs = new List< InBoundMsgDesc>();
    string input = "%MGFS: \"FM02.01\",2.1,0,16,3,2,2\"FM03.01\",3.1,0,16,2,2,2OK";
    string regexPatt = "\"FM[0-9]*.[0-9]*\",[0-9]*.[0-9]*,[0-9],[0-9]*,[0-9],[0-9],[0-9]";
    Regex regex = new Regex(regexPatt);
    MatchCollection matches = regex.Matches(input);
    foreach (Match match in matches) {
      int lastIdx = match.Index + match.Length;
      string str = input[match.Index..lastIdx];
      InBoundMsgDesc ib = InBoundMsgDesc.FromString(str);
      Console.WriteLine(JsonConvert.SerializeObject(ib, new Newtonsoft.Json.Converters.StringEnumConverter()));
    }
  }
}
