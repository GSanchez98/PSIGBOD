using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SIGBOD.MConfiguracion
{
    public partial class FCai : Form
    {
        public FCai()
        {
            InitializeComponent();
        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;
        public string diasRestantes = "";

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdCai.Text == "")
            {
                MessageBox.Show("No se cuenta con un registro para editar.", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                MessageBox.Show("Seleccione un registro de la tabla", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Editar)
                valor = 2;
                Restablecer(3);
            }
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            AgrEdit(3);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
            this.Close();
        }

        private void FCai_Load(object sender, EventArgs e)
        {
            CargarRegistroActual();
            Cargar();
            valor = 0;
            DGListadoCai.DefaultCellStyle.Font = new Font("Century Gothic", 11);
        }

        private void CargarRegistroActual()
        //GIMENA: Esta funcion se implementa de manera independiente para poder cargarse en cualquier parte del codigo.
        //GIMENA: Encargada de listar todos los registros correspondientes a la tabla.
        private void Cargar()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            // GIMENA: Esta parte del codigo se encarga de mostrar los datos del ultimo registro asignado a CAI
            string cadena = "Select * FROM Ventas.Cai where id_Cai = (Select MAX(id_Cai) FROM Ventas.Cai) AND activo_Cai = 1";

            // GIMENA: Esta parte del codigo se encarga de llenar el listado para los cargos
            string cadena = "Select * from Ventas.Cai where activo_cai = 1";
            SqlCommand comando = new(cadena, conexion.conectarBD);
            SqlDataReader Lectura = comando.ExecuteReader();
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            DGListadoCai.DataSource = tabla;
            DGListadoCai.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            if (Lectura.Read())
            {
                txtIdCai.Text = Lectura.GetValue(0).ToString();
                txtCAI.Text = Lectura.GetValue(1).ToString();
                txtFechaLim.Text = Lectura.GetValue(2).ToString();
                txtInicial.Text = Lectura.GetValue(3).ToString();
                txtFinal.Text = Lectura.GetValue(4).ToString(); 
                
                // Evaluacion del tiempo de vencimiento
                if (Convert.ToDateTime(txtFechaLim.Text) > DateTime.Now)
                {
                    diasRestantes = (DateTime.Now - Convert.ToDateTime(txtFechaLim.Text)).ToString(@"dd\d\ hh\h\ mm\m\ ");
                    lbEstado.Visible = true;
                    lbEstado.Text = "El registro de facturación CAI vencerá en " + diasRestantes;
                }
                else
                {
                    lbEstado.Visible = true;
                    lbEstado.Text = "El registro de facturación CAI ha vencido";
                }
            }
            else
            {
                txtIdCai.Clear();
                txtCAI.Clear();
                txtFechaLim.Value = DateTime.Now;
                txtInicial.Clear();
                txtFinal.Clear();
                lbEstado.Visible = false;
            }
            conexion.Cerrar();
        }


        // GIMENA: Funcion que nos permite agregar o editar un registro.
        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Ventas.Cai(cai_Cai,FechaLimite_Cai,NumeroInicial_Cai,NumeroFinal_Cai,activo_Cai,agrego_Cai,fecha_agrego_Cai) VALUES (@cai_Cai,@FechaLimite_Cai,@NumeroInicial_Cai,@NumeroFinal_Cai,@activo_Cai,@agrego_Cai,@fecha_agrego_Cai)";
                try
                {
                    DateTime fechaLimiteEmision = Convert.ToDateTime(txtFechaLim.Text);
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@cai_Cai", txtCAI.Text);
                    comando.Parameters.AddWithValue("@FechaLimite_Cai", fechaLimiteEmision);
                    comando.Parameters.AddWithValue("@NumeroInicial_Cai", txtInicial.Text);
                    comando.Parameters.AddWithValue("@NumeroFinal_Cai", txtFinal.Text);
                    comando.Parameters.AddWithValue("@activo_Cai", 1);
                    comando.Parameters.AddWithValue("@agrego_Cai", 0);
                    comando.Parameters.AddWithValue("@fecha_agrego_Cai", DateTime.Today);
                    comando.ExecuteNonQuery();

                    // GIMENA: Actualizar del tiempo de vencimiento
                    if (Convert.ToDateTime(txtFechaLim.Text) > DateTime.Now)
                    {
                        diasRestantes = (DateTime.Now - Convert.ToDateTime(txtFechaLim.Text)).ToString(@"dd\d\ hh\h\ mm\m\ ");
                        lbEstado.Visible = true;
                        lbEstado.Text = "El registro de facturación CAI vencerá en " + diasRestantes;
                    }
                    else
                    {
                        lbEstado.Visible = true;
                        lbEstado.Text = "El registro de facturación CAI ha vencido";
                    }

                    // GIMENA: Al crear un nuevo registro de CAI se inhabilita el anterior.
                    string desfaseCai = "UPDATE Ventas.Cai SET activo_Cai = 0 WHERE id_Cai = (Select MAX(id_Cai)-1 FROM Ventas.Cai)";
                    try
                    {
                        SqlCommand cmdDesfaseCai = new SqlCommand(desfaseCai, conexion.conectarBD);
                        cmdDesfaseCai.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                    }

                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar();
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
                string cadena = "UPDATE Ventas.Cai SET cai_Cai=@cai_Cai,FechaLimite_Cai=@FechaLimite_Cai,NumeroInicial_Cai=@NumeroInicial_Cai,NumeroFinal_Cai=@NumeroFinal_Cai,activo_Cai=@activo_Cai,agrego_Cai=@agrego_Cai,fecha_agrego_Cai=@fecha_agrego_Cai WHERE id_Cai=" + txtIdCai.Text;
                try
                {
                    DateTime fechaLimiteEmision = Convert.ToDateTime(txtFechaLim.Text);
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@cai_Cai", txtCAI.Text);
                    comando.Parameters.AddWithValue("@FechaLimite_Cai", fechaLimiteEmision);
                    comando.Parameters.AddWithValue("@NumeroInicial_Cai", txtInicial.Text);
                    comando.Parameters.AddWithValue("@NumeroFinal_Cai", txtFinal.Text);
                    comando.Parameters.AddWithValue("@activo_Cai", 1);
                    comando.Parameters.AddWithValue("@agrego_Cai", 0);
                    comando.Parameters.AddWithValue("@fecha_agrego_Cai", DateTime.Today);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    Cargar();
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

            }
            else if (x == 3) // GIMENA: Habilitar/Inhabilitar
            {
                DialogResult dialogResult = MessageBox.Show("Esta seguro que desea cambiar el estado de este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    ConexionBD conexion = new();
                    conexion.Abrir();
                    string cadena = "UPDATE Ventas.Cai SET activo_Cai=@activo_Cai WHERE id_Cai=" + txtIdCai.Text;
                    try
                    {
                        SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                        if (btnEliminar.Text == "Habilitar")
                        {
                            comando.Parameters.AddWithValue("@activo_Cai", 1);
                        }
                        else
                        {
                            comando.Parameters.AddWithValue("activo_Cai", 0);
                        }
                        comando.ExecuteNonQuery();
                        conexion.Cerrar();
                        // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                        Cargar();
                        Restablecer(4);
                        valor = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ERROR: " + ex.Message);
                    }
                }
            }
        }

        private void Restablecer(int x)
        {
            if (x == 1)
            {
                // GIMENA: Opcion cuando se trata de un nuevo registro
                txtCAI.Text = "";
                txtFechaLim.Text = "";
                txtInicial.Text = "";
                txtFinal.Text = "";

                txtCAI.Enabled = true;
                txtFechaLim.Enabled = true;
                txtInicial.Enabled = true;
                txtFinal.Enabled = true;


                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtCAI.Enabled = false;
                txtFechaLim.Enabled = false;
                txtInicial.Enabled = false;
                txtFinal.Enabled = false;

                lbEstado.Visible = true;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtCAI.Enabled = true;
                txtFechaLim.Enabled = true;
                txtInicial.Enabled = true;
                txtFinal.Enabled = true;

                lbEstado.Visible = true;
                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtCAI.Text = "";
                txtFechaLim.Text = "";
                txtInicial.Text = "";
                txtFinal.Text = "";

                txtCAI.Enabled = false;
                txtFechaLim.Enabled = false;
                txtInicial.Enabled = false;
                txtFinal.Enabled = false;

                lbEstado.Visible = false;
                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        private void DGListadoCai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valor = 0;
                txtIdCai.Text = DGListadoCai.CurrentRow.Cells[0].Value.ToString();
                txtCAI.Text = DGListadoCai.CurrentRow.Cells[1].Value.ToString();
                txtFechaLim.Text = DGListadoCai.CurrentRow.Cells[2].Value.ToString();
                txtInicial.Text = DGListadoCai.CurrentRow.Cells[3].Value.ToString();
                txtFinal.Text = DGListadoCai.CurrentRow.Cells[4].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presento un error" + ex.Message);
            }
        }

        private void txtFechaLim_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnLista_Click(object sender, EventArgs e)
        private void txtCAI_KeyPress(object sender, KeyPressEventArgs e)
        {
            DGListadoCai.Visible = true;
            //convertir texto a mayúscula mientras se escribe
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
    }
}
