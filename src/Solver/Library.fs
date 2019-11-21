//namespace Solver

//module Say =
 //   let hello name =
  //      printfn "Hello %s" name
module Library

// Get an array of integers for a given row 'i' in a given board 'l'
let row i (l:list<int>) = [for j in 0..8 -> l.[(i * 9) + j]]

// Get an array of integers for a given column 'i' in a given board 'l'
let col i (l:list<int>) = [for j in 0..8 -> l.[(j * 9) + i]]

// Get an array of integers for a given cell 'i' in a given board 'l'
let cell i (l:list<int>) = [for j in [0; 1; 2; 9; 10; 11; 18; 19; 20] -> l.[(((i / 3) * 9) * 3) + ((i % 3) * 3) + j]]

// Get the row number for an individual item
let getRow i = i / 9

// Get the column number for an individual item
let getCol i = i % 9

// Get the cell number for an individual item
// Todo: Fix this into a forumula!!
let getCell i = match i with
                | 00 | 01 | 02 | 09 | 10 | 11 | 18 | 19 | 20 -> 0
                | 03 | 04 | 05 | 12 | 13 | 14 | 21 | 22 | 23 -> 1
                | 06 | 07 | 08 | 15 | 16 | 17 | 24 | 25 | 26 -> 2
                | 27 | 28 | 29 | 36 | 37 | 38 | 45 | 46 | 47 -> 3
                | 30 | 31 | 32 | 39 | 40 | 41 | 48 | 49 | 50 -> 4
                | 33 | 34 | 35 | 42 | 43 | 44 | 51 | 52 | 53 -> 5                
                | 54 | 55 | 56 | 63 | 64 | 65 | 72 | 73 | 74 -> 6
                | 57 | 58 | 59 | 66 | 67 | 68 | 75 | 76 | 77 -> 7
                | 60 | 61 | 62 | 69 | 70 | 71 | 78 | 79 | 80 -> 8
                | _ -> -1

// Check to see if list 'l' contains item 'i'
let rec contains i (l:list<int>) = match l with
                                   | [] -> false
                                   | head::tail -> (head = i) || (contains i tail)

// Check to see if list 'l' is 9 elements long and contains full range of 1-9
// (we can apply this to rows, columns or cells)
let isComplete (l:list<int>) = (l.Length = 9) && 
                               [1..9] |> List.forall(fun i -> contains i l)

// Check to see if list 'l' (the board) is complete.
let isBoardisComplete (l:list<int>) = (l.Length = 9 * 9) &&
                                      [0..8] |> List.forall(fun i -> isComplete (row i l)) &&
                                      [0..8] |> List.forall(fun i -> isComplete (col i l)) &&
                                      [0..8] |> List.forall(fun i -> isComplete (cell i l))

// Get the positions in the board 'l' which currently have the value zero (unassigned)
let zeroPositions (l:list<int>) = [0..(l.Length-1)] |> List.where(fun i -> (l.[i] = 0))

// Get the list of numbers currently taken for a particular item 'i' in board 'l'
let getTakenValues i (l:list<int>) =  List.distinct((row (getRow i) l) @ (col (getCol i) l) @ (cell (getCell i) l)) |> List.where(fun i -> i <> 0)

// Get the list of missing numbers for row/column/cell 'l'
let findMissingNumbers (l:list<int>) = [1..9] |> List.where(fun i -> not (contains i l))

let singleCells (l:list<int>) = zeroPositions l |> List.where(fun i -> ((getTakenValues i l).Length = 8))

let solutions (l:list<int>) = [for i in singleCells l -> (i, findMissingNumbers (getTakenValues i l))]
