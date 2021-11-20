namespace ViewModels

open ReactiveUI
open System.Collections.ObjectModel
open Models

type HolstViewModel() =
    inherit ViewModelBase()

    let mutable list = new ObservableCollection<ArrayEl>()
    member __.Cells with get() = list

