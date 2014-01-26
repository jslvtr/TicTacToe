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
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] == null)
                {
                    scores[i] = new Score(player1, player2, score);
                    return;
                }
            }
            scores[scores.Length] = new Score(player1, player2, score);

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
                    i++;
                while (arr[j].getDiff() > pivot.getDiff())
                    j--;
                if (i <= j)
                {
                    tmp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = tmp;
                    i++;
                    j--;
                }
            };

            return i;
        }
    }
}
