using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SIGBOD.MBodega
{
    public partial class FProveedores : Form
    {
        public FProveedores()
        {
            InitializeComponent();
        }

        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;
        private string Estado = "";
        public string imagen = "";


        // GIMENA: FUNCION QUE PERIMITE AGREGAR O EDITAR UN REGISTRO
        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Compras.Proveedores(nombre_Proveedor,telefono_Proveedor,direccion_Proveedor,estado_Proveedor,agrego_Proveedor,fecha_agrego_Proveedor,rtn_Proveedor)VALUES(@nombre_Proveedor,@telefono_Proveedor,@direccion_Proveedor,@estado_Proveedor,@agrego_Proveedor,@fecha_agrego_Proveedor,@rtn_Proveedor)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //FALTA LA IMAGEN

                    comando.Parameters.AddWithValue("@identidad_Empleado", txtId.Text);
                    comando.Parameters.AddWithValue("@rtn_Proveedor", txtRtn.Text);
                    comando.Parameters.AddWithValue("@nombre_Proveedor", txtNombre.Text);
                    comando.Parameters.AddWithValue("@telefono_Proveedor", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@direccion_Proveedor", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@correo_Empleado", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@estado_Empleado", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Empleado", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Empleado", usuarioActivo);

                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    //Cargar(1);
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

                    ////Inicio: Proceso para almacenar la imagen
                    //saveFileDialog1 = new SaveFileDialog();

                    //// Identificar formatos permitidos
                    //saveFileDialog1.Filter = "Imagenes JPG,PNG|*.jpg;*.png";

                    //// Directorio donde se almacenan las imagenes
                    ////saveFileDialog1.InitialDirectory = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod";
                    //saveFileDialog1.InitialDirectory = @"D:\Hesler Alvarado\Documents\ImagenesSigbod\Empleados";

                    ////obtine el numero de identidad del empleado y lo muestra en la ventana de almacenamiento.
                    //saveFileDialog1.FileName = txtIdentidad.Text;

                    //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    //{
                    //    MemoryStream ms = new MemoryStream();
                    //    PBEmpleado.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    //    txtRuta.Text = saveFileDialog1.FileName;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("No ha almacenado ninguna imagen para este registro", "ADVERTENCIA", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    //}

                    //// si no selecciona ninguna imagen se proporciona una imagen por defecto.
                    //if (txtRuta.Text == "")
                    //{
                    //    //txtRuta.Text = @"C:\Users\Public\Pictures\Sigbod\Empleados_Sigbod\Perfil.jpg";
                    //    txtRuta.Text = @"D:\Hesler Alvarado\Documents\PSIGBOD\imagenes\Perfil.png";
                    //}

                    DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
                    DateTime fechaIngreso = Convert.ToDateTime(txtFechaIng.Text);

                    comando.Parameters.AddWithValue("@identidad_Empleado", txtRtn.Text);
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
                    //Cargar(1);
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
                        //Cargar(1);
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
                    //Cargar(1);
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
                txtRtn.Text = "";
                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                txtDireccion.Text = "";

                txtRtn.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtDireccion.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = false;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtRtn.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                txtDireccion.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
                btnEstado.Text = "Habilitar";
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtRtn.Enabled = true;
                txtNombre.Enabled = true;
                txtTelefono.Enabled = true;
                txtCorreo.Enabled = true;
                txtDireccion.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtRtn.Text = "";
                txtNombre.Text = "";
                txtTelefono.Text = "";
                txtCorreo.Text = "";
                txtDireccion.Text = "";

                txtRtn.Enabled = false;
                txtNombre.Enabled = false;
                txtTelefono.Enabled = false;
                txtCorreo.Enabled = false;
                txtDireccion.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
        }
    }
}
