
module TestOnTask2_1 
    open NUnit.Framework
    open FsUnit

    let [<Test>] ``Find first element`` () =
                Task_2_1.findValue 10 [10; 4; 7; 8; 9] |> should equal 0

    let [<Test>] ``Find middle element`` () =
                Task_2_1.findValue 7 [10; 4; 7; 8; 9] |> should equal 2

    let [<Test>] ``Find last element`` () =
                Task_2_1.findValue 9 [10; 4; 7; 8; 9] |> should equal 4

    let [<Test>] ``Find not exist element`` () =
                Task_2_1.findValue 11 [10; 4; 7; 8; 9] |> should equal -1