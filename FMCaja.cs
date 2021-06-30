﻿using System;
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
    public partial class FMCaja : Form
    {
        public FMCaja()
        {
            InitializeComponent();
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

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AbrirFormApertura(object FormCai)
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

        private void AbrirFormGastos(object FormGastos)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fGastos = FormGastos as Form;
            fGastos.TopLevel = false;
            fGastos.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fGastos);
            this.PContenedor.Tag = fGastos;
            fGastos.Show();
        }

        private void AbrirFormTasaCambioDolar(object FormTasa)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fTasa = FormTasa as Form;
            fTasa.TopLevel = false;
            fTasa.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fTasa);
            this.PContenedor.Tag = fTasa;
            fTasa.Show();
        }

        private void AbrirFormCierreCaja(object FormCierre)
        {
            if (this.PContenedor.Controls.Count > 0)
                this.PContenedor.Controls.RemoveAt(0);
            Form fCierre= FormCierre as Form;
            fCierre.TopLevel = false;
            fCierre.Dock = DockStyle.Fill;
            this.PContenedor.Controls.Add(fCierre);
            this.PContenedor.Tag = fCierre;
            fCierre.Show();
        }
        private void btnApertura_Click(object sender, EventArgs e)
        {
            AbrirFormApertura(new MCaja.FArqueoCaja());
        }

        private void Contenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnGastos_Click(object sender, EventArgs e)
        {
            AbrirFormGastos(new MCaja.FGastos());
        }

        private void btnTasaCambio_Click(object sender, EventArgs e)
        {
            AbrirFormTasaCambioDolar(new MCaja.FTasaCambio());
        }

        private void btnCierre_Click(object sender, EventArgs e)
        {
            AbrirFormCierreCaja(new MCaja.FCierreCaja());
        }
    }
}
