using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    public class Nodo
    {    
        //ubicacion del nodos
        public double x { get; set; }
        public double y { get; set; }
        //grado del nodos
        public int gradoEntrada { get; set; }
        public int gradoSalida { get; set; }
        public string nombre { get; set; }
        public int radio { get; set; }
        public int indice { get; set; }
        //para guardar la lista con el orden para las busquedas DFS y BFS
        public Nodo nodoPadre { get; set; }
        public bool visitado { get; set; }

        public Nodo()
        {
            //int[,] matrizTransiciones = new int[,];
        }
    }
}
