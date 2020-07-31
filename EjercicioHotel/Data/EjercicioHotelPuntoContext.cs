using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EjercicioHotel.Models;
using MongoDB.Bson;
using MongoDB.Driver;


namespace EjercicioHotel.Data
{
    public class EjercicioHotelPuntoContext : DbContext
    {
        private readonly IMongoDatabase BDMongo;
        public EjercicioHotelPuntoContext ()
        {
            MongoClient Mc = new MongoClient("mongodb://maribel:01234mmmmm56789*@SG-Prueba-36649.servers.mongodirector.com:27017/Hotel");
            BDMongo = Mc.GetDatabase("Hotel");
        }

        public IMongoCollection<PuntoTuristico> PuntoTuristico
        {
            get
            {
                return BDMongo.GetCollection<PuntoTuristico>("PuntoTuristico");
            }
        }
    }
}
