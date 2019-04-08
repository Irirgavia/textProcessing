using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing
{
    public class DictionaryWord : IComparable<DictionaryWord>
    {
        public string word;
        public List<int> locationLine;

        public int CompareTo(DictionaryWord other)
        {
            return String.Compare(this.word,other.word, true);
        }

        public DictionaryWord()
        {
            word = null;
            locationLine = new List<int>();
        }
    }
    public class Dictionary
    {
        public List<DictionaryWord> dictionaryWords;

        public Dictionary(Text text)
        {
            dictionaryWords = new List<DictionaryWord>();
            List<string> words = new List<string>();

            foreach (Sentence sentence in text.sentences)
            {
                foreach(string someWord in sentence.word)
                {
                    DictionaryWord dictionaryWord = new DictionaryWord();
                    dictionaryWord.word = someWord;
                    dictionaryWord.locationLine.Add(sentence.locationLine);

                    if (words.IndexOf(dictionaryWord.word) != -1)
                    {
                        dictionaryWords[words.IndexOf(dictionaryWord.word)].locationLine.Add(sentence.locationLine);
                    }
                    else
                    {
                        dictionaryWords.Add(dictionaryWord);
                        words.Add(dictionaryWord.word);
                    }
                }
                
            }

            dictionaryWords.Sort();
        }
    }
}
