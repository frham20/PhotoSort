using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoSort
{
    public class Photo : NotifyPropertyChanged
    {
        private string filename = string.Empty;
        private DateTime dateTaken = DateTime.MinValue;

        public string Filename
        {
            get
            {
                return this.filename;
            }
            set
            {
                this.filename = value;
                this.RaisePropertyChanged();
            }
        }

        public Photo(string filename)
        {

        }

        private void LoadMetaData()
        {
            using (var bmpStream = new FileStream(this.filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var decoder = BitmapDecoder.Create(bmpStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                var metaData = decoder.Metadata;
            }
        }
    }
}
