using Microsoft.VisualStudio.TestTools.UnitTesting;
using textProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace textProcessing.Tests
{
    [TestClass()]
    public class TextTests
    {
        [TestMethod()]
        public void SortSentencesTest()
        {
            List<Sentence> expectedSentences = new List<Sentence>();
            List<Sentence> sentences = new List<Sentence>();

            Sentence sentence0 = new Sentence(new List<string> { "It", "is", "also", "sentence" }, new List<char> { ' ', ' ', ' ', '.' }, 1);
            Sentence sentence1 = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', '.' }, 1);

            expectedSentences.Add(sentence1);
            expectedSentences.Add(sentence0);

            sentences.Add(sentence0);
            sentences.Add(sentence1);

            Text text = new Text(sentences);
            text.SortSentences();

            CollectionAssert.AreEqual(expectedSentences, text.sentences);
        }

        [TestMethod()]
        public void FindWordInInterrogativeSentencesTest()
        {
            Sentence sentence0 = new Sentence(new List<string> { "Is", "it", "sentence" }, new List<char> { ' ', ' ', '?' }, 1);
            Sentence sentence1 = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', ' ', '.' }, 1);

            List<Sentence> sentences = new List<Sentence>();
            sentences.Add(sentence0);
            sentences.Add(sentence1);

            int wordLength = 2;
            Text text = new Text(sentences);

            List<string> expectedWord = new List<string> { "Is", "it" };

            CollectionAssert.AreEqual(expectedWord, text.FindWordInInterrogativeSentences(wordLength));
        }

        [TestMethod()]
        public void RemoveWordsFromConsonantsTest()
        {
            Sentence sentence0 = new Sentence(new List<string> { "Is", "it", "good", "sentence" }, new List<char> { ' ', ' ', ' ', '?' }, 1);
            Sentence sentence1 = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', '.' }, 1);

            List<Sentence> sentences = new List<Sentence>();
            sentences.Add(sentence0);
            sentences.Add(sentence1);

            int wordLength = 4;
            Text text = new Text(sentences);
            text.RemoveWordsFromConsonants(wordLength);

            Text expectedText = new Text(sentences);
            expectedText.sentences[0].removeWord("good", ' ');

            CollectionAssert.AreEqual(expectedText.sentences, text.sentences);
        }

        [TestMethod()]
        public void ReplaceWordsToSubstringTest()
        {
            Sentence sentence0 = new Sentence(new List<string> { "Is", "it", "good", "sentence" }, new List<char> { ' ', ' ', ' ', '?' }, 1);
            Sentence sentence1 = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', '.' }, 1);

            List<Sentence> sentences = new List<Sentence>();
            sentences.Add(sentence0);
            sentences.Add(sentence1);

            int indexSentence = 0;
            int wordLength = 4;
            string substring = "a";

            Text text = new Text(sentences);
            text.ReplaceWordsToSubstring(indexSentence, wordLength, substring);

            Text expectedText = new Text(sentences);
            expectedText.sentences[0].word[2] = substring;

            CollectionAssert.AreEqual(expectedText.sentences, text.sentences);
        }

        [TestMethod()]
        public void MakeConcordanceTest()
        {
            Sentence sentence0 = new Sentence(new List<string> { "Is", "it", "good", "sentence" }, new List<char> { ' ', ' ', ' ', '?' }, 1);
            Sentence sentence1 = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', '.' }, 1);

            List<Sentence> sentences = new List<Sentence>();
            sentences.Add(sentence0);
            sentences.Add(sentence1);

            string fileOutput = "Concordance.txt";
            Text text = new Text(sentences);
            text.MakeConcordance(fileOutput);

            string actualRezult = "";

            using(StreamReader file = new StreamReader(fileOutput))
            {
                while(file.Peek() > -1)
                {
                    char symbol = (char)file.Read();
                    if (symbol != '\n' && symbol != '\r') actualRezult += symbol;
                }
            }

            string epectedRezult = "good[ 1: 1 ]Is[ 1: 1 ]is[ 1: 1 ]it[ 1: 1 ]It[ 1: 1 ]sentence[ 2: 1 1 ]";

            Assert.AreEqual(epectedRezult, actualRezult);
        }
    }
}