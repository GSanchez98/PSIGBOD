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
    public partial class FUsuarios : Form
    {
        public FUsuarios()
        {
            InitializeComponent();
        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
        }

        private void Restablecer(int x)
        {
            if (x == 1)
            {
                // GIMENA: Opcion cuando se trata de un nuevo registro
                txtIdUsuario.Text = "";
                txtAcceso.Text = "";
                txtxClave.Text = "";
                cmbEmpleado.Text = "Seleccione empleado";

                txtAcceso.Enabled = true;
                txtxClave.Enabled = true;
                cmbEmpleado.Enabled = true;
                PnAccesos.Enabled = true;


                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = false;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtAcceso.Enabled = false;
                txtxClave.Enabled = false;
                cmbEmpleado.Enabled = false;
                PnAccesos.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
                btnEstado.Text = "Habilitar";
                PBEmpleado.Load(@"D:\Hesler Alvarado\Documents\PSIGBOD\imagenes\Perfil.png");
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtAcceso.Enabled = true;
                txtxClave.Enabled = true;
                cmbEmpleado.Enabled = true;
                PnAccesos.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtIdUsuario.Text = "";
                txtAcceso.Text = "";
                txtxClave.Text = "";
                cmbEmpleado.Text = "Seleccione empleado";
                PBEmpleado.Load(@"D:\Hesler Alvarado\Documents\PSIGBOD\imagenes\Perfil.png");

                txtAcceso.Enabled = false;
                txtxClave.Enabled = false;
                cmbEmpleado.Enabled = false;
                PnAccesos.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
            }
        }

        //COMBO PARA EL LISTADO DE LOS EMPLEADOS
        public void llenacombobox()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_Empleado, nombre_Empleado FROM Usuarios.Empleados", conexion.conectarBD);
            //se indica el nombre de la tabla
            da.Fill(ds, "Usuarios.Empleados");
            cmbEmpleado.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbEmpleado.ValueMember = "id_Empleado";
            cmbEmpleado.DisplayMember = "nombre_Empleado";
        }

        private void FUsuarios_Load(object sender, EventArgs e)
        {
            llenacombobox();//llenamos eel combobox perteneciente a los usuarios
            //cmbEmpleado.Text = "Seleccione empleado";
            valor = 0;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {                
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadenaUsuarios = "INSERT INTO Usuarios.Usuarios(acceso_Usuario,clave_Usuario,id_empleado_Usuario,estado_Usuario,fecha_agrego_Usuario,agrego_Usuario)VALUES(@acceso_Usuario,@clave_Usuario,@id_empleado_Usuario,@estado_Usuario,@fecha_agrego_Usuario,@agrego_Usuario)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadenaUsuarios, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@acceso_Usuario", txtAcceso.Text);
                    comando.Parameters.AddWithValue("@clave_Usuario", txtxClave.Text);
                    comando.Parameters.AddWithValue("@id_empleado_Usuario", Convert.ToInt32(cmbEmpleado.SelectedValue));
                    comando.Parameters.AddWithValue("@estado_Usuario", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Usuario", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Usuario", 0);

                    comando.ExecuteNonQuery();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    //Cargar(1);
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

                string cadenaAccesoUsuario = "INSERT INTO Permisos.acceso_usuarios(ver_usuarios,agregar_usuarios,editar_usuarios,inhabilitar_usuarios,id_Usuario)VALUES(@ver_usuarios,@agregar_usuarios,@editar_usuarios,@inhabilitar_usuarios,@id_Usuario)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadenaAccesoUsuario, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@ver_usuarios", chVerUsuarios.Checked);
                    comando.Parameters.AddWithValue("@agregar_usuarios", chAgUsuarios.Checked);
                    comando.Parameters.AddWithValue("@editar_usuarios", chModUsuarios.Checked);
                    comando.Parameters.AddWithValue("@inhabilitar_usuarios", chEliUsuarios.Checked);
                    comando.Parameters.AddWithValue("@fecha_agrego_Usuario", DateTime.Now);

                    // PENDIENTE
                    comando.Parameters.AddWithValue("@id_Usuario", 1);

                    comando.ExecuteNonQuery();
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
                string cadena = "UPDATE Usuarios.Empleados SET acceso_Usuario=@acceso_Usuario,clave_Usuario=@clave_Usuario,id_empleado_Usuario=@id_empleado_Usuario,estado_Usuario=@estado_Usuario,fecha_agrego_Usuario=@fecha_agrego_Usuario,agrego_Usuario=@agrego_Usuario WHERE id_Usuario=" + txtIdUsuario.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@acceso_Usuario", txtAcceso.Text);
                    comando.Parameters.AddWithValue("@clave_Usuario", txtxClave.Text);
                    comando.Parameters.AddWithValue("@id_empleado_Usuario", Convert.ToInt32(cmbEmpleado.SelectedValue));
                    comando.Parameters.AddWithValue("@estado_Usuario", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Usuario", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Usuario", 0);
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
                    string cadena = "UPDATE Usuarios.Usuarios SET estado_Usuario=@estado_Usuario WHERE id_Usuario=" + txtIdUsuario.Text;
                    try
                    {
                        SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                        if (btnEstado.Text == "Habilitar")
                        {
                            comando.Parameters.AddWithValue("@estado_Usuario", 1);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("@estado_Usuario", 0);
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdUsuario.Text == "")
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
            AgrEdit(3);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
            this.Close();
        }

        private void txtxClave_Leave(object sender, EventArgs e)
        {
            idUsuario(txtAcceso.Text);
        }

        private void idUsuario (string acceso)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-KPH7FI1; initial Catalog=SIGBOD; Integrated Security=True");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT id_Usuario FROM Usuarios.Usuarios WHERE acceso_Usuario = @acceso", con);
            cmd.Parameters.AddWithValue("@acceso", acceso);
            SqlDataReader da = cmd.ExecuteReader();
            while (da.Read())
            {
                txtIdUsuario.Text = da.GetValue(0).ToString();
            }
            con.Close();
        }

        private void btnLista_Click(object sender, EventArgs e)
        {
            FListas listado = new FListas(3);
            AddOwnedForm(listado);
            listado.Show();
        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
