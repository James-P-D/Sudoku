# Sudoku
A Sudoku puzzle solver with algorithm in F# and UI in C# Winforms

![Screenshot](https://github.com/James-P-D/Sudoku/blob/master/Screenshot.gif)

## Method

Most Sudoku solvers I came across simply chose to implement [Peter Norvig's method](https://norvig.com/sudoku.html), but I decided I'd rather use the same method I use when solving the puzzle with pen-and-paper.

### Easy Cells

The easiest step for finding the value for a particular cell, is to find an empty cell and then figure out what it *can't* be.

For example, lets consider the following board taken from the [Guardian Easy Sudoku #4,612 from Mon 18 Nov, 2019](https://www.theguardian.com/lifeandstyle/2019/nov/18/sudoku-4612-easy).

![Easy Cells Screenshot](https://github.com/James-P-D/Sudoku/blob/master/Easy.gif)

In the above initial board, concentrate on the 9x9 square in the middle-left of the board, and in this square, look at the cell in the very centre. We can instantly tell that value of this cell can't be `3 or 5` because of other values in the 9x9 square. We can also tell that is can't be `1, 3 or 6` because of other cells in the same column. Finally, from analysing the values in the rest of the row we can also tell that it can't be `2, 3, 7, 8, or 9`. Putting all this information together and we know the cell can't be `1, 2, 3, 5, 6, 7, 8 or 9`, and must therefore be `4`.

### Medium Cells

Having found all the easy cells, we find ourselves with the following board:

![Medium Cells Screenshot](https://github.com/James-P-D/Sudoku/blob/master/Medium.gif)

There are no more easy solutions to be found, but let's consider the central 9x9 square, in particular, the empty cell at the bottom. 