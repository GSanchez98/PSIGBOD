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
using System.Net;
using System.Net.NetworkInformation;

namespace SIGBOD.MCaja
{
    public partial class FDenominaciones : Form
    {
        public FDenominaciones()
        {
            InitializeComponent();
        }
        


        private void CargarDenominaciones()
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            // GIMENA: llenando las denominaciones
            string cadenaL = "Select id_Denominacion as No, tipo_Denominacion as Tipo, denominacion_Denominacion as Denominacion, valor_Denominacion as Valor from Caja.Denominaciones where idmoneda_Denominacion = 1";
            SqlCommand comandoL = new(cadenaL, conexion.conectarBD);
            SqlDataAdapter adaptadorL = new SqlDataAdapter(comandoL);
            DataTable tablaL = new DataTable();
            adaptadorL.Fill(tablaL);
            dgDenomLps.DataSource = tablaL;

            string cadenaD = "Select id_Denominacion as No, tipo_Denominacion as Tipo, denominacion_Denominacion as Denominacion, valor_Denominacion as Valor from Caja.Denominaciones where idmoneda_Denominacion = 2";
            SqlCommand comandoD = new(cadenaD, conexion.conectarBD);
            SqlDataAdapter adaptadorD = new SqlDataAdapter(comandoD);
            DataTable tablaD = new DataTable();
            adaptadorD.Fill(tablaD);
            dgDenomD.DataSource = tablaD;

            conexion.Cerrar();
        }

        private void FDenominaciones_Load(object sender, EventArgs e)
        {
            // GIMENA: Cargamos valores de las denominaciones
            CargarDenominaciones();
        }

        private void TotalL(object sender, DataGridViewCellEventArgs e)
        {
            // GIMENA: Finción que permite realizar la multiplicación entre los valores de las denominaciones.
            double total = 0;
            foreach (DataGridViewRow row in dgDenomLps.Rows)
            {
                total += Convert.ToDouble(row.Cells["Cantid"].Value) * Convert.ToDouble(row.Cells["Val"].Value);
            }
            txtL.Text= total.ToString("0.00");
        }

        private void TotalD(object sender, DataGridViewCellEventArgs e)
        {
            // GIMENA: Finción que permite realizar la multiplicación entre los valores de las denominaciones.
            double total = 0;
            foreach (DataGridViewRow row in dgDenomD.Rows)
            {
                total += Convert.ToDouble(row.Cells["Cant"].Value) * Convert.ToDouble(row.Cells["Valo"].Value);
            }
            txtD.Text = total.ToString("0.00");
        }

        private void pnContenedor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnApertura_Click(object sender, EventArgs e)
        {
            // GIMENA: Funcion para obtener la IP de una Máquina
            IPHostEntry host;
            string localIP = "";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            // GIMENA: Almacenar Aperturar Caja
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena = "INSERT INTO Caja.AperturaCaja(estacion_Aperturacaja,total_lempiras_Aperturacaja,total_dolares_Aperturacaja,agrego_Aperturacaja,fechahora_Aperturacaja,cierre_Aperturacaja) VALUES (@estacion_Aperturacaja,@total_lempiras_Aperturacaja,@total_dolares_Aperturacaja,@agrego_Aperturacaja,@fechahora_Aperturacaja,@cierre_Aperturacaja)";
            try
            {
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                comando.Parameters.AddWithValue("@estacion_Aperturacaja", localIP);
                comando.Parameters.AddWithValue("@total_lempiras_Aperturacaja", Convert.ToDecimal(txtL.Text));
                comando.Parameters.AddWithValue("@total_dolares_Aperturacaja", Convert.ToDecimal(txtD.Text));
                comando.Parameters.AddWithValue("@agrego_Aperturacaja", 0);
                comando.Parameters.AddWithValue("@fechahora_Aperturacaja", DateTime.Today);
                // GIMENA: Este estado cambiara a 0 una vez que la caja sea cerrada.
                comando.Parameters.AddWithValue("@cierre_Aperturacaja", 1);
                comando.ExecuteNonQuery();                

                // GIMENA: Almacenar Detalle de Aperturar Caja
                ObtenerIdApertura(txtUsuario.Text);
                // GIMENA: Lectura para DG de Lempiras
                foreach (DataGridViewRow row in dgDenomLps.Rows)
                {
                    string cadenaDet = "INSERT INTO Caja.DetalleApertura(id_apertura_detalleApertura,id_denominacion_detalleApertura,cantidad_detalleApertura) VALUES (@id_apertura_detalleApertura,@id_denominacion_detalleApertura,@cantidad_detalleApertura)";
                    try
                    {
                        SqlCommand comandoDet = new SqlCommand(cadenaDet, conexion.conectarBD);
                        comandoDet.Parameters.AddWithValue("@id_apertura_detalleApertura", txtIdApertura.Text);
                        comandoDet.Parameters.AddWithValue("@id_denominacion_detalleApertura", Convert.ToString(row.Cells[1].Value));
                        comandoDet.Parameters.AddWithValue("@cantidad_detalleApertura", Convert.ToString(row.Cells[0].Value));
                        comandoDet.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                    }
                }
                // GIMENA: Lectura para DG de Dolares
                foreach (DataGridViewRow row in dgDenomD.Rows)
                {
                    string cadenaDet = "INSERT INTO Caja.DetalleApertura(id_apertura_detalleApertura,id_denominacion_detalleApertura,cantidad_detalleApertura) VALUES (@id_apertura_detalleApertura,@id_denominacion_detalleApertura,@cantidad_detalleApertura)";
                    try
                    {
                        SqlCommand comandoDet = new SqlCommand(cadenaDet, conexion.conectarBD);
                        comandoDet.Parameters.AddWithValue("@id_apertura_detalleApertura", txtIdApertura.Text);
                        comandoDet.Parameters.AddWithValue("@id_denominacion_detalleApertura", Convert.ToString(row.Cells[1].Value));
                        comandoDet.Parameters.AddWithValue("@cantidad_detalleApertura", Convert.ToString(row.Cells[0].Value));
                        comandoDet.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                    }
                }
                conexion.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void ObtenerIdApertura(string agrego)
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena = "SELECT MAX(id_Aperturacaja) FROM Caja.AperturaCaja WHERE agrego_Aperturacaja = @agrego";
            try
            {
                SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                comando.Parameters.AddWithValue("@agrego", agrego);
                SqlDataReader da = comando.ExecuteReader();
                while (da.Read())
                {
                    txtIdApertura.Text = da.GetValue(0).ToString();
                }
                conexion.Cerrar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
        }

        private void tbMontosApertura_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
