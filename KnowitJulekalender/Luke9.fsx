#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#load "File.fs"

open System
open System.Collections.Generic

let uri = new System.Uri("http://pastebin.com/raw/2vstb018");
let file = File.download uri

type Account = { Id: Guid; Amount: int }
type Transaction = { From: Guid option; To: Guid; Amount: int }

let parseTransaction (line: string) = 
    let parseGuid str = 
        match str with 
        | "None" -> None
        | _ -> Some(new Guid(str))
    let cols = line.Split(',')
    { From = parseGuid cols.[0]; To = new Guid(cols.[1]); Amount = (int)cols.[2] }

let parsedTransactions = file.Split('\n') |> Seq.map parseTransaction |> Seq.toList

let accounts = Dictionary<Guid, int>()

let addAmount id amount =
    if accounts.ContainsKey(id) then
        accounts.[id] <- accounts.[id] + amount
    else
        accounts.Add(id, amount)

let withdrawAmount id amount =
    accounts.[id] <- accounts.[id] - amount

let runTransaction transaction =
    match transaction.From with
    | None -> addAmount transaction.To transaction.Amount
    | Some x -> 
        addAmount transaction.To transaction.Amount
        withdrawAmount x transaction.Amount

let runTransactions() =
    parsedTransactions |> Seq.iter runTransaction

let answer = 
    runTransactions()
    accounts |> Seq.filter (fun x -> x.Value > 10) |> Seq.length