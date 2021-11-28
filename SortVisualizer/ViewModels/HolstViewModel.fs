namespace ViewModels

open System.Collections.ObjectModel
open Models
open System.Threading.Tasks
open Avalonia
open Avalonia.Threading

type HolstViewModel() =
    inherit ViewModelBase()

    let list = new ObservableCollection<ArrayEl>()    
    member __.Array with get() = list
    member val Height = 0.0 with get,set
    member val Weight = 0.0 with get,set
    member this.Visualize(valueList:int [],sorter:Sorter,delay:int)=
        let cnvrtToArray list =
            let elWeight = this.Weight / float(valueList.Length * 2)
            let mutable x = elWeight / 2.0
            let mutable y = 0.0
            [|for value in list do
                let size = 
                    if value > 20 && value < int this.Height then 
                        y <- this.Height - float value
                        new Size(elWeight,float value)                  
                    elif value > int this.Height then 
                        y <- 0.0
                        new Size(elWeight,this.Height)
                    else 
                        y <- this.Height - 20.0
                        new Size(elWeight,20.0)
                new ArrayEl(value,new Point(x,y),size)
                x <- x + elWeight*2.0|]

        let task = async{
            let mutable lastArr = cnvrtToArray valueList 
            lastArr |> Array.map( fun x -> lock list (fun () -> list.Add x)) |> ignore

            for arr in sorter.Sort valueList do                
                Dispatcher.UIThread.InvokeAsync(fun () -> list.Clear()).Wait() |> ignore  
                let newArr = cnvrtToArray arr
                sorter.AddVisual(newArr,lastArr)
                for el in newArr do                      
                    Dispatcher.UIThread.InvokeAsync(fun () -> list.Add el).Wait() |> ignore   
                    do! Task.Delay delay |> Async.AwaitTask
                lastArr <- newArr    
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