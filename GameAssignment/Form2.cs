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

            //IEnumerable<Score> qry = scores;
            //if (sortCols.Length > 0)
            //{
            //    IOrderedEnumerable<int[]> sorted =
            //        qry.OrderBy(row => row[sortCols[0]]);
            //    for (int i = 1; i < sortCols.Length; i++)
            //    {
            //        int col = sortCols[i]; // for capture
            //        sorted = sorted.ThenBy(row => row[col]);
            //    }
            //    qry = sorted;
            //}

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
    }
}
