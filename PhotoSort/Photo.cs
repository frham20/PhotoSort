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
        private GPSCoordinates gps = null;

        public string Filename
        {
            get
            {
                return this.filename;
            }
            private set
            {
                this.filename = value;
                this.RaisePropertyChanged();
            }
        }

        public DateTime DateTaken
        {
            get
            {
                return this.dateTaken;
            }
            private set
            {
                this.dateTaken = value;
                this.RaisePropertyChanged();
            }
        }

        public GPSCoordinates GPS
        {
            get
            {
                return this.gps;
            }
            private set
            {
                this.gps = value;
                this.RaisePropertyChanged();
            }
        }

        public Photo(string filename)
        {
            this.filename = filename;
        }

        private void LoadMetaData()
        {
            using (var bmpStream = new FileStream(this.filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                var decoder = BitmapDecoder.Create(bmpStream, BitmapCreateOptions.None, BitmapCacheOption.None);
                var metaData = decoder.Metadata;
                if (metaData == null)
                    return;

                //extract date
                if (DateTime.TryParse(metaData.DateTaken, out var date))
                    this.DateTaken = date;

                //extract GPS Coordinates
                //metaData.

            }
        }
    }
}
