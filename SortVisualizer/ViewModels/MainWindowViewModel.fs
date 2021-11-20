namespace ViewModels

open Avalonia.Controls

type MainWindowViewModel() =
    inherit ViewModelBase()
    let holst = new HolstViewModel()
    let menu = new MenuVisualizerViewModel()
    member __.Holst with get() = holst
    member __.Menu with get() = menu
    member this.Sort() =
        holst.Instalize(menu.Array)