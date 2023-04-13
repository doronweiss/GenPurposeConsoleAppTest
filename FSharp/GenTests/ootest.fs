module ootest

open System

type public Person(firstName : string, lastName : string) = 
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


let p = Person("doron", "weiss")
printfn "full name is: %s" p.FullName
