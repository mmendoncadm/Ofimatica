using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EjercicioHotel;
using MongoDB.Bson;
using MongoDB.Driver;


namespace EjercicioHotel.Data
{
    public class EjercicioHotelContext : DbContext
    {
        private readonly IMongoDatabase BDMongo;
        public EjercicioHotelContext () 
        {
            MongoClient Mc = new MongoClient("mongodb://maribel:01234mmmmm56789*@SG-Prueba-36649.servers.mongodirector.com:27017/Hotel");
            BDMongo = Mc.GetDatabase("Hotel");
        }

        public IMongoCollection<Huesped> Huesped
        {
            get
            {
                return BDMongo.GetCollection<Huesped>("Huesped");
            }
        }
        //public DbSet<EjercicioHotel.Huesped> Huesped { get; set; }
    }

    
}
