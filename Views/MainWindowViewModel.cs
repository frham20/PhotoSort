using PhotoSort.Views.Commands;
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

        public RelayCommand RemoveSelectedSourceDirectoriesCommand { get; private set; }

        public MainWindowViewModel()
        {
             this.RemoveSelectedSourceDirectoriesCommand = new RelayCommand(this.RemoveSelectedItems);
        }

        private void RemoveSelectedItems(object parameter)
        {
            var selectionCopy = this.selectedSourceDirectories.ToList();
            foreach(var item in selectionCopy)
                AppData.Current.Config.Directories.Remove(item);

            this.SelectedSourceDirectories.Clear();
            if (this.SourceDirectories.Count > 0)
                this.SelectedSourceDirectories.Add(this.SourceDirectories[0]);               
        }
    }
}
