namespace Converters
open Avalonia.Data.Converters
open System.Globalization
open System.Linq
open System
open System.Text

type FShartListConverter() =
    interface IValueConverter with
        member this.Convert(value: obj, targetType: System.Type, parameter: obj, culture: CultureInfo): obj =          
            let list: int list = downcast value
            let mutable str = new StringBuilder()
            if list.Count() >= 2 then
                for y in list.Take(list.Count()-1) do 
                    str <- str.Append($"{y.ToString()}, ")
            str <- str.Append(list.Last().ToString())
            upcast str.ToString()            

        member this.ConvertBack(value: obj, targetType: System.Type, parameter: obj, culture: CultureInfo): obj =
            let str:string = downcast value
            let answer= try
                            str.Split(",") |> Array.map(fun x-> Int32.Parse(x.Trim())) |> Array.toList
                        with
                        | _ -> [0]
            upcast answer   

