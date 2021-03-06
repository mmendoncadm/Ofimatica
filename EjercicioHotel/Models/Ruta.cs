﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.OrTools;
using Google.OrTools.Algorithms;
using Google.OrTools.ConstraintSolver;
using Google.OrTools.Graph;
using Google.OrTools.LinearSolver;
using Google.OrTools.Sat;
using Google.OrTools.Util;

namespace EjercicioHotel.Models
{

    public class Ruta
    {
        private static Ruta instancia;
        public long[] Recorrido { get; }

        public long Km { get; set; }

        public string CadenaRecorrido { get; set; }

        long[,] Distancias = {
           {0,5,-1,-1,3,-1},
           {5,0,8,3,-1,6},
           {-1,8,0,4,7,-1},
           {-1,3,4,0,2,8},
           {3,-1,7,2,0,9},
           {-1,6,-1,8,9,0},
             };

        private Ruta (){  
        }

        public static Ruta getInstancia()
        {
            if (instancia == null)
            {
                instancia = new Ruta();
                instancia.Distancia();
            }
            return instancia;
        }

        
        private void Distancia() {

        long routeDistance = 0;
        RoutingSearchParameters searchParameters =
         operations_research_constraint_solver.DefaultRoutingSearchParameters();
        searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

        DataModel data = new DataModel(Distancias,1,0);
        RoutingIndexManager manager = new RoutingIndexManager(
            data.DistanceMatrix.GetLength(0),
            data.VehicleNumber,
            data.Depot);
        RoutingModel routing = new RoutingModel(manager);

        int transitCallbackIndex = routing.RegisterTransitCallback(
         (long fromIndex, long toIndex) => {
         // Convert from routing variable Index to distance matrix NodeIndex.
         var fromNode = manager.IndexToNode(fromIndex);
         var toNode = manager.IndexToNode(toIndex);
         return data.DistanceMatrix[fromNode, toNode];});

          routing.SetArcCostEvaluatorOfAllVehicles(transitCallbackIndex);

          Assignment solution = routing.SolveWithParameters(searchParameters);
        
            
            var index = routing.Start(0);
            int indice = 1;
            string CadenaR = "";
            string c = "";
            while (routing.IsEnd(index) == false)
            {
                //Recorrido[indice] = manager.IndexToNode((int)index);
                c = manager.IndexToNode((int)index).ToString();
                CadenaR = CadenaR + " - " + c;
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
                indice++;
            }
              //Recorrido[indice] = manager.IndexToNode((int)index);
               c = manager.IndexToNode((int)index).ToString();
               CadenaR = CadenaR + " - " + c;

            Km =routeDistance;
            CadenaRecorrido = CadenaR;
        }

        /*public void CrearMatrizRecorrido(bool[] Puntos)
         {
             long[,] Distancias = {
            {0,5,0,0,3,0},
            {5,0,8,3,0,6},
            {0,8,0,4,7,0},
            {0,3,4,0,2,8},
            {3,0,7,2,0,9},
            {0,6,0,8,9,0},
              };
             int i=0;
             int j=0;
             int[] AuxPunto;
             int cont = 0;
             //inicializar matriz de distancias en 0
             for ( i = 0; i < Puntos.Length; i++)
             {
                for ( j = 0; i < Puntos.Length; i++)
                 {
                     DistanciasPuntos[i, j] = 0;
                 }
                 //Subindice de la matriz de distancia asociado al punto
                 if (Puntos[i])
                    // AuxPunto[i] = cont;
                  }
             for (i = 0; i < Puntos.Length; i++)
             {
                 for (j = 0; i < Puntos.Length; i++)
                 {
                     DistanciasPuntos[i, j] = 0;
                 }
                 //Subindice de la matriz de distancia asociado al punto
                 if (Puntos[i])
                     AuxPunto[i] = cont;
             }
             for (i=0;i<Puntos.Length; i++)
                    
             PuntosSeleccionados Distancias[i]
                 for (int j = 0; i < Distancias.Length; j++)
                 {

                 }
             }
             long[,] RutaRecorrido
             PuntosSeleccionados
         }*/


    }
}
