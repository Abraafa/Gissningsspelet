using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace FinalTurnIn
{

    public class UserInputValidator : IUserInputValidator
    {
        public bool Validate(string input, out int userGuess)
        {
            userGuess = 0;
            var validator = new NumberValidator();
            var validationResult = validator.Validate(input);
            if (validationResult.IsValid)
            {
                userGuess = int.Parse(input);
                return userGuess >= 1 && userGuess <= 100;
            }
            return false;
        }
    }

    public class NumberValidator : AbstractValidator<string>
    {
        public NumberValidator()
        {
            RuleFor(x => x).Matches(@"^\d+$").WithMessage("Input måste vara ett nummer.");
        }
    }

    public interface IUserInputValidator
    {
        bool Validate(string input, out int userGuess);
    }

}
