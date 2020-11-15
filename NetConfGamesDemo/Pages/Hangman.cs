using NetConfGamesDemo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetConfGamesDemo.Pages
{
    public partial class Hangman
    {
        string letters = "abcdefghijklmnopqrstuvwxyz";

        List<string> languages = new List<string>
            {
                "blazor",
                "xamarin",
                "aspnet",
                "csharp",
                "cosmosdb"
            };

        string answer = "";
        private WordSpotlight WordSpotlight;
        List<char> guessed = new List<char>();


        private void PickWord()
        {
            answer = languages[new Random().Next(0, languages.Count)];
            Console.WriteLine(answer);
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();
            PickWord();
            
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
            WordSpotlight.CalculateWord(answer, guessed);
        }

        int maxWrong = 6;
        int mistakes = 0;
        string currentImage = "/images/0.jpg";
        string message = "";
        Dictionary<char, Letter> LetterComponents = 
            new Dictionary<char, Letter>();

        private void HandleGuess(char letter)
        {
            if (guessed.IndexOf(letter) == -1)
            {
                guessed.Add(letter);
            }
            if(answer.IndexOf(letter) >= 0)
            {
                WordSpotlight.CalculateWord(answer, guessed);
                CheckIfGameWon();
            }
            else if(answer.IndexOf(letter) == -1)
            {
                mistakes++;
                CheckIfGameLost();
                currentImage = $"/images/{mistakes}.jpg";
            }
            Console.WriteLine(letter);
        }

        private void CheckIfGameWon()
        {
            if (WordSpotlight.WordStatus.Replace(" ", "") == answer)
            {
                message = "You won!!";
            }
        }

        private void CheckIfGameLost()
        {
            if (mistakes == maxWrong)
            {
                message = "You Lost!!";
                DisableLetters();
            }
        }

        private void DisableLetters()
        {
            foreach (var letter in LetterComponents)
            {
                letter.Value.LetterStyle = "disabled";
                letter.Value.CanExecute = false;
            }
        }

        private void EnableLetters()
        {
            foreach (var letter in LetterComponents)
            {
                letter.Value.LetterStyle = "";
                letter.Value.CanExecute = true;
            }
        }

        private void Reset()
        {
            mistakes = 0;
            guessed = new List<char>();
            currentImage = "/images/0.jpg";
            PickWord();
            WordSpotlight.CalculateWord(answer, guessed);
            EnableLetters();
            message = "";
        }


    }
}
