// Fibonaccirekken er en tallrekke som genereres ved at man adderer de to foregående tallene i rekken. f.eks. om man starter med 1 og 2 blir de første 10 termene 1, 1, 2, 3, 5, 8, 13, 21, 34 og 55
// Finn summen av alle partall i denne rekken som er mindre enn 4.000.000.000
open System
open System.Numerics

let fib = 
    Seq.unfold (fun (a,b) -> Some(a+b, (b, a+b))) (0L,1L)

let partall num = 
    num % 2L = 0L

let answer = 
    fib
    |> Seq.takeWhile (fun x -> x < 4000000000L)
    |> Seq.filter partall
    |> Seq.sum