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
    public partial class FMBodega : Form
    {
        public FMBodega()
        {
            InitializeComponent();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMaximizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnRestaurar.Visible = true;
            btnMaximizar.Visible = false;
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void btnRestaurar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnRestaurar.Visible = false;
            btnMaximizar.Visible = true;
        }

        private void AbrirFormProveedores(object FormCierre)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fCierre = FormCierre as Form;
            fCierre.TopLevel = false;
            fCierre.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fCierre);
            this.PContenedor.Tag = fCierre;
            fCierre.Show();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            AbrirFormProveedores(new MBodega.FProveedores());
        }
    }
}
