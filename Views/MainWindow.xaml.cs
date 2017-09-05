using System;
using System.Collections.Generic;
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
        public MainWindow()
        {
            InitializeComponent();

            this.sourceFolderList.SelectionMode = SelectionMode.Extended;
            this.sourceFolderList.SelectionChanged += SourceFolderList_SelectionChanged;

            this.sourceFolderList.AllowDrop = true;
            this.sourceFolderList.DragEnter += SourceFolderList_DragEnter;
            this.sourceFolderList.DragOver += SourceFolderList_DragOver;
            this.sourceFolderList.Drop += SourceFolderList_Drop;
        }

        private void SourceFolderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vm = this.DataContext as MainWindowViewModel;
            if (vm == null)
                return;

            foreach (var removed in e.RemovedItems.OfType<string>())
                vm.SelectedSourceDirectories.Remove(removed);

            foreach (var added in e.AddedItems.OfType<string>())
                vm.SelectedSourceDirectories.Add(added);
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
