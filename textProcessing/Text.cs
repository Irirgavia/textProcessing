using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing
{
    public class Text
    {
        public List<Sentence> sentences;

        public void SortSentences()
        {
            sentences.Sort();
        }

        public List<string> FindWordInInterrogativeSentences(int wordLength)
        {
            //Во всех вопросительных предложениях текста найти и напечатать без повторений слова заданной длины
            
            List<string> words = new List<string>();

            foreach (var someSentence in sentences)
            {
                List<string> usedWords = new List<string>();
                if (someSentence.Type == SentenceType.interrogative)
                {
                    bool isWordUsed = false;

                    foreach(var word in someSentence.word)
                    {
                        foreach (string item in usedWords)
                        {
                            if (word == item) isWordUsed = true;
                        }

                        if (word.Length == wordLength && isWordUsed == false)
                        {
                            words.Add(word);
                            usedWords.Add(word);
                        }
                    }
                }
            }

            return words;
        }

        public void RemoveWordsFromConsonants(int wordLength)
        {
            //Из текста удалить все слова заданной длины, начинающиеся на согласную букву

            List<char> consonantsRus = new List<char>
            { 'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ',
              'Б', 'В', 'Г', 'Д', 'Ж', 'З', 'Й', 'К', 'Л', 'М', 'Н', 'П', 'Р', 'С', 'Т', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ',};

            List<char> consonantsEng = new List<char>
            { 'b', 'c', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'p', 'q', 'r', 's', 't', 'v', 'w', 'x', 'z',
              'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };
            
            for (int i = 0; i < sentences.Count; i++)
            {
                for (int j = 0; j < sentences[i].word.Count; j++) 
                {
                    bool isConsonantsFirst = false;

                    foreach (char consonant in consonantsRus)
                    {
                        if (sentences[i].word[j][0] == consonant) isConsonantsFirst = true;
                    }

                    foreach (char consonant in consonantsEng)
                    {
                        if (sentences[i].word[j][0] == consonant) isConsonantsFirst = true;
                    }

                    if (sentences[i].word[j].Length == wordLength && isConsonantsFirst)
                    {
                        sentences[i].removeWord(sentences[i].word[j], sentences[i].symbolAfterWord[j]);
                    }

                }
            }
        }

        public void ReplaceWordsToSubstring(int indexSentence, int wordLength, string substring)
        {
            /*В некотором предложении текста слова заданной длины заменить указанной подстрокой, длина которой
            может не совпадать с длиной слова.*/
            
            Sentence sentence = new Sentence();
            sentence = sentences[indexSentence];

            for (int i = 0; i < sentence.word.Count; i++)
            {
                if (sentence.word[i].Length == wordLength)
                {
                    sentence.word[i] = substring;
                }
            }

            sentences[indexSentence] = sentence;
        }

        public void MakeConcordance(string fileOutput)
        {
            //Анализ встечаемости слов
            Dictionary dictinary = new Dictionary(this);

            using (StreamWriter file = new StreamWriter(fileOutput))
            {
                for (int i = 0; i < dictinary.dictionaryWords.Count; i++)
                {
                    file.Write(dictinary.dictionaryWords[i].word + "[ " + dictinary.dictionaryWords[i].locationLine.Count + ": ", false, System.Text.Encoding.Default);
                    foreach (int item in dictinary.dictionaryWords[i].locationLine)
                    {
                        file.Write(item + " ");
                    }
                    file.Write("]", false, System.Text.Encoding.Default);
                    file.WriteLine();
                }
            }
        }

        public Text()
        {
            sentences = new List<Sentence>();
        }

        public Text(List<Sentence> sentence)
        {
            this.sentences = sentence;
        }
    }
}
