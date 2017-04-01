namespace TextDifference
{
    partial class MainForm
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
            this.LoadText1Button = new System.Windows.Forms.Button();
            this.LoadText2Button = new System.Windows.Forms.Button();
            this.Text1RichTextBox = new System.Windows.Forms.RichTextBox();
            this.Text2RichTextBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // LoadText1Button
            // 
            this.LoadText1Button.Location = new System.Drawing.Point(160, 47);
            this.LoadText1Button.Name = "LoadText1Button";
            this.LoadText1Button.Size = new System.Drawing.Size(83, 35);
            this.LoadText1Button.TabIndex = 0;
            this.LoadText1Button.Text = "Load Text 1";
            this.LoadText1Button.UseVisualStyleBackColor = true;
            this.LoadText1Button.Click += new System.EventHandler(this.LoadText1Button_Click);
            // 
            // LoadText2Button
            // 
            this.LoadText2Button.Location = new System.Drawing.Point(566, 47);
            this.LoadText2Button.Name = "LoadText2Button";
            this.LoadText2Button.Size = new System.Drawing.Size(84, 35);
            this.LoadText2Button.TabIndex = 1;
            this.LoadText2Button.Text = "Load Text 2";
            this.LoadText2Button.UseVisualStyleBackColor = true;
            this.LoadText2Button.Click += new System.EventHandler(this.LoadText2Button_Click);
            // 
            // Text1RichTextBox
            // 
            this.Text1RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Text1RichTextBox.Location = new System.Drawing.Point(25, 91);
            this.Text1RichTextBox.Name = "Text1RichTextBox";
            this.Text1RichTextBox.Size = new System.Drawing.Size(370, 442);
            this.Text1RichTextBox.TabIndex = 4;
            this.Text1RichTextBox.Text = "";
            // 
            // Text2RichTextBox
            // 
            this.Text2RichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text2RichTextBox.Location = new System.Drawing.Point(417, 91);
            this.Text2RichTextBox.Name = "Text2RichTextBox";
            this.Text2RichTextBox.Size = new System.Drawing.Size(386, 442);
            this.Text2RichTextBox.TabIndex = 5;
            this.Text2RichTextBox.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.ClientSize = new System.Drawing.Size(826, 559);
            this.Controls.Add(this.Text2RichTextBox);
            this.Controls.Add(this.Text1RichTextBox);
            this.Controls.Add(this.LoadText2Button);
            this.Controls.Add(this.LoadText1Button);
            this.MinimumSize = new System.Drawing.Size(842, 598);
            this.Name = "MainForm";
            this.Text = "Text Difference";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadText1Button;
        private System.Windows.Forms.Button LoadText2Button;
        private System.Windows.Forms.RichTextBox Text1RichTextBox;
        private System.Windows.Forms.RichTextBox Text2RichTextBox;
    }
}

