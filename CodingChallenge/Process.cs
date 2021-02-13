using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodingChallenge.Utilities
{
    public class Process
    {
        public string ProcessCodingChallenge(string stringList, string wordList)
        {

            List<string> sourceWordList = this.SplitSource(stringList);

            List<string> filterWordList = this.SplitFilter(wordList);

            List<DataObject.WordCounter> wordCounterList = new List<DataObject.WordCounter>();

            if (filterWordList.Any())
                foreach (var word in filterWordList)
                {
                    wordCounterList.Add(this.Test(sourceWordList, word));
                }

            StringBuilder sb = new StringBuilder();

            int count = 0;

            if (wordCounterList.Any())
                foreach (var wordCounter in wordCounterList)
                {
                    sb.AppendLine(this.CreateLine(count, wordCounter.Word, wordCounter.PositionList));
                    count++;
                }

            return (sb != null) ? sb.ToString() : string.Empty;
        }

        private bool CheckifAbbrev(string source)
        {
            string[] abbrev = new string[] { "Mrs", "Mr", "E.g", "I.E", "Bb", "Dr", "St","Alt"  };

            foreach (var s in abbrev)
            {
                if (source.EndsWith(s, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> SplitSource(string text)
        {           
            List<string> output = new List<string>();

            int startIndex = 0;

            for (int i = 0; i < text.Length;i++)
            {
                //string test = text.Substring(startIndex, i - startIndex+1);

                int c = (i == (text.Length - 1)) ? 1 : 2;

                if  (text.Substring(i,c).Equals(". ") || (c == 1))
                {             
                    if (!CheckifAbbrev(text.Substring(startIndex, i-startIndex )))
                    {
                        output.Add(text.Substring(startIndex, i - startIndex));
                        startIndex = i+2;
                    }
                }

            }

            return output.ToList();

            //Convert the string into an array of words  
            //return text.Split(new string[] { ". ",","," ","\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            //return text.Split(new string[] { ". ", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();         
        }

        private List<string> SplitFilter(string text)
        {
            //distinct the word filter else remove Distinct
            return text.Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToList();
        }

        private DataObject.WordCounter Test(List<string> source, string word)
        {

            DataObject.WordCounter wordCounter = null;

            if (source.Any())
            {
                var obj = source.Select((sentence, index) => new { index = index + 1, list = sentence.Split(new string[] { ", "," " }, StringSplitOptions.RemoveEmptyEntries).ToList().FindAll(s => s.Equals(word, StringComparison.OrdinalIgnoreCase)) })
                    .Where(x => x.list.Count > 0).ToList();

                List<string> indexes = new List<string>();

                foreach (var index in obj)
                {
                    indexes.AddRange(new List<string>(Enumerable.Repeat(index.index.ToString(), index.list.Count)));
                }

                wordCounter = new DataObject.WordCounter()
                {
                    Word = word,
                    PositionList = indexes
                };

            }

            return wordCounter;
        }


        //Coud also use this to count the occurence
        private int CountWord(List<string> source, string word)
        {
            return source.Count(f => f == word);
        }

        private string CreateLine(int counter, string word, List<string> indexes)
        {
            // a. this {3:1,6,8}
            //<alphabet increment>. {word}<space>{<times appear in article sentences>:line#/s from article}

            char digit = (char)(97 + counter % 26);  // utf/ascii code 97 == 'a'

            return (new String(digit, counter / 26 + 1)) + ". " + word + " {" + indexes.Count() + ":" + String.Join(",", indexes) + "}";
        }

    }
}
