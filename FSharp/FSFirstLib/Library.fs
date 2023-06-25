namespace FSFirstLib
open System
open ServiceStack.OrmLite.Sqlite

module FSFirstLib =
    type Person(firstName : string, lastName : string, birthDate : DateTime) = 
    /// The fullName of the Person, computed when the object is constructed
      let fullName = String.Format("{0} {1}",firstName, lastName)
      let age = (DateTime.Now - birthDate).ToString()
      new() = Person("", "", DateTime.Now)
      new(firstName : string) = Person(firstName, "", DateTime.Now)
      // methods
      member public this.FirstName = firstName
      member public this.LastName = lastName
      member public this.FullName = fullName
      member public this.Detailrs = fullName + " " + age

    let MyMax a b =
        if a >= b  then a else b


    let rec SumF2 (l : List<int>)=
      //List.sum l
      match l with
      | h::t -> h + (SumF2 t)
      | _ -> 0

    let SumF2Seq (l : seq<int>)=
      Seq.sum l

    //let GetIDByNum (num : int) : string =
    //  let factory =  new ServiceStack.OrmLite.OrmLiteConnectionFactory("c:\Projects\AirTouch\CarMk2.5\run_dir\cardb.db", ServiceStack.OrmLite.SqliteDialect.Provider);
    //  let conn = new ServiceStack.OrmLite.OrmLiteConnection( factory)
    //  conn.
      

    //  "ddd"