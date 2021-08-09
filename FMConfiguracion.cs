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

namespace SIGBOD
{
    public partial class FMConfiguracion : Form
    {
        public FMConfiguracion()
        {
            InitializeComponent();
        }

        //Variables
        public int ver_usuarios, agregar_usuarios, editar_usuarios, inhabilitar_usuarios;
        public int ver_empleados, agregar_empleados, editar_empleados, inhabilitar_empleados;

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRes.Visible = true;
            btnMax.Visible = false;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            btnRes.Visible = false;
            btnMax.Visible = true;
        }

        private void btnRes_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRes.Visible = false;
            btnMax.Visible = true;
        }
        // Llamado al formulario dentro del panel.
        private void AbrirFormEmpleados(object FormEmpleados)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fEmpl = FormEmpleados as Form;
            fEmpl.TopLevel = false;
            fEmpl.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fEmpl);
            this.PContenedor.Tag = fEmpl;
            fEmpl.Show();
        }

        private void AbrirFormCargos(object FormCargos)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fCargos = FormCargos as Form;
            fCargos.TopLevel = false;
            fCargos.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fCargos);
            this.PContenedor.Tag = fCargos;
            fCargos.Show();
        }

        private void AbrirFormUsuarios(object FormUsuarios)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fUsuarios = FormUsuarios as Form;
            fUsuarios.TopLevel = false;
            fUsuarios.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fUsuarios);
            this.PContenedor.Tag = fUsuarios;
            fUsuarios.Show();
        }

        private void AbrirFormCai(object FormCai)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fCai = FormCai as Form;
            fCai.TopLevel = false;
            fCai.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fCai);
            this.PContenedor.Tag = fCai;
            fCai.Show();
        }

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormEmpleados(new FEmpleados());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormCargos(new FCargos());
        }

        private void PContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            AbrirFormUsuarios(new FUsuarios());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormCai(new MConfiguracion.FCai());
        }

        private void FMConfiguracion_Load(object sender, EventArgs e)
        {
            verificarPermisosUsuarios();
            verificarEmpleados();
        }

        private void verificarPermisosUsuarios()
        {
            int idUsuarioActivo;
            idUsuarioActivo = Variables.idUsuario;
            ConexionBD conexion = new();
            conexion.Abrir();
            // Usuarios
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos.acceso_usuarios WHERE id_Usuario = @usuario", conexion.conectarBD);
            cmd.Parameters.AddWithValue("@usuario", idUsuarioActivo);
            SqlDataReader da = cmd.ExecuteReader();

            if (da.Read())
            {
                ver_usuarios = Convert.ToInt32(da.GetValue(1).ToString());
                agregar_usuarios = Convert.ToInt32(da.GetValue(2).ToString());
                editar_usuarios = Convert.ToInt32(da.GetValue(3).ToString());
                inhabilitar_usuarios = Convert.ToInt32(da.GetValue(4).ToString());
            }
            else
            {
                //
            }
            conexion.Cerrar();
            if (ver_usuarios == 1 || agregar_usuarios == 1 || editar_usuarios == 1 || inhabilitar_usuarios == 1)
            {
                button6.Enabled = true;
            }
            else
            {
                button6.Enabled = false;
            }
            
        }

        private void verificarEmpleados()
        {
            int idUsuarioActivo;
            idUsuarioActivo = Variables.idUsuario;
            ConexionBD conexion = new();
            conexion.Abrir();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos.acceso_empleados WHERE id_Usuario = @usuario", conexion.conectarBD);
            cmd.Parameters.AddWithValue("@usuario", idUsuarioActivo);
            SqlDataReader da = cmd.ExecuteReader();

            if (da.Read())
            {
                ver_empleados = Convert.ToInt32(da.GetValue(1).ToString());
                agregar_empleados = Convert.ToInt32(da.GetValue(2).ToString());
                editar_empleados = Convert.ToInt32(da.GetValue(3).ToString());
                inhabilitar_empleados = Convert.ToInt32(da.GetValue(4).ToString());
            }
            else
            {
                //
            }

            conexion.Cerrar();

            if (ver_empleados == 1 || agregar_empleados == 1 || editar_empleados == 1 || inhabilitar_empleados == 1)
            {
                btnEmpleados.Enabled = true;
            }
            else
            {
                btnEmpleados.Enabled = false;
            }
        }
    }
}
