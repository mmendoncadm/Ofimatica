using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EjercicioHotel.Models
{
    public class PuntoTuristico : Punto
    {
        [Key]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public string Ubicación { get; set; }

        public bool VerificarEstado() { return true; }
    }
}
