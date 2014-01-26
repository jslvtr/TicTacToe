using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameAssignment
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button[,] buttons = new System.Windows.Forms.Button[3, 3];
        private string play1, play2;
        private Form2 highscores;

        public Form1()
        {
            highscores = new Form2();
            loadScores();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j] = new System.Windows.Forms.Button();
                    buttons[i, j].Size = new System.Drawing.Size(75, 50);
                    buttons[i, j].Location = new System.Drawing.Point(17 + 82 * i, 13 + 55 * j);
                    buttons[i, j].Font = new System.Drawing.Font("Trebuchet MS", 14.25F, FontStyle.Bold);
                    buttons[i, j].Click += new System.EventHandler(ClickButton);
                }
            }
            InitializeComponent();
        }

        /*
         * This method adds the buttons that were created on the constructor to the Form.
         */
        public void CreateButtons()
        {

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Controls.Add(buttons[i, j]);
                }
            }
            label2.Text = play1;
            label4.Text = "0 : 0";
        }

        /*
         * This is the method that gets called when we press the "Rules" button (or label)
         */
        private void label6_Click(object sender, EventArgs e)
        {
            // We have clicked on the "Rules" button
            MessageBox.Show("Click on a button if it is your turn. Player 1 = 'X', Player 2 = 'O'.\nHighscores get saved when you reset the score."); 
        }

        /*
         * This is the method that gets called when we press the "Play again" button (or label)
         * If the score is 0:0 we assume the user wants the score reset.
         * If the score isn't 0:0 then we simply clear the board, ready for another game.
         */
        private void label5_Click(object sender, EventArgs e)
        {
            // We have clicked on the "Play again" button
            // If the board is totally empty, we assume user wants a score reset
            bool scoreReset = true;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(buttons[i, j].Text != "") scoreReset = false;
                }
            }
            if (!scoreReset)
            {
                reset();
            }
            else
            {
                highscores.addScore(play1, play2, label4.Text);
                label4.Text = "0 : 0";
            }
        }

        /*
         * In this method we simply create the High-score Form and then un-hide it (show it).
         */
        private void label7_Click(object sender, EventArgs e)
        {
            // We have clicked on Highscore
            highscores.display();
            highscores.Show();
        }

        /*
         * This method is used to change all the text of the buttons to "", essentially resetting the board.
         */
        private void reset()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    buttons[i, j].Text = "";
                }
            }
        }

        /*
         * This method is used whenever a button is clicked.
         * We first check what symbol we are going to write, depending on whose turn it is.
         * Then, if the button is empty, we write the appropriate character on it.
         * Then we check for if someone has won the game.
         * If the button wasn't empty, we display a message saying that the button wasn't empty and thus cannot be clicked.
         */
        public void ClickButton(Object sender, System.EventArgs e)
        {
            String character = "X";
            if (label2.Text == play2)
            {
                character = "O";
            }
            Button btn = (Button)sender;
            if (btn.Text == "")
            {
                label2.Text = (label2.Text == play1 ? play2 : play1);
                btn.Text = character;
                checkWin();
            }
            else
            {
                MessageBox.Show("You cannot press a button once it's already owned by a player!");
            }
        }

        /*
         * This is the method used to check if someone has won the game.
         */
        private void checkWin()
        {
            // This is used to count the number of buttons that are equal vertical.
            int vertical = 0;
            // This is used to remember the last button on the column.
            Button lastVertical = null;

            // Here we iterate through the rows.
            for (int i = 0; i < 3; i++)
            {
                vertical = 0;
                
                // Here we iterate through the columns.
                for (int j = 0; j < 3; j++)
                {
                    if (lastVertical != null)
                    {
                        if (buttons[i, j].Text == lastVertical.Text && buttons[i, j].Text != "")
                        {
                            // We add one to vertical if it wasn't empty and it's text is the same as the current buttons' text.
                            vertical++;
                        }
                    }
                    else
                    {
                        // If the previous vertical button was null (meaning we are at the beginning of a row), we set `vertical = 0`.
                        vertical = 0;
                    }

                    // Then we set the last vertical button to be the current button, after all the checks.
                    lastVertical = buttons[i, j];

                    // For the current column, if all the the buttons have the same text, and it is not empty,
                    if (buttons[0, j].Text == buttons[1, j].Text && buttons[1, j].Text == buttons[2, j].Text && buttons[0, j].Text != "")
                    {
                        // If the current players' turn is Player 1, then Player 2 must've won (since the check happens after changing the turn)
                        if (label2.Text == play1)
                        {
                            MessageBox.Show(play2 + " wins laterally at " + (i+1) + "!");
                            increasePlayerScore(2);
                        }

                        // If the current players' turn is Player 2, then Player 1 must've won (since the check happens after changing the turn)
                        else
                        {
                            MessageBox.Show(play1 + " wins laterally at " + (i+1) + "!");
                            increasePlayerScore(1);
                        }
                        // Then we reset the board and break out of the loop.
                        reset();
                        break;
                    }
                }
                // If vertical == 2 this means that 3 buttons were identical, and not empty. Someone has won vertically!
                if (vertical == 2)
                {
                    // If the current players' turn is Player 1, then Player 2 must've won (since the check happens after changing the turn)
                    if (label2.Text == play1)
                    {
                        MessageBox.Show(play2 + " wins vertically at " + (i+1) + "!");
                        increasePlayerScore(2);
                    }

                    // If the current players' turn is Player 2, then Player 1 must've won (since the check happens after changing the turn)
                    else
                    {
                        MessageBox.Show(play1 + " wins vertically at " + (i+1) + "!");
                        increasePlayerScore(1);
                    }
                    // Then we reset the board and break out of the loop.
                    reset();
                    break;
                }

            }

            // Now check the two vertical win situations
            if ((buttons[0, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[2, 2].Text && buttons[0, 0].Text != "") ||
                buttons[2, 0].Text == buttons[1, 1].Text && buttons[1, 1].Text == buttons[0, 2].Text && buttons[2, 0].Text != "")
            {
                if (label2.Text == play1)
                {
                    MessageBox.Show(play2 + " wins diagonally!");
                    increasePlayerScore(2);
                }
                else
                {
                    MessageBox.Show(play1 + " wins diagonally!");
                    increasePlayerScore(1);
                }
                reset();
            }

        }

        /*
         * This method increases the player score for a player that is passed as a parameter.
         * We simply modify the label that shows the score.
         */
        private void increasePlayerScore(int player)
        {
            string score = label4.Text;
            if (player == 1)
            {
                string p1 = score.Split(':')[0];
                int player1 = int.Parse(p1);
                player1++;
                label4.Text = player1 + ":" + score.Split(':')[1];
            }
            else
            {
                string p2 = score.Split(':')[1];
                int player2 = int.Parse(p2);
                player2++;
                label4.Text = score.Split(':')[0] + ":" + player2;
            }
        }

        /*
         * button1 is what we press when deciding the Player names at the start of the game.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            if (label8.Text.Contains("1"))
            {
                play1 = textBox1.Text;
                label8.Text = "Enter name for player 2";
                textBox1.Text = "Player 2";
                // Remember to select all the text in the textbox so that it is easier for the user to simply press "Enter" if they agree with "Player 2" being used.
                textBox1.SelectAll();
            }
            else
            {
                // We hide all the buttons and proceed to creating the rest of the board.
                play2 = textBox1.Text;
                label8.Hide();
                button1.Hide();
                textBox1.Hide();
                CreateButtons();
            }
        }

        /*
         * Here we make sure that we can press "Enter" when selecting player names,
         * rather than having to press the button. Simply for speed!
         */
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && textBox1.Visible)
            {
                button1_Click(sender, e);
            }
        }

        /*
         * This method is used to load the scores from a text file.
         * What happens is that after creating the high-scores form, this simply adds every score in the file to the form.
         */
        private void loadScores()
        {
            string line;
            // We check that the file exists...
            if (System.IO.File.Exists(@".\scoresave.txt"))
            {
                // We use `using` so that the StreamReader is destroyed at the end of the block
                using (System.IO.StreamReader file = new System.IO.StreamReader(@".\scoresave.txt"))
                {
                    while ((line = file.ReadLine()) != null)
                    {
                        try
                        {
                            string[] lineSplit = line.Split(',');
                            highscores.addScore(lineSplit[0], lineSplit[1], lineSplit[2]);
                        }
                        catch (Exception e)
                        {

                        }
                        
                    }
                }
            }
        }

        /*
         * Here we save the scores to the text file.
         * If the file doesn't exist, we create it first.
         * Then we simply go through all our scores and write them to file in .CSV format.
         */
        private void saveScores()
        {
            Score[] scores = highscores.getScores();

            // Create the file if it doesn't exist...
            if (!System.IO.File.Exists(@".\scoresave.txt"))
            {
                System.IO.File.Create(@".\scoresave.txt");
            }

            // Once again we use `using`.
            using(System.IO.StreamWriter file = new System.IO.StreamWriter(@".\scoresave.txt")) {
                foreach (Score s in scores)
                {
                    // If the score isn't null, we write it to the file.
                    if (s != null)
                    {
                        file.WriteLine(s.getPlayer1() + "," + s.getPlayer2() + "," + s.getScore());
                    }
                    // If the score is null, we know that we have gotten to the end of the scores, because they will always be in order.
                    else
                    {
                        break;
                    }
                }
            }
        }

        /*
         * This method makes sure that we save the scores before closing the application.
         */
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            saveScores();
        }
    }
}