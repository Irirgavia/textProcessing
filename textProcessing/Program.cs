using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            string nameFile = "Text.txt";
            Text text = Parsing.MadeText(nameFile);

            List<string> menu = new List<string>
            {
                "What do you want to do?",
                "Click 0 - Exit.",
                "Click 1 - Output all sentences of the given text in ascending order of the number of words in each of them.",
                "Click 2 - In all interrogative sentences of the text, find and print without repetition words of a given length.",
                "Click 3 - From the text, delete all words of a given length, starting with a consonant letter.",
                "Click 4 - In some sentence of the text of a word of a given length, replace the specified substring.",
                "Click 5 - An analysis of words in the text.",
                ""
            };

            string line = null;
            while (line != "0")
            {
                foreach (string option in menu)
                    Console.WriteLine(option);

                line = Console.ReadLine();

                int temp = 0;

                if (int.TryParse(line, out temp))
                {
                    switch (int.Parse(line))
                    {
                        case 1:
                            {
                                text.SortSentences();

                                foreach (Sentence sentence in text.sentences)
                                {
                                    Console.WriteLine(sentence.ToString());
                                }
                                Console.WriteLine();
                            }
                            break;

                        case 2:
                            {
                                string wordLength;
                                int tempResult = 0;

                                do
                                {
                                    Console.WriteLine("Input the length of words: ");
                                    wordLength = Console.ReadLine();
                                }
                                while (int.TryParse(wordLength, out tempResult) == false);
                                
                                List<string> words = text.FindWordInInterrogativeSentences(int.Parse(wordLength));

                                foreach(var word in words)
                                {
                                    Console.WriteLine(word);
                                }

                                Console.WriteLine();
                            }
                            break;

                        case 3:
                            {
                                string wordLength;
                                int tempResult = 0;

                                do
                                {
                                    Console.WriteLine("Input the length of words: ");
                                    wordLength = Console.ReadLine();
                                }
                                while (int.TryParse(wordLength, out tempResult) == false);

                                text.RemoveWordsFromConsonants(int.Parse(wordLength));

                                foreach (Sentence sentence in text.sentences)
                                {
                                    Console.WriteLine(sentence.ToString());
                                }
                                Console.WriteLine();
                            }
                            break;

                        case 4:
                            {
                                string wordLength;
                                string indexSentence;
                                string subString;
                                int tempResult = 0;

                                do
                                {
                                    Console.WriteLine("Input the length of words: ");
                                    wordLength = Console.ReadLine();
                                }
                                while (int.TryParse(wordLength, out tempResult) == false);

                                do
                                {
                                    Console.WriteLine("Input the number of sententes: ");
                                    indexSentence = Console.ReadLine();
                                }
                                while (int.TryParse(indexSentence, out tempResult) == false);
                                
                                Console.WriteLine("Input the substring: ");
                                subString = Console.ReadLine();
                                
                                text.ReplaceWordsToSubstring(int.Parse(indexSentence), int.Parse(wordLength), subString);

                                foreach (Sentence sentence in text.sentences)
                                {
                                    Console.WriteLine(sentence.ToString());
                                }
                                Console.WriteLine();
                            }
                            break;

                        case 5:
                            {
                                string concordance = "Concordance.txt";
                                text.MakeConcordance(concordance);
                            }
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid option. Repeat.");
                }
            }
        }
    }
}