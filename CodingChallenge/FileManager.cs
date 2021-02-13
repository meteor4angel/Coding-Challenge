using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace CodingChallenge.Utilities
{
    public static class FileManager
    {
        public static void GetInputFromFile(out string strInput, string inputFileLocation)
        {
            string path = Directory.GetCurrentDirectory();
            inputFileLocation = Path.Combine(path, inputFileLocation);

            var fileStream = new FileStream(inputFileLocation, FileMode.Open, FileAccess.Read);

            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                strInput = streamReader.ReadToEnd();
            }

        }

        public static void SetOutputToFile(string strInput, string outputFileLocation)
        {
            string path = Directory.GetCurrentDirectory();
            outputFileLocation = Path.Combine(path, outputFileLocation);

            using (StreamWriter streamWriter = new StreamWriter(outputFileLocation))
            {
                streamWriter.Write(strInput);
            }
        }
    }


}

