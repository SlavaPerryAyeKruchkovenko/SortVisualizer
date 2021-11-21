namespace ViewModels

open Avalonia.Controls
open Models

type MainWindowViewModel() =
    inherit ViewModelBase()
    let holst = new HolstViewModel()
    let menu = new MenuVisualizerViewModel()
    member __.Holst with get() = holst
    member __.Menu with get() = menu
    member this.Sort() =
        holst.Visualize(menu.Array |> List.toArray,new ShakerSort(),menu.Delay)