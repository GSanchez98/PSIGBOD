using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SIGBOD
{
    public partial class FCargos : Form
    {
        public FCargos()
        {
            InitializeComponent();

        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;


        //GIMENA: Esta funcion se implementa de manera independiente para poder cargarse en cualquier parte del codigo.
        //GIMENA: Encargada de listar todos los registros correspondientes a la tabla.
        private void Cargar()
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            // GIMENA: Esta parte del codigo se encarga de llenar el listado para los cargos
            string cadena = "Select * from cargos";
            SqlCommand comando = new(cadena, conexion.conectarBD);
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            DGListadoCargos.DataSource = tabla;
            DGListadoCargos.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);

            conexion.Cerrar();
        }

        private void FCargos_Load(object sender, EventArgs e)
        {
            // GIMENA: Se llama a la funcion cargar para mostrar los registros actuales.
            Cargar();
            valor = 0;
        }


        // GIMENA: Funcion que nos permite agregar o editar un registro.
        private void AgrEdit(int x)
        {
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Cargos(Cargo,Descripcion) VALUES (@Cargo,@Descripcion)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    comando.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
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
            else if (x == 2) // GIMENA: Modificar
            {
                
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "UPDATE Cargos SET Cargo=@Cargo ,Descripcion=@Descripcion WHERE CCargo="+txtCCargo.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    comando.Parameters.AddWithValue("@Cargo", txtCargo.Text);
                    comando.Parameters.AddWithValue("@Descripcion", txtDescripcion.Text);
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
        }


        private void Restablecer(int x)
        {
            if (x == 1){
                // GIMENA: Opcion cuando se trata de un nuevo registro
                txtCCargo.Text = "";
                txtCargo.Text = "";
                txtDescripcion.Text = "";

                txtCargo.Enabled = true;
                txtDescripcion.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtCargo.Enabled = false;
                txtDescripcion.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtCargo.Enabled = true;
                txtDescripcion.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnEliminar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtCCargo.Text = "";
                txtCargo.Text = "";
                txtDescripcion.Text = "";

                txtCargo.Enabled = false;
                txtDescripcion.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Guardar)
            valor = 1;
            Restablecer(1);
           
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            AgrEdit(valor);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtCCargo.Text == "")
            {
                MessageBox.Show("Seleccione un registro de la tabla", "INFORMACION", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // GIMENA: Se inicia variacle con valor uno para guardar, Reestablecer(Editar)
                valor = 2;
                Restablecer(3);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                valor = 0;
                txtCCargo.Text = DGListadoCargos.CurrentRow.Cells[0].Value.ToString();
                txtCargo.Text = DGListadoCargos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = DGListadoCargos.CurrentRow.Cells[2].Value.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presento un error" + ex.Message);
            }

        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea eliminar este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                string cadena = "DELETE FROM Cargos WHERE CCargo=" + txtCCargo.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
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
            else if (dialogResult == DialogResult.No)
            {
                // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                Cargar();
                Restablecer(4);
                valor = 0;
            }            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}


