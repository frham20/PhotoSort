using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort
{
    public class Config : NotifyPropertyChanged
    {
        private ObservableCollection<string> directories = new ObservableCollection<string>();

        public ObservableCollection<string> Directories
        {
            get
            {
                return this.directories;
            }
        }
    }
}
