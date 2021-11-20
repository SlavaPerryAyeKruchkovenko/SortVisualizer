namespace ViewModels

open ReactiveUI
open System.Collections.ObjectModel
open Models
open System.Linq
open System.Threading.Tasks
open Avalonia
open System.Collections.Generic

type HolstViewModel() =
    inherit ViewModelBase()

    let mutable list = new ObservableCollection<ArrayEl>()
    member __.Array with get() = list
    member val Height = 0.0 with get,set
    member val Weight = 0.0 with get,set
    member this.Visualize(sorter:Sorter,delay:int) =
        async{
            for arr in sorter.Sort(list.ToArray()) do
                list.Clear()
                for el in arr do
                    list.Add(el) 
                do! Task.Delay(delay)|> Async.AwaitTask 
            
        }|> Async.Start
    member this.Instalize(valueList:int IEnumerable) =
        let elWeight = this.Weight / float(valueList.Count() * 2)
        let mutable x = 0.0
        let mutable y = 0.0
        for value in valueList do
            y <- this.Height - float value
            list.Add(new ArrayEl(value,new Point(x,y),elWeight))
            x <- elWeight*2.0