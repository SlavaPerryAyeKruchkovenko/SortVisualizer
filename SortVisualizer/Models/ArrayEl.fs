namespace Models

open Avalonia

type ArrayEl(value,loc,weight) =
    member val Value = value with get
    member val Location: Point = loc with get
    member val MarginLoc:Thickness = new Thickness(loc.X,loc.Y) with get
    member val Weight:int = weight with get

