using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Hangman - Periodic table edition";
            new Program().Run();
        }

        private void Run()
        {
            bool keepPlaying = true;
            Hangman hangman = new Hangman();

            while (keepPlaying)
            {
                RenderGame(hangman);
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    keepPlaying = false;
                }
                else if (Char.IsLetter(keyInfo.KeyChar))
                {
                    hangman.Guess(keyInfo.KeyChar);
                    if (hangman.Lives == 0 || hangman.HasPlayerWon())
                    {
                        RenderGame(hangman);
                        Console.WriteLine("Press [Enter] to play again.");
                        Console.ReadLine();
                        hangman = new Hangman();
                    }
                }
            }
        }

        private void RenderGame(Hangman hangman)
        {
            string word = hangman.Word;
            word.ToCharArray().ToList().ForEach(c =>
            {
                if (!hangman.Guesses.Contains(c))
                {
                    word = word.Replace(c, '-');
                }
            });

            Console.Clear();
            Console.WriteLine("Press [Esc] to exit.");
            Console.Write("\nWord: ");
            hangman.Word.ToCharArray().ToList().ForEach(c =>
            {
                if (hangman.Lives == 0)
                {
                    Console.ForegroundColor = hangman.Guesses.Contains(c) ? ConsoleColor.Gray : ConsoleColor.Red;
                }
                Console.Write(hangman.Lives == 0 ? c.ToString() : (hangman.Guesses.Contains(c) ? c.ToString() : "-"));
            });
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            Console.WriteLine("\nLives: " + hangman.Lives);


            List<char> usedLetters = new List<char>();
            Console.Write("\nGuesses: ");
            hangman.Guesses.ForEach(c =>
            {
                Console.ForegroundColor = !hangman.Word.Contains(c) ? ConsoleColor.Red : (usedLetters.Contains(c) ? ConsoleColor.Yellow : ConsoleColor.Gray);
                Console.Write(c + " ");
                usedLetters.Add(c);
            });
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine();

            if (hangman.Lives <= 0)
            {
                Console.WriteLine("\nYou lost.");
            }
            else if (hangman.HasPlayerWon())
            {
                Console.WriteLine("\nYou won!");
            }
            else
            {
                Console.Write("\n> ");
            }
        }
    }
}
