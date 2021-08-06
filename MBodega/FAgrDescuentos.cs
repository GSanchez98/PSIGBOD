using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SIGBOD.MBodega
{
    public partial class FAgrDescuentos : Form
    {
        public FAgrDescuentos()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (txtPorcentaje.Text != "")
            {
                // GIMENA: VALOR PORCENTUAL
                string valorig = txtPorcentaje.Text;
                decimal valdec = Convert.ToDecimal(valorig) / 100;
                txtVPorcentual.Text = valdec.ToString("N2");

                // GIMENA: ETIQUETA
                txtEPorcentual.Text = txtPorcentaje.Text;
            }
            else
            {
                // GIMENA: VALOR PORCENTUAL
                txtVPorcentual.Text = "";

                // GIMENA: ETIQUETA
                txtEPorcentual.Text = "";
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Cerramos la ventana actual para proceguir con los porcentajes.
            this.Dispose();
            this.Close();
        }

        //SIN DATOS BD
        public void llenarCombo()
        {
            FDescuentos primero = Owner as FDescuentos;
            primero.cmbDesc.Items.Add(txtEPorcentual.Text);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // GIMENA: llamando al usuario responsable.
            int usuarioActivo = Variables.idUsuario;

            // GIMENA: Insertando los valores a parametros generales
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena = "INSERT INTO General.Parametros_Generales(tipo_Parametro ,descripcion_Parametro , valor_decimal_Parametro, fecha_agrego_Parametro ,agrego_Parametro, valor_Parametro) VALUES (@tipo_Parametro ,@descripcion_Parametro ,@valor_decimal_Parametro, @fecha_agrego_Parametro ,@agrego_Parametro, @valor_Parametro)";
            try
            {
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                comando.Parameters.AddWithValue("@tipo_Parametro", "PORCENTAJE");
                comando.Parameters.AddWithValue("@descripcion_Parametro", txtEPorcentual.Text);
                comando.Parameters.AddWithValue("@valor_decimal_Parametro", Convert.ToDecimal(txtVPorcentual.Text));

                comando.Parameters.AddWithValue("@valor_Parametro", 0);
                comando.Parameters.AddWithValue("@fecha_agrego_Parametro", DateTime.Today);
                comando.Parameters.AddWithValue("@agrego_Parametro", usuarioActivo);
                comando.ExecuteNonQuery();
                conexion.Cerrar();


                // GIMENA: limpiando los valores para reiniciar.
                txtPorcentaje.Text = "";
                txtVPorcentual.Text = "";
                txtEPorcentual.Text = "";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            llenarComboBD();
            // GIMENA: Cerramos la ventana actual para proceguir con los porcentajes.
            this.Dispose();
            this.Close();
        }

        public void llenarComboBD()
        {
            FDescuentos primero = Owner as FDescuentos;
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet dsd = new DataSet();
            SqlDataAdapter dad = new SqlDataAdapter("SELECT id_Parametro, valor_decimal_Parametro, descripcion_Parametro FROM SIGBOD.General.Parametros_Generales where tipo_Parametro = 'PORCENTAJE'", conexion.conectarBD);
            //se indica el nombre de la tabla
            dad.Fill(dsd, "General.Parametros_Generales");
            primero.cmbDesc.DataSource = dsd.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            primero.cmbDesc.ValueMember = "id_Parametro";
            primero.cmbDesc.DisplayMember = "descripcion_Parametro";
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
