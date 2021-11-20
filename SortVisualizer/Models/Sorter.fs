namespace Models

open System.Collections.Generic
open System.Linq

[<AbstractClass>]
type Sorter()=
    abstract member Sort: ArrayEl [] -> ArrayEl [] seq

type ShakerSort() = 
    inherit Sorter()
    override this.Sort(array) =
        let mutable flag = false 
        let swap j =
            flag <- true
            let num = array.[j]
            array.[j] <- array.[j+1]
            array.[j+1] <- num         
            array

        seq{for i in 0..array.Count()/2 do            
                if flag then
                    flag <- false 
                    for j in 0 .. (array.Count() - i - 1) do
                        if array.[j].Value > array.[j+1].Value then
                            yield swap j
                    for j in (array.Count() - 2 - i) .. -1 ..0 do
                        if array.[j-1].Value > array.[j].Value then
                            yield swap(j-1)}