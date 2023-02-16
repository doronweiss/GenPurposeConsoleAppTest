using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace ORMLiteTest {

  public enum RowStateEnum {
    pending,
    cleaning,
    cleaned,
    abandond
  };

  public class RowData {
    public const string DatetimeFormat = "MM/dd/yy HH:mm:ss";
    [AutoIncrement]
    [PrimaryKey]
    [Required]
    public int id { set; get; }
    [Required]
    public int rowNum { get; set; }
    [Required]
    public RowStateEnum _rowState { set; get; } = 0;
    public int abandonCount = 0;
    [Ignore]
    public RowStateEnum rowState {
      get => _rowState;
      set {
        abandonCount = value == RowStateEnum.abandond ? abandonCount + 1 : 0;
        _rowState = value;
      }
    }
  }


  class Program {
    static OrmLiteConnectionFactory connFact = null;
    public static OrmLiteConnectionFactory Factory => connFact;

    public static bool EnsureDBExists() {
      try {
        using IDbConnection db = Factory.Open();
        // misc data
        if (!db.TableExists<RowData>()) {
          db.CreateTable<RowData>();
          db.Insert<RowData>(new RowData() {
            rowNum = 1, rowState = RowStateEnum.pending
          });
          db.Insert<RowData>(new RowData() {
            rowNum = 1, rowState = RowStateEnum.pending
          });
        }
        return true;
      } catch (Exception ex) {
        return false;
      }
    }


    public static bool InitConnectionFactory() {
      string connStr = Path.Combine(Directory.GetCurrentDirectory(), "rowsdatabase.db");
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

    public static List<RowData> LoadRows (){
      try {
        try {
          using IDbConnection db = Factory.Open();
          List<RowData> cprs = db.Select<RowData>();
          return cprs;
        } catch {
          return null;
        }
      } catch (Exception ex) {
        return null;
      }
    }

    public static bool SaveRows(List<RowData> plan) {
      try {
        using IDbConnection db = Factory.Open();
        // delete existing data
        db.DeleteAll<RowData>();
        // save new rows
        db.SaveAll<RowData>(plan);
      } catch (Exception ex) {
        return false;
      }
      return true;
    }


    static void Main(string[] args) {
      List<RowData> rows;
      if (!InitConnectionFactory())
        return;
      if (!EnsureDBExists())
        return;
      rows = LoadRows();
      if (rows == null) {
        Console.WriteLine("load => NULL");
        return;
      }
      rows[0].rowState = RowStateEnum.abandond;
      rows[0].rowState = RowStateEnum.abandond;
      SaveRows(rows);
      rows = LoadRows();
      Console.WriteLine("Hello World!");
    }
  }
}
