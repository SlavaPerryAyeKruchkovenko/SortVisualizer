namespace ViewModels

open ReactiveUI
open System.Collections.ObjectModel
open Models
open System.Linq
open System.Threading.Tasks
open Avalonia
open System.Collections.Generic
open Avalonia.Threading

type HolstViewModel() =
    inherit ViewModelBase()

    let mutable list = new ObservableCollection<ArrayEl>()
    member __.Array with get() = list
    member val Height = 0.0 with get,set
    member val Weight = 0.0 with get,set
    member this.Visualize(valueList:int [],sorter:Sorter,delay:int) =
        let task =async{
            let a = sorter.Sort(valueList)
            for arr in a do
                Dispatcher.UIThread.InvokeAsync(fun () -> list.Clear()).Wait() |> ignore             
                for el in arr do                      
                    Dispatcher.UIThread.InvokeAsync(fun () -> list.Add(el)).Wait() |> ignore            
                    do!Task.Delay(delay) |> Async.AwaitTask
        }
        task |> Async.Start
    (*member this.Instalize() =
        list.Clear()
        let elWeight = this.Weight / float(valueList.Count() * 2)
        let mutable x = 0.0
        let mutable y = 0.0
        for value in valueList do
            y <- this.Height - float value
            list.Add(new ArrayEl(value,new Point(x,y),elWeight))
            x <- x+elWeight*2.0*)