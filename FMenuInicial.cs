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
    public partial class FMenuInicial : Form
    {
        public FMenuInicial()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // GIMENA: Se hace el llamado al formulario de cargos
            FCargos MantenimientoCargos = new();
            MantenimientoCargos.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // GIMENA: Se hace el llamado al formulario de cargos
            FEmpleados MantenimientoEmp = new();
            MantenimientoEmp.Show();
        }

        private void btnMCaja_Click(object sender, EventArgs e)
        {
            FMCaja menucaja = new();
            menucaja.Show();
        }

        private void btnMBodega_Click(object sender, EventArgs e)
        {
            FMBodega menubodega = new();
            menubodega.Show();
        }

        private void btnMCajero_Click(object sender, EventArgs e)
        {
            FMCajero menucajero = new();
            menucajero.Show();
        }

        private void btnMConfiguraciones_Click(object sender, EventArgs e)
        {
            FMConfiguracion menuconfiguracion = new();
            menuconfiguracion.Show();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbMConfiguraciones_Click(object sender, EventArgs e)
        {
            FMConfiguracion menuconfiguracion = new();
            menuconfiguracion.Show();
        }

        private void lbMCajero_Click(object sender, EventArgs e)
        {
            FMCajero menucajero = new();
            menucajero.Show();
        }

        private void lbMBodega_Click(object sender, EventArgs e)
        {
            FMBodega menubodega = new();
            menubodega.Show();
        }

        private void lbMCaja_Click(object sender, EventArgs e)
        {
            FMCaja menucaja = new();
            menucaja.Show();
        }

        private void FMenuInicial_Load(object sender, EventArgs e)
        {
            //MessageBox.Show(Variables.idUsuario.ToString());
        }
    }
}
