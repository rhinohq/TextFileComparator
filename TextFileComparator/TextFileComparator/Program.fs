// Learn more about F# at http://fsharp.org

open System
open System.Collections.Generic
open System.IO
open System.Linq

let findLongerLines (lines : IEnumerable<string>) : string[] =
    let average_line_length = lines.Select(fun x -> x.Length).Average() |> Convert.ToInt32
    lines.Where(fun x -> x.Length > average_line_length + 1).ToArray()

let sameInstanceOfLine (lines_one : IEnumerable<string>) (lines_two : IEnumerable<string>) : bool = 
    if lines_one.Count() > lines_two.Count() then
        lines_two.Any(fun x -> lines_one.Contains x)
    else 
        lines_one.Any(fun x -> lines_two.Contains x)

let checkLines lines_one lines_two = 
    if sameInstanceOfLine lines_one lines_two  
        then Console.WriteLine "One or more lines are the same"
    else 
        Console.WriteLine "Files are in no way similar"

let printLines (lines : string[]) =
    for line in lines do
        Console.WriteLine(line + "\n")

[<EntryPoint>]
let main argv =
    printfn "Welcome to the Text File Comparator!"

    printfn "Enter File 1:"
    let file_one_path = Console.ReadLine()

    printfn "Enter File 2:"
    let file_two_path = Console.ReadLine()

    printfn "\nReading text files..."
    let file_one_contents = File.ReadAllText file_one_path
    let file_two_contents = File.ReadAllText file_two_path
    printfn "Read text files\n"

    if file_one_contents.Equals file_two_contents then
        printfn "Files are the same"
        let longer_lines = File.ReadLines file_one_path |> findLongerLines
        printLines longer_lines
    else if file_one_contents.Contains file_two_contents 
        then printfn "File 1 contains file 2"
    else if file_two_contents.Contains file_one_contents 
        then printfn "File 2 contains file 1"
    else
        printfn "Files are not the same. Checking individual lines..."
        let lines_one = File.ReadLines(file_one_path)
        let lines_two = File.ReadLines(file_two_path)
        checkLines lines_one lines_two

    0 // return an integer exit code

