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
        private const int TEXTBOX_WIDTH = 30;
        private const int TEXTBOX_HEIGHT = 30;
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
            int[] board =
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
            };

            InitialiseBoard(board);
            InitializeComponent();

            this.button1.Focus();
        }
        
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

        void InitialiseBoard(int[] board)
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

        int[] CreateArray()
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

        private void button1_Click(object sender, EventArgs e)
        {
            //FSharpList<int> list = new FSharpList<int>(CreateArray());

            var foo = Library.solutions(ListModule.OfSeq(CreateArray()));
            var bar = foo.FirstOrDefault();
            if (bar != null)
            {
                int i1 = bar.Item1;
                int i2 = bar.Item2.FirstOrDefault();
                this.InputBoxes[i1].Text = i2.ToString();
            }
        }
    }
}
