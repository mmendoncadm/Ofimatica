using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;


namespace EjercicioHotel.Models
{
    public class Conexion
    {
    
                    
       private IMongoDatabase BDMongo;
       public Conexion(){
            
            MongoClient Mc = new MongoClient("mongodb://maribel:01234mmmmm56789*@SG-Prueba-36649.servers.mongodirector.com:27017/Hotel");

            var BDMongo = Mc.GetDatabase("Hotel");
  
        }
    }

}
