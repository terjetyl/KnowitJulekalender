// En skøyen alv har gjemt pakkene til nissen og julaften står i fare! Alven etterlot seg et kart over med et rødt kryss midt på finnmarksvidda med tekst 'start here'. På baksiden av kartet er det instruksjoner som sier hvor du skal gå fra krysset. Du har fått som oppgave å hjelpe nissen med å finne pakkene og redde julaften.
//
// Skattekartet har veldig mange steg, men du ser kjapt at det bare består av 4 forskjellige instruksjoner, å gå x antall meter nord (north), sør (south), øst (east), eller vest (west). Du bestemmer deg for å lage et program som samler disse stegene og returnerer antall meter nord og antall meter vest, hvor et negativt tall betyr motsatt retning.
//
// Eksempel:
// walk 10 meters north
// walk 10 meters south
// walk 10 meters west
// walk 10 meters east
// walk 3 meters north
// walk 2 meters east
//
// gir resultatet:
// 3,-2
//
// Skattekart: http://pastebin.com/BZrAMcN2

#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#load "File.fs"

let file = File.download (System.Uri("http://pastebin.com/raw/BZrAMcN2"))

type Directions =  W | E | S | N

type Step = { Direction: Directions; Distance: int }

let translateDirection direction = 
    match direction with
    | "south" -> Directions.S
    | "east" -> Directions.E
    | "north" -> Directions.N
    | "west" -> Directions.W
    | _ -> failwith "Should not happen"

let createStep direction distance = 
    { Direction = (translateDirection direction); Distance = (int)distance }

let parsedLines = file.Split('\n') 
                    |> Seq.map (fun x -> x.Trim()) 
                    |> Seq.map (fun x -> x.Split(' '))
                    |> Seq.map (fun x -> createStep x.[3] x.[1])
                    |> Seq.toList

let getDistance line =
    match line.Direction with
    | (Directions.E | Directions.S) -> -line.Distance
    | _ -> line.Distance

let distance = 
    let distNorthSouth = parsedLines |> Seq.filter (fun x -> x.Direction = Directions.N || x.Direction = Directions.S) |> Seq.sumBy (fun x -> getDistance x)
    let distEastWest = parsedLines |> Seq.filter (fun x -> x.Direction = Directions.E || x.Direction = Directions.W) |> Seq.sumBy (fun x -> getDistance x)
    (distNorthSouth, distEastWest)