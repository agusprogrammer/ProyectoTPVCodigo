using MahApps.Metro.Controls;
using proyecto.tpv.agustin.Componentes;
using proyecto.tpv.agustin.Modelo;
using proyecto.tpv.agustin.MVVM;
using proyecto.tpv.agustin.Vista.ControlesUsuario;
using proyecto.tpv.agustin.Vista.Dialogos;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace proyecto.tpv.agustin
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private proyecto_tpvEntities tpvEnt;

        private usuarios usuLogueado; //Usuario logueado
        private MVTiposProductos mVTiposProds; //MV de tipos de producto, tiene lista de tipos de productos
        private MVCategorias mVCategorias; //MV categorias, selecciona categorias a partir de un tipo
        private MVProductos mVProductos; //MV productos, selecciona productos a partir de una categoria
        private MVVentasDeProductos mVVentaProd;
        //MV de ventas de productos, tiene la lista 
        //de cada venta de producto

        private List<categorias> listaCategorias;
        //lista de categorias
        private List<productos> listaProds;

        //lista de productos a comprar
        private List<productos> listaProdsCompra; 
        
        //lista de botones con productos
        private List<ventas_de_productos> listaVentaProd;

        //Producto seleccionado por el boton
        private productos prodSelectBtn;

        /// <summary>
        /// Constructor del MainWindow
        /// </summary>
        /// <param name="ent">Conexion con la base de datos</param>
        /// <param name="usu">Usuario que ha iniciado sesion</param>
        public MainWindow(proyecto_tpvEntities ent, usuarios usu)
        {
            InitializeComponent();
            tpvEnt = ent;
            usuLogueado = new usuarios();
            usuLogueado = usu;
            mVTiposProds = new MVTiposProductos(tpvEnt);
            mVCategorias = new MVCategorias(tpvEnt);
            mVProductos = new MVProductos(tpvEnt);
            mVVentaProd = new MVVentasDeProductos(tpvEnt);
            //DataContext = mVVentaProd; //Datacontext, comentado

            listaCategorias = new List<categorias>();
            listaProds = new List<productos>(); //Lista de productos
            listaProdsCompra = new List<productos>(); //lista de la compra
            listaVentaProd = new List<ventas_de_productos>();

            prodSelectBtn = new productos();

            cargarTiposProductos();
        }

        
        /// <summary>
        /// Evento para acceder a los datos de usuario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MostrarUsu_Click(object sender, RoutedEventArgs e)
        {
            DialogInfoUsu diagInfoUsu = new DialogInfoUsu(tpvEnt, usuLogueado);
            diagInfoUsu.ShowDialog();

        }
        

        /// <summary>
        /// Evento para acceder al panel de administracion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAdministracion_Click(object sender, RoutedEventArgs e)
        {
            DialogAdminProg diagAdmin = new DialogAdminProg(tpvEnt);
            diagAdmin.Show();
        }
        
        /// <summary>
        /// evento para volver al login de la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            LoginDialog ventanaLogin = new LoginDialog();
            ventanaLogin.Show();
            this.Close();
        }
        
        /// <summary>
        /// Evento para salir de la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSalirAppMain_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        
        /// <summary>
        /// Metodo que carga los botones de tipos de productos
        /// </summary>
        private void cargarTiposProductos()
        {
            foreach (tipos_de_productos tiposProd in mVTiposProds.listaTiposProds)
            {
                System.Drawing.Image imgProd = obtenerImgTipoProd(tiposProd);

                //Pasar de System.Drawing.Image a imagen para el componente
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgProd);
                IntPtr hBitmap = bmp.GetHbitmap();
                System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                
                Image imgBtn = new Image();
                //imgBtn.Source = new BitmapImage(new Uri("pack://application:,,/Recursos/Iconos/" + "iconApp4Black.png"));
                imgBtn.Source = WpfBitmap;
                imgBtn.Height = 70;

                TextBlock textoBtn = new TextBlock()
                {
                    Text = tiposProd.tipo_de_producto,
                    Margin = new Thickness(0, 5, 0, 0),
                    FontSize = 12
                };

                StackPanel stkPanelBtn = new StackPanel();

                stkPanelBtn.Children.Add(imgBtn);
                stkPanelBtn.Children.Add(textoBtn);

                BtnTipoProd btnTProd = new BtnTipoProd()
                {
                    Width = 200, //160
                    Height = 120,
                    BorderBrush = Brushes.Black,
                    Background = Brushes.Transparent,
                    Margin = new Thickness(10, 10, 10, 10),
                    Content = stkPanelBtn,
                    tipoProdBtn = tiposProd,
                };

                btnTProd.Click += new RoutedEventHandler(botonTipoProducto_Click);

                wrapTiposProd.Children.Add(btnTProd);


            }
        }

        /// <summary>
        /// Metodo que obtiene las imagenes para los botones de 
        /// tipos de productos
        /// </summary>
        /// <param name="tiposProd">Objeto tipos de producto</param>
        /// <returns>Retorna una imagen System.Drawing.Image</returns>
        private System.Drawing.Image obtenerImgTipoProd(tipos_de_productos tiposProd)
        {
            List<categorias> listaCateg = new List<categorias>();
            listaCateg = tiposProd.categorias.Cast<categorias>().ToList();

            productos prod = new productos();

            foreach (categorias categ in listaCateg)
            {

                List<productos> listaProd = new List<productos>();
                listaProds = categ.productos.Cast<productos>().ToList();
                
                //Obtenemos el primer producto
                prod = listaProds[0];
                
            }

            byte[] byteImg = prod.imagen_producto;

            System.Drawing.Image imgProd = pasarDeArrayByteAImg(byteImg);

            return imgProd;
        }

        /// <summary>
        /// Metodo para transformar un array de byte en una imagen
        /// </summary>
        /// <param name="byteImg">Array de bytes</param>
        /// <returns>Retorna una imagen System.Drawing.Image</returns>
        private System.Drawing.Image pasarDeArrayByteAImg(byte[] byteImg)
        {
            
            System.Drawing.Image imgBtn = (System.Drawing.Bitmap)((new System.Drawing.ImageConverter()).ConvertFrom(byteImg));

            return imgBtn;
        }
        
        /// <summary>
        /// Evento de los botones de 
        /// tipos de producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void botonTipoProducto_Click(object sender, RoutedEventArgs e)
        {
            wrapProductos.Children.Clear();
            listaCategorias = mVCategorias.listaPorCategoria(((BtnTipoProd)sender).tipoProdBtn.cod_tipo_producto);
            
            foreach (categorias categ in listaCategorias)
            {
                
                listaProds = mVProductos.listaPorProductos(categ.cod_categoria);

                //Botones de productos
                //Cargar productos
                foreach (productos prod in listaProds)
                {
                    //Poner imagen en el boton
                    byte[] imgByteProd = prod.imagen_producto;

                    System.Drawing.Image imgBitmap = pasarDeArrayByteAImg(imgByteProd);

                    //convertimos la imagen obtenida a partir del array de bytes
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(imgBitmap);
                    IntPtr hBitmap = bmp.GetHbitmap();
                    System.Windows.Media.ImageSource WpfBitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    

                    //System.Windows.Controls.Image
                    Image imgBtn = new Image();
                    
                    //imgBtn.Source = new BitmapImage(new Uri("pack://application:,,/Recursos/Iconos/" + "iconApp4Black.png"));
                    imgBtn.Source = WpfBitmap;
                    imgBtn.Height = 70;
                    

                    TextBlock textoBtn = new TextBlock()
                    {
                        Text = prod.descripcion,
                        Margin = new Thickness(0, 5, 0, 0),
                        FontSize = 12
                    };

                    StackPanel stkPanelBtn = new StackPanel();

                    stkPanelBtn.Children.Add(imgBtn);
                    stkPanelBtn.Children.Add(textoBtn);

                    BtnProducto btnProd = new BtnProducto()
                    {
                        Width = 160,
                        Height = 120,
                        BorderBrush = Brushes.Black,
                        Background = Brushes.Transparent,
                        Margin = new Thickness(10, 10, 10, 10),
                        Content = stkPanelBtn,
                        prodBtn = prod
                    };

                    btnProd.Click += new RoutedEventHandler(botonProducto_Click);

                    wrapProductos.Children.Add(btnProd);
                    
                }
                
            }

        }

        /// <summary>
        /// Evento del boton de producto
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void botonProducto_Click(object sender, RoutedEventArgs e)
        {
            
            prodSelectBtn = ((BtnProducto)sender).prodBtn;
            
            listaProductos.Children.Clear();
            
            if (listaVentaProd.Count() == 0)
            {

                ventas_de_productos ventaProd = new ventas_de_productos();

                //Primer producto que se anyade
                //Agregamos el primero de la lista (para poder recorrerla)
                ventaProd.cod_producto = prodSelectBtn.cod_producto;
                ventaProd.cantidad_vendida = 1;
                ventaProd.precio_total_unidades = prodSelectBtn.precio;
                ventaProd.productos = prodSelectBtn;
                ventaProd.cod_producto = prodSelectBtn.cod_producto;
                //Falta el codigo de la venta que se agrega en el siguiente dialogo
                listaVentaProd.Add(ventaProd);
                listaProdsCompra.Add(prodSelectBtn);
                
                mVVentaProd.setListaProductos(listaVentaProd);
                UCListaProds uCListaProds = new UCListaProds(tpvEnt, mVVentaProd, usuLogueado);

                listaProductos.Children.Add(uCListaProds);

            }
            else
            {
                bool existe = false;
                for (int j = 0; j < listaVentaProd.Count(); j++)
                {
                    if (listaVentaProd[j].productos.cod_producto == prodSelectBtn.cod_producto)
                    {
                        existe = true;
                        listaVentaProd[j].cantidad_vendida = listaVentaProd[j].cantidad_vendida + 1;
                        listaVentaProd[j].precio_total_unidades = listaVentaProd[j].precio_total_unidades + prodSelectBtn.precio;
                        
                    }
                    
                }

                if (existe == false)
                {
                    ventas_de_productos ventaProd = new ventas_de_productos();

                    ventaProd.cod_producto = prodSelectBtn.cod_producto;
                    ventaProd.cantidad_vendida = 1;
                    ventaProd.precio_total_unidades = prodSelectBtn.precio;
                    ventaProd.productos = prodSelectBtn;
                    ventaProd.cod_producto = prodSelectBtn.cod_producto;
                    //Falta el codigo de la venta que se agrega en el siguiente dialogo
                    listaVentaProd.Add(ventaProd);
                    listaProdsCompra.Add(prodSelectBtn);
                    
                }
                
                mVVentaProd.setListaProductos(listaVentaProd);
                UCListaProds uCListaProds = new UCListaProds(tpvEnt, mVVentaProd, usuLogueado);

                listaProductos.Children.Add(uCListaProds);

            }
            
        }
        
        /// <summary>
        /// Metodo para obtener las ventas de cada producto
        /// saca la lista de la compra
        /// </summary>
        private void sacarListaCompra()
        {

            for (int i = 0; i < listaProdsCompra.Count(); i++)
            {

                productos prod = listaProdsCompra[i];

                ventas_de_productos ventaProd = new ventas_de_productos();
                if (listaVentaProd.Count() == 0)
                {
                    //Primer producto que se anyade
                    //Agregamos el primero de la lista (para poder recorrerla)
                    ventaProd.cod_producto = prod.cod_producto;
                    ventaProd.cantidad_vendida = 1;
                    ventaProd.precio_total_unidades = prod.precio;
                    ventaProd.productos = prod;
                    //Falta el codigo de la venta que se agrega en el siguiente dialogo
                    listaVentaProd.Add(ventaProd);

                    //Anyadir al MV la lista de productos
                    
                    mVVentaProd.setListaProductos(listaVentaProd);
                    UCListaProds uCListaProds = new UCListaProds(tpvEnt, mVVentaProd, usuLogueado);

                    listaProductos.Children.Add(uCListaProds);

                }
                else
                {
                    //Si el producto existe, se aumentan sus valores
                    //Si existe la venta de producto
                    bool existe = false;
                    
                    for (int j = 0; j < listaVentaProd.Count(); j++)
                    {
                        if (listaVentaProd[j].cod_producto == prod.cod_producto)
                        {

                            existe = true;
                            //Se aumentan sus valores

                            listaVentaProd[j].cantidad_vendida = listaVentaProd[j].cantidad_vendida + 1;
                            listaVentaProd[j].precio_total_unidades = listaVentaProd[j].precio_total_unidades + prod.precio;

                            //Poner la lista de productos
                            listaProductos.Children.Clear();

                            mVVentaProd.setListaProductos(listaVentaProd);
                            UCListaProds uCListaProds = new UCListaProds(tpvEnt, mVVentaProd, usuLogueado);

                            listaProductos.Children.Add(uCListaProds);

                        }

                    }
                    

                    if (existe == false)
                    {
                        //Se agrega a la lista
                        ventaProd.cod_producto = prod.cod_producto;
                        ventaProd.cantidad_vendida = 1;
                        ventaProd.precio_total_unidades = prod.precio;
                        ventaProd.productos = prod;
                        //Falta el codigo de la venta que se agrega en el siguiente dialogo
                        listaVentaProd.Add(ventaProd);

                        listaProductos.Children.Clear();

                        mVVentaProd.setListaProductos(listaVentaProd);
                        UCListaProds uCListaProds = new UCListaProds(tpvEnt, mVVentaProd, usuLogueado);

                        listaProductos.Children.Add(uCListaProds);
                    }
                    
                    
                }

            }
            

        }



    }
}
