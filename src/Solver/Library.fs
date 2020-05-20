//namespace Solver

module Library

// Get an array of integers indexing the items for a particular row
let row_indexes i = [for j in 0..8 -> ((i * 9) + j)]

// Get an array of integers indexing the items for a particular column
let col_indexes i = [for j in 0..8 -> ((j * 9) + i)]

// Get an array of integers indexing the items for a particular cell
let cell_indexes i = [for j in [0; 1; 2; 9; 10; 11; 18; 19; 20] -> ((((i / 3) * 9) * 3) + ((i % 3) * 3) + j)]

// Get an array of integers for a given row 'i' in a given board 'l'
let row i (l:list<int>) = [for j in (row_indexes i) -> l.[j]]

// Get an array of integers for a given column 'i' in a given board 'l'
let col i (l:list<int>) = [for j in (col_indexes i) -> l.[j]]

// Get an array of integers for a given cell 'i' in a given board 'l'
let cell i (l:list<int>) = [for j in (cell_indexes i) -> l.[j]]

// Get the row number for an individual item
let get_row i = i / 9

// Get the column number for an individual item
let get_col i = i % 9

// Get the cell number for an individual item
// TODO: Fix this into a forumula!!
let get_cell i = if (List.contains i (cell_indexes 0)) then 0
                 elif (List.contains i (cell_indexes 1)) then 1
                 elif (List.contains i (cell_indexes 2)) then 2
                 elif (List.contains i (cell_indexes 3)) then 3
                 elif (List.contains i (cell_indexes 4)) then 4
                 elif (List.contains i (cell_indexes 5)) then 5
                 elif (List.contains i (cell_indexes 6)) then 6
                 elif (List.contains i (cell_indexes 7)) then 7
                 elif (List.contains i (cell_indexes 8)) then 8
                 else -1

// Returns number of occurances of 'item' in list 'l'
let occurances_of (item:int) (l:list<int>) = l |> Seq.filter ((=) item) |> Seq.length

// Checks if a list (row, column or square) is valid (altough not necessarily
// complete, hence why we do not check for occurances of zero)
let is_valid (l:list<int>) = (occurances_of 1 l) <= 1 &&
                             (occurances_of 2 l) <= 1 &&
                             (occurances_of 3 l) <= 1 &&
                             (occurances_of 4 l) <= 1 &&
                             (occurances_of 5 l) <= 1 &&
                             (occurances_of 6 l) <= 1 &&
                             (occurances_of 7 l) <= 1 &&
                             (occurances_of 8 l) <= 1 &&
                             (occurances_of 9 l) <= 1

let is_valid2 (l:list<int>) = [1..9] |> List.forall(fun i -> (occurances_of i l) <= 1)

// Check to see if list 'l' contains item 'i'
let rec contains i (l:list<int>) = match l with
                                   | [] -> false
                                   | head::tail -> (head = i) || (contains i tail)
//
//// Check to see if list 'l' is 9 elements long and contains full range of 1-9
//// (we can apply this to rows, columns or cells)
//let isComplete (l:list<int>) = (l.Length = 9) && 
//                               [1..9] |> List.forall(fun i -> contains i l)
//
//// Check to see if list 'l' (the board) is complete.
//let isBoardisComplete (l:list<int>) = (l.Length = 9 * 9) &&
//                                      [0..8] |> List.forall(fun i -> isComplete (row i l)) &&
//                                      [0..8] |> List.forall(fun i -> isComplete (col i l)) &&
//                                      [0..8] |> List.forall(fun i -> isComplete (cell i l))

// Get the positions in the board 'l' which currently have the value zero (unassigned)
let zero_positions (l:list<int>) = [0..(l.Length-1)] |> List.where(fun i -> (l.[i] = 0))

// Get the list of numbers currently taken for a particular item 'i' in board 'l'
let get_taken_values i (l:list<int>) =  List.distinct((row (get_row i) l) @ (col (get_col i) l) @ (cell (get_cell i) l)) |> List.where(fun i -> i <> 0)

// Get the list of missing numbers for row/column/cell 'l'
let find_missing_numbers (l:list<int>) = [1..9] |> List.where(fun i -> not (contains i l))

// Get the list of cells where the number of number of taken values is already 8
let single_cells (l:list<int>) = zero_positions l |> List.where(fun i -> ((get_taken_values i l).Length = 8))

// In list 'l' change item at index 'i' to value 'x' (need 'n' for counting)
let rec replace_item_in_list_rec (l:list<int>) i x n = if l = [] then []
                                                       elif i = n then x::(replace_item_in_list_rec (List.tail l) i x (n + 1))
                                                       else (List.head l)::(replace_item_in_list_rec (List.tail l) i x (n + 1))
// In list 'l' change item at index 'i' to value 'x'
let replace_item_in_list (l:list<int>) i x = replace_item_in_list_rec l i x 0


let find_possible_numbers i (l:list<int>) = let cell_number = (get_cell i)
                                            printfn "i = %d square = %d" i cell_number
                                            let cell_item_index = cell_indexes cell_number
                                            printfn "%A" cell_item_index
                                            let existing_numbers_in_cell = cell cell_number l
                                            let misNos = find_missing_numbers(existing_numbers_in_cell)
                                            printfn "missing numbers for square %d are %A" cell_number misNos
                                            printfn "-------------------------------------"
                                            i

let rec in_both_lists (l1:list<int>) (l2:list<int>) = if ((l1 = []) || (l2 = [])) then []
                                                      elif (List.contains (List.head l1) l2) then (List.head l1)::(in_both_lists (List.tail l1) l2)
                                                      else (in_both_lists (List.tail l1) l2)

let find_zero_positions_in_cell i (l:list<int>) = let cell_number = (get_cell i)
                                                  let current_cell_indexes = cell_indexes cell_number
                                                  let all_zero_indexes = zero_positions l
                                                  let all_zero_indexes_in_current_cell = in_both_lists current_cell_indexes all_zero_indexes
                                                  all_zero_indexes_in_current_cell

let do_stuff i (l:list<int>) = let all_zero_indexes_in_cell = find_zero_positions_in_cell i l
                               printfn "items in cell that are zero = %A" all_zero_indexes_in_cell
                               let missing_numbers_in_cell = find_missing_numbers (cell (get_cell i) l)
                               printfn "missing numbers in cell = %A" missing_numbers_in_cell
                               -1

//////////////////////////////////////////////////////////////////////////////////////////////////////////

let findEasySolutions (l:list<int>) = [for i in (single_cells l) -> (i, find_missing_numbers (get_taken_values i l))]

//[for i in [48] -> (i, find_possible_numbers i l)]
//let findMediumSolutions (l:list<int>) = [for i in zeroPositions l -> (i, findPossibleNumbers i l)]
let findMediumSolutions (l:list<int>) = do_stuff 48 l // [for i in [48] -> (i, find_possible_numbers i l)]