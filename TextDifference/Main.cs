using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextDifference
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Text1RichTextBox.SelectionBackColor = Color.Yellow;
            Text1RichTextBox.AutoWordSelection = false;
        }

        private void LoadText1Button_Click(object sender, EventArgs e)
        {
            SelectFileFromDialog();

            HighlightText();
        }

        void SelectFileFromDialog()
        {
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {

                        using (TextReader reader = new StreamReader(myStream, Encoding.GetEncoding(1252), true)) // Using special encoding to get Swedish åäö letters
                        {
                            String text = reader.ReadToEnd();
                            Text1RichTextBox.Text = text;
                        }


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        void HighlightText()
        {
            if (String.IsNullOrEmpty(Text1RichTextBox.Text))
            {
                return;
            }
            int start = 0;
            int end = Text1RichTextBox.Text.LastIndexOf("hoppade");

            Text1RichTextBox.SelectAll();
            Text1RichTextBox.SelectionBackColor = Color.Transparent;

            while (start < end)
            {
                Text1RichTextBox.Find("hoppade", start, Text1RichTextBox.TextLength, RichTextBoxFinds.MatchCase);

                Text1RichTextBox.SelectionBackColor = Color.Yellow;

                start = Text1RichTextBox.Text.IndexOf("hoppade", start) + 1;
            }
        }

        private void LoadText2Button_Click(object sender, EventArgs e)
        {

        }
    }
}
