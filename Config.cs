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
        private ObservableCollection<string> sourceDirectories = new ObservableCollection<string>();
        private string destinationDirectory = string.Empty;

        public ObservableCollection<string> SourceDirectories
        {
            get
            {
                return this.sourceDirectories;
            }
        }

        public string DestinationDirectory
        {
            get
            {
                return this.destinationDirectory;
            }
            set
            {
                this.destinationDirectory = value;
                this.RaisePropertyChanged();
                this.RaisePropertyChanged("IsValid");
            }
        }

        public bool IsValid
        {
            get
            {
                if (this.sourceDirectories.Count == 0)
                    return false;

                if (this.sourceDirectories.Any(x => !System.IO.Directory.Exists(x)))
                    return false;

                if (string.IsNullOrWhiteSpace(this.destinationDirectory))
                    return false;

                return true;
            }
        }

        public Config()
        {
            this.sourceDirectories.CollectionChanged += SourceDirectories_CollectionChanged;
        }

        private void SourceDirectories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.RaisePropertyChanged("IsValid");
        }
    }
}
