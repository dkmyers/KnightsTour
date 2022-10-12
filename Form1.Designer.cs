
namespace TheKnightsTour
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
            this.NextMoveButton = new System.Windows.Forms.Button();
            this.yTextBox = new System.Windows.Forms.TextBox();
            this.xTextBox = new System.Windows.Forms.TextBox();
            this.xLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.stepsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // NextMoveButton
            // 
            this.NextMoveButton.Location = new System.Drawing.Point(13, 826);
            this.NextMoveButton.Name = "NextMoveButton";
            this.NextMoveButton.Size = new System.Drawing.Size(75, 23);
            this.NextMoveButton.TabIndex = 0;
            this.NextMoveButton.Text = "Next Move";
            this.NextMoveButton.UseVisualStyleBackColor = true;
            this.NextMoveButton.Click += new System.EventHandler(this.NextMoveButton_Click);
            // 
            // yTextBox
            // 
            this.yTextBox.Location = new System.Drawing.Point(688, 829);
            this.yTextBox.Name = "yTextBox";
            this.yTextBox.Size = new System.Drawing.Size(100, 20);
            this.yTextBox.TabIndex = 1;
            // 
            // xTextBox
            // 
            this.xTextBox.Location = new System.Drawing.Point(688, 803);
            this.xTextBox.Name = "xTextBox";
            this.xTextBox.Size = new System.Drawing.Size(100, 20);
            this.xTextBox.TabIndex = 2;
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(623, 806);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(59, 13);
            this.xLabel.TabIndex = 3;
            this.xLabel.Text = "Starting X: ";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(623, 832);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(59, 13);
            this.yLabel.TabIndex = 4;
            this.yLabel.Text = "Starting Y: ";
            // 
            // stepsLabel
            // 
            this.stepsLabel.AutoSize = true;
            this.stepsLabel.Location = new System.Drawing.Point(10, 803);
            this.stepsLabel.Name = "stepsLabel";
            this.stepsLabel.Size = new System.Drawing.Size(137, 13);
            this.stepsLabel.TabIndex = 5;
            this.stepsLabel.Text = "Number of Moves Taken: 0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 861);
            this.Controls.Add(this.stepsLabel);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.xLabel);
            this.Controls.Add(this.xTextBox);
            this.Controls.Add(this.yTextBox);
            this.Controls.Add(this.NextMoveButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button NextMoveButton;
        private System.Windows.Forms.TextBox yTextBox;
        private System.Windows.Forms.TextBox xTextBox;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label stepsLabel;
    }
}

