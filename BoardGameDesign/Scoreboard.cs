using System;
using System.Collections.Generic;

namespace BoardGameDesign
{
    public class Scoreboard
    {
        protected Dictionary<Player, int> ScoreRecords { get; set; }

        public Scoreboard(Player p1, Player p2, int initScore = 0)
        {
            ScoreRecords = new Dictionary<Player, int>();
            ScoreRecords[p1] = initScore;
            ScoreRecords[p2] = initScore;
        }

        public void SetScore(Player player, int score)
        {
            ScoreRecords[player] = score;
        }

        public void ModifyScore(Player player, int delta)
        {
            ScoreRecords[player] += delta;
        }

        public void DeleteAll()
        {
            ScoreRecords.Clear();
        }

        public void ResetScores()
        {
            foreach (KeyValuePair<Player, int> entry in ScoreRecords)
            {
                ScoreRecords[entry.Key] = 0;
            }
        }

        public void DisplayScores()
        {
            Console.WriteLine("================================================");
            Console.WriteLine("                   Scoreboard                   ");
            Console.WriteLine("================================================");
            Console.WriteLine();
            foreach (KeyValuePair<Player, int> entry in ScoreRecords)
            {
                Console.WriteLine($"\t{entry.Key}:\t{entry.Value} pts.");
            }
            Console.WriteLine();
            Console.WriteLine("================================================");
        }
    }
}