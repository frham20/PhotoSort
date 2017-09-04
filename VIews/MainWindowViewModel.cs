using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort.Views
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        public string Title
        {
            get
            {
                return "PhotoSort";
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}
