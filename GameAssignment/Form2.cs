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
            InitializeComponent();
        }

        public void addScore(string player1, string player2, string score)
        {
            Score toAdd = new Score(player1, player2, score);
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == null)
                {
                    scores[i] = toAdd;
                    sortScoreArray(scores, 0, i);
                    return;
                }
            }
            if (toAdd.getDiff() > scores[scores.Length].getDiff())
            {
                scores[scores.Length] = toAdd;
            }

            sortScoreArray(scores, 0, scores.Length);

        }

        private void shiftArrayUp(Object[] arr)
        {
            Object[] output = new Object[arr.Length];
            for (int i = 1; i < arr.Length; i++)
            {
                output[i - 1] = arr[i];
            }
            output[arr.Length] = null;
            arr = output;
        }

        private void sortScoreArray(Score[] arr, int left, int right)
        {
            int index = partition(arr, left, right);
            if (left < index - 1)
                sortScoreArray(arr, left, index - 1);
            if (index < right)
                sortScoreArray(arr, index, right);
        }

        private int partition(Score[] arr, int left, int right)
        {
            int i = left, j = right;
            Score tmp;
            Score pivot = arr[(left + right) / 2];

            while (i <= j)
            {
                while (arr[i].getDiff() < pivot.getDiff())
                {
                    i++;
                }
                while (arr[j].getDiff() > pivot.getDiff())
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

        public void display()
        {
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
         * This is so that the form doesn't get destroyed, only hidden
         * on clicking the red "x" at the top right.
         * This makes it so that we can click the "Highscore" button many times.
         */
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

    }
}
