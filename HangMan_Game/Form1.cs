using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HangMan_Game
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string word = "";
        List<Label> labels = new List<Label>();
        private int incorrectGuesses;
        int amount = 0;

        public int IncorrectGuesses { get => incorrectGuesses; set => incorrectGuesses = value; }

        string GetRandomWord()
        {
            WebClient WC = new WebClient();
            string wordList = WC.DownloadString("https://www.dictionary-thesaurus.com/wordlists/Adjectives%28929%29.txt");
            string[] words = wordList.Split('\n');
            Random ran = new Random();
            return words[ran.Next(0, words.Length - 1)];
        }
        void MakeLabels()
        {
            GetRandomWord();
            char[] chars = word.ToCharArray();
            int between = 400 / chars.Length - 1;
            for (int i = 0; i < chars.Length - 1; i++)
            {
                labels.Add(new Label());
                labels[i].Location = new Point((i * between) + 10, 80);
                labels[i].Text = "_";
                labels[i].Parent = groupBox1;
                labels[i].BringToFront();
                labels[i].CreateControl();

            }
            label1.Text = "Word Length:" + (chars.Length - 1).ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Visible = IncorrectGuesses >= 1;
            pictureBoxStage2.Visible = IncorrectGuesses >= 2;
            pictureBoxStage3.Visible = IncorrectGuesses >= 3;
            pictureBoxStage4.Visible = IncorrectGuesses >= 4;
            pictureBoxStage5.Visible = IncorrectGuesses >= 5;
            pictureBoxStage6.Visible = IncorrectGuesses >= 6;
            pictureBoxStage7.Visible = IncorrectGuesses >= 7;
            pictureBoxStage8.Visible = IncorrectGuesses >= 8;





        }

        private void button4_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if (!char.IsLetter(letter))
            {
                MessageBox.Show("You can only  subimt letters", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letter)
                        labels[i].Text = letter.ToString();

                }
                foreach (Label l in labels)
                    if (l.Text == "_") return;
                MessageBox.Show(" You have WON!", " CONGRATS!");
            }
            else
            {
                MessageBox.Show("The Letter you Guessed is not in the Word", "SORRY");
                label2.Text += " " + letter.ToString() + ",";
                amount++;
                if (amount == 9)
                {
                    MessageBox.Show("SORRY you Lost! The word was " + word);

                }
                

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(textBox2.Text == word)
            {
                MessageBox.Show(" You Have won!", "CONGRATS!");

            }
            else
            {
                MessageBox.Show(" The word that you Guessed is Wrong! ", "SORRY!");
                    amount++;

                if (amount == 9)
                {
                    MessageBox.Show("SORRY you Lost! The word was " + word);

                }
            }
        }
        
    }
     
}
