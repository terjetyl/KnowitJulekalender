
#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#load "File.fs"

let uri = new System.Uri("http://pastebin.com/raw/e0bE4naA");
let file = File.download uri

type RelationshipType = 
    | Friends
    | Hates

type Relationship = { Type: RelationshipType; Name1: string; Name2: string; Cameleon: string }
let createRelationShip t name1 name2 cameleon = 
    { Type = t; Name1 = name1; Name2 = name2; Cameleon = cameleon }

let removeExcessWhitespace str = 
    let trimmer = new System.Text.RegularExpressions.Regex(@"\s\s+");
    trimmer.Replace(str, " ")

let lines = file.Split('\n') |> Seq.map removeExcessWhitespace

let parseLine (line: string) = 
    let cols = line.Split(' ')
    match cols.[0] with
    | "friends" -> createRelationShip RelationshipType.Friends cols.[1] cols.[2] ""
    | _ -> createRelationShip RelationshipType.Hates cols.[0] cols.[2] ""

let findCameleon friendship haters =
    let person1hates = haters |> Seq.exists (fun x -> friendship.Name1 = x.Name1 && friendship.Name2 = x.Name2)
    let person2hates = haters |> Seq.exists (fun x -> friendship.Name2 = x.Name1 && friendship.Name1 = x.Name2)
    if person1hates = person2hates then
        ""
    else if person1hates then
        friendship.Name1
    else
        friendship.Name2


let relationShips = lines |> Seq.map parseLine |> Seq.toList

let haters = relationShips |> Seq.filter (fun x -> x.Type = RelationshipType.Hates) |> Seq.toList
let friends = relationShips |> Seq.filter (fun x -> x.Type = RelationshipType.Friends) |> Seq.toList
let cameleons = friends |> Seq.map (fun x -> createRelationShip (RelationshipType.Friends) (x.Name1) (x.Name2) (findCameleon x haters))

let answer = cameleons |> Seq.filter (fun x -> x.Cameleon <> "") |> Seq.groupBy (fun x -> x.Cameleon) |> Seq.sortByDescending (fun x -> (snd x) |> Seq.distinct |> Seq.length)
