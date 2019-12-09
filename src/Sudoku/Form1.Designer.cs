namespace Sudoku
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.stepSolveButton = new System.Windows.Forms.Button();
            this.fullSolveButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // stepSolveButton
            // 
            this.stepSolveButton.Location = new System.Drawing.Point(413, 65);
            this.stepSolveButton.Name = "stepSolveButton";
            this.stepSolveButton.Size = new System.Drawing.Size(123, 82);
            this.stepSolveButton.TabIndex = 0;
            this.stepSolveButton.Text = "Step Solve";
            this.stepSolveButton.UseVisualStyleBackColor = true;
            this.stepSolveButton.Click += new System.EventHandler(this.stepSolveButton_Click);
            // 
            // fullSolveButton
            // 
            this.fullSolveButton.Location = new System.Drawing.Point(413, 153);
            this.fullSolveButton.Name = "fullSolveButton";
            this.fullSolveButton.Size = new System.Drawing.Size(123, 82);
            this.fullSolveButton.TabIndex = 1;
            this.fullSolveButton.Text = "Full Solve";
            this.fullSolveButton.UseVisualStyleBackColor = true;
            this.fullSolveButton.Click += new System.EventHandler(this.fullSolveButton_Click);
            // 
            // resetButton
            // 
            this.resetButton.Location = new System.Drawing.Point(413, 241);
            this.resetButton.Name = "resetButton";
            this.resetButton.Size = new System.Drawing.Size(123, 82);
            this.resetButton.TabIndex = 2;
            this.resetButton.Text = "Reset";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 418);
            this.Controls.Add(this.resetButton);
            this.Controls.Add(this.fullSolveButton);
            this.Controls.Add(this.stepSolveButton);
            this.Name = "Form1";
            this.Text = "Sudoku Solver";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button stepSolveButton;
        private System.Windows.Forms.Button fullSolveButton;
        private System.Windows.Forms.Button resetButton;
    }
}

