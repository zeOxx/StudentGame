using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Perihelion.Models
{
    class HighscoreTable
    {
        public static int numOfItemsInTable = 10;

        public string[] ranks   = new string[numOfItemsInTable];
        public string[] names   = new string[numOfItemsInTable];
        public int[] scores     = new int[numOfItemsInTable];

        public HighscoreTable()
        {
            //empty object
        }

        public HighscoreTable(string[] ranks, string[] names, int[] scores)
        {
            Ranks = ranks;
            Names = names;
            Scores = scores;
        }

        public string[] Ranks
        {
            get { return this.ranks; }
            set { this.ranks = value; }
        }

        public string[] Names
        {
            get { return this.names; }
            set { this.names = value; }
        }

        public int[] Scores
        {
            get { return this.scores; }
            set { this.scores = value; }
        }
    }
}
