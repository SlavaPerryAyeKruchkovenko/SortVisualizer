namespace Models

open System.Collections.Generic
open System.Linq

[<AbstractClass>]
type Sorter()=
    abstract member Sort: int [] -> int [] []
    abstract member AddVisual: ArrayEl [] * ArrayEl [] -> unit

type ShakerSort() = 
    inherit Sorter()
    override this.Sort array =
        let mutable flag = true 
        let swap j =
            flag <- true
            let num = array.[j]
            array.[j] <- array.[j+1]
            array.[j+1] <- num    
            array |> Seq.toArray

        [|for i in 0..array.Count()/2 do            
            if flag then
                flag <- false 
                for j in i .. (array.Count() - i - 2) do
                    if array.[j] > array.[j+1] then
                        yield swap j
                for j in (array.Count() - 2 - i) .. -1 ..i+1 do
                    if array.[j-1] > array.[j] then
                        yield swap(j-1)|]
    override this.AddVisual(newArr,lastArr) = 
        let count = ref 0
        let value (num:'a ref) = num.Value
        let mutable isExist = false
        while count.Value < lastArr.Length && not isExist do
            let c = value count
            if newArr.[c].Value <> lastArr.[c].Value then
                newArr.[c].IsSelect <- true
                newArr.[c+1].IsSelect <- true
                isExist <- true
            incr count

type MergeSort() = 
    inherit Sorter()
    override this.Sort array =
        let mutable list = [[]]    
        let rec sort array = 
            let merge left right = 
                let rec mrg left right cont =
                    match (left, right) with
                    | ([], right) -> cont right
                    | (left, []) -> cont left
                    | (x::left', y::right') ->
                        if x < y 
                        then mrg left' right (fun rs -> cont (x::rs))
                        else mrg left right' (fun rs -> cont (y::rs))
                mrg left right id
            let count = array |> List.length
            if count > 1 then
                let mutable left = array |> List.take(count/2)
                let mutable right = array |> List.skip(count/2)
                if left.Length > 1 then
                    left <- sort left
                else
                    list <- left :: list
                if right.Length > 1 then
                    right <- sort right
                else
                    list <- right :: list
                let result = merge left right
                list <- result :: list
                result
            else
                list <- array :: list   
                array
        let newArr = array |> Array.toList
        sort newArr|> ignore
        list |> List.rev |> List.toArray |> Array.map(fun x-> x |> List.toArray)
    override this.AddVisual(newtArr,lastArr) = 
        ()