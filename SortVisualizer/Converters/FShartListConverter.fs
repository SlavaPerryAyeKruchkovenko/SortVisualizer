namespace Converters
open Avalonia.Data.Converters
open System.Globalization
open System.Linq
open System
open System.Text
open Avalonia.Media

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
                        | _ -> [30;20;10]
            upcast answer   

type ColorConverter()=
    interface IValueConverter with
        member this.Convert(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj = 
            let isSelect:bool = downcast value 
            if isSelect then
                upcast Brushes.Red
            else
                upcast Brush.Parse "#f4a460"
        member this.ConvertBack(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj = 
            upcast false

