module program
// For more information see https://aka.ms/fsharp-console-apps
printfn "Hello from F#"

//let forUpFunction() =
//  printfn "for i = 1 to 10 do\r\n"
//  for i = 1 to 10 do
//    printf "%d " i
//    printfn "\r"
 
//let forDownFunction() =
//  printfn "for i = 10 downto 1 do\r\n"
//  for i = 10 downto 1 do
//    printf "%d " i
 
     
//forUpFunction()
//forDownFunction()

let findANumberInAList theList theNum =
  let mutable index = 0;
  let mutable found=false
  let mutable current =0;
  let listHasItem = (List.exists (fun el -> el = theNum) theList)
  if listHasItem then 
    while not found do
      current <- List.nth theList index
      if(current = theNum) then
        printfn "Found %A in source list at index %A" theNum index
        found <- true
      index <- index + 1

let findfs theList theNum = 
  match List.tryFindIndex (fun x  -> x = theNum) theList with
  | Some value -> value
  | None -> -1
                 
let sourceList = [1..10]
let mutable tgt = 4
printfn "SourceList = %A\r\n" sourceList 
findANumberInAList sourceList tgt
let f1 = findfs  sourceList tgt
printfn "Found %A in source list at index %A" tgt f1
tgt <- 11
let f2 = findfs  sourceList tgt
printfn "Found %A in source list at index %A" tgt f2
