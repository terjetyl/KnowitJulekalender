// Kongen av Indonesia har som tradisjon å sende sine julehilsener kryptert til sine venner. I år skjedde det en glipp og kongen sendte meldingen til alle i hele verden med en email adresse, vi har også fått meldingen og trenger hjelp til å dekryptere den. Med meldingen fulgte også følgende instruksjoner på hvordan den kan dekrypteres:
//
// For å dekryptere meldingen må man først legge sammen parene i listen, ett par er første og siste element, andre og nest siste element og så videre. Når du har alle verdiene kan du oversette disse til bokstaver, hvor a = 1 og z = 26.
// Kryptertmelding: http://pastebin.com/xfX3msCL

#r @"..\packages\FSharp.Data.2.3.2\lib\net40\FSharp.Data.dll"
#load "File.fs"

open System

let uri = new Uri("http://pastebin.com/raw/xfX3msCL");
let file = File.download uri

let romanToNumber roman =
    match roman with
    | "0" -> 0
    | "I" -> 1
    | "II" -> 2
    | "III" -> 3
    | "IV" -> 4
    | "V" -> 5
    | "VI" -> 6
    | "VII" -> 7
    | "VIII" -> 8
    | "IX" -> 9
    | "X" -> 10
    | "XI" -> 11
    | "XII" -> 12
    | "XIII" -> 13
    | "XIV" -> 14
    | "XV" -> 15
    | _ -> raise (ArgumentException("Unknown literal: " + roman))

let alfabet = ["0"; "a"; "b"; "c"; "d"; "e"; "f"; "g"; "h"; "i"; "j"; "k"; "l"; "m"; "n"; "o"; "p"; "q"; "r"; "s"; "t"; "u"; "v"; "w"; "x"; "y"; "z"]

let romans = file.Trim(' ', '[', ']').Split(',')

let nums = romans |> Seq.map (fun x -> romanToNumber (x.Trim())) |> Seq.toList

let numToLetter num = 
    alfabet.[num]

let decode = 
    nums 
    |> List.take (nums.Length / 2) 
    |> List.mapi (fun i x -> x + nums.[nums.Length - (i+1)]) 
    |> List.map numToLetter
    |> List.toArray

let answer = String.Join("", decode)