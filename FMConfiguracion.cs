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
    public partial class FMConfiguracion : Form
    {
        public FMConfiguracion()
        {
            InitializeComponent();
        }

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

        private void btnEmpleados_Click(object sender, EventArgs e)
        {
            AbrirFormEmpleados(new FEmpleados());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormCargos(new FCargos());
        }
    }
}
