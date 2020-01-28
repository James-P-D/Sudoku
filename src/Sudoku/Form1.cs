using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.FSharp.Collections;

namespace Sudoku
{
    public partial class Form1 : Form
    {
        private const int TEXTBOX_WIDTH = 20;
        private const int TEXTBOX_HEIGHT = 20;
        private const int BOARD_MARGIN = 50;
        private const int CELL_MARGIN = 10;
        private const int SMALL_MARGIN = 5;

        private List<TextBox> InputBoxes = new List<TextBox>();

        public Form1()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    TextBox textBox = new System.Windows.Forms.TextBox();
                    textBox.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                    int xLocation = (x * TEXTBOX_WIDTH) + BOARD_MARGIN + SMALL_MARGIN;
                    if (x >= 3) xLocation += CELL_MARGIN;
                    if (x >= 6) xLocation += CELL_MARGIN;
                    int yLocation = (y * TEXTBOX_HEIGHT) + BOARD_MARGIN + SMALL_MARGIN;
                    if (y >= 3) yLocation += CELL_MARGIN;
                    if (y >= 6) yLocation += CELL_MARGIN;

                    textBox.Location = new System.Drawing.Point(xLocation, yLocation);
                    textBox.Name = "textBox1";
                    textBox.Size = new System.Drawing.Size(TEXTBOX_WIDTH, TEXTBOX_HEIGHT);
                    textBox.TabIndex = 0;
                    textBox.Text = "0";
                    textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
                    textBox.TextChanged += TextBox_TextChanged;
                    this.Controls.Add(textBox);
                    this.InputBoxes.Add(textBox);
                }
            }

            //https://www.theguardian.com/lifeandstyle/2019/nov/18/sudoku-4612-easy
            InitialiseBoard(new int[]
            {
                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 3, 0,  0, 0, 0,  1, 6, 0,
                0, 6, 7,  0, 3, 5,  0, 0, 4,

                6, 0, 8,  1, 2, 0,  9, 0, 0,
                0, 9, 0,  0, 8, 0,  0, 3, 0,
                0, 0, 2,  0, 7, 9,  8, 0, 6,

                8, 0, 0,  6, 9, 0,  3, 5, 0,
                0, 2, 6,  0, 0, 0,  0, 9, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,
            });

            //this.ResetBoard();

            InitializeComponent();

            this.stepSolveButton.Focus();
        }

        private void ResetBoard()
        {
            InitialiseBoard(new int[]
            {
                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,

                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,

                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,
                0, 0, 0,  0, 0, 0,  0, 0, 0,
            });
        }

        #region UI Event Handlers

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (!(sender as TextBox).Text.Equals(string.Empty))
            { 
                int intVal;
                if (!int.TryParse(((TextBox) sender).Text, out intVal))
                {
                    ((TextBox) sender).Text = string.Empty;
                }
                else
                {
                    if ((intVal < 1) || (intVal > 9))
                    {
                        ((TextBox) sender).Text = string.Empty;
                    }
                }
            }
        }

        private void stepSolveButton_Click(object sender, EventArgs e)
        {
            this.Solve(true);
        }

        private void fullSolveButton_Click(object sender, EventArgs e)
        {
            this.Solve(false);
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            this.ResetBoard();
        }

        #endregion

        private void InitialiseBoard(int[] board)
        {
            if ((board.Length != 9 * 9) || (this.InputBoxes.Count != 9 * 9))
            {
                return;
            }

            for (int i = 0; i < board.Length; i++)
            {
                this.InputBoxes[i].Text = board[i] == 0 ? " " : board[i].ToString();
            }
        }

        private int[] CreateArray()
        {
            List<int> list = new List<int>();
            for (int i = 0; i < this.InputBoxes.Count; i++)
            {
                int intVal;
                if (!int.TryParse(this.InputBoxes[i].Text, out intVal))
                {
                    list.Add(0);
                }
                else
                {
                    list.Add(intVal);
                }
            }

            return list.ToArray();
        }

        private void Solve(bool single)
        {
            var easySolutions = Library.findEasySolutions(ListModule.OfSeq(CreateArray()));
            Console.WriteLine("{0} easy solutions found", easySolutions.Length);
            
            while (easySolutions.Length > 0)
            {
                foreach (var easySolution in easySolutions)
                {
                    int index = easySolution.Item1;
                    int solution = easySolution.Item2.FirstOrDefault();

                    Console.WriteLine("Easy solution: ({0}, {1}) = {2}", (index / 9), (index % 9), solution);
                    this.InputBoxes[index].Text = solution.ToString();
                    if (single)
                    {
                        return;
                    }
                }

                easySolutions = Library.findEasySolutions(ListModule.OfSeq(CreateArray()));
            }

            // Find 6 in middle square!
            Console.WriteLine(("Now here!"));
        }
    }
}
