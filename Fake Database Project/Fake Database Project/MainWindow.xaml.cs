using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Fake_Database_Project.ViewModel;

namespace Fake_Database_Project
{
    public partial class MainWindow : Window
    {
        readonly DataViewModel viewmodel = new DataViewModel();
        readonly CancellationToken cancellationToken = new CancellationToken();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = viewmodel;
            Processing.Visibility = Visibility.Visible;
            await Task.Run(() => viewmodel.LoadAllData(), cancellationToken);
            string SearchStr = SearchBox.Text;
            await Task.Run(() => viewmodel.Searching(SearchStr), cancellationToken);
            string PageStr = PageBox.Text;
            await Task.Run(() => viewmodel.Paging(PageStr), cancellationToken);
            Processing.Visibility = Visibility.Collapsed;
        }

        private async void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            Processing.Visibility = Visibility.Visible;
            string SearchStr = SearchBox.Text;
            await Task.Run(() => viewmodel.Searching(SearchStr), cancellationToken);
            PageBox.Text = "1";
            string PageStr = PageBox.Text;
            await Task.Run(() => viewmodel.Paging(PageStr), cancellationToken);
            Processing.Visibility = Visibility.Collapsed;
        }

        private async void PageBox_KeyUp(object sender, KeyEventArgs e)
        {
            string PageStr = PageBox.Text;
            if (e.Key == Key.Enter)
                await Task.Run(() => viewmodel.Paging(PageStr), cancellationToken);
        }
    }
}