using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryLogic
{

    public class readHistory{

        private string _pathToFile;

        public readHistory(string path)
        {
            _pathToFile = path;

        }

        public Dictionary<string, SearchItem> ReadHistory()
        {
            var searchItems = new Dictionary<string, SearchItem>();

            using (StreamReader readfile = new StreamReader(_pathToFile))
            {
                readfile.ReadLine();

                string fileline;

                while ((fileline = readfile.ReadLine()) != null)
                {
                    SearchItem words = ReadItems(fileline);

                    if (searchItems.ContainsKey(words.searchText))
                    {
                        return searchItems;

                    }
                    else
                    {
                        SearchItem newWord = new SearchItem(words.searchText);
                        newWord.searchResult = words.searchResult;

                        searchItems.Add(newWord.searchText, newWord);
                    }
                }

            }

            return searchItems;
        }

        public SearchItem ReadItems(string line)
        {
            string[] chars = line.Split('.');

            string text = chars[0];
            string res = chars[0];

            var itemReturned = new SearchItem(text);
            itemReturned.searchResult = res;
            return  itemReturned;

        }
    }
   public class SearchData
    {
        [JsonProperty("shortdef")]
        
        public string defstring { get; set; }
    }


    public class SearchDataItem
    {
        [JsonProperty("meta")]
        public string meta { get; set; }


        [JsonProperty("hwi")]
        public string hwi { get; set; }


        [JsonProperty("fl")]
        public string fl { get; set; }


        [JsonProperty("ins")]
        public string ins { get; set; }


        [JsonProperty("gram")]
        public string gram { get; set; }


        [JsonProperty("def")]
        public string def { get; set; }


        [JsonProperty("dros")]
        public string dros { get; set; }


        [JsonProperty("shortdef")]
        public string shortdef { get; set; }
    }
}
