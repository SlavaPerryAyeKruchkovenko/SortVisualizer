namespace Models

open System.Collections.Generic
open System.Linq

[<AbstractClass>]
type Sorter()=
    abstract member Sort: int [] -> int [] []

type ShakerSort() = 
    inherit Sorter()
    override this.Sort(array) =
        let mutable flag = true 
        let swap j =
            flag <- true
            let num = array.[j]
            array.[j] <- array.[j+1]
            array.[j+1] <- num    
            array.ToArray()

        [|for i in 0..array.Count()/2 do            
            if flag then
                flag <- false 
                for j in i .. (array.Count() - i - 2) do
                    if array.[j] > array.[j+1] then
                        yield swap j
                for j in (array.Count() - 2 - i) .. -1 ..i+1 do
                    if array.[j-1] > array.[j] then
                        yield swap(j-1)
                yield array|]