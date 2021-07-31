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
            int usuarioActivo = Variables.idUsuario;
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Compras.Proveedores(nombre_Proveedor,telefono_Proveedor,direccion_Proveedor,correo_Proveedor,estado_Proveedor,agrego_Proveedor,fecha_agrego_Proveedor,rtn_Proveedor)VALUES(@nombre_Proveedor,@telefono_Proveedor,@direccion_Proveedor,@correo_Proveedor,@estado_Proveedor,@agrego_Proveedor,@fecha_agrego_Proveedor,@rtn_Proveedor)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //FALTA LA IMAGEN

                    comando.Parameters.AddWithValue("@rtn_Proveedor", txtRtn.Text);
                    comando.Parameters.AddWithValue("@nombre_Proveedor", txtNombre.Text);
                    comando.Parameters.AddWithValue("@telefono_Proveedor", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@direccion_Proveedor", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@correo_Proveedor", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@estado_Proveedor", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Proveedor", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Proveedor", usuarioActivo);

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
                string cadena = "UPDATE Compras.Proveedores SET nombre_Proveedor=@nombre_Proveedor,telefono_Proveedor=@telefono_Proveedor,direccion_Proveedor=@direccion_Proveedor,correo_Proveedor=@correo_Proveedor,estado_Proveedor=@estado_Proveedor,agrego_Proveedor=@agrego_Proveedor,fecha_agrego_Proveedor=@fecha_agrego_Proveedor,rtn_Proveedor=@rtn_Proveedor WHERE id_Proveedor=" + txtId.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@rtn_Proveedor", txtRtn.Text);
                    comando.Parameters.AddWithValue("@nombre_Proveedor", txtNombre.Text);
                    comando.Parameters.AddWithValue("@telefono_Proveedor", txtTelefono.Text);
                    comando.Parameters.AddWithValue("@direccion_Proveedor", txtDireccion.Text);
                    comando.Parameters.AddWithValue("@correo_Proveedor", txtCorreo.Text);
                    comando.Parameters.AddWithValue("@estado_Proveedor", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Proveedor", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Proveedor", usuarioActivo);
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
                    string cadena = "UPDATE Compras.Proveedores SET estado_Proveedor=@estado_Proveedor WHERE id_Proveedor=" + txtId.Text;
                    try
                    {
                        SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                        if (btnEstado.Text == "Habilitar")
                        {
                            comando.Parameters.AddWithValue("@estado_Proveedor", 1);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@estado_Proveedor", 0);
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
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

        private void btnEstado_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string cadena = "DELETE FROM Compras.Proveedores WHERE id_Proveedor=" + txtId.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
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
                Restablecer(4);
                valor = 0;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            
        }
    }
}
