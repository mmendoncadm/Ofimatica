using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EjercicioHotel.Models
{
    public class Recorrido
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime Fecha { get; set; }

        public long KmRecorridos { get; set; }

        public string Ruta { get; set; }
        public Recorrido() { }

        //public long[] Puntos { get; }
        //public ICollection<Huesped> HuespedesRegistrados { get; }
        //public void AgregarHuesped(Huesped h, int i)
        //{
        //    HuespedesRegistrados.Add(h);
        //}
        //public void EliminarHuesped(Huesped h, int i)
        //{
        //    HuespedesRegistrados.Remove(h);
        //}
    }
}
