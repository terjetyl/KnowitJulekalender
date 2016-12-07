// Finn det minste naturlige tallet som ender på 6 og som har følgende egenskap:
// - hvis man fjerner det siste tallet og plasserer det først så blir tallet fire ganger så stort som det opprinnelige tallet.

open System

let startsWith6 num = 
    num.ToString().StartsWith("6")

let swapLastNum (num: int) = 
    num, (num.ToString().Substring(1) + num.ToString().Substring(0, 1) |> Int32.Parse)

let fourTimesBigger (swapped, org) = 
    swapped = (org * 4) 

let answer = 
    {60..1000000} 
    |> Seq.filter startsWith6
    |> Seq.map swapLastNum
    |> Seq.find fourTimesBigger