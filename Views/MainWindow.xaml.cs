using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhotoSort.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm = null;
        private bool updatingVM = false;
        private bool updatingUI = false;

        public MainWindow()
        {
            InitializeComponent();

            this.vm = this.DataContext as MainWindowViewModel;
            if(this.vm != null)
                this.vm.SelectedSourceDirectories.CollectionChanged += SelectedSourceDirectories_CollectionChanged;

            this.DataContextChanged += MainWindow_DataContextChanged;

            this.sourceFolderList.SelectionMode = SelectionMode.Extended;
            this.sourceFolderList.SelectionChanged += SourceFolderList_SelectionChanged;

            this.sourceFolderList.AllowDrop = true;
            this.sourceFolderList.DragEnter += SourceFolderList_DragEnter;
            this.sourceFolderList.DragOver += SourceFolderList_DragOver;
            this.sourceFolderList.Drop += SourceFolderList_Drop;
        }

        private void SelectedSourceDirectories_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (this.updatingVM)
                return;

            this.updatingUI = true;

            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    {
                        foreach (var added in e.NewItems)
                            this.sourceFolderList.SelectedItems.Add(added);
                        break;
                    }
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    {
                        foreach (var removed in e.OldItems)
                            this.sourceFolderList.SelectedItems.Remove(removed);
                        break;
                    }
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    {
                        this.sourceFolderList.SelectedItems.Clear();
                        break;
                    }
            }

            this.updatingUI = false;
        }

        private void MainWindow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.vm != null)
                this.vm.SelectedSourceDirectories.CollectionChanged -= SelectedSourceDirectories_CollectionChanged;

            this.vm = this.DataContext as MainWindowViewModel;

            if (this.vm != null)
                this.vm.SelectedSourceDirectories.CollectionChanged += SelectedSourceDirectories_CollectionChanged;
        }

        private void SourceFolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.updatingUI)
                return;

            if (this.vm == null)
                return;

            this.updatingVM = true;

            foreach (var removed in e.RemovedItems.OfType<string>())
                this.vm.SelectedSourceDirectories.Remove(removed);

            foreach (var added in e.AddedItems.OfType<string>())
                this.vm.SelectedSourceDirectories.Add(added);

            this.updatingVM = false;
        }

        private void SourceFolderList_Drop(object sender, DragEventArgs e)
        {
            var filenames = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (filenames == null)
                return;

            var curDirectories = new HashSet<string>(AppData.Current.Config.Directories);
            var validDirectories = new HashSet<string>(filenames.Select(x => System.IO.Directory.Exists(x) ? x : System.IO.Path.GetDirectoryName(x))
                                                                .Where(x => !curDirectories.Contains(x)));
            foreach (var dir in validDirectories)
                AppData.Current.Config.Directories.Add(dir);
        }

        private void SourceFolderList_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }

        private void SourceFolderList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;           
        }
    }
}
