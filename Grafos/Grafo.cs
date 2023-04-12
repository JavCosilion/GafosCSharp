using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grafos
{
    public class Grafo
    {
        public int n { get; set; }
        public List<Nodo> nodos { get; set; }
        public double[,] matrizDeCostos { get; set; }
        public bool[,] matrizDeTransiciones { get; set; }
        public List<Nodo> Pendiente { get; set; }
        public List<Nodo> Padres { get; set; }


        public Grafo(int n)
        {

            this.n = n;
            this.nodos = new List<Nodo>();
            this.matrizDeCostos = new double[n, n];
            this.matrizDeTransiciones = new bool[n, n];

            for (int i = 0; i < n; i++)
            {
                Nodo nodoNuevo = new Nodo();
                nodoNuevo.nombre = ((char)i + 65).ToString();
                nodoNuevo.gradoEntrada = 0;
                nodoNuevo.gradoSalida = 0;
                nodoNuevo.radio = 15;
                nodoNuevo.indice = i;
                this.nodos.Add(nodoNuevo);
                nodoNuevo.x = i;
                nodoNuevo.y = i;
            }
        }
        public bool BFS_anchura(Nodo nodoOrigen, Nodo nodoDestino)
        {
            List<Nodo> pendiente = new List<Nodo>();
            pendiente.Add(nodoOrigen);
            for (int j = 0; j < pendiente.Count; j++)
            {
                for (int i = 0; i < n; i++)

                    if (matrizDeTransiciones[pendiente[j].indice, i] && nodos[i].visitado == false)
                    {
                        nodos[pendiente[j].indice].visitado = true;
                        nodos[i].nodoPadre = pendiente[j];
                        if (nodos[i] == nodoDestino)
                        {
                            return true;
                        }
                        pendiente.Add(nodos[i]);
                    }
            }
            return false;
        }
        public bool DFS_profundidad(Nodo nodoOrigen, Nodo nodoDestino)
        {
            bool[,] matriz = this.matrizDeTransiciones;

            //if (nodoOrigen == nodoDestino)
            //{
            //    return true;
            //}
            //else
            //{
                for (int i = 0; i < this.n; i++)
                {
                    if (matriz[nodoOrigen.indice, i] == true && !nodos[i].visitado)
                    {
                        nodoOrigen.visitado = true;
                        try
                        {
                            this.Padres.Add(nodoOrigen);
                        }
                        catch (Exception) { }
                        
                        nodos[i].nodoPadre = nodoOrigen;
                        if (nodos[i] == nodoDestino)
                        {
                            return true;
                        }
                        if (DFS_profundidad(nodos[i], nodoDestino))
                        {
                            return true;
                        }
                    }
                }
                return false;
            //}
        }
        public void reiniciar()
        {
            for(int i = 0; i <this.n; i++)
            {
                nodos[i].nodoPadre = null;
                nodos[i].visitado = false;
            }
        }
        public bool expandir(Nodo origen, Nodo destino, List<Nodo> pendientes)
        {
            return false;

        }
        public void dijktra(Nodo nodoOrigen)
        {
            //hacer
        }
        public int calcularGradoSalida(Grafo grafico, int nodoIndex)
        {
            int gradoSalida = 0;
            for (int i = 0; i < grafico.n; i++)
            { 
                if (grafico.matrizDeTransiciones[nodoIndex, i] == true)
                {
                    gradoSalida++;
                }
            }
            return gradoSalida;            
        }
        public int calcularGradoEntrada(Grafo grafico, int nodoIndex)
        {
            int gradoEntrada = 0;
            for (int i = 0; i < grafico.n; i++)
            {
                if (grafico.matrizDeTransiciones[i, nodoIndex] == true)
                {
                    gradoEntrada++;
                }
            }
            return gradoEntrada;
        }
    }
}
