using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace textProcessing
{
    public static class Parsing
    {
        public static Text MadeText(string fileWithBook)
        {
            Text text = new Text();

            string lineWord = null;
            bool isSentenceEnd = false;
            int countOfString = 1;

            Sentence sentence = new Sentence();

            StreamReader file = new StreamReader(fileWithBook, Encoding.GetEncoding(1251));
            while (file.Peek() > -1)
            {
                char currentSymbol = (char)file.Read();

                if (isSentenceEnd == true && (currentSymbol == ' ' || currentSymbol == '\r'))
                {
                    if (currentSymbol != ' ') isSentenceEnd = false;

                    if (currentSymbol == '\r') countOfString++;

                    continue;
                }
                else isSentenceEnd = false;

                if (currentSymbol != ' ' && currentSymbol != '.' && currentSymbol != ',' && currentSymbol != '!' && currentSymbol != '?' && currentSymbol != '-' && currentSymbol != '"' &&
                     currentSymbol != '(' && currentSymbol != ')' && currentSymbol != '/' && currentSymbol != '\r' && currentSymbol != '\n' && currentSymbol != '\t')
                {
                    lineWord += currentSymbol;
                }
                else
                {
                    if (currentSymbol != '.' && currentSymbol != '!' && currentSymbol != '?' && currentSymbol != '\r' && currentSymbol != '\n')
                    {
                        if (lineWord != null && lineWord != " ")
                        {
                            sentence.word.Add(lineWord);
                            sentence.symbolAfterWord.Add(currentSymbol);
                            lineWord = null;
                        }
                    }
                    else
                    {
                        if (currentSymbol == '\n' || lineWord == null) continue;

                        sentence.locationLine = countOfString;

                        if (currentSymbol != '\r')
                        {
                            sentence.symbolAfterWord.Add(currentSymbol);
                        }
                        else
                        {
                            sentence.symbolAfterWord.Add(' ');
                            countOfString++;
                        }

                        sentence.word.Add(lineWord);
                        lineWord = null;
                        text.sentences.Add(sentence);
                        sentence = new Sentence();
                        isSentenceEnd = true;
                    }
                }
            }
            file.Close();

            return text;
        }
    }
}
