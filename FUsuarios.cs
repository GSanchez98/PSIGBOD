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
                txtAcceso.Enabled = true;
                txtxClave.Enabled = true;
                cmbEmpleado.Enabled = true;

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
                PBEmpleado.Load(@"C:\Users\Gmn\source\repos\SIGBOD\SIGBOD\imagenes\Perfil.png");

                txtAcceso.Enabled = false;
                txtxClave.Enabled = false;
                cmbEmpleado.Enabled = false;

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
            cmbEmpleado.Text = "Seleccione empleado";
            valor = 0;
        }
    }
}
