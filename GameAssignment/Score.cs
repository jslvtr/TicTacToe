using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameAssignment
{
    public class Score
    {
        private string player1, player2;
        private string score;
        private int diff;

        public Score(string p1, string p2, string sc)
        {
            player1 = p1;
            player2 = p2;
            score = sc;
            diff = Math.Abs(int.Parse(sc.Split(':')[0]) - int.Parse(sc.Split(':')[1]));
        }

        public string getPlayer1()
        {
            return this.player1;
        }

        public string getPlayer2()
        {
            return this.player2;
        }

        public string getScore()
        {
            return this.score;
        }

        public int getDiff()
        {
            return this.diff;
        }
    }
}
