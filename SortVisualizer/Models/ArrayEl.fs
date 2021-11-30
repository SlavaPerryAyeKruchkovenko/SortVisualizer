namespace Models

open Avalonia
type Conditions =
    | defaultCond = 0
    | left = 1
    | right = 2
    | select = 3
type ArrayEl(value,loc,size) =
    let mutable location:Point = loc
    member val Value:int = value with get,set   
    member val Size:Size = size with get,set
    member __.Location 
        with get() = location 
        and set(value) = location <- value 
    member __.MarginLoc with get() = new Thickness(location.X,location.Y)  
    member val Condition = Conditions.defaultCond with get,set
    member val Key = 0 with get,set

