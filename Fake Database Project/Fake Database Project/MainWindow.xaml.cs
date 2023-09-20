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
<<<<<<< HEAD
        readonly DataViewModel viewmodel = new DataViewModel();
        readonly CancellationToken cancellationToken = new CancellationToken();
        public MainWindow()
        {
            InitializeComponent();
=======
        readonly DataViewModel viewmodel;
        readonly CancellationToken cancellationToken;
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new DataViewModel();
            cancellationToken = new CancellationToken();
            DataContext = viewmodel;
>>>>>>> bb2356b5b73ddf74ed25a2a0451dcf7608d6fa2d
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            DataContext = viewmodel;
            Processing.Visibility = Visibility.Visible;
            await Task.Run(() => viewmodel.LoadData(), cancellationToken);
            using(var Db=new MobileDbContext())
                await Task.Run(() => viewmodel.LoadQuery(Db.Mobiles.ToList()), cancellationToken);
            await Task.Delay(200, cancellationToken);
            await Task.Run(() => viewmodel.Paging("1"), cancellationToken);
            Processing.Visibility = Visibility.Collapsed;
=======

            DataProcessing.Visibility = Visibility.Visible;
            using (var Db = new MobileDbContext())
                await Task.Run(() => viewmodel.LoadData(Db.Mobiles.ToList()));
            DataProcessing.Visibility = Visibility.Collapsed;
>>>>>>> bb2356b5b73ddf74ed25a2a0451dcf7608d6fa2d
        }

        private async void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
<<<<<<< HEAD
            Processing.Visibility = Visibility.Visible;
            string SearchStr = SearchBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.Trim().ToLower());
            await Task.Run(() => viewmodel.LoadQuery(viewmodel.Data.Where(delegate (Mobiles m)
             {
                 string name = m.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                 return name.Contains(SearchStr);
             }).ToList()), cancellationToken);
            await Task.Run(() => viewmodel.Paging("1"), cancellationToken);
            PageBox.Text = "1";
            Processing.Visibility = Visibility.Collapsed;
        }

        private async void TextBox_KeyUp_1(object sender, KeyEventArgs e)
        {
            string PageNum = PageBox.Text;
            if (e.Key == Key.Enter)
                await Task.Run(() => viewmodel.Paging(PageNum), cancellationToken);
=======
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
>>>>>>> bb2356b5b73ddf74ed25a2a0451dcf7608d6fa2d
        }
    }
}