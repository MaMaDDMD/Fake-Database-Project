using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Fake_Database_Project.Model;

namespace Fake_Database_Project.ViewModel
{
    public class DataViewModel
    {
        public ObservableCollection<Mobiles> Data { get; } = new ObservableCollection<Mobiles>();
        public Task LoadData(List<Mobiles> mobiles)
        {
            Data.Clear();
            if (mobiles != null)
                foreach (var item in mobiles)
                    Data.Add(item);
            return Task.CompletedTask;
        }
    }
}