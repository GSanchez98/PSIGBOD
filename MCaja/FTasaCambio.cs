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

namespace SIGBOD.MCaja
{
    public partial class FTasaCambio : Form
    {
        public FTasaCambio()
        {
            InitializeComponent();
        }

        private void CargarTasaActual()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            // GIMENA: Esta parte del codigo se encarga de mostrar los datos del ultimo registro asignado a CAI
            string cadena = "SELECT cambio_moneda FROM Caja.Monedas WHERE id_moneda = 2";
            SqlCommand comando = new(cadena, conexion.conectarBD);
            SqlDataReader Lectura = comando.ExecuteReader();

            if (Lectura.Read())
            {
                decimal valdec = Convert.ToDecimal(Lectura.GetValue(0).ToString());
                txtValorRegistrado.Text = valdec.ToString("N2");
            }
            else
            {
                txtValorRegistrado.Clear();
                txtFechaNac.Value = DateTime.Now;
                cmbMoneda.SelectedIndex = 0;
            }
            conexion.Cerrar();
        }
        private void Restablecer(int x)
        {
            if (x == 1)
            {
                txtNuevoMonto.Text = "";
                txtNuevoMonto.Select();

                txtNuevoMonto.Enabled = true;

                btnNuevo.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                CargarTasaActual();

                txtNuevoMonto.Text = "0.00"; 

                txtNuevoMonto.Enabled = false;

                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                CargarTasaActual();

                txtNuevoMonto.Text = "0.00";

                txtNuevoMonto.Enabled = false;

                btnNuevo.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        private void FTasaCambio_Load(object sender, EventArgs e)
        {
            CargarTasaActual();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            Restablecer(1);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // GIMENA: Primero guardamos los datos en la tabla tasa_cambio
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena = "INSERT INTO Caja.Tasa_cambio(id_moneda_Tasacambio ,valor_Tasacambio ,estado ,agrego_Tasacambio ,fecha_agrego_Tasacambio) VALUES (@id_moneda_Tasacambio ,@valor_Tasacambio ,@estado ,@agrego_Tasacambio ,@fecha_agrego_Tasacambio)";
            try
            {
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                comando.Parameters.AddWithValue("@id_moneda_Tasacambio", 2); // identificador designado para dolares ($)
                comando.Parameters.AddWithValue("@valor_Tasacambio", txtNuevoMonto.Text);
                comando.Parameters.AddWithValue("@estado", 1); // Sería el regisro mas actual por lo tanto esta activo (1)
                comando.Parameters.AddWithValue("@agrego_Tasacambio", 0);
                comando.Parameters.AddWithValue("@fecha_agrego_Tasacambio", DateTime.Today);

                // GIMENA: Al crear un nuevo registro se inhabilita el anterior.
                string desfaseTasaCambio = "UPDATE Caja.Tasa_cambio SET estado = 0 WHERE id_Tasacambio = (Select MAX(id_Tasacambio) FROM SIGBOD.Caja.Tasa_cambio)";
                try
                {
                    SqlCommand cmdDesfaseTasaCambio = new SqlCommand(desfaseTasaCambio, conexion.conectarBD);
                    cmdDesfaseTasaCambio.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

                comando.ExecuteNonQuery();
                
                conexion.Cerrar();
                // Gimena: Actualizamos la tabla de monedas para colocar el valor actual                
                ActualizacionMonedaDolar();
                Restablecer(2);                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void desfaseTasaCambio()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
                
            conexion.Cerrar();
        }

        private void ActualizacionMonedaDolar()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena = "UPDATE Caja.Monedas SET cambio_moneda=@cambio_moneda, agrego_moneda=@agrego_moneda ,fecha_agrego_moneda=@fecha_agrego_moneda WHERE id_moneda = 2";
            try
            {
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                comando.Parameters.AddWithValue("@cambio_moneda", txtNuevoMonto.Text);
                comando.Parameters.AddWithValue("@agrego_moneda", 0);
                comando.Parameters.AddWithValue("@fecha_agrego_moneda", DateTime.Today);
                comando.ExecuteNonQuery();
                conexion.Cerrar();
                Restablecer(2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Reestablecer(Cancelar)
            Restablecer(4);
            this.Close();
        }
    }
}
