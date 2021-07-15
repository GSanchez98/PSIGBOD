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

namespace SIGBOD.MCaja
{
    public partial class FCierreCaja : Form
    {
        public FCierreCaja()
        {
            InitializeComponent();
        }

        public int agregar_cierre;
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FCierreCaja_Load(object sender, EventArgs e)
        {
            verificarPermisos();
        }

        private void verificarPermisos()
        {
            int idUsuarioActivo;
            idUsuarioActivo = Variables.idUsuario;
            ConexionBD conexion = new();
            conexion.Abrir();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos.acceso_caja WHERE id_Usuario = @usuario", conexion.conectarBD);
            cmd.Parameters.AddWithValue("@usuario", idUsuarioActivo);
            SqlDataReader da = cmd.ExecuteReader();

            if (da.Read())
            {
                agregar_cierre = Convert.ToInt32(da.GetValue(2).ToString());
            }
            else
            {
                //
            }

            conexion.Cerrar();

            if (agregar_cierre > 0)
            {
                btnCerrarCaja.Enabled = true;
            }
            else
            {
                btnCerrarCaja.Enabled = false;
            }
        }

    }
}
