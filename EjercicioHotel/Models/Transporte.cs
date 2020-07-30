using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EjercicioHotel
{
    public class Transporte
    {

        //public ObjectId _id { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        //public ObjectId _id { get; set; }
        public DateTime Fecha { get; set; }
        public string Ruta { get; set; }
        public long Km { get; set; }

        public Transporte() { }

    

    }
}



