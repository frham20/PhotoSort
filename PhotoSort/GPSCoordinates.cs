using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort
{
    public struct DMS
    {
        public double degrees;
        public double minutes;
        public double seconds;
    }

    public class GPSCoordinates
    {
        public DMS Latitude { get; set; } = new DMS { degrees = 0.0, minutes = 0.0, seconds = 0.0 };
        public DMS Longitude { get; set; } = new DMS { degrees = 0.0, minutes = 0.0, seconds = 0.0 };
        public double Altitude { get; set; } = 0.0;
    }
}
