using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchText.Logic;

namespace PrintResult
{
    public class PrintResult
    {
        public void printResult(SearchResult result)
        {
            foreach (var source in result.FileResult.Keys)//each key is a name of a file or a path to a site
            {
                foreach (var line in result.FileResult[source])
                {
                    Console.WriteLine(source + " : " + line);
                }
            }
        }
    }
}
