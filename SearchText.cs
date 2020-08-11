using SearchText.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SearchText
{
    public class SearchText
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private InputTranslate _inputTranslate;
        
        public SearchText()
        {
            _inputTranslate = new InputTranslate();
        }
        public SearchResult Grep (string[] inputString)
        {
            log.Debug("Grep function started");
            var wc = new WebClient();
            SearchResult searchResult = new SearchResult();
            StringComparison comparer = StringComparison.Ordinal;
            searchResult.FileResult = new Dictionary<string, List<string>>();
            var translated = _inputTranslate.TranslateInput(inputString);
            if (translated.IsInsensetive)
                comparer = StringComparison.OrdinalIgnoreCase;
            foreach (var source in translated.Sources)//searching in all files
            {
                if (source.IsFile && File.Exists(source.Path) || !source.IsFile)
                {
                    try
                    {
                        string[] lines;
                        if (source.IsFile)
                            lines = File.ReadAllLines(source.Path);
                        else
                        {
                            var webData = wc.DownloadString(source.Path);
                            lines = webData.Split('.');
                        }
                            foreach (var line in lines)
                        {
                            var contains = line.IndexOf(translated.Word, comparer) >= 0;
                            if (contains && translated.IsReversed == false || !contains && translated.IsReversed)
                            {
                                if (!searchResult.FileResult.ContainsKey(source.Path))//the file is not in the result list yet
                                    searchResult.FileResult.Add(source.Path, new List<string>());
                                log.Debug(String.Format("Found line {0}", line));
                                searchResult.FileResult[source.Path].Add(line);//add the line that we found
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(String.Format("An Error occourd while searching, ex: ", ex.Message));
                        log.Error(String.Format("Error: {0}"), ex);
                    }
                }
            }
            return searchResult;
        }
    }
}
