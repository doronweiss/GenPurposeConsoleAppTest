using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenPurpNetCoreTest {
  class SerialParseTest {
    enum MessageStateEnum {
      WaitS,
      WaitComma,
      Data,
      WaitZ
    }

    static StringBuilder sb = new StringBuilder("");

    private static MessageStateEnum msgState = MessageStateEnum.WaitS;
    static void ParseString(string serialIn) {
      foreach (char ch in serialIn) {
        switch (msgState) { 
          case MessageStateEnum.WaitS:
            if (ch == 's') {
              sb.Clear();
              msgState = MessageStateEnum.WaitComma;
            }
            break;
          case MessageStateEnum.WaitComma:
            if (ch == ',') 
              msgState = MessageStateEnum.Data;
            else
              msgState = MessageStateEnum.WaitS;
            break;
          case MessageStateEnum.Data:
            if (ch == ',')
              msgState = MessageStateEnum.WaitZ;
            else
              sb.Append(ch);
            break;
          case MessageStateEnum.WaitZ:
            if (ch == 'z') {
              Console.WriteLine($"Received: {sb.ToString()}");
            }
            msgState = MessageStateEnum.WaitComma;
            break;
        }
      }
    }


    public static void Run() {
      ParseString("s,123;333");
      ParseString("444,z");
    }
  }
}
