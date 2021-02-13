using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodingChallenge.Utilities;
using System.Text;
using System;

namespace CodingChallenge_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestWithDuplicate()
        {
            Process process = new Process();

            string article = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Lorem");
            sb.AppendLine("ipsum");
            sb.AppendLine("Lorem");

            
            string  result = process.ProcessCodingChallenge(article, sb.ToString());
            //Should remove duplicate words; else could change logic
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("a. Lorem {1:1}");
            expected.AppendLine("b. ipsum {1:1}");
            Assert.AreEqual(expected.ToString(), result);
        }

        [TestMethod]
        public void TestWithAbbrev()
        {
            Process process = new Process();

            string article = @"I didn't know whether to use 'Mr' or 'Ms' so I asked Dr. Jeanne for help. It's a journalistic error compounded by slipshod editing said, but I didn't know. Sometimes, it's even harder to compose with abbrev like i.e. or e.g. or the St. with stands for Saint or Alt. for Altitude";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Didn't");
            sb.AppendLine("Know");
            sb.AppendLine("I");
            sb.AppendLine("'Mr'");
            sb.AppendLine("Dr.");
            sb.AppendLine("Alt.");
            sb.AppendLine("St.");


            string result = process.ProcessCodingChallenge(article, sb.ToString());
            //Should remove duplicate words; else could change logic
            StringBuilder expected = new StringBuilder();
            expected.AppendLine("a. Didn't {2:1,2}");
            expected.AppendLine("b. Know {2:1,2}");
            expected.AppendLine("c. I {3:1,1,2}");
            expected.AppendLine("d. 'Mr' {1:1}");
            expected.AppendLine("e. Dr. {1:1}");
            expected.AppendLine("f. Alt. {1:3}");
            expected.AppendLine("g. St. {1:3}");

            Assert.AreEqual(expected.ToString(), result.ToString());
            //CollectionAssert.AreEqual(expected.ToString(), result);
        }
    }
}
