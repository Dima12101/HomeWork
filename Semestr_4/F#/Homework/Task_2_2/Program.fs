module public Task_2_2 

let rec findValue index value list =
    match list with
    | [] -> -1
    | headEl :: tailL -> if (headEl = value) then index else findValue (index + 1) value tailL
    
let value = 7
let list = [3; 7; 8; 9; 10]
printfn "value %i finded on %i index" value (findValue 0 value list)


let rec sortList list resultList = 
    match list with
    | [] -> resultList
    | headEl :: tailL -> let rec putValue value accList tempList = 
                            match tempList with
                            | [] -> value :: accList
                            | headElTempL :: tailTempL -> if (value > headElTempL) then putValue value (headElTempL::accList) tailTempL else  (headElTempL::value::accList)@tailTempL
                         sortList tailL (putValue headEl [] resultList)
let tlist = [5; 4; 9; 6; 8]                
printfn "Sorted list: %A" (sortList tlist [])