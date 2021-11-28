namespace ViewModels

open ReactiveUI
open System.Reflection;
open System.Linq
open Models
type MenuVisualizerViewModel()=
    inherit ViewModelBase()
    let mutable array = [10;20;30]
    let mutable delay = 100
    let mutable sorts = Assembly.GetAssembly(typedefof<Sorter>).GetTypes().Where(fun t -> t.IsSubclassOf(typedefof<Sorter>));
    let mutable select = sorts.First()
    member __.Array
        with get() = array
        and set(value) = __.RaiseAndSetIfChanged(&array, value) |> ignore
    member __.Delay 
        with get() = delay
        and set(value) = __.RaiseAndSetIfChanged(&delay, value) |> ignore
    member __.Sorts
        with get() = sorts
        and set(value) = __.RaiseAndSetIfChanged(&sorts, value) |> ignore
    member __.Select 
        with get() = select
        and set(value) = if value <> null then __.RaiseAndSetIfChanged(&select, value) |> ignore
