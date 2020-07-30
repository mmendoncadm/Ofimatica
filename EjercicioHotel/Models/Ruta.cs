using System;
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
       public long[,] DistanciaPuntos { get; set; }

       public PuntoTuristico[] Puntos { get; set; }

        public long[,]  PuntosSeleccionados { get; set; }

       public long[] Recorrido { get; }
       

       long[,] rut = {
           {0,5,0,0,3,0},
           {5,0,8,3,0,6},
           {0,8,0,4,7,0}, 
           {0,3,4,0,2,8},
           {3,0,7,2,0,9},
           {0,6,0,8,9,0},
        };
       public long Distancia(long[,] ruta) {

            long routeDistance = 0;
            RoutingSearchParameters searchParameters =
  operations_research_constraint_solver.DefaultRoutingSearchParameters();
        searchParameters.FirstSolutionStrategy = FirstSolutionStrategy.Types.Value.PathCheapestArc;

        DataModel data = new DataModel(PuntosSeleccionados,1,0);
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
             int indice = 0;
            while (routing.IsEnd(index) == false)
            {
                Recorrido[indice] = manager.IndexToNode((int)index);
                var previousIndex = index;
                index = solution.Value(routing.NextVar(index));
                routeDistance += routing.GetArcCostForVehicle(previousIndex, index, 0);
            }
               Recorrido[indice] = manager.IndexToNode((int)index);

            return routeDistance;
        }


    }
}
