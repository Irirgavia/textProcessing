using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing
{
    public enum SentenceType
    {
        interrogative,
        exclamatory,
        narrative,
        unknown
    }

    public class Sentence : IComparable<Sentence>
    {
        public List<string> word;
        public List<char> symbolAfterWord;
        public int locationLine;

        public void addWord(string newWord, char newSymbolAfterWord)
        {
            word.Add(newWord);
            symbolAfterWord.Add(newSymbolAfterWord);
        }

        public void removeWord(string removedWord, char removedSymbolAfterWord)
        {
            word.Remove(removedWord);
            symbolAfterWord.Remove(removedSymbolAfterWord);
        }

        public SentenceType Type
        {
            get
            {
                if (symbolAfterWord[word.Count - 1] == '?')
                {
                    return SentenceType.interrogative;
                }

                if (symbolAfterWord[word.Count - 1] == '!')
                {
                    return SentenceType.exclamatory;
                }

                if (symbolAfterWord[word.Count - 1] == '.')
                {
                    return SentenceType.narrative;
                }

                return SentenceType.unknown;
            }
        }

        public override string ToString()
        {
            string sentence = null;

            for (int j = 0; j < word.Count; j++)
            {
                sentence += word[j] + symbolAfterWord[j];
            }
            return sentence;
        }

        public Sentence()
        {
            word = new List<string>();
            symbolAfterWord = new List<char>();
            locationLine = 1;
        }

        public Sentence(List<string> word, List<char> symbolAfterWord, int locationLine)
        {
            this.word = word;
            this.symbolAfterWord = symbolAfterWord;
            this.locationLine = locationLine;
        }

        public int CompareTo(Sentence other)
        {
            if (this.word.Count > other.word.Count)
            {
                return 1;
            }
            else if (this.word.Count < other.word.Count)
            {
                return -1;
            }
            return 0;
        }

    }
}
