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

namespace SIGBOD.MCaja
{
    public partial class FGastos : Form
    {
        public FGastos()
        {
            InitializeComponent();
        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;
        public int agregar_gastos, editar_gastos;
        private void Restablecer(int x)
        {
            if (x == 1)
            {
                // GIMENA: Opcion cuando se trata de un nuevo registro
                lblSimbolo.Text = "L.";
                cmbMoneda.SelectedIndex = 0;
                txtMonto.Text = "";
                txtDescripconGasto.Text = "";
                txtIdGasto.Text = "";

                txtMonto.Enabled = true;
                txtIdGasto.Enabled = true;
                txtDescripconGasto.Enabled = true;
                cmbMoneda.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtMonto.Enabled = false;
                txtIdGasto.Enabled = false;
                txtDescripconGasto.Enabled = false;
                cmbMoneda.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtMonto.Enabled = true;
                txtIdGasto.Enabled = true;
                txtDescripconGasto.Enabled = true;
                cmbMoneda.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                lblSimbolo.Text = "L.";
                cmbMoneda.SelectedIndex = 0;
                txtMonto.Text = "";
                txtDescripconGasto.Text = "";
                txtIdGasto.Text = "";

                txtMonto.Enabled = false;
                txtIdGasto.Enabled = false;
                txtDescripconGasto.Enabled = false;
                cmbMoneda.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }
        public void llenacombobox()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_moneda ,nombre_moneda FROM Caja.Monedas", conexion.conectarBD);
            //se indica el nombre de la tabla
            da.Fill(ds, "Caja.Monedas");
            cmbMoneda.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbMoneda.ValueMember = "id_moneda";
            cmbMoneda.DisplayMember = "nombre_moneda";
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdGasto.Text == "")
            {
                MessageBox.Show("Seleccione un gasto de la lista para poder modificarlo.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Editar)
                valor = 2;
                Restablecer(3);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        // GIMENA: Funcion que nos permite agregar o editar un registro.
        private void AgrEdit(int x)
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

            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Caja.Gastos(descripcion_gastos,monto_gastos,Estacion_gastos,agrego_gastos,fecha_agrego_gastos,id_moneda) VALUES (@descripcion_gastos,@monto_gastos,@Estacion_gastos,@agrego_gastos,@fecha_agrego_gastos,@id_moneda)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@monto_gastos", txtMonto.Text);
                    comando.Parameters.AddWithValue("@id_moneda", Convert.ToInt32(cmbMoneda.SelectedValue));
                    comando.Parameters.AddWithValue("@descripcion_gastos", txtDescripconGasto.Text);
                    comando.Parameters.AddWithValue("@Estacion_gastos", localIP);
                    comando.Parameters.AddWithValue("@agrego_gastos", 0);
                    comando.Parameters.AddWithValue("@fecha_agrego_gastos", DateTime.Today);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }
            }
            else if (x == 2) // GIMENA: Modificar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "UPDATE Caja.Gastos SET descripcion_gastos=@descripcion_gastos,monto_gastos=@monto_gastos,Estacion_gastos=@Estacion_gastos,agrego_gastos=@agrego_gastos,fecha_agrego_gastos=@fecha_agrego_gastos,id_moneda=@id_moneda WHERE id_gastos=" + txtIdGasto.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    comando.Parameters.AddWithValue("@monto_gastos", txtMonto.Text);
                    comando.Parameters.AddWithValue("@id_moneda", Convert.ToInt32(cmbMoneda.SelectedValue));
                    comando.Parameters.AddWithValue("@descripcion_gastos", txtDescripconGasto.Text);
                    comando.Parameters.AddWithValue("@Estacion_gastos", localIP);
                    comando.Parameters.AddWithValue("@agrego_gastos", 0);
                    comando.Parameters.AddWithValue("@fecha_agrego_gastos", DateTime.Today);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

            }
        }

        private void FGastos_Load(object sender, EventArgs e)
        {
            llenacombobox();
            verificarPermisos();
        }

        private void verificarPermisos()
        {
            int idUsuarioActivo;
            idUsuarioActivo = Variables.idUsuario;
            ConexionBD conexion = new();
            conexion.Abrir();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Permisos.acceso_caja WHERE id_Usuario = @usuario", conexion.conectarBD);
            cmd.Parameters.AddWithValue("@usuario", idUsuarioActivo);
            SqlDataReader da = cmd.ExecuteReader();

            if (da.Read())
            {
                agregar_gastos = Convert.ToInt32(da.GetValue(3).ToString());
                editar_gastos = Convert.ToInt32(da.GetValue(4).ToString());
            }
            else
            {
                //
            }

            conexion.Cerrar();

            if (agregar_gastos > 0)
            {
                btnNuevo.Enabled = true;
            }
            else
            {
                btnNuevo.Enabled = false;
            }
            if (editar_gastos > 0)
            {
                btnEditar.Enabled = true;
            }
            else
            {
                btnEditar.Enabled = false;
            }
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
            this.Close();
        }
    }
}
