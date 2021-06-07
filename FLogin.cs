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
    public partial class FLogin : Form
    {
        public FLogin()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConexionBD conexion = new();
            conexion.Abrir();

            //CODIGO PARA LISTAR
            string cadena = "Select * from usuarios";
            SqlCommand comando = new(cadena, conexion.conectarBD);
            SqlDataAdapter adaptador = new SqlDataAdapter(comando);
            DataTable tabla = new DataTable();
            adaptador.Fill(tabla);
            dataGridView1.DataSource = tabla;
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //***********************************************************************************************************************************//
            // NOTA: SI INGRESA EL USUARIO Y CLAVE CORRECTA LLAMA AL MENU PRINCIPAL.
            //***********************************************************************************************************************************//
            FMenuInicial menui = new();
            menui.Show();
        }
    }
}
