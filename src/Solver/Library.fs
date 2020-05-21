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
let is_valid (l:list<int>) = [1..9] |> List.forall(fun i -> (occurances_of i l) <= 1)

// Check if whole board is valid
let is_board_valid (l:list<int>) = [0..8] |> List.forall(fun i -> is_valid (row i l)) &&
                                   [0..8] |> List.forall(fun i -> is_valid (col i l)) &&
                                   [0..8] |> List.forall(fun i -> is_valid (cell i l))

// Check to see if list 'l' contains item 'i'
let rec contains i (l:list<int>) = match l with
                                   | [] -> false
                                   | head::tail -> (head = i) || (contains i tail)

// Check to see if list 'l' is 9 elements long and contains full range of 1-9
// (we can apply this to rows, columns or cells)
let is_complete (l:list<int>) =
    (l.Length = 9) && 
    [1..9] |> List.forall(fun i -> contains i l)

// Check to see if list 'l' (the board) is complete.
let is_board_complete (l:list<int>) =
    (l.Length = 9 * 9) &&
    [0..8] |> List.forall(fun i -> is_complete (row i l)) &&
    [0..8] |> List.forall(fun i -> is_complete (col i l)) &&
    [0..8] |> List.forall(fun i -> is_complete (cell i l))

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

// Reduce lists 'l1' and 'l2' to produce list containing only items from both lists
let rec in_both_lists (l1:list<int>) (l2:list<int>) = if ((l1 = []) || (l2 = [])) then []
                                                      elif (List.contains (List.head l1) l2) then (List.head l1)::(in_both_lists (List.tail l1) l2)
                                                      else (in_both_lists (List.tail l1) l2)

// Find empty positions in a cell
let find_zero_positions_in_cell i (l:list<int>) = let cell_number = (get_cell i)
                                                  let current_cell_indexes = cell_indexes cell_number
                                                  let all_zero_indexes = zero_positions l
                                                  let all_zero_indexes_in_current_cell = in_both_lists current_cell_indexes all_zero_indexes
                                                  all_zero_indexes_in_current_cell

// Get valid positions for number 'n' amongst the empty positions specified by 'zero_positions'
// on board 'l'
let rec get_valid_positions n (zero_positions:list<int>) (l:list<int>) = 
    if (zero_positions = [])
        then []
    elif (is_board_valid (replace_item_in_list l (List.head zero_positions) n))
        then (List.head zero_positions)::(get_valid_positions n (List.tail zero_positions) l)
        else (get_valid_positions n (List.tail zero_positions) l)

// Check if second element in tuple is array of length 1 and value matches that of 'i'
let check_second_element x i =
    let (a, b) = x
    ((List.length b) = 1) && (b.[0] = i)

// Find possible positions for missing numbers in a cell
let find_possible_positions_for_missing_numbers i (l:list<int>) =
    let all_zero_indexes_in_cell = find_zero_positions_in_cell i l
    let missing_numbers_in_cell = find_missing_numbers (cell (get_cell i) l)
    [for j in missing_numbers_in_cell -> (j, get_valid_positions j all_zero_indexes_in_cell l)]

// Gets possible numbers and filters down to items that have only 1 possible solution
let find_filtered_possible_numbers i (l:list<int>) =
    let possible_positions = find_possible_positions_for_missing_numbers i l
    let possible_positions_filtered = List.filter (fun x -> (check_second_element x i)) (possible_positions)                                
    [for (a, _) in possible_positions_filtered -> a]

// Find single possible numbers for all items that are currently zero
let find_single_possible_numbers (l:list<int>) =
    [for i in (zero_positions l) -> (i, find_filtered_possible_numbers i l)]

// Find easy solutions on board 'l'
let findEasySolutions (l:list<int>) = 
    [for i in (single_cells l) -> (i, find_missing_numbers (get_taken_values i l))]

// Find medium solutions on board 'l'
let findMediumSolutions (l:list<int>) = 
    List.filter(fun (a, b) -> List.length b <> 0) (find_single_possible_numbers l)
                                        
