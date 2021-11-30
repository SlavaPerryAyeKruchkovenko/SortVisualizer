namespace Models

open System.Collections.Generic
open System.Linq

[<AbstractClass>]
type Sorter()=
    abstract member Sort: int [] -> int [] []
    abstract member AddVisual: ArrayEl [] []-> unit
    

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
    override this.AddVisual(conditionArr) = 
        let newArr = conditionArr.[0]
        let lastArr = conditionArr.[1]
        let count = ref 0
        let value (num:'a ref) = num.Value
        let mutable isExist = false
        while count.Value < lastArr.Length && not isExist do
            let c = value count
            if newArr.[c].Value <> lastArr.[c].Value then
                newArr.[c].Condition <- Conditions.select
                newArr.[c+1].Condition <- Conditions.select
                isExist <- true
            incr count
type MergeSort() = 
    inherit Sorter()
    let mutable queue = new Queue<Conditions>()
    override this.Sort array =
        let mutable list = []            
        let rec sort array = 
            let merge left right = 
                let rec mrg left right cont =
                    match (left, right) with
                    | ([], right) -> cont right
                    | (left, []) -> cont left
                    | (x::left', y::right') ->
                        if x < y 
                        then 
                            let resL = mrg left' right (fun rs -> cont (x::rs))
                            list <- resL :: list 
                            queue.Enqueue Conditions.select
                            resL
                        else 
                            let resR = mrg left right' (fun rs -> cont (y::rs))
                            list<-resR::list 
                            queue.Enqueue Conditions.select
                            resR
                mrg left right id
            let count = array |> List.length
            if count > 1 then
                let mutable left = array |> List.take(count/2)
                let mutable right = array |> List.skip(count/2)
                list <- left :: list
                queue.Enqueue Conditions.left
                list <- right :: list
                queue.Enqueue Conditions.right

                if left.Length > 1 then
                    left <- sort left

                if right.Length > 1 then
                    right <- sort right                    
                merge left right
            else
                list <- array :: list   
                queue.Enqueue Conditions.defaultCond
                array
        let newArr = array |> Array.toList
        sort newArr|> ignore
        list |> List.rev |> List.toArray |> Array.map(fun x-> x |> List.toArray)
    override this.AddVisual(conditionArr) = 
        let newArr = conditionArr.[0]
        let lastArr = conditionArr.[1]
        let defaultArr = conditionArr.[2]        

        let cond = queue.Dequeue()
        if cond = Conditions.select then
            let count = ref 0
            let value (num:'a ref) = num.Value
            let mutable isExist = false
            while count.Value < lastArr.Length && not isExist do
                let c = value count
                if newArr.[c].Value = lastArr.[c].Value then
                    newArr.[c].Condition <- Conditions.select
                    newArr.[c+1].Condition <- Conditions.select
                    isExist <- true
                incr count
        else
            for el in newArr do
                el.Condition <- cond
            for el in defaultArr do
                for newEl in newArr do
                    if el.Value = newEl.Value then
                        el.Condition <- Conditions.select