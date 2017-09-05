using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort.Views
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private ObservableCollection<string> selectedSourceDirectories = new ObservableCollection<string>();

        public string Title
        {
            get
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;
                return $"PhotoSort v{version.Major}.{version.Minor} build {version.Build}";
            }
        }

        public ObservableCollection<string> SourceDirectories
        {
            get
            {
                return AppData.Current.Config.Directories;
            }
        }

        public ObservableCollection<string> SelectedSourceDirectories
        {
            get
            {
                return this.selectedSourceDirectories;
            }
        }

        public MainWindowViewModel()
        {

        }
    }
}
