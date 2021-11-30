namespace ViewModels

open Avalonia.Controls
open Models
open ReactiveUI
open System.Reactive
open System.Threading
open System

type MainWindowViewModel()=
    inherit ViewModelBase()
    let holst = new HolstViewModel()
    let menu = new MenuVisualizerViewModel()  
    member val Token:CancellationTokenSource = null with get,set   
    member __.Holst with get() = holst
    member __.Menu with get() = menu
    member __.Start = ReactiveCommand.Create<Unit> __.Sort
    member this.Sort() =      
        if this.Token = null then
            this.Token <- new CancellationTokenSource()
            this.Token.Cancel()
        if this.Token.IsCancellationRequested then
            this.Token <- new CancellationTokenSource()
            let sorter = downcast Activator.CreateInstance menu.Select
            holst.Visualize(menu.Array |> List.toArray,sorter,menu.Delay,this.Token)
        Unit.Default
        //return Task.Run( ()->)