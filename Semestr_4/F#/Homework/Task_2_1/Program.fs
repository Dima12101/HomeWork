module public Task_2_1 

let public findValue value list =
        let rec _findValue _index _value _list =
            match _list with
            | [] -> -1
            | headEl :: tailL -> if (headEl = _value) then _index else _findValue (_index + 1) _value tailL
        _findValue 0 value list
    
let public testFunc value = value + 1
(*
let value = 5
let list = [5; 6; 7; 9]
printfn "value %i finded on %i index" value (findValue value list) *)
