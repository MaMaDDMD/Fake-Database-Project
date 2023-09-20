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
        private readonly CancellationToken cancelationtoken = new CancellationToken();
        public ObservableCollection<Mobiles> CurrentShowingData { get; } = new ObservableCollection<Mobiles>();
        public List<Mobiles> Data { get; set; } = new List<Mobiles>();
        private List<Mobiles> Query = new List<Mobiles>();
        public readonly MobileDbContext Db = new MobileDbContext();
        public async Task LoadData() => await Task.Run(() => Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Data = Db.Mobiles.ToList())), cancelationtoken);
        public async Task LoadQuery(List<Mobiles> mobiles)
        {
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Query.Clear()));
                if (mobiles != null)
                    Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Query=mobiles));
            },cancelationtoken);
        }
        public async Task Paging(string PageNumStr)
        {
            if (int.TryParse(PageNumStr, out int PageNum))
            {
                if (PageNum > 0 && PageNum < 10001)
                {
                    await Task.Run(() =>
                    {
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => CurrentShowingData.Clear()));
                        var query = Query.Where((m, i) => i >= 100 * (PageNum - 1) && i < 100 * PageNum);
                        foreach (var item in query)
                            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal,new Action(()=> CurrentShowingData.Add(item)));
                    },cancelationtoken);
                }
                else
                    MessageBox.Show("Your number must be from 1 to 10000...!");
            }
            else
                MessageBox.Show("Please enter a number not anything else...!");
        }
    }
}