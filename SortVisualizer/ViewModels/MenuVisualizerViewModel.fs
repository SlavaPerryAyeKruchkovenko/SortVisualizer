namespace ViewModels

open ReactiveUI

type MenuVisualizerViewModel()=
    inherit ViewModelBase()
    let mutable array = [10;20;30]
    let mutable delay = 100
    let mutable sorts = 0
    member __.Array
        with get() = array
        and set(value) = __.RaiseAndSetIfChanged(&array, value) |> ignore
    member __.Delay 
        with get() = delay
        and set(value) = __.RaiseAndSetIfChanged(&delay, value) |> ignore
    member __.Sorts
        with get() = sorts
        and set(value) = __.RaiseAndSetIfChanged(&sorts, value) |> ignore
