namespace Views

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml

type HolstView () as this = 
    inherit UserControl ()
    let mutable bound = new Rect()
    do this.InitializeComponent()
    
    member private this.InitializeComponent() =
        AvaloniaXamlLoader.Load(this)
    member this.ChangeSize(object:obj,e:AvaloniaPropertyChangedEventArgs) =
        if object :? Canvas then
            let canvas:Canvas = downcast object
            if bound <> canvas.Bounds then
                bound <- canvas.Bounds 
                canvas.Width <- bound.Width
                canvas.Height <- bound.Height