using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EjercicioHotel
{
    public class Huesped
    {

        //public ObjectId _id { get; set; }
            [BsonId]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            //public ObjectId _id { get; set; }
            public string DocIdentificacion { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }

            public Huesped() { }

            public Huesped(string nom, string ape)
            {
                this.Nombre = nom;
                this.Apellido = ape;
            }




        }
    }



