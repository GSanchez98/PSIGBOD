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
    public partial class FinicioSesion : Form
    {
        public FinicioSesion()
        {
            InitializeComponent();
        }

        private void FinicioSesion_Load(object sender, EventArgs e)
        {
            ConexionBD conexion = new();
            conexion.Abrir();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            //***********************************************************************************************************************************//
            // NOTA: SI INGRESA EL USUARIO Y CLAVE CORRECTA LLAMA AL MENU PRINCIPAL.
            //***********************************************************************************************************************************//
            verificarUsuario(txtLogin.Text, txtPass.Text);
            //FMenuInicial menui = new();
            //menui.Show();
            //Variables.idUsuario = 1;
        }

        private void verificarUsuario(string usuario, string clave)
        {
            string claveEncriptada = encriptarCadena(clave);
            ConexionBD conexion = new();
            conexion.Abrir();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Usuarios.Usuarios WHERE acceso_Usuario = @nombre AND clave_Usuario = @clave", conexion.conectarBD);
            cmd.Parameters.AddWithValue("@nombre", usuario);
            cmd.Parameters.AddWithValue("@clave", claveEncriptada);
            SqlDataReader da = cmd.ExecuteReader();

            if (da.Read())
            {
                Variables.idUsuario = Convert.ToInt32(da.GetValue(0).ToString());
                FMenuInicial menui = new();
                menui.Show();
            }
            else
            {
                Variables.idUsuario = 0;
                MessageBox.Show("Usuario o contraseña inválidos");
                txtLogin.Clear();
                txtPass.Clear();
            }
            conexion.Cerrar();
        }

        string encriptarCadena(string cadena)
        {
            string resultado = string.Empty;
            Byte[] encriptar = System.Text.Encoding.Unicode.GetBytes(cadena);
            resultado = Convert.ToBase64String(encriptar);
            return resultado;
        }


        string desencriptar(string cadena)
        {
            string resultado = string.Empty;
            Byte[] desencriptar = Convert.FromBase64String(cadena);
            resultado = System.Text.Encoding.Unicode.GetString(desencriptar);
            return resultado;
        }

        private void Contenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
