using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryLogic
{
    public class SearchItem
    {
        public string searchText { get; set; }
        public string searchResult { get; set; }

        public SearchItem(string query)
        {
            searchText = query;
          

        }

        public override string ToString() => $"{searchText} ({searchResult})";
    }
}
