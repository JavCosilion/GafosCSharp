using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Grafos
{
    //for(int i=0; i<n i++)
    //lista1.items.add(((char)i+68).tostring
    public partial class Grafos : Form
    {
        Grafo grafico;
        bool vuelta;
        int nodoVuelta;
        
        public Grafos()
        {            
            InitializeComponent();            
        }
        private void Grafos_Load(object sender, EventArgs e)
        {
            
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (txtNodo.Text != "" && int.Parse(txtNodo.Text) <= 26)
                {
                    dibujarGrafo(grafico, pictureBoxGrafo.Width, pictureBoxGrafo.Height, e.Graphics);
                    pintarLinea(grafico.nodos, grafico.matrizDeTransiciones, e.Graphics);
                    if (vuelta)
                    {
                        dibujarAlMismo(e.Graphics, nodoVuelta);
                    }
                    dibujarRuta(grafico.nodos[comboDestinoBusq.SelectedIndex], e.Graphics);
                    for(int i = 0; i<grafico.n; i++)
                    {
                        dibujarGrado(e.Graphics, grafico.nodos[i].indice);
                    }
                    
                }
                if (txtNodo.Text != "" && int.Parse(txtNodo.Text) > 26)
                {
                    MessageBox.Show("Limite de 26");
                }
            }
            catch (Exception) { }
        }
        public void dibujarGrado(Graphics e, int indice)
        {
            int Index = comboNodoGrado.SelectedIndex;
            string g_Salida = grafico.calcularGradoSalida(grafico, indice).ToString();
            string G_Entrada = grafico.calcularGradoEntrada(grafico, indice).ToString();
            Font fuente = new Font("Arial", 8);
            e.DrawString("°E:" + G_Entrada + " " + "°S:" + g_Salida, fuente, Brushes.Black, (int)grafico.nodos[indice].x - 45 , (int)grafico.nodos[indice].y);
        }
        public void dibujarAlMismo(Graphics e, int actual)
        {
            Font fuente = new Font("Arial", 12);
            e.DrawString("↵", fuente, Brushes.Black, (int)grafico.nodos[actual].x + 15, (int)grafico.nodos[actual].y + 20);
        }
        public void pintarArista(Nodo origen, Nodo destino, Graphics e, bool ruta)
        {
            Pen linea = new Pen(Color.SlateGray);
            linea.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

            if (ruta)
            {
                linea = new Pen(Color.SteelBlue);
                linea.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                linea.Width = 3;
            }

            double a = origen.x - destino.x;
            double b = origen.y - destino.y;
            double h = Math.Sqrt(b * b + a * a);

            a *= origen.radio / h;
            b *= destino.radio / h;

            
            e.DrawLine(linea, (int)(origen.x - a+15), (int)(origen.y - b+15), (int)(destino.x + a+15), (int)(destino.y + b+15));
        }
        public void pintarLinea(List<Nodo> listaNodos, bool[,] matriz, Graphics e)
        {
            for (int i = 0; i < grafico.n; i++)
            {
                for (int j = 0; j < grafico.n; j++)
                {
                    if (matriz[i, j] == true)
                    {
                        pintarArista(listaNodos[i], listaNodos[j], e, false);

                    }
                }
            }
        }
       
        public static void dibujarGrafo(Grafo grafo, int ancho, int largo, Graphics g)
        {
            largo -= 45;
            double radio = 100;
            int angulo = 360 / grafo.n;
            float j = 0;
            for (int i = 0; i < grafo.n; i ++, j-= angulo)
            {
                grafo.nodos[i].x = (radio * Math.Cos(j * Math.PI / 180) + ancho / 2);
                grafo.nodos[i].y = (radio * Math.Sin(j * Math.PI / 180) + largo / 2);

                g.DrawEllipse(Pens.SteelBlue, (int)grafo.nodos[i].x, (int)grafo.nodos[i].y, 30, 30);

                Font fuente = new Font("Arial", 12);
                StringFormat formato = new StringFormat();
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;

                g.DrawString(((char)(i + 65)).ToString(), fuente, Brushes.Black, (int)grafo.nodos[i].x + 15, (int)grafo.nodos[i].y + 15, formato);
            }
            
            
        }
        private void botonAgregar_Click(object sender, EventArgs e)
        {
            
        }

        private void iTalk_ThemeContainer1_Click(object sender, EventArgs e)
        {

        }

        private void iTalk_Button_21_Click(object sender, EventArgs e)
        {            
            try
            {
                grafico = new Grafo(Int32.Parse(txtNodo.Text));                
                pictureBoxGrafo.Refresh();
                for (int i = 0; i < grafico.n; i++)
                {
                    comboOrigenTran.Items.Add(((char)(i + 65)).ToString());
                    comboDestinoTran.Items.Add(((char)(i + 65)).ToString());
                    comboOrigenBusq.Items.Add(((char)(i + 65)).ToString());
                    comboDestinoBusq.Items.Add(((char)(i + 65)).ToString());
                    comboNodoGrado.Items.Add(((char)(i + 65)).ToString());
                    comboOrigenDikstra.Items.Add(((char)(i + 65)).ToString());
                }
                

                AgregarEspaciosDataGrid(DGVMatrizAgregaciones);
                AgregarEspaciosDataGrid(DGVMatrizCostos);

                btnCrear.Enabled = false;
            }
            catch (Exception ex) { }
        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtNodo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNodo_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnAgregarTran_Click(object sender, EventArgs e)
        {
            int renglon = comboOrigenTran.SelectedIndex;
            int columna = comboDestinoTran.SelectedIndex;

            if (txtCosto.Text == "")
            {
                int valor = 0;
                txtCosto.Text = valor.ToString();
            }

            DGVMatrizAgregaciones[comboDestinoTran.SelectedIndex + 1, comboOrigenTran.SelectedIndex + 1].Value = 1;
            DGVMatrizCostos[comboDestinoTran.SelectedIndex + 1, comboOrigenTran.SelectedIndex + 1].Value = double.Parse(txtCosto.Text);

            grafico.matrizDeTransiciones[renglon, columna] = true;
            grafico.matrizDeCostos[renglon, columna] = double.Parse(txtCosto.Text);

            if (renglon == columna)
            {
                vuelta = true;
                nodoVuelta = columna;
            }

            pictureBoxGrafo.Refresh();
        }

        public void AgregarEspaciosDataGrid(DataGridView matriz)
        {            
            matriz.Columns.Add("", "");
            matriz.Columns[0].Width = 22;

            for (int i = 1; i <= grafico.n; i++)
            {             
                matriz.Columns.Add("", "");
                matriz.Columns[i].Width = 22;           
                matriz.Rows.Add("", "");
                matriz.Rows[i].Height = 20;

            }
            for (int i = 1; i <= grafico.n; i++)
            {
                matriz[i, 0].Value = ((char)(i + 64)).ToString();
                matriz[0, i].Value = ((char)(i + 64)).ToString();
            }
            for (int i = 1; i <= grafico.n; i++)
            {
                for (int j = 1; j <= grafico.n; j++)
                {
                    matriz[i, j].Value = "0";
                }
            }
            
        }

        private void dataGridViewCosto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBoxGrafo_Click(object sender, EventArgs e)
        {

        }
        

        private void iTalk_Button_21_Click_1(object sender, EventArgs e)
        {
            int Index = comboNodoGrado.SelectedIndex;
            LBLgradoSalida.Text = grafico.calcularGradoSalida(grafico, Index).ToString();
            LBLgradoEntrada.Text = grafico.calcularGradoEntrada(grafico, Index).ToString();
        }

        private void BtnBusqueda_Click(object sender, EventArgs e)
        {
            int origen = comboOrigenBusq.SelectedIndex;
            int destino = comboDestinoBusq.SelectedIndex;

            if (comboTipoBusq.Text == "Anchura")
            {
                grafico.BFS_anchura(grafico.nodos[origen], grafico.nodos[destino]);                
            }
            else
            {
                grafico.DFS_profundidad(grafico.nodos[origen], grafico.nodos[destino]);
            }
            pictureBoxGrafo.Refresh();
        }

        private void LBLgradoSalida_Click(object sender, EventArgs e)
        {

        }

        private void BtnDikstra_Click(object sender, EventArgs e)
        {

        }
        public void dibujarRuta(Nodo nodoDesino, Graphics e)
        {
            if(nodoDesino.nodoPadre != null)
            {
                pintarArista(nodoDesino.nodoPadre, nodoDesino, e, true);
                dibujarRuta(nodoDesino.nodoPadre, e);
            }
        }

        private void iTalk_Button_21_Click_2(object sender, EventArgs e)
        {
            grafico.reiniciar();
            pictureBoxGrafo.Refresh();
        }
    }
}
