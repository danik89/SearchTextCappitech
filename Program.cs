using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchTextRunner
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                SearchText.SearchText searchText = new SearchText.SearchText();
                PrintResult.PrintResult printResult = new PrintResult.PrintResult();
                if (args[0] != "grep")
                    Console.WriteLine("In order to search for words, please start the search with the word 'grep'");
                else
                {
                    log.Debug("Starting To Check If The Word Exists");
                    printResult.printResult(searchText.Grep(args));
                }
            }
        }
    }
}
