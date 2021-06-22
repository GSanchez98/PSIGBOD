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
using SIGBOD.MConfiguracion;

namespace SIGBOD
{
    public partial class FEmpleados : Form
    {
        OpenFileDialog getImage = new();
        public FEmpleados()
        {
            InitializeComponent();
        }

        //Constructor encargado de obtener datos de lista
        //public FEmpleados(string CEmpleado, 
        //                    string Identidad, 
        //                    string Nombre, 
        //                    string telefono,
        //                    string correo,
        //                    string direccion,
        //                    int valorCombo,
        //                    string fechaNac,
        //                    string fechaIng,
        //                    string salario,
        //                    string ruta)
        //{
        //    int valorIndex;
        //    InitializeComponent();
            
        //}
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;
        private string Estado = "";
        public string imagen = "";
        public string rutaBase = @"C:\Users\Public\Pictures\Sigbod";
        public string rutaEmpleados = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod";

        // CARGA PARA EL LISTADO DE TODOS LOS EMPLEADOS
        private void Cargar(int estado)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            if (estado == 1)
            {
                // GIMENA: Esta parte del codigo se encarga de llenar el listado para los empleados
                string cadena = "Select * from Usuarios.Empleados where estado_Empleado = 1";
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
                string cadena = "Select * from Usuarios.Empleados where estado_Empleado = 0";
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
            //MessageBox.Show("este es el index: "+ cmbCargo.SelectedIndex.ToString());
            Cargar(1);
            llenacombobox();//llama al método llenacombobox
            llenarComboEstado();
            DGListadoEmpleados.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            // cmbCargo.Text = "ccc";
            PBEmpleado.Load(@"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg");
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
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_Cargo, nombre_Cargo FROM Usuarios.Cargos", conexion.conectarBD);
            //se indica el nombre de la tabla
            da.Fill(ds, "Usuarios.Cargos");
            cmbCargo.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbCargo.ValueMember = "id_Cargo";
            cmbCargo.DisplayMember = "nombre_Cargo";
        }

        // GIMENA: FUNCION QUE PERIMITE AGREGAR O EDITAR UN REGISTRO
        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Usuarios.Empleados(identidad_Empleado,nombre_Empleado,telefono_Empleado,correo_Empleado,direccion_Empleado,id_cargo_Empleado,fecha_nacimiento_Empleado,fecha_ingreso_Empleado,salario_Empleado,Imagen_Empleado,estado_Empleado,fecha_agrego_Empleado,agrego_Empleado)VALUES(@identidad_Empleado,@nombre_Empleado,@telefono_Empleado,@correo_Empleado,@direccion_Empleado,@id_cargo_Empleado,@fecha_nacimiento_Empleado,@fecha_ingreso_Empleado,@salario_Empleado,@Imagen_Empleado,@estado_Empleado,@fecha_agrego_Empleado,@agrego_Empleado)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //Inicio: Proceso para almacenar la imagen
                    saveFileDialog1 = new SaveFileDialog();

                    // Identificar formatos permitidos
                    saveFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

                    // Directorio donde se almacenan las imagenes
                    saveFileDialog1.InitialDirectory = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod";

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
                        txtRuta.Text = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg";
                    }

                    DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
                    DateTime fechaIngreso = Convert.ToDateTime(txtFechaIng.Text);

                    comando.Parameters.AddWithValue("@identidad_Empleado", txtIdentidad.Text);
                    comando.Parameters.AddWithValue("@nombre_Empleado", txtNombre.Text);
                    comando.Parameters.AddWithValue("@telefono_Empleado", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@direccion_Empleado", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@correo_Empleado", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@id_cargo_Empleado", Convert.ToInt32(cmbCargo.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha_nacimiento_Empleado", fechaNacimiento);
                    comando.Parameters.AddWithValue("@fecha_ingreso_Empleado", fechaIngreso);
                    comando.Parameters.AddWithValue("@salario_Empleado", txtSalario.Text);
                    comando.Parameters.AddWithValue("@Imagen_Empleado", txtRuta.Text);
                    comando.Parameters.AddWithValue("@estado_Empleado", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Empleado", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Empleado", 0);

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
                string cadena = "UPDATE Usuarios.Empleados SET identidad_Empleado=@identidad_Empleado,nombre_Empleado=@nombre_Empleado,telefono_Empleado=@telefono_Empleado,correo_Empleado=@correo_Empleado,direccion_Empleado=@direccion_Empleado,id_cargo_Empleado=@id_cargo_Empleado,fecha_nacimiento_Empleado=@fecha_nacimiento_Empleado,fecha_ingreso_Empleado=@fecha_ingreso_Empleado,salario_Empleado=@salario_Empleado,Imagen_Empleado=@Imagen_Empleado,estado_Empleado=@estado_Empleado,fecha_agrego_Empleado=@fecha_agrego_Empleado,agrego_Empleado=@agrego_Empleado WHERE id_Empleado=" + txtCEmpleado.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //Inicio: Proceso para almacenar la imagen
                    saveFileDialog1 = new SaveFileDialog();

                    // Identificar formatos permitidos
                    saveFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

                    // Directorio donde se almacenan las imagenes
                    saveFileDialog1.InitialDirectory = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod";

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
                        txtRuta.Text = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg";
                    }

                    DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
                    DateTime fechaIngreso = Convert.ToDateTime(txtFechaIng.Text);

                    comando.Parameters.AddWithValue("@identidad_Empleado", txtIdentidad.Text);
                    comando.Parameters.AddWithValue("@nombre_Empleado", txtNombre.Text);
                    comando.Parameters.AddWithValue("@telefono_Empleado", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@direccion_Empleado", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@correo_Empleado", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@id_cargo_Empleado", Convert.ToInt32(cmbCargo.SelectedValue));
                    comando.Parameters.AddWithValue("@fecha_nacimiento_Empleado", fechaNacimiento);
                    comando.Parameters.AddWithValue("@fecha_ingreso_Empleado", fechaIngreso);
                    comando.Parameters.AddWithValue("@salario_Empleado", txtSalario.Text);
                    comando.Parameters.AddWithValue("@Imagen_Empleado", txtRuta.Text);
                    comando.Parameters.AddWithValue("@estado_Empleado", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Empleado", DateTime.Today);
                    comando.Parameters.AddWithValue("@agrego_Empleado", 0);
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
                    string cadena = "UPDATE Usuarios.Empleados SET estado_Empleado=@estado_Empleado WHERE id_Empleado=" + txtCEmpleado.Text;
                    try
                    {
                        SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                        if (btnEstado.Text == "Habilitar")
                        {
                            comando.Parameters.AddWithValue("@estado_Empleado", 1);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@estado_Empleado", 0);
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
                //cmbCargo.Text = "Seleccione cargo";

                txtIdentidad.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtFechaIng.Enabled = true;
                txtFechaNac.Enabled = true;
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
                txtFechaIng.Enabled = false;
                txtFechaNac.Enabled = false;
                txtSalario.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
                btnEstado.Text = "Habilitar";
                PBEmpleado.Load(@"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg");
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtIdentidad.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtFechaIng.Enabled = true;
                txtFechaNac.Enabled = true;
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
                //cmbCargo.Text = "Seleccione cargo";
                PBEmpleado.Load(@"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg");

                txtIdentidad.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                txtFechaIng.Enabled = false;
                txtFechaNac.Enabled = false;
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
                string cadena = "DELETE FROM Usuarios.Empleados WHERE id_Empleado=" + txtCEmpleado.Text;
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
            int valorIndex;
            try
            {
                valor = 0;
                txtCEmpleado.Text = DGListadoEmpleados.CurrentRow.Cells[0].Value.ToString();
                txtIdentidad.Text = DGListadoEmpleados.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = DGListadoEmpleados.CurrentRow.Cells[2].Value.ToString();
                txtTelefono.Text = DGListadoEmpleados.CurrentRow.Cells[3].Value.ToString();
                txtCorreo.Text = DGListadoEmpleados.CurrentRow.Cells[4].Value.ToString();
                txtDireccion.Text = DGListadoEmpleados.CurrentRow.Cells[5].Value.ToString();
                //MessageBox.Show(DGListadoEmpleados.CurrentRow.Cells[6].Value.ToString());
                valorIndex = Convert.ToInt32(DGListadoEmpleados.CurrentRow.Cells[6].Value);
                txtFechaNac.Text = DGListadoEmpleados.CurrentRow.Cells[7].Value.ToString();
                txtFechaIng.Text = DGListadoEmpleados.CurrentRow.Cells[8].Value.ToString();
                txtSalario.Text = DGListadoEmpleados.CurrentRow.Cells[9].Value.ToString();
                txtRuta.Text = DGListadoEmpleados.CurrentRow.Cells[10].Value.ToString();
                PBEmpleado.Load(DGListadoEmpleados.CurrentRow.Cells[10].Value.ToString());
                cmbCargo.SelectedValue = valorIndex;

                // Si el estado es 1 el boton indicara que se puede inhabilitar caso contrario solo se podra habilitar.
                Estado = DGListadoEmpleados.CurrentRow.Cells[8].Value.ToString();
                btnEstado.Text = (Estado == "1") ? "Inhabilitar" : "Habilitar";
                btnEstado.Enabled = true;
               // MessageBox.Show(valorIndex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presento un error" + ex.Message);
            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtBuscar_TextChanged_1(object sender, EventArgs e)
        {

            ConexionBD conexion = new();
            conexion.Abrir();
            if (RBCodigo.Checked == true & CBEstado.Text == "Habilitado")
            {
                string cadena = "SELECT * FROM Usuarios.Empleados WHERE identidad_Empleado LIKE @identidad_Empleado + '%' and estado_Empleado = 1";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@identidad_Empleado", txtBuscar.Text);

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
            else if (RBCodigo.Checked == true & CBEstado.Text == "Inhabilitado")
            {
                string cadena = "SELECT * FROM Empleados WHERE identidad_Empleado LIKE @identidad_Empleado + '%' and estado_Empleado = 0";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@identidad_Empleado", txtBuscar.Text);

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
            else if (RBNombre.Checked == true & CBEstado.Text == "Habilitado")
            {
                string cadena = "SELECT * FROM Usuarios.Empleados WHERE nombre_Empleado LIKE @nombre_Empleado + '%' and estado_Empleado = 1";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@nombre_Empleado", txtBuscar.Text);

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
                string cadena = "SELECT * FROM Usuarios.Empleados WHERE nombre_Empleado LIKE @nombre_Empleado + '%' and estado_Empleado = 0";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    //comando.ExecuteNonQuery();
                    comando.Parameters.AddWithValue("@nombre_Empleado", txtBuscar.Text);

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

        private void btnLista_Click(object sender, EventArgs e)
        {
            FListas listado = new FListas(1);
            AddOwnedForm(listado);
            listado.Show();
            //DGListadoEmpleados.Visible = true;
            //label1.Visible = true;
            //label3.Visible = true;
            //label4.Visible = true;
            //txtBuscar.Visible = true;
            //RBCodigo.Visible = true;
            //RBNombre.Visible = true;
            //CBEstado.Visible = true;
        }


        private void txtIdentidad_Leave(object sender, EventArgs e)
        {
            cargarEmpleado(txtIdentidad.Text);
        }

        // Encargada de traer los datos del empleado si exite
        private void cargarEmpleado(string identidad)
        {
            int valorIndex;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPH7FI1; initial Catalog=SIGBOD; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios.Empleados WHERE identidad_Empleado = @identidad", con);
            cmd.Parameters.AddWithValue("@identidad", identidad);
            SqlDataReader da = cmd.ExecuteReader();
            
                if (da.Read())
                {
                    txtNombre.Text = da.GetValue(2).ToString();
                    txtTelefono.Text = da.GetValue(3).ToString();
                    txtCorreo.Text = da.GetValue(4).ToString();
                    txtDireccion.Text = da.GetValue(5).ToString();
                    valorIndex = Convert.ToInt32(da.GetValue(6).ToString());
                    txtFechaNac.Text = da.GetValue(7).ToString();
                    txtFechaIng.Text = da.GetValue(8).ToString();
                    txtSalario.Text = da.GetValue(9).ToString();
                    txtRuta.Text = da.GetValue(10).ToString();
                    PBEmpleado.Load(da.GetValue(10).ToString());
                    cmbCargo.SelectedValue = valorIndex;
            }
            else
            {
                txtNombre.Clear();
                txtTelefono.Clear();
                txtCorreo.Clear();
                txtDireccion.Clear();
                txtSalario.Clear();
                txtRuta.Clear();
                txtFechaNac.Value = DateTime.Now;
                txtFechaIng.Value = DateTime.Now;
                PBEmpleado.Load(@"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg");
                //PBEmpleado.Image = null;
            }
            

           

            con.Close();
        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }


        //interface IAddItem
        //{
        //    void AddNewItem(DataGridViewRow row);
        //}
    } 
}
