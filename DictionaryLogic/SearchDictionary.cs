using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace DictionaryLogic
{
   public class SearchLogic
    {
        public Dictionary<string, SearchItem> HistoryList
        {get;  set; }
        public string SearchResult { get; set; }
        
       
     


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
              
             var  result =  JsonConvert.DeserializeObject<IList<JObject>>(datares);
             

                foreach (var item in result)
                {
                    searchStore.searchResult += item.SelectToken("shortdef");
                }

            
            }
            catch (HttpRequestException e)
            {

                Console.WriteLine(e);
            }

            return searchStore.searchResult;
        }
    }
}
