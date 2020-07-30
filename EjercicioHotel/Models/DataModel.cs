using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioHotel.Models
{
    class DataModel
    {
        public long[,] DistanceMatrix { get; set; } 
        public int VehicleNumber { get; set; }
        public int Depot { get; set; }

        public DataModel (long[,] m, int v, int d)
        {
            DistanceMatrix = m;
            VehicleNumber = v;
            Depot = d;

    }
    };
}
