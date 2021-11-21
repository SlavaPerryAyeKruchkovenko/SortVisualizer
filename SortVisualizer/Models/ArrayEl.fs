namespace Models

open Avalonia

type ArrayEl(value,loc,weight) =
    let mutable location:Point = loc
    member val Value:int = value with get,set   
    member val Weight:float = weight with get,set
    member __.Location 
        with get() = location 
        and set(value) = location <- value 
    member __.MarginLoc with get() = new Thickness(location.X,location.Y)  
    member val IsSelect = false with get,set

