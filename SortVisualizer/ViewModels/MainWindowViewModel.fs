namespace ViewModels

open Avalonia.Controls
open Models
open ReactiveUI
open System.Reactive
open System.Threading.Tasks
open System.IO
open System

type MainWindowViewModel() =
    inherit ViewModelBase()
    let holst = new HolstViewModel()
    let menu = new MenuVisualizerViewModel()
    member val isEx = true with get,set
    member __.Holst with get() = holst
    member __.Menu with get() = menu
    member __.Start = ReactiveCommand.Create<Unit>(__.Sort,__.WhenAnyValue(fun vm -> vm.isEx))
    member this.Sort() =
        this.isEx <- false
        let sorter = downcast Activator.CreateInstance(menu.Select)
        holst.Visualize(menu.Array |> List.toArray,sorter,menu.Delay)
        this.isEx <- true
        Unit.Default
        //return Task.Run( ()->)