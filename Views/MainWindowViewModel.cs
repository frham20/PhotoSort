using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return string.Format("PhotoSort v{0}.{1} build {2}", version.Major, version.Minor, version.Build);
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}
