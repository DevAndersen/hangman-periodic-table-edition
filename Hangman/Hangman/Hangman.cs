using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hangman
{
    public class Hangman
    {
        public string Word { get; }
        public int Lives { get; set; }
        public List<Char> Guesses { get; }

        public Hangman()
        {
            Word = Elements.GetRandomElement();
            Lives = 10;
            Guesses = new List<Char>();
        }

        public void Guess(Char c)
        {
            c = Char.ToLower(c);
            if (Guesses.Contains(c) || !Word.ToLower().Contains(c))
            {
                Lives--;
            }
            Guesses.Add(c);
        }

        public bool HasPlayerWon()
        {
            foreach (Char c in Word.ToLower())
            {
                if (!Guesses.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
