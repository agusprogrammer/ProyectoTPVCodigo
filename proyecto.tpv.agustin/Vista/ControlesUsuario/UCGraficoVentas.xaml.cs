using MySql.Data.MySqlClient;
using proyecto.tpv.agustin.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace proyecto.tpv.agustin.Vista.ControlesUsuario
{
    /// <summary>
    /// Lógica de interacción para UCGraficoVentas.xaml
    /// </summary>
    public partial class UCGraficoVentas : UserControl
    {
        private proyecto_tpvEntities tpvEnt;    //Conexion de la base de datos

        /// <summary>
        /// Constructor UCGraficoVentas 
        /// </summary>
        /// <param name="ent">Conexion de la base de datos que le pasamos por parametro</param>
        public UCGraficoVentas(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
            
            seleccionarVentasMes();
        }
        
        /// <summary>
        /// Metodo para seleccionar los datos en el grafico de ventas
        /// </summary>
        private void seleccionarVentasMes()
        {
            // Creamos el objeto conexión
            MySqlConnection con = new MySqlConnection("Server=localhost;Database=proyecto_tpv;Uid=root;Pwd=mysql");
            // Creamos el DataSet
            DataSet ds = new DataSet();
            // Conectamos con la base de datos
            con.Open();
            // Creamos el DataAdapter al cuál ya le pasamos el comando a ejecutar
            MySqlDataAdapter adapt = new MySqlDataAdapter("select monthname(ventas.fecha) as mes, count(ventas.cod_venta) as numeroVentas from ventas group by monthname(ventas.fecha); ", con);
            // Obtenemos la información de la base de datos
            adapt.Fill(ds, "ventas");
            // Asociamos la fuente de datos del gráfico con el DataAdapter

            //recorremos y obtenemos la lista
            List<KeyValuePair<string, int>> valueList = new List<KeyValuePair<string, int>>();
            
            foreach (DataRow pRow in ds.Tables["ventas"].Rows)
            {
                string nombreMes = (string) pRow["mes"];
                string numVentas = pRow["numeroVentas"].ToString();

                int numVentasNum = int.Parse(numVentas);

                valueList.Add(new KeyValuePair<string, int>(nombreMes, numVentasNum));
                
            }

            chart1.DataContext = valueList; 
            
            con.Close();
        }

    }
}
