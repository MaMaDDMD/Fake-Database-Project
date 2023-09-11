using System;
using System.Linq;
using System.Windows;
using Fake_Database_Project.Model;
using Fake_Database_Project.ViewModel;

namespace Fake_Database_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataViewModel viewmodel;
        MobileDbContext db = new MobileDbContext();
        public MainWindow()
        {
            InitializeComponent();
            viewmodel = new DataViewModel();
            DataContext = viewmodel;
        }

        private void SearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            viewmodel.LoadData(db.Mobiles.ToList());
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            string SearchStr = SearchBox.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.Trim().ToLower());
            viewmodel.LoadData(db.Mobiles.Where(delegate (Mobiles m)
            {
                string name = m.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                string brandname = m.BrandName.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower());
                return name.Contains(SearchStr) || brandname.Contains(SearchStr);
            }).ToList());
        }
    }
}
