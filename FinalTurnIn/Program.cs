using FluentValidation;
using Spectre.Console;
using Figgle;

namespace FinalTurnIn
{

    class Program
    {
        static void Main(string[] args)
        {

            var validator = new UserInputValidator();
            var highScoreStorage = new FileHighScoreStorage();
            var game = new Game(validator, highScoreStorage);

            AnsiConsole.MarkupLine("[bold green]Välkommen till Gissningsspelet![/]");
            AnsiConsole.MarkupLine(Figgle.FiggleFonts.Starwars.Render("Gissningslek"));

            while (true)
            {
                AnsiConsole.MarkupLine("[bold blue]1. Starta spel[/]");
                AnsiConsole.MarkupLine("[bold blue]2. Visa high scores[/]");
                AnsiConsole.MarkupLine("[bold red]3. Avsluta[/]");
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                    .AddChoices("Starta spel", "Visa high scores", "Avsluta"));

                switch (choice)
                {
                    case "Starta spel":
                        game.Start();
                        break;
                    case "Visa high scores":
                        highScoreStorage.DisplayHighScores();
                        break;
                    case "Avsluta":
                        return;
                }
            }
        }
    }


}