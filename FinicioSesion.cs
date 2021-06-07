using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            FMenuInicial menui = new();
            menui.Show();
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
