using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace SIGBOD
{
    public partial class FEmpleados : Form
    {
        OpenFileDialog getImage = new();
        public FEmpleados()
        {
            InitializeComponent();
        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;
        private string Estado = "";
        public string imagen = "";
        public string rutaBase = @"C:\Users\Gmn\Documents\ImagenesSigbod\";
        public string rutaEmpleados = @"C:\Users\Gmn\Documents\ImagenesSigbod\Empleados\";

        // CARGA PARA EL LISTADO DE TODOS LOS EMPLEADOS
        private void Cargar(int estado)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            if (estado == 1)
            {
                // GIMENA: Esta parte del codigo se encarga de llenar el listado para los empleados
                string cadena = "Select * from empleados where estado = 1";
                SqlCommand comando = new(cadena, conexion.conectarBD);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                DGListadoEmpleados.DataSource = tabla;
                DGListadoEmpleados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            else if(estado == 0)
            {
                // GIMENA: Esta parte del codigo se encarga de llenar el listado para los empleados
                string cadena = "Select * from empleados where estado = 0";
                SqlCommand comando = new(cadena, conexion.conectarBD);
                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);
                DGListadoEmpleados.DataSource = tabla;
                DGListadoEmpleados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
            }
            conexion.Cerrar();
        }

        // PASAR DATOS DE LA LISTA A LOS TEXT
        private void DGListadoCargos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        //DATOS DE CARGA AL INICIO DE LA VENTANA
        private void FEmpleados_Load(object sender, EventArgs e)
        {
            Cargar(1);
            llenacombobox();//llama al método llenacombobox
            llenarComboEstado();
            cmbCargo.Text = "Seleccione cargo";
            PBEmpleado.Load(@"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil.png");
            valor = 0;  
        }

        // COMBO PARA EL ESTADO DE LOS REGISTROS
        public void llenarComboEstado()
        {
            CBEstado.DisplayMember = "Text";
            CBEstado.ValueMember = "Value";
            CBEstado.SelectedIndex = CBEstado.Items.IndexOf("Habilitado");

            CBEstado.Items.Add(new { Text = "Habilitado", Value = 1 });
            CBEstado.Items.Add(new { Text = "Inhabilitado", Value = 2 });
            CBEstado.SelectedIndex = 0;
        }

        //COMBO PARA EL LISTADO DE LOS CARGOS
        public void llenacombobox()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT CCargo,Cargo FROM Cargos", conexion.conectarBD);
            //se indica el nombre de la tabla
            da.Fill(ds, "Cargos");
            cmbCargo.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbCargo.ValueMember = "CCargo";
            cmbCargo.DisplayMember = "Cargo";
        }

        // GIMENA: FUNCION QUE PERIMITE AGREGAR O EDITAR UN REGISTRO
        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Empleados(Identidad,Nombre,Telefono,Correo,Direccion,Cargo,Salario,Imagen,Estado) VALUES (@Identidad,@Nombre,@Telefono,@Correo,@Direccion,@Cargo,@Salario,@Imagen,@Estado)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //Inicio: Proceso para almacenar la imagen
                    saveFileDialog1 = new SaveFileDialog();

                    // Identificar formatos permitidos
                    saveFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

                    // Directorio donde se almacenan las imagenes
                    saveFileDialog1.InitialDirectory = @"C:\Users\Gmn\Documents\ImagenesSigbod\Empleados";

                    //obtine el numero de identidad del empleado y lo muestra en la ventana de almacenamiento.
                    saveFileDialog1.FileName = txtIdentidad.Text;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MemoryStream ms = new MemoryStream();
                        PBEmpleado.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        txtRuta.Text = saveFileDialog1.FileName;
                    }
                    else
                    {
                        MessageBox.Show("No ha almacenado ninguna imagen para este registro", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    // si no selecciona ninguna imagen se proporciona una imagen por defecto.
                    if (txtRuta.Text=="") {
                        txtRuta.Text = @"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil";
                    }

                    comando.Parameters.AddWithValue("@Identidad", txtIdentidad.Text);
                    comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    comando.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@Cargo", Convert.ToInt32(cmbCargo.SelectedValue));
                    comando.Parameters.AddWithValue("@Salario", txtSalario.Text);
                    comando.Parameters.AddWithValue("@Imagen", txtRuta.Text);
                    comando.Parameters.AddWithValue("@Estado", 1);

                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar(1);
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
            else if (x == 2) // GIMENA: Modificar
            {

                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "UPDATE Empleados SET Identidad=@Identidad,Nombre=@Nombre,Telefono=@Telefono,Correo=@Correo,Direccion=@Direccion,Cargo=@Cargo,Salario=@Salario,Imagen=@Imagen WHERE CEmpleado=" + txtCEmpleado.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //Inicio: Proceso para almacenar la imagen
                    saveFileDialog1 = new SaveFileDialog();

                    // Identificar formatos permitidos
                    saveFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

                    // Directorio donde se almacenan las imagenes
                    saveFileDialog1.InitialDirectory = @"C:\Users\Gmn\Documents\ImagenesSigbod\Empleados";

                    //obtine el numero de identidad del empleado y lo muestra en la ventana de almacenamiento.
                    saveFileDialog1.FileName = txtIdentidad.Text;

                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MemoryStream ms = new MemoryStream();
                        PBEmpleado.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        txtRuta.Text = saveFileDialog1.FileName;
                    }
                    else
                    {
                        MessageBox.Show("No ha almacenado ninguna imagen para este registro", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    }

                    // si no selecciona ninguna imagen se proporciona una imagen por defecto.
                    if (txtRuta.Text == "")
                    {
                        txtRuta.Text = @"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil";
                    }

                    comando.Parameters.AddWithValue("@Identidad", txtIdentidad.Text);
                    comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
                    comando.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@Correo", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@Direccion", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@Cargo", Convert.ToInt32(cmbCargo.SelectedValue));
                    comando.Parameters.AddWithValue("@Salario", txtSalario.Text);
                    comando.Parameters.AddWithValue("@Imagen", txtRuta.Text);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar(1);
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

            }
            else if (x == 3) // GIMENA: Habilitar/Inhabilitar
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea cambiar el estado de este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ConexionBD conexion = new();
                    conexion.Abrir();
                    string cadena = "UPDATE Empleados SET estado=@Estado WHERE CEmpleado=" + txtCEmpleado.Text;
                    try
                    {
                        SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                        if (btnEstado.Text == "Habilitar")
                        {
                            comando.Parameters.AddWithValue("@Estado", 1);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@Estado", 0);
                        }
                        comando.ExecuteNonQuery();
                        conexion.Cerrar();
                        // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                        Cargar(1);
                        Restablecer(4);
                        valor = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar(1);
                    Restablecer(4);
                    valor = 0;
                }

            }
        }

        // GIMENA: ACCION QUE PERMITE REFRESCAR LA PANTALLA UNA VEZ REALIZADA UNA ACCION
        private void Restablecer(int x)
        {
            if (x == 1)
            {
                // GIMENA: Opcion cuando se trata de un nuevo registro
                txtCEmpleado.Text = "";
                txtIdentidad.Text = "";
                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                txtDireccion.Text = "";
                txtSalario.Text = "";
                txtRuta.Text = "";
                cmbCargo.Text = "Seleccione cargo";

                txtIdentidad.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtDireccion.Enabled = true;
                txtSalario.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = false;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtIdentidad.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                txtDireccion.Enabled = false;
                txtSalario.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
                btnEstado.Text = "Habilitar";
                PBEmpleado.Load(@"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil.png");
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtIdentidad.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtDireccion.Enabled = true;
                txtSalario.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtCEmpleado.Text = "";
                txtIdentidad.Text = "";
                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                txtDireccion.Text = "";
                txtSalario.Text = "";
                txtRuta.Text = "";
                cmbCargo.Text = "Seleccione cargo";
                PBEmpleado.Load(@"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil.png");

                txtIdentidad.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = true;
                txtDireccion.Enabled = false;
                txtSalario.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
            }
        }



        // GIMENA: BOTON QUE HACE LLAMADO A FUNCION DE ELIMINAR
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string cadena = "DELETE FROM Empleados WHERE CEmpleado=" + txtCEmpleado.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar(1);
                    Restablecer(4);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
            else if (dialogResult == DialogResult.No)
            {
                // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                Cargar(1);
                Restablecer(4);
                valor = 0;
            }
        }


        // GIMENA: BOTON ENCARGADO DE CARGAR LAS IMAGENES
        private void button1_Click(object sender, EventArgs e)
        {
            prodimg();
        }

        // GIMENA: FUNCION DESIGNADA PARA DAR FORMATO A UNA IMAGEN
        private void prodimg()
        {
            crearCarpetasEmpleados();

            //MIME TYPES FILTRAR QUE SOLO ACEPTE 2 TIPOS DE EXTENCION
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            //OBTENEMOS LA EXTENCION DE LA IMAGEN 
            string extimg = System.IO.Path.GetExtension(openFileDialog1.FileName);

            //EL CODIGO DEL PRODUCTO SERA EL NOMBRE Y LE AGREGAMOS LA EXTENCION DE LA IMAGEN
            string nombimg = (txtIdentidad.Text) + extimg;
            txtRuta.Text = rutaEmpleados + nombimg;


            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PBEmpleado.Image = Image.FromFile(openFileDialog1.FileName);
                PBEmpleado.SizeMode = PictureBoxSizeMode.CenterImage;
                PBEmpleado.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        // GIMENA: FUNCION DESIGNADA PARA CREAR LA CARPETA QUE ALMACENARA LOS PERFILES DE LOS EPLEADOS
        private void crearCarpetasEmpleados()
        {
            //VARIBLE QUE CONTENDRA EL NOMBRE DE LA CARPETA EMPLEADOS
            string ImgEmpleados = rutaBase + @"\Empleados";
            //string ImgEmpleados = Application.StartupPath + @"\ImgEmpleados";

            try
            {
                if (!Directory.Exists(ImgEmpleados))//&& Directory.Exists(imgProductos))
                {
                    MessageBox.Show("Se creara un directorio para almacenar los perfiles de los empleados");
                    Directory.CreateDirectory(ImgEmpleados);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrio un Error :\n " + ex);
            }
        }

        // GIMENA: CAJA DE TEXTO QUE PERMITE FILTRAR LOS DATOS QUE HAY EN UN GRID
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        ConexionBD conexion = new();
        conexion.Abrir();
            if (RBCodigo.Checked == true & CBEstado.Text == "Habilitado")
            {
                string cadena = "SELECT * FROM Empleados WHERE Identidad LIKE @Identidad + '%' and Estado = 1";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@Identidad", txtBuscar.Text);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    DGListadoEmpleados.DataSource = dt;

                    conexion.Cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }else if (RBCodigo.Checked == true & CBEstado.Text == "Inhabilitado")
            {
                string cadena = "SELECT * FROM Empleados WHERE Identidad LIKE @Identidad + '%' and Estado = 0";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@Identidad", txtBuscar.Text);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    DGListadoEmpleados.DataSource = dt;

                    conexion.Cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }else if (RBNombre.Checked == true & CBEstado.Text == "Habilitado")
            {
                string cadena = "SELECT * FROM Empleados WHERE Nombre LIKE @Nombre + '%' and Estado = 1";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@Nombre", txtBuscar.Text);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    DGListadoEmpleados.DataSource = dt;

                    conexion.Cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
            else if (RBNombre.Checked == true & CBEstado.Text == "Inhabilitado")
            {
                string cadena = "SELECT * FROM Empleados WHERE Nombre LIKE @Nombre + '%' and Estado = 0";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@Nombre", txtBuscar.Text);

                    SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                    DataTable dt = new DataTable();
                    adaptador.Fill(dt);

                    DGListadoEmpleados.DataSource = dt;

                    conexion.Cerrar();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
        }

        // GIMENA: FUNCION PARA FILTRAR LA LISTA SEGUN LOS DATOS HABILITADOS O INHABILITADOS
        private void CBEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CBEstado.Text == "Habilitado")
            {
                Cargar(1);
            }
            else
            {
                Cargar(0);
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCEmpleado.Text == "")
            {
                MessageBox.Show("Seleccione un registro de la tabla", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Editar)
                valor = 2;
                Restablecer(3);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            AgrEdit(3);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
            this.Close();
        }

        private void DGListadoEmpleados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valor = 0;
                txtCEmpleado.Text = DGListadoEmpleados.CurrentRow.Cells[0].Value.ToString();
                txtIdentidad.Text = DGListadoEmpleados.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = DGListadoEmpleados.CurrentRow.Cells[2].Value.ToString();
                txtTelefono.Text = DGListadoEmpleados.CurrentRow.Cells[3].Value.ToString();
                txtCorreo.Text = DGListadoEmpleados.CurrentRow.Cells[4].Value.ToString();
                txtDireccion.Text = DGListadoEmpleados.CurrentRow.Cells[5].Value.ToString();
                cmbCargo.SelectedValue = DGListadoEmpleados.CurrentRow.Cells[6].Value.ToString();
                txtSalario.Text = DGListadoEmpleados.CurrentRow.Cells[7].Value.ToString();
                txtRuta.Text = DGListadoEmpleados.CurrentRow.Cells[8].Value.ToString();
                PBEmpleado.Load(DGListadoEmpleados.CurrentRow.Cells[8].Value.ToString());

                // Si el estado es 1 el boton indicara que se puede inhabilitar caso contrario solo se podra habilitar.
                Estado = DGListadoEmpleados.CurrentRow.Cells[8].Value.ToString();
                btnEstado.Text = (Estado == "1") ? "Inhabilitar" : "Habilitar";
                btnEstado.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presento un error" + ex.Message);
            }
        }
    } 
}
