using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Fake_Database_Project.Model;
using Fake_Database_Project.ViewModel;

namespace Fake_Database_Project
{
    public partial class MainWindow : Window
    {
        readonly DataViewModel viewmodel;
        readonly MobileDbContext Db;
        public MainWindow()
        {
            InitializeComponent();
            Db = new MobileDbContext();
            viewmodel = new DataViewModel();
            DataContext = viewmodel;
        }

        private async void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            DataProcessing.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            await viewmodel.LoadData(Db.Mobiles.ToList());
            await Task.Delay(2000);
            DataProcessing.Visibility = Visibility.Collapsed;
        }

        private async void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            QueryProcessing.Visibility = Visibility.Visible;
            string SearchStr = SearchBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.Trim().ToLower());
            await Task.Delay(2000);
            await viewmodel.LoadData(Db.Mobiles.Where(delegate (Mobiles m)
            {
                string name = m.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                string brandname = m.BrandName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                return name.Contains(SearchStr) || brandname.Contains(SearchStr);
            }).ToList());
            await Task.Delay(2000);
            QueryProcessing.Visibility = Visibility.Collapsed;
        }
    }
}