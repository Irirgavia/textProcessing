using Microsoft.VisualStudio.TestTools.UnitTesting;
using textProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing.Tests
{
    [TestClass()]
    public class SentenceTests
    {
        [TestMethod()]
        public void ToStringTest()
        {
            string sentenceText = "It is sentence.";
            Sentence sentence = new Sentence(new List<string> { "It", "is", "sentence" }, new List<char> { ' ', ' ', '.' }, 1);

            Assert.AreEqual(sentenceText, sentence.ToString());
        }
    }
}