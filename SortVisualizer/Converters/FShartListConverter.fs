namespace Converters
open Avalonia.Data.Converters
open System.Globalization
open System.Linq
open System
open System.Text
open Avalonia.Media
open Models

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
            let cond:Conditions = downcast value 
            match cond with
            | Conditions.defaultCond -> upcast Brush.Parse "#f4a460"
            | Conditions.select -> upcast Brushes.Red
            | Conditions.left -> upcast Brushes.Blue
            | Conditions.right -> upcast Brushes.Black
            | _ -> upcast Brushes.Aqua
               
        member this.ConvertBack(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj = 
            upcast Conditions.defaultCond
type TypesConverter() =
    interface IValueConverter with
        member this.Convert(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj = 
            let t:Type= downcast value
            let str = t.ToString()
            let arr = str.Split('.')
            if arr.Length > 1 then
                upcast Array.last(arr)
            else
                upcast arr.[0]
        member this.ConvertBack(value: obj, targetType: Type, parameter: obj, culture: CultureInfo): obj = 
            value

