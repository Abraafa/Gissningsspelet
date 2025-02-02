using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace FinalTurnIn
{
    public class Game
    {
        private int _numberToGuess;
        private List<PlayerResult> _leaderboard;
        private IUserInputValidator _validationService;

        public Game(IUserInputValidator validationService, FileHighScoreStorage highScoreStorage)
        {
            _validationService = validationService;
            _leaderboard = new List<PlayerResult>();
        }

        public void Start()
        {
            
            AnsiConsole.Markup("[bold yellow]Vad heter du?[/]");
            string playerName = Console.ReadLine();

            AnsiConsole.Markup("[bold yellow]Nu börjar spelet! Gissa ett nummer mellan 1 och 100.[/]");

            _numberToGuess = new Random().Next(1, 101); 
            int userGuess = 0;
            int guessCount = 0;

            while (userGuess != _numberToGuess)
            {
                userGuess = GetUserInput();
                guessCount++;

                if (userGuess > _numberToGuess)
                {
                    AnsiConsole.Markup("[red]För högt! Försök igen.[/]");
                }
                else if (userGuess < _numberToGuess)
                {
                    AnsiConsole.Markup("[yellow]För lågt! Försök igen.[/]");
                }
            }

            AnsiConsole.Markup("[green]Rätt gissat! Du har vunnit![/]");

            _leaderboard.Add(new PlayerResult(playerName, guessCount));

            var sortedLeaderboard = _leaderboard.OrderBy(r => r.NumberOfGuesses).ToList();

            int playerRank = sortedLeaderboard.FindIndex(r => r.PlayerName == playerName) + 1; 
            AnsiConsole.Markup($"[bold blue]Du gissade rätt på {guessCount} försök! Du är nummer {playerRank} på rankingen.[/]");
        }

        private int GetUserInput()
        {
            int userGuess = 0;
            bool validInput = false;

            while (!validInput)
            {
                AnsiConsole.Markup("[bold]Gissa ett nummer mellan 1 och 100:[/]");
                string input = Console.ReadLine();

                try
                {
                    validInput = _validationService.Validate(input, out userGuess);
                    if (!validInput)
                    {
                        throw new ArgumentException("Gissningen måste vara ett nummer mellan 1 och 100.");
                    }
                }
                catch (Exception ex)
                {
                    AnsiConsole.Markup($"[red]Fel vid inmatning: {ex.Message}[/]");
                }
            }
            return userGuess;
        }
    }

    public class PlayerResult
    {
        public string PlayerName { get; }
        public int NumberOfGuesses { get; }

        public PlayerResult(string playerName, int numberOfGuesses)
        {
            PlayerName = playerName;
            NumberOfGuesses = numberOfGuesses;
        }
    }


}
