module File

open System
open FSharp.Data

let download (uri: Uri) =
    Http.RequestString (uri.ToString())
