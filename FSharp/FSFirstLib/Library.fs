namespace FSFirstLib
open System
open ServiceStack.OrmLite.Sqlite

module FSFirstLib =
    type Person(firstName : string, lastName : string) = 
    /// The fullName of the Person, computed when the object is constructed
      let fullName = String.Format("{0} {1}",firstName, lastName)
      //2nd constructor
      new() = Person("", "")
      //3rd constructor
      new(firstName : string) = Person(firstName, "")
      // 'this' specifies a name for the object's self identifier.
      // In instance methods, it must appear before the member name.
      member public this.FirstName = firstName
      member public this.LastName = lastName
      member public this.FullName = fullName


    let MyMax a b =
        if a >= b  then a else b


    let rec SumF2 (l : List<int>)=
      List.sum l
      //match l with
      //| h::t -> h + (SumF2 t)
      //| _ -> 0

    let rec SumF2Seq (l : seq<int>)=
      Seq.sum l

    //let GetIDByNum (num : int) : string =
    //  let factory =  new ServiceStack.OrmLite.OrmLiteConnectionFactory("c:\Projects\AirTouch\CarMk2.5\run_dir\cardb.db", ServiceStack.OrmLite.SqliteDialect.Provider);
    //  let conn = new ServiceStack.OrmLite.OrmLiteConnection( factory)
    //  conn.
      

    //  "ddd"