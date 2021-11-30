namespace ViewModels

open System.Collections.ObjectModel
open Models
open System.Threading.Tasks
open Avalonia
open Avalonia.Threading
open System.Threading
open System.Diagnostics
open System.Linq

type HolstViewModel() =
    inherit ViewModelBase()

    let list = new ObservableCollection<ArrayEl>()    
    member __.Array with get() = list
    member val Height = 0.0 with get,set
    member val Weight = 0.0 with get,set
    member this.Visualize(valueList,sorter:Sorter,delay:int,token:CancellationTokenSource)=
        let cnvrtToArray list height maxHeight=
            let elWeight = this.Weight / float(Seq.length list * 2)
            let mutable x = elWeight / 2.0
            let mutable y = 0.0
            [|for value in list do
                let size = 
                    if value > 20 && value < int maxHeight then 
                        y <- height - float value
                        new Size(elWeight,float value)                  
                    elif value > int maxHeight then 
                        y <- 0.0
                        new Size(elWeight,height)
                    else 
                        y <- height - 20.0
                        new Size(elWeight,20.0)
                new ArrayEl(value,new Point(x,y),size)
                x <- x + elWeight*2.0|]
        list.Clear()
        if sorter :? MergeSort then
            async{
                let height = this.Height / 2.0              
                let defaultArr = cnvrtToArray valueList height height
                let mutable lastArr = defaultArr
                defaultArr |> Array.map(fun x -> lock list (fun () -> list.Add x)) |> ignore

                do! Task.Delay delay |> Async.AwaitTask

                for arr in sorter.Sort valueList do
                    if not token.IsCancellationRequested then
                        defaultArr |> Array.map(fun x-> x.Condition <- Conditions.defaultCond) |> ignore
                        Dispatcher.UIThread.InvokeAsync(fun () -> list.Clear()).Wait()
                        let newArr = cnvrtToArray arr this.Height height 
                        sorter.AddVisual([|newArr;lastArr;defaultArr|])    

                        for el in Array.concat([defaultArr;newArr]) do      
                            Dispatcher.UIThread.InvokeAsync(fun () -> list.Add el).Wait()

                        do! Task.Delay delay |> Async.AwaitTask   
                        lastArr <- newArr 
                token.Cancel()
            } |> Async.Start 
        else
            async{
                let mutable lastArr = cnvrtToArray valueList this.Height this.Height
                lastArr |> Array.map( fun x -> lock list (fun () -> list.Add x)) |> ignore

                do! Task.Delay delay |> Async.AwaitTask

                for arr in sorter.Sort valueList do         
                    if not token.IsCancellationRequested then
                        Dispatcher.UIThread.InvokeAsync(fun () -> list.Clear()).Wait() |> ignore  
                        let newArr = cnvrtToArray arr this.Height this.Height
                        sorter.AddVisual([|newArr;lastArr|])

                        for el in newArr do                      
                            Dispatcher.UIThread.InvokeAsync(fun () -> list.Add el).Wait() |> ignore   

                        do! Task.Delay delay |> Async.AwaitTask   
                        lastArr <- newArr    
                token.Cancel()
            } |> Async.Start
            
    (*member this.Instalize() =
        list.Clear()
        let elWeight = this.Weight / float(valueList.Count() * 2)
        let mutable x = 0.0
        let mutable y = 0.0
        for value in valueList do
            y <- this.Height - float value
            list.Add(new ArrayEl(value,new Point(x,y),elWeight))
            x <- x+elWeight*2.0*)