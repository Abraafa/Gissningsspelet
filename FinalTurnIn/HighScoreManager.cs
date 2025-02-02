using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTurnIn
{
    public class HighScoreManager : IHighScoreStorage
    {
        private List<int> highScores = new List<int>();

        public void SaveHighScore(int score)
        {
            highScores.Add(score);
            highScores = highScores.OrderBy(s => s).Take(5).ToList();  // Håller bara topp 5
            DisplayHighScores();
        }

        public void DisplayHighScores()
        {
            Console.WriteLine("\nTop 5 High Scores:");
            foreach (var score in highScores)
            {
                Console.WriteLine(score);
            }
        }
    }

}
