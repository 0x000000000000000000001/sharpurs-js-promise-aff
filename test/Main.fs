module Test.Main

open System
open System.Threading.Tasks

let helloPromise : obj = Task.FromResult("Hello" :> obj) :> obj

let goodbyePromise : obj =
    let tcs = TaskCompletionSource<obj>()
    tcs.SetException(new Exception("Goodbye"))
    tcs.Task :> obj

let errPromise : obj =
    let tcs = TaskCompletionSource<obj>()
    tcs.SetException(new Exception("err"))
    tcs.Task :> obj

type JSError(value: obj) =
    inherit Exception(if value = null then "null" else value.ToString())
    member this.Value = value

let customErrPromise : obj =
    let tcs = TaskCompletionSource<obj>()
    let map = Map.empty.Add("code", "err" :> obj)
    tcs.SetException(new JSError(map))
    tcs.Task :> obj


