module GenTests

type Person = {
    Name: string
    Age: int
    City: string
}

let original = { Name = "Alice"; Age = 30; City = "New York" }
let modified = { original with Age = 31; City = "Boston" }

printfn "Original: %A" original
printfn "Modified: %A" modified


// Create a mutable list
let peoplecs = ResizeArray<Person>([
    { Name = "Alice"; Age = 30; City = "New York" }
    { Name = "Bob"; Age = 25; City = "Boston" }
    { Name = "Charlie"; Age = 35; City = "Chicago" }
])

// Find the index of the person you want to replace
let index = peoplecs.FindIndex(fun p -> p.Name = "Bob")

// Replace with a modified version
if index >= 0 then
    peoplecs.[index] <- { peoplecs.[index] with Age = 26; City = "Seattle" }

printfn "Modified C# style: %A" peoplecs

let peoplefs = [
    { Name = "Alice"; Age = 30; City = "New York" }
    { Name = "Bob"; Age = 25; City = "Boston" }
    { Name = "Charlie"; Age = 35; City = "Chicago" }
]

// Replace Bob with a modified version
let updatedPeople = 
    peoplefs 
    |> List.map (fun p -> 
        if p.Name = "Bob" then 
            { p with Age = 26; City = "Seattle" }
        else 
            p)

printfn "Modified F# style: %A" updatedPeople
