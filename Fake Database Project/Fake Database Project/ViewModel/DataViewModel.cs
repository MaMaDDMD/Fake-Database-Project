using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Fake_Database_Project.Model;

namespace Fake_Database_Project.ViewModel
{
    public class DataViewModel
    {
        readonly CancellationToken cancelationtoken = new CancellationToken();
        public ObservableCollection<Mobiles> CurrentShowingData { get; set; } = new ObservableCollection<Mobiles>();
        List<Mobiles> AllData { get; set; } = new List<Mobiles>();
        List<Mobiles> SearchResult { get; set; } = new List<Mobiles>();
        public async Task LoadAllData()
        {
            using var Db = new MobileDbContext();
            await Task.Run(() => AllData = Db.Mobiles.ToList(), cancelationtoken);
        }
        public async Task Searching(string SearchText)
        {
            await Task.Run(() =>
            {
                SearchResult.Clear();
                SearchResult = AllData.Where(m => m.Name.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.ToLower().Contains(SearchText.Split(' ', StringSplitOptions.RemoveEmptyEntries).Aggregate("", (s1, s2) => s1 + s2, s => s.Trim().ToLower())))).ToList();
            }, cancelationtoken);
        }
        public async Task Paging(string PageNumStr)
        {
            if (int.TryParse(PageNumStr, out int PageNum))
            {
                if (PageNum > 0 && PageNum <= SearchResult.Count / 100 + 1)
                    await Task.Run(() =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => CurrentShowingData.Clear()));
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => SearchResult.Where((m, i) => i >= 100 * (PageNum - 1) && i < 100 * PageNum).ToList().ForEach(r => CurrentShowingData.Add(r))));
                    }, cancelationtoken);
                else
                    MessageBox.Show($"Out Of Range...!");
            }
            else
                MessageBox.Show("Please enter a number not anything else...!");
        }
    }
}