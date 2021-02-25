module MiningPullMonitoring.Commands

open System
open System.Net.Http
open FSharp.Json
open MiningPullMonitoring.Types

let printCurrentStats state =
    Console.Clear()
    
    let date = DateTime.Now.ToLongTimeString()
    printfn "Время: %s" date
    
    let currentH = state.data.currentHashrate / 1000000.
    printfn "Текущий Hash: %f" currentH
    
    let avgH = state.data.averageHashrate / 1000000.
    printfn "Средний Hash: %f" avgH
    
    let reportedH = state.data.reportedHashrate / 1000000.
    printfn "Заявленный Hash: %f" reportedH
        
    let balanceCoin = double(state.data.unpaid) / 1000000000000000000.
    printfn "Баланс: %.5f" balanceCoin
    
    let coinsPerMonth = state.data.coinsPerMin * 60.0 * 24.0 * 30.0
    printfn "Монет в месяц: %.4f" coinsPerMonth
    
    let exchangeRate = (1. / state.data.coinsPerMin) * state.data.usdPerMin
    printfn "Курс: %.2f" exchangeRate
    

let getAsync (client: HttpClient) (url:string)  =
    async {
        let! response = client.GetAsync(url) |> Async.AwaitTask
        response.EnsureSuccessStatusCode() |> ignore
        return! response.Content.ReadAsStringAsync() |> Async.AwaitTask
    }

let getJson =
    async {
        let wallet = "0x8126eb543fcd478de62c6021a0fcefc475a9c18c"
        let url = sprintf "https://api.ethermine.org/miner/%s/currentStats" wallet
        use client = new HttpClient()
        return! getAsync client url
    }

let parseJsonToCurrentStats json =
    Json.deserialize<CurrentStats> json