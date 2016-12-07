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

open FSharp.Data

let file = Http.RequestString "http://pastebin.com/raw/BZrAMcN2"

type Direction = 
    | W
    | E
    | S
    | N

type Line = { Direction: Direction; Distance: int }

let translateDirection direction = 
    match direction with
    | "south" -> Direction.S
    | "east" -> Direction.E
    | "north" -> Direction.N
    | "west" -> Direction.W
    | _ -> failwith "Should not happen"

let createLine direction distance = 
    { Direction = (translateDirection direction); Distance = (int)distance }

let lines = file.Split('\n') 
            |> Seq.map (fun x -> x.Trim()) 
            |> Seq.map (fun x -> x.Split(' '))
            |> Seq.map (fun x -> createLine x.[3] x.[1])
            |> Seq.toList

let getDistance line =
    match line.Direction with
    | Direction.E -> -line.Distance
    | Direction.S -> -line.Distance
    | _ -> line.Distance

let distance = 
    let distNorthSouth = lines |> Seq.filter (fun x -> x.Direction = Direction.N || x.Direction = Direction.S) |> Seq.sumBy (fun x -> getDistance x)
    let distEastWest = lines |> Seq.filter (fun x -> x.Direction = Direction.E || x.Direction = Direction.W) |> Seq.sumBy (fun x -> getDistance x)
    (distNorthSouth, distEastWest)