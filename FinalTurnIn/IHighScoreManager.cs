using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTurnIn
{
    public interface IHighScoreStorage
    {
        void SaveHighScore(int score);
        void DisplayHighScores();
    }

}
