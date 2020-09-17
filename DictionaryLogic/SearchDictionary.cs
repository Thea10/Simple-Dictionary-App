using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DictionaryLogic
{
   public class SearchLogic
    {
        public Dictionary<string, SearchItem> HistoryList
        { get;  set; } = new Dictionary<string, SearchItem>();
        public string SearchResult { get; set; }
       public static string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string filePath = $"{docPath}/searchhistorytemp.txt";

  

        


public void checkForExistingData()
        {

            if (File.Exists(filePath))
            {
                readHistory readTemp = new readHistory(filePath);

                this.HistoryList = readTemp.ReadHistory();


            }
            else
            {
              //  File.Create(filePath);
               StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "searchhistorytemp.txt"));
              

            }
          

        }


        public  async Task<string>  CheckWord(string word)
        {
           string searchUrl = "https://www.dictionaryapi.com/api/v3/references/learners/json/";
           string key = "6a236d98-45b6-4cce-bd3c-befb8ef8d403";

            string searchSource = $"{searchUrl}{word}?key={key}";

            SearchItem searchStore = new SearchItem(word);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            HttpClient piper = new HttpClient();

            try
            {
                HttpResponseMessage res = await piper.GetAsync(searchSource);
                res.EnsureSuccessStatusCode();

                string datares = await res.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(datares))
                {
                    searchStore.searchResult = "We couldn't find this word, please try another";


                }
                else
                {
                    var result = JsonConvert.DeserializeObject<IList<JObject>>(datares);

                    foreach (var item in result)
                    {
                        searchStore.searchResult += item.SelectToken("shortdef")[0];
                       // searchStore.searchResult.Trim('[');
                        //searchStore.searchResult.Trim(']');
                    }
                      

                    if (HistoryList.ContainsKey(word))
                    {
                        return searchStore.searchResult;

                    }
                    else
                    {
                        HistoryList.Add(word, searchStore);
                        string wordInfo = String.Concat(Environment.NewLine, $"{word}: {searchStore}.");
                      
                        File.AppendAllText(filePath, wordInfo +  Environment.NewLine);

                    }

                }
              
            
               
            }
            catch (HttpRequestException e)
            {
                searchStore.searchResult = e.ToString();

                Console.WriteLine(e.ToString());
            }


         
            

            return searchStore.searchResult;
        }
    }
}
