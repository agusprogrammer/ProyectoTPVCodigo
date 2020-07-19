using CrystalDecisions.CrystalReports.Engine;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using proyecto.tpv.agustin.Servicios;
using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para UCFactura.xaml
    /// </summary>
    public partial class UCFactura : UserControl
    {
        private proyecto_tpvEntities tpvEnt;        //Conexion con la base de datos
        private MVVentasDeProductos mVVentaProds;   //MV de ventas de productos

        /// <summary>
        /// Constructor de UCFactura
        /// </summary>
        /// <param name="ent">Conexion de la base de datos que le pasamos por parametro</param>
        public UCFactura(proyecto_tpvEntities ent)
        {
            InitializeComponent();
            tpvEnt = ent;
            mVVentaProds = new MVVentasDeProductos(tpvEnt);
            DataContext = mVVentaProds;
            
        }

        
    }
}
