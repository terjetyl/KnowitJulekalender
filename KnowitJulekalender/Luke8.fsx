
#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#load "File.fs"

let uri = new System.Uri("http://pastebin.com/raw/dJ7cT4AF");
let file = File.download uri

let moves = file.Split('\n') |> Seq.map int |> Seq.toList
let ladders = [(3,17); (8,10); (15,44); (22,5); (39,56); (49,75); (62,45); (64,19); (65,73); (80,12); (87,79)]
let players = List.init 1337 (fun x -> 1)

let mutable ladderCount = 0

let checkLadder field = 
    let ladderHit = ladders |> List.exists (fun x -> fst x = field)
    if ladderHit then
        let ladder = ladders |> List.find (fun x -> fst x = field)
        ladderCount <- ladderCount + 1
        (snd ladder)
    else
        field

let move start count (index: int) = 
    let res = start + count
    match res with 
    | 90 -> failwith ("We have a winner at index: " + index.ToString() + ". Total ladder hits is " + ladderCount.ToString())
    | x when x > 90 -> start
    | _ -> checkLadder res

let mutable round = -1
let rec checkRound (players: int list) : int list = 
    round <- round + 1
    players |> List.mapi (fun i x -> move x moves.[i + (round * 1337)] i) |> checkRound

let findWinner() =
    checkRound players
