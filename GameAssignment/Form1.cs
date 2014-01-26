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

        private void label6_Click(object sender, EventArgs e)
        {
            // We have clicked on the "Rules" button
            MessageBox.Show("Click on a button if it is your turn. Player 1 = 'X', Player 2 = 'O'.\nHighscores get saved when you reset the score."); 
        }

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

        private void label7_Click(object sender, EventArgs e)
        {
            // We have clicked on Highscore
            highscores.display();
            highscores.Show();
        }

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

        public void ClickButton(Object sender, System.EventArgs e)
        {
            String character = "X";
            if (label2.Text == play2)
            {
                character = "O";
            }
            label2.Text = (label2.Text == play1 ? play2 : play1);
            Button btn = (Button)sender;
            btn.Text = character;
            checkWin();
        }

        private void checkWin()
        {
            int lateral = 0;
            Button lastLateral = null;
            for (int i = 0; i < 3; i++)
            {
                lateral = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (lastLateral != null)
                    {
                        if (buttons[i, j].Text == lastLateral.Text && buttons[i, j].Text != "")
                        {
                            lateral++;
                        }
                    }
                    else
                    {
                        lateral = 0;
                    }
                    lastLateral = buttons[i, j];
                    if (buttons[0, j].Text == buttons[1, j].Text && buttons[1, j].Text == buttons[2, j].Text && buttons[0, j].Text != "")
                    {
                        if (label2.Text == play1)
                        {
                            MessageBox.Show(play2 + " wins laterally at " + (i+1) + "!");
                            increasePlayerScore(2);
                        }
                        else
                        {
                            MessageBox.Show(play1 + " wins laterally at " + (i+1) + "!");
                            increasePlayerScore(1);
                        }
                        reset();
                        break;
                    }
                }
                if (lateral == 2)
                {
                    if (label2.Text == play1)
                    {
                        MessageBox.Show(play2 + " wins vertically at " + (i+1) + "!");
                        increasePlayerScore(2);
                    }
                    else
                    {
                        MessageBox.Show(play1 + " wins vertically at " + (i+1) + "!");
                        increasePlayerScore(1);
                    }
                    
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (label8.Text.Contains("1"))
            {
                play1 = textBox1.Text;
                label8.Text = "Enter name for player 2";
                textBox1.Text = "Player 2";
                textBox1.SelectAll();
            }
            else
            {
                play2 = textBox1.Text;
                label8.Hide();
                button1.Hide();
                textBox1.Hide();
                CreateButtons();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                button1_Click(sender, e);
            }
        }
    }
}