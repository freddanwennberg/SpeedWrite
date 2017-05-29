namespace SpeedWrite
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
            this.inputWord = new System.Windows.Forms.TextBox();
            this.resultWordList = new System.Windows.Forms.RichTextBox();
            this.resultApprox = new System.Windows.Forms.RichTextBox();
            this.inputDistance = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.SelectedWord = new System.Windows.Forms.TextBox();
            this.debugTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // inputWord
            // 
            this.inputWord.Location = new System.Drawing.Point(12, 12);
            this.inputWord.Name = "inputWord";
            this.inputWord.Size = new System.Drawing.Size(276, 20);
            this.inputWord.TabIndex = 0;
            this.inputWord.TextChanged += new System.EventHandler(this.inputWord_TextChanged);
            // 
            // resultWordList
            // 
            this.resultWordList.Location = new System.Drawing.Point(12, 38);
            this.resultWordList.Name = "resultWordList";
            this.resultWordList.Size = new System.Drawing.Size(276, 368);
            this.resultWordList.TabIndex = 1;
            this.resultWordList.Text = "";
            // 
            // resultApprox
            // 
            this.resultApprox.Location = new System.Drawing.Point(294, 38);
            this.resultApprox.Name = "resultApprox";
            this.resultApprox.Size = new System.Drawing.Size(276, 368);
            this.resultApprox.TabIndex = 2;
            this.resultApprox.Text = "";
            // 
            // inputDistance
            // 
            this.inputDistance.Location = new System.Drawing.Point(294, 12);
            this.inputDistance.Name = "inputDistance";
            this.inputDistance.Size = new System.Drawing.Size(276, 20);
            this.inputDistance.TabIndex = 3;
            this.inputDistance.TextChanged += new System.EventHandler(this.InputDistance_TextChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.AutoWordSelection = true;
            this.richTextBox1.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.richTextBox1.Location = new System.Drawing.Point(12, 434);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(558, 246);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            this.richTextBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.richTextBox1_KeyUp);
            this.richTextBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.richTextBox1_MouseDown);
            // 
            // SelectedWord
            // 
            this.SelectedWord.Location = new System.Drawing.Point(586, 434);
            this.SelectedWord.Name = "SelectedWord";
            this.SelectedWord.Size = new System.Drawing.Size(321, 20);
            this.SelectedWord.TabIndex = 5;
            // 
            // debugTextBox
            // 
            this.debugTextBox.Location = new System.Drawing.Point(576, 12);
            this.debugTextBox.Name = "debugTextBox";
            this.debugTextBox.Size = new System.Drawing.Size(662, 394);
            this.debugTextBox.TabIndex = 6;
            this.debugTextBox.Text = "";
            this.debugTextBox.TextChanged += new System.EventHandler(this.debugTextBox_Changed);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(586, 460);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1250, 754);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.debugTextBox);
            this.Controls.Add(this.SelectedWord);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.inputDistance);
            this.Controls.Add(this.resultApprox);
            this.Controls.Add(this.resultWordList);
            this.Controls.Add(this.inputWord);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox inputWord;
        private System.Windows.Forms.RichTextBox resultWordList;
        private System.Windows.Forms.RichTextBox resultApprox;
        private System.Windows.Forms.TextBox inputDistance;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox SelectedWord;
        private System.Windows.Forms.RichTextBox debugTextBox;
        private System.Windows.Forms.Button button1;
    }
}

