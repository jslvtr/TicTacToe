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
    public partial class Form2 : Form
    {

        private Score[] scores;

        public Form2()
        {
            scores = new Score[15];
        }

        /*
         * This method adds a new score to the array.
         * Then it orders the array so that all the scores are at the top.
         */
        public void addScore(string player1, string player2, string score)
        {
            Score toAdd = new Score(player1, player2, score);
            if (toAdd.getDiff() == 0) return;
            for (int i = 0; i < scores.Length; i++)
            {
                // If any of the scores is null, we add the new score there and then return.
                // Then we order the array by the scores we have in the array.
                // This makes sure that there are no errors when trying to order, in case there were any `null` values to order by.
                if (scores[i] == null)
                {
                    scores[i] = toAdd;
                    sortScoreArray(scores, 0, i);
                    return;
                }
            }
            // If the score we're trying to add is greater than the last score (which is the smallest) then we add it.
            if (toAdd.getDiff() > scores[scores.Length-1].getDiff())
            {
                scores[scores.Length-1] = toAdd;
            }

            sortScoreArray(scores, 0, scores.Length-1);

        }

        /*
         * This method is used to sort the array.
         * Together with `half` it implements a quicksort.
         */
        private void sortScoreArray(Score[] arr, int left, int right)
        {
            int index = half(arr, left, right);
            if (left < index - 1)
                sortScoreArray(arr, left, index - 1);
            if (index < right)
                sortScoreArray(arr, index, right);
        }

        /*
         * Gets the halfpoint of the array and performs substitutions.
         * Essentially the implementation of the quicksort.
         */
        private int half(Score[] arr, int left, int right)
        {
            int i = left, j = right;
            Score tmp;
            Score pivot = arr[(left + right) / 2];

            while (i <= j)
            {
                while (arr[i].getDiff() > pivot.getDiff())
                {
                    i++;
                }
                while (arr[j].getDiff() < pivot.getDiff())
                {
                    j--;
                }
                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    j--;
                }
            }

            return i;
        }

        /*
         * This method creates the labels to display the scores in the form.
         * It is called every time we want to display the form, just in case they change.
         */
        public void display()
        {
            removeAll();
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == null)
                {
                    break;
                }
                Label p1 = new Label();
                Label p2 = new Label();
                Label score = new Label();
                Label index = new Label();

                p1.Text = scores[i].getPlayer1();
                p1.Size = new Size(80, 25);
                p2.Text = scores[i].getPlayer2();
                p2.Size = new Size(80, 25);
                score.Text = scores[i].getScore();
                score.Size = new Size(80, 25);
                index.Text = i + "";
                index.Size = new Size(25, 25);

                int y = 43 + i * 30;

                // Index location = 17
                index.Location = new Point(17, y);
                Controls.Add(index);

                // Player 1 location = 52
                p1.Location = new Point(52, y);
                Controls.Add(p1);

                // Player 2 location = 136
                p2.Location = new Point(136, y);
                Controls.Add(p2);

                // Score location = 227
                score.Location = new Point(227, y);
                Controls.Add(score);
            }
        }

        /*
         * This removes all components from the Form, and then re-adds the title labels.
         */
        private void removeAll()
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                this.Controls.RemoveAt(i);
            }
            InitializeComponent();
        }

        /*
         * This is so that the form doesn't get destroyed, only hidden
         * on clicking the red "x" at the top right.
         * This makes it so that we can click the "Highscore" button many times.
         */
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        public Score[] getScores()
        {
            return this.scores;
        }

    }
}