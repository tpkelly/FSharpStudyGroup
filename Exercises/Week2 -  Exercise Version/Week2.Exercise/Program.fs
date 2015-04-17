module Program

open Exercise
open System.Diagnostics
[<EntryPoint>]
let main args = 
    drawAndSaveFractalTree()
    printfn "DONE!"
    System.Console.ReadLine() |> ignore
    0