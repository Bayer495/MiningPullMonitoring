open System
open System.Threading
open MiningPullMonitoring.Commands

[<EntryPoint>]
let main argv =
    while true do
        getJson |> Async.RunSynchronously |> parseJsonToCurrentStats |> printCurrentStats
        Thread.Sleep(1000 * 60 * 2)
    0 