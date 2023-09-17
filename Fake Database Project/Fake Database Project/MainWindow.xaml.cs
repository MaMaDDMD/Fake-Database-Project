using System;
using System.Linq;
using System.Threading;
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
        readonly CancellationToken cancellationToken;
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new DataViewModel();
            cancellationToken = new CancellationToken();
            DataContext = viewmodel;
        }

        private async void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {

            DataProcessing.Visibility = Visibility.Visible;
            using (var Db = new MobileDbContext())
                await Task.Run(() => viewmodel.LoadData(Db.Mobiles.ToList()));
            DataProcessing.Visibility = Visibility.Collapsed;
        }

        private async void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            QueryProcessing.Visibility = Visibility.Visible;
            string SearchStr = SearchBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.Trim().ToLower());
            using (var Db = new MobileDbContext())
            {
                await Task.Run(() => viewmodel.LoadData(Db.Mobiles.Where(delegate (Mobiles m)
                 {
                     string name = m.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                     return name.Contains(SearchStr);
                 }).ToList()), cancellationToken);
            }
            QueryProcessing.Visibility = Visibility.Collapsed;
        }
    }
}