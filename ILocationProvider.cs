using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSort
{
    public interface ILocationProvider
    {
        Location Resolve(GPSCoordinates coordinates);
    }
}
