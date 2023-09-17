using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Fake_Database_Project.Model;

namespace Fake_Database_Project.ViewModel
{
    public class DataViewModel
    {
        public ObservableCollection<Mobiles> Data { get; } = new ObservableCollection<Mobiles>();
        public async Task LoadData(List<Mobiles> mobiles)
        {
            await Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Data.Clear()));
            await Task.Run(() =>
            {
                if (mobiles != null)
                    foreach (var item in mobiles)
                        Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() => Data.Add(item)));
            });
        }
    }
}