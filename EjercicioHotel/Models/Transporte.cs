using System;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using EjercicioHotel.Models;

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
        public string Recorrido { get; set; }
        public long Km { get; set; }
        public bool[] Puntos { get; set; }
        public Transporte() {
            Ruta ruta = new Ruta();
            string Rec = "";

            //ruta.CrearMatrixRecorrido(Puntos);
            Km = ruta.Distancia();
          /*  for (int i = 0; i < ruta.Recorrido.Length; i++)
            {

                Rec = Rec + " - " + ruta.Recorrido[i].ToString();

            }*/
        }

        public void AgregarPunto(int p)
        {
            Puntos[p]=true;
 
        }

        public void ActualizarRecorrido()
        {
            
            
        }



    }
}



