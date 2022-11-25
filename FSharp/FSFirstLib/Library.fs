namespace FSFirstLib
open ServiceStack.OrmLite.Sqlite

module FSFirstLib =
    let MyMax a b =
        if a >= b  then a else b


    let rec SumF2 (l : List<int>)=
      match l with
      | h::t -> h + (SumF2 t)
      | _ -> 0

    //let GetIDByNum (num : int) : string =
    //  //let factory =  new ServiceStack.OrmLite.OrmLiteConnectionFactory("c:\Projects\AirTouch\CarMk2.5\run_dir\cardb.db", ServiceStack.OrmLite.SqliteDialect.Provider);
    //  use db = ServiceStack.OrmLite.Sqlite. OpenDbConnection "c:\Projects\AirTouch\CarMk2.5\run_dir\cardb.db"
    //  let cprs = db. <RobotPersistentBase>();

    //  "ddd"