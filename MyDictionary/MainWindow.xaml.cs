using DictionaryLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SearchLogic SearchLogic { get; set; } = new SearchLogic();
        public string SearchString { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SearchLogic.checkForExistingData();
            this.DataContext = SearchLogic;    
        }

    

        private void searchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchString = searchBox.Text;
        }

        private async void searchButton_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(SearchString))
            {
                searchStringBox.Text = "Please type in a word";
                return;
            }

            else
            {
                try
                {

                    var search = await SearchLogic.CheckWord(SearchString);
                    //   this.SearchLogic.HistoryList.Add(SearchString, search);

                    searchStringBox.Text = SearchString;
                    searchResultBox.Text = search;

                    this.lblsearchhistory.ItemsSource = SearchLogic.HistoryList.Values;
                    this.lblsearchhistory.Items.Refresh();
                  
                }
                catch (Exception ex)
                {
                    searchResultBox.Text = ex.Message;
                    Console.WriteLine(ex);
                }

            }
           
              
            

        }

        private void allItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


        }

        private void lblsearchhistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
