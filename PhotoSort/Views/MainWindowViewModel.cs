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
                return AppData.Current.Config.SourceDirectories;
            }
        }

        public ObservableCollection<string> SelectedSourceDirectories
        {
            get
            {
                return this.selectedSourceDirectories;
            }
        }

        public string DestinationDirectory
        {
            get
            {
                return AppData.Current.Config.DestinationDirectory;
            }
            set
            {
                AppData.Current.Config.DestinationDirectory = value;
            }
        }

        public RelayCommand RemoveSelectedSourceDirectoriesCommand { get; private set; }
        public RelayCommand ProcessCommand { get; private set; }

        public MainWindowViewModel()
        {
             this.RemoveSelectedSourceDirectoriesCommand = new RelayCommand(this.RemoveSelectedItems);
             this.ProcessCommand = new RelayCommand(this.Process, this.CanProcess);

            AppData.Current.Config.PropertyChanged += Config_PropertyChanged;
        }

        private void Config_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "DestinationDirectory":
                case "SourceDirectories":
                    {
                        this.RaisePropertyChanged(e.PropertyName);
                        break;
                    }
                case "IsValid":
                    {
                        this.ProcessCommand.RaiseCanExecute();
                        break;
                    }
            }
        }

        private void Process(object parameter)
        {
            System.Windows.MessageBox.Show("Process!");
        }

        private bool CanProcess(object parameter)
        {
            return AppData.Current.Config.IsValid;
        }

        private void RemoveSelectedItems(object parameter)
        {
            var selectionCopy = this.selectedSourceDirectories.ToList();
            foreach(var item in selectionCopy)
                AppData.Current.Config.SourceDirectories.Remove(item);

            this.SelectedSourceDirectories.Clear();
            if (this.SourceDirectories.Count > 0)
                this.SelectedSourceDirectories.Add(this.SourceDirectories[0]);               
        }
    }
}
