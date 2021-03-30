using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public int lengthOfTextWhileSaving;
        public int length;
        public string textBeforeSaving;
        public bool isSaveFirstTime = false;
        public string filePath;
        public float fontSize = 8;
        public string fontName;
        public Enum fontStyle;
        public string fontFamily = "Microsoft Sans Serif";
        public bool isFileSave = false;


        public Form1()
        {
            InitializeComponent();
        }

        //Save as method call.
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile(); 
        }

        //Save code
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        { 
            if(isSaveFirstTime == false)
            {
                SaveFile();
            }
            if(isSaveFirstTime == true)
            {
                textBeforeSaving = textBox1.Text;
                File.WriteAllText(filePath, textBox1.Text);
                Asterisk();
            }   
        }

        private void label1_Click(object sender, EventArgs e){ }

        //Open file code.
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (label1.Text.Length > 0)
            {
                if (label1.Text.Contains("*"))
                {
                    DialogResult result = MessageBox.Show("Do you want to discard the changes and open File?", "Unsaved Changes Alert", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        //do nothing
                    }
                    if (result == DialogResult.Yes)
                    {
                        OpenFile();
                    }
                }
            }
            else if (textBox1.Text == "" && label1.Text.Contains(""))
            {
                OpenFile();
            }
        }

        //Save or unsaves Asterisk on bottom right of screen.
        private void Textbox1_keyUp(object sender, KeyEventArgs e)
        {
            label2.Text = textBox1.Text.Length.ToString() + " Letters";
            Asterisk();
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e){}

 
        //Cross button event handler
        private void FC(object sender, FormClosedEventArgs e)
        {
            if (textBox1.Text.Length > 0 && label1.Text.Contains("*"))
            {
                DialogResult result1 = MessageBox.Show("Document unsaved. Do you want to Save and close the document", "Unsaved Changes Alert", MessageBoxButtons.YesNo);
                if (textBeforeSaving != textBox1.Text)
                {
                    if (result1 == DialogResult.Yes)
                    {
                        SaveFile();
                        System.Windows.Forms.Application.Exit();
                    }
                    else
                    {
                        System.Windows.Forms.Application.Exit();
                    }
                }

            }

        }


        //Exit option in Menu bar
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e){ }

        private void label2_Click(object sender, EventArgs e){}

        private void Form1_Load(object sender, EventArgs e)
        {
            label3.Text = "Font size: 8 Font Family: " + fontFamily;
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontSize = 8;
            textBox1.Font = new System.Drawing.Font(fontFamily, fontSize);
            FontSizeShow();
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            textBox1.Font = new System.Drawing.Font(fontFamily, fontSize=fontSize+2);
            FontSizeShow();
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Font = new System.Drawing.Font(fontFamily, fontSize = fontSize - 2);
            FontSizeShow();
        }

        private void label3_Click(object sender, EventArgs e){ }

        private void abourToolStripMenuItem_Click(object sender, EventArgs e){ }

        //Cut
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                textBox1.Cut();
            }
        }

        //Paste
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste();
        }

        //Undo
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.CanUndo == true)
                textBox1.Undo();
                textBox1.ClearUndo();
            }

        //Copy
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textBox1.SelectedText != "")
            {
                textBox1.Copy();
            }
        }

        //New Window open code
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isFileSave == false && textBox1.Text.Length > 0)
            {
                DialogResult newButtonResult = MessageBox.Show("Document unsaved. Do you want to Open new file", "Unsaved Changes Alert", MessageBoxButtons.YesNo);
                if (newButtonResult == DialogResult.Yes)
                {
                    NewForm();
                }
            }
            else {
                NewForm();
            }
        }

        //About window opens
        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        //New Window method
        public void NewForm()
        {
            this.Hide();
            Form1 NewWindow = new Form1();
            NewWindow.ShowDialog();
            this.Close();
        }

        //Open file method
        public void OpenFile()
        {
            openFileDialog1.Filter = "Untitled| *.txt";
            openFileDialog1.ShowDialog();
            textBox1.Text = File.ReadAllText(openFileDialog1.FileName);
        }

        //Save file Method/Function
        public void SaveFile()
        {
            saveFileDialog1.Filter = "Text file|*.txt";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName == "")
            {
                MessageBox.Show("You did not select any file");
            }
            else
            {
                File.WriteAllText(saveFileDialog1.FileName, textBox1.Text);

                filePath = saveFileDialog1.FileName;
                label1.Text = filePath;
                textBeforeSaving = textBox1.Text;
                isSaveFirstTime = true;
                isFileSave = true;
            }
        }

        //Asterisk show method
        public void Asterisk()
        {
            label2.Text = textBox1.Text.Length.ToString() + " Letters";
            if (textBeforeSaving != textBox1.Text)
            {
                label1.Text = saveFileDialog1.FileName + "*";
                isFileSave = false;
            }

            if (textBeforeSaving == textBox1.Text)
            {
                label1.Text = saveFileDialog1.FileName;
                isFileSave = true;
            }

            if (textBox1.Text.Length == 0)
            {
                label1.Text = "";
            }
        }

        //FontSizeShow method
        public void FontSizeShow()
        {
            label3.Text = "Font size:" + fontSize + " Font Family: " + fontFamily;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void showFontsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
                fontSize = fontDialog1.Font.Size;
                fontName = fontDialog1.Font.Name;
                textBox1.Font = new System.Drawing.Font(fontName, fontSize);
                label3.Text = "Font size: " + fontSize + " Font Family: " + fontName;
        }
    }
}

