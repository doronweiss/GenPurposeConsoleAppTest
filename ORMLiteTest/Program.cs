using System;
using System.Data;
using System.IO;
using System.Threading;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ORMLiteTest {

  public class AuxDataStore {
    public const string DatetimeFormat = "MM/dd/yy HH:mm:ss";
    [AutoIncrement]
    [PrimaryKey]
    [Required]
    public int id { set; get; }
    [Required]
    public int itemKey { set; get; } = 0;
    [Required]
    [StringLength(100)]
    public object itemValue { set; get; }
  }


  class Program {
    static OrmLiteConnectionFactory connFact = null;
    public static OrmLiteConnectionFactory Factory => connFact;

    public static bool EnsureDBExists() {
      try {
        using IDbConnection db = Factory.Open();
        // misc data
        if (!db.TableExists<AuxDataStore>()) {
          db.CreateTable<AuxDataStore>();
          db.Insert<AuxDataStore>(new AuxDataStore() {
            itemKey = 0, itemValue = DateTime.Now
          });
          db.Insert<AuxDataStore>(new AuxDataStore() {
            itemKey = 1, itemValue = DateTime.Now
          });
        }
        return true;
      } catch (Exception ex) {
        return false;
      }
    }

    public static bool UpdateMiscData(int itemKey, object itemValue) {
      // return UpdateMiscData2(itemKey, itemType, itemValue);
      try {
        using IDbConnection db = Factory.Open();
        int rows = db.UpdateOnly(new AuxDataStore { itemValue = (DateTime)itemValue },
          onlyFields: p => p.itemValue,
          where: x => x.itemKey == (int)itemKey);
        return rows > 0;
      } catch (Exception ex) {
        return false;
      }
    }


    public static bool InitConnectionFactory() {
      string connStr = Path.Combine(Directory.GetCurrentDirectory(), "polytexdb.db");
      connFact = new OrmLiteConnectionFactory(connStr, SqliteDialect.Provider);
      if (!EnsureDBExists()) {
        if (File.Exists(connStr)) {
          try {
            File.Delete(connStr);
            if (!EnsureDBExists()) {
              return false;
            }
          } catch (Exception ex) {
            return false;
          }
        }
      }
      return true;
    }

    DateTime GetData(int index) {
      return DateTime.Now;
    }


    static void Main(string[] args) {
      if (!InitConnectionFactory())
        return;
      if (!EnsureDBExists())
        return;
      Thread.Sleep(1000);
      UpdateMiscData(0, DateTime.Now);
      Console.WriteLine("Hello World!");
    }
  }
}
