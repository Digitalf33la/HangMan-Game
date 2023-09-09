using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
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
        int amount = 0;

        

        string GetRandomWord()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    string wordList = wc.DownloadString("https://www.dictionary-thesaurus.com/wordlists/Adjectives%28929%29.txt");
                    string[] words = wordList.Split('\n');
                    Random ran = new Random();

                    if (words.Length > 0)
                    {
                        int randomIndex = ran.Next(0, words.Length - 1);
                        return words[randomIndex].Trim(); // Trim to remove leading/trailing whitespace
                    }
                    else
                    {
                        return "No words available"; // Handle the case where no words are retrieved.
                    }
                }
                catch (WebException ex)
                {
                    // Handle the web request exception (e.g., network issues, website unavailable).
                    // You can log the error or return a default word in case of failure.
                    return "Error: " + ex.Message;
                }
            }
        }
        void MakeLabels()
        {
            string word = GetRandomWord(); // Assume GetRandomWord() returns a valid random word.
            char[] chars = word.ToCharArray();

            int spacing = 950 / chars.Length; // Calculate spacing based on word length.

            for (int i = 0; i < chars.Length; i++) // Loop through all characters.
            {
                Label label = new Label(); // Create a new label.
                label.Location = new Point(i * spacing + 10, 80); // Set the label's position.
                label.Text = "_"; // Set the label's text.
                groupBox1.Controls.Add(label); // Add the label to groupBox1 controls.

                

                // Optionally, you can store the labels in a list if needed.
                labels.Add(label);
            }

            label1.Text = "Word Length: " + chars.Length; // Display the correct word length.
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetRandomWord();
            MakeLabels();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            char letter = textBox1.Text.ToLower().ToCharArray()[0];
            if (!char.IsLetter(letter))
            {
                MessageBox.Show("You can only  subimt letters", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (word.Contains(letter))
            {
                char[] letters = word.ToCharArray();
                bool allLettersGuessed = true; // Initialize a flag to check if all letters are guessed.

                for (int i = 0; i < letters.Length; i++)
                {
                    if (letters[i] == letter)
                    {
                        labels[i].Text = letter.ToString();
                    }

                    // Check if there are any underscores left.
                    if (labels[i].Text == "_")
                    {
                        allLettersGuessed = false; // Set the flag to false if there's an underscore.
                    }
                }

                // Check if all letters are guessed.
                if (allLettersGuessed)
                {
                    MessageBox.Show("You have WON!", "CONGRATS!");
                }
            }
            else
            {
                MessageBox.Show("The Letter you Guessed is not in the Word", "SORRY");
                label2.Text += " " + letter.ToString() + ","; // Add the guessed letter to label2.
                amount++; // Increase the number of incorrect guesses.
                

                // Determine which image (pictureBox) to display based on the number of incorrect guesses.
                if (amount >= 1)
                {
                    pictureBox1.Visible = true; // Show the first stage image.
                }
                
                if (amount >= 2)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = true; // Show the second stage image.

                }
                if (amount >= 3)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = true; // Show the third stage image.
                }
                if (amount >= 4)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = false;
                    pictureBoxStage4.Visible = true; // Show the fourth stage image.
                }
                if (amount >= 5)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = false;
                    pictureBoxStage4.Visible = false;
                    pictureBoxStage5.Visible = true; // Show the fifth stage image.
                }
                if (amount >= 6)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = false;
                    pictureBoxStage4.Visible = false;
                    pictureBoxStage5.Visible = false;
                    pictureBoxStage6.Visible = true; // Show the sixth stage image.
                }
                if (amount >= 7)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = false;
                    pictureBoxStage4.Visible = false;
                    pictureBoxStage5.Visible = false;
                    pictureBoxStage6.Visible = false;
                    pictureBoxStage7.Visible = true; // Show the seventh stage image.
                }
                if (amount >= 8)
                {
                    pictureBox1.Visible = false;
                    pictureBoxStage2.Visible = false;
                    pictureBoxStage3.Visible = false;
                    pictureBoxStage4.Visible = false;
                    pictureBoxStage5.Visible = false;
                    pictureBoxStage6.Visible = false;
                    pictureBoxStage7.Visible = false;
                    pictureBoxStage8.Visible = true; // Show the eighth stage image.
                }

                if (amount == 9)
                {
                    this.Hide();
                    MessageBox.Show("SORRY you Lost! The word was " + GetRandomWord());

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

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Game has Started");
        }

        private void button3_Click(object sender, EventArgs e)
        {
           DialogResult option = MessageBox.Show("Game has stopped", "Stop", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(option == DialogResult.Yes)
            {
                this.Close();
            }
            else
            {
                this.Show();
            }
        }
    }
     
}
