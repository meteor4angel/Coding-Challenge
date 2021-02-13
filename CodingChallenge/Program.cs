using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingChallenge.Utilities;
using System.Configuration;

namespace CodingChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            string wordList = string.Empty;
            string stringList = string.Empty;

            string inputFileLocation = ConfigurationManager.AppSettings["InputLocation"].ToString();
            string outputFileLocation = ConfigurationManager.AppSettings["OutputLocation"].ToString();

            string sourceFile = inputFileLocation + @"\" + ConfigurationManager.AppSettings["StringList"].ToString();
            string filterFile = inputFileLocation + @"\" + ConfigurationManager.AppSettings["WordList"].ToString();
            string outputFile = outputFileLocation + @"\" + ConfigurationManager.AppSettings["OutputFileName"].ToString();

            FileManager.GetInputFromFile(out stringList, sourceFile);

            FileManager.GetInputFromFile(out wordList, filterFile);

            Process process = new Process();
            string endResult = process.ProcessCodingChallenge(stringList, wordList);

            FileManager.SetOutputToFile(endResult, outputFile);

            Console.WriteLine(endResult);
            Console.ReadKey();
            

        }

    }
}
