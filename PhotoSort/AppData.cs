using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort
{
    public class AppData : NotifyPropertyChanged
    {
        private static AppData instance = new AppData();
        private Config config = new Config();

        public static AppData Current
        {
            get
            {
                return instance;
            }
        }

        public Config Config
        {
            get
            {
                return this.config;
            }
            set
            {
                if (this.config == value)
                    return;

                this.config = value;
                this.RaisePropertyChanged();
            }
        }
    }
}
