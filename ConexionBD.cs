using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace SIGBOD
{
    public class ConexionBD
    {
        //***********************************************************************************************************************************//
        // NOTA: MAS ADELANTE CREAR VARIABLES QUE ESTABLEZCAN LOS DATOS DE CONEXION FIJOS Y SI ESTOS DAN ERROR QUE LO SOLICITE EN UNA VENTANA.
        //***********************************************************************************************************************************//


        // GIMENA: Creando cadena de conexion para la base de datos
        // GIMENA: Mi srv: DESKTOP-858IMG2\\SQLEXPRESS
        string cadena = "Data Source=DESKTOP-858IMG2\\SQLEXPRESS; initial Catalog=SIGBOD; Integrated Security=True";
        public SqlConnection conectarBD = new();

        public ConexionBD()
        {
            conectarBD.ConnectionString = cadena;
        }
  
        public void Abrir()
        {
            try
            {
                conectarBD.Open();
                // GIMENA: En caso que se quiera mostrar un mensaje cuando la conexion se ha establecido con exito.
                // MessageBox.Show("Se ha conectado con exito a la BD","Estado de conexión", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }catch(Exception ex)
            {
                MessageBox.Show("Error al intentar conectarse a la BD"+ ex.Message,"Estado de conexión",MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void Cerrar()
        {
            conectarBD.Close();
        }



    }
}
