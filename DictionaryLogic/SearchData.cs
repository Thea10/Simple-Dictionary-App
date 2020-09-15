using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryLogic
{
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
