using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EjercicioHotel.Models
{
    public class Traslado
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Fecha { get; set; }
        public int Punto { get; set; }
        public string IdHuesped { get; set; }

        public Huesped huesped { get; set; }

    }
}
