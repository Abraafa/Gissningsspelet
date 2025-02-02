using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Xml;
using Spectre.Console;

namespace FinalTurnIn
{

    public class FileHighScoreStorage : IHighScoreStorage
    {
        private const string filePath = "highscores.json";

        public void SaveHighScore(int score, string playerName)
        {
            var scores = LoadScores();
            scores.Add(new PlayerResult(playerName, score));
            scores = scores.OrderBy(s => s.NumberOfGuesses).Take(5).ToList();  // Håller bara topp 5

            File.WriteAllText(filePath, JsonConvert.SerializeObject(scores, Newtonsoft.Json.Formatting.Indented));
        }

        public void DisplayHighScores()
        {
            var scores = LoadScores();
            AnsiConsole.MarkupLine("\nTop 5 High Scores:");
            foreach (var score in scores)
            {
                AnsiConsole.MarkupLine($"{score.PlayerName} - {score.NumberOfGuesses} försök");
            }
        }

        private List<PlayerResult> LoadScores()
        {
            if (!File.Exists(filePath))
                return new List<PlayerResult>();

            var json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<PlayerResult>>(json) ?? new List<PlayerResult>();
        }

        public void SaveHighScore(int score)
        {
            throw new NotImplementedException();
        }
    }


}
