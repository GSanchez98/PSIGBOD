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
    public partial class FDescuentos : Form
    {
        public FDescuentos()
        {
            InitializeComponent();
        }
        // GIMENA: Variable que nos permitira evaluar si se esta agregando o editando un registro.
        public int valor = 0;

        private void FDescuentos_Load(object sender, EventArgs e)
        {
            llenacomboProductos();
            cmbProducto.Text = "Seleccione un producto de la lista";
            llenacomboDesc();
            txtCodigo.Text = "";
            txtPrecio.Text = "0.00";
            cmbDesc.Text = "0";
            cmbDesc.Enabled = false;
        }

        public void llenacomboProductos()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter("SELECT id_Producto, nombre_Producto FROM Inventario.Producto WHERE estado_Producto = 1", conexion.conectarBD);
            //se indica el nombre de la tabla
            da.Fill(ds, "Inventario.Producto");
            cmbProducto.DataSource = ds.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbProducto.ValueMember = "id_Producto";
            cmbProducto.DisplayMember = "nombre_Producto";
            cmbProducto.SelectedItem = null;

        }

        public void llenacomboDesc()
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            DataSet dsd = new DataSet();
            SqlDataAdapter dad = new SqlDataAdapter("SELECT id_Parametro, valor_decimal_Parametro, descripcion_Parametro FROM SIGBOD.General.Parametros_Generales where tipo_Parametro = 'PORCENTAJE'", conexion.conectarBD);
            //se indica el nombre de la tabla
            dad.Fill(dsd, "General.Parametros_Generales");
            cmbDesc.DataSource = dsd.Tables[0].DefaultView;
            //se especifica el campo de la tabla
            cmbDesc.ValueMember = "id_Parametro";
            cmbDesc.DisplayMember = "descripcion_Parametro";
            cmbDesc.SelectedItem = null;
            
        }                


        private void button1_Click(object sender, EventArgs e)
        {
            FAgrDescuentos segundo = new FAgrDescuentos();
            AddOwnedForm(segundo);
            segundo.ShowDialog();
        }


        private void cmbDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbProducto.SelectedItem != null)
            {
                double descuento = int.Parse(cmbDesc.Text);
                double precio = Convert.ToDouble(txtPrecio.Text);
                double descuentoaplicado = precio * (descuento / 100);
                double descuentoTotal = precio - descuentoaplicado;
                txtPor.Text = descuentoTotal.ToString();
            }
        }

        private void cmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbProducto.SelectedItem != null)
            {
                if (valor == 1) {
                    cmbDesc.Enabled = true;
                }
                
                ConexionBD conexion = new();
                string sql = "SELECT id_Producto, precio_producto FROM Inventario.Producto WHERE id_Producto = " + cmbProducto.SelectedValue.ToString();
                SqlCommand cmd = new SqlCommand(sql, conexion.conectarBD);
                SqlDataReader myreader;
                try
                {
                    conexion.Abrir();
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        txtCodigo.Text = myreader["id_Producto"].ToString();
                        txtPrecio.Text = myreader["precio_producto"].ToString();
                    }
                }
                catch (Exception)
                {
                    conexion.Cerrar();
                }
            }
            else
            {
                txtCodigo.Text = " - ";
                txtPrecio.Text = "0.00";
                cmbDesc.Text = "0.00";
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

        private void AgrEdit(int x)
        {
            int usuarioActivo = Variables.idUsuario;
            if (x == 1) // GIMENA: Agregar
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "INSERT INTO Ventas.Descuentos(id_producto_Descuento,procentaje_Descuento,fecha_inicio_Descuento ,fecha_fin_Descuento,nuevo_precio_Descuento,precio_Descuento,estado_Descuento ,agrego_Descuento ,fecha_agrego_Descuento)VALUES(@id_producto_Descuento ,@procentaje_Descuento ,@fecha_inicio_Descuento ,@fecha_fin_Descuento ,@nuevo_precio_Descuento, @precio_Descuento ,@estado_Descuento ,@agrego_Descuento ,@fecha_agrego_Descuento)";
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    //FALTA LA IMAGEN
                    DateTime fechaInicio = Convert.ToDateTime(txtFechaIni.Text);
                    DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

                    comando.Parameters.AddWithValue("@id_producto_Descuento", txtCodigo.Text);
                    comando.Parameters.AddWithValue("@procentaje_Descuento", cmbDesc.Text);
                    comando.Parameters.AddWithValue("@fecha_inicio_Descuento", fechaInicio);
                    comando.Parameters.AddWithValue("@fecha_fin_Descuento", fechaFin);
                    comando.Parameters.AddWithValue("@precio_Descuento", Convert.ToDouble(txtPrecio.Text));
                    comando.Parameters.AddWithValue("@nuevo_precio_Descuento", Convert.ToDouble(txtPor.Text));
                    comando.Parameters.AddWithValue("@estado_Descuento", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Descuento", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Descuento", usuarioActivo);

                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    //Cargar(1);
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
                string cadena = "UPDATE Ventas.Descuentos SET id_producto_Descuento=@id_producto_Descuento,procentaje_Descuento=@procentaje_Descuento,fecha_inicio_Descuento=@fecha_inicio_Descuento ,fecha_fin_Descuento=@fecha_fin_Descuento,nuevo_precio_Descuento=@nuevo_precio_Descuento,precio_Descuento=@precio_Descuento,estado_Descuento=@estado_Descuento,agrego_Descuento=@agrego_Descuento,fecha_agrego_Descuento=@fecha_agrego_Descuento WHERE id_Descuento = " + txtIdDesc.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);

                    DateTime fechaInicio = Convert.ToDateTime(txtFechaIni.Text);
                    DateTime fechaFin = Convert.ToDateTime(txtFechaFin.Text);

                    comando.Parameters.AddWithValue("@id_producto_Descuento", txtCodigo.Text);
                    comando.Parameters.AddWithValue("@procentaje_Descuento", cmbDesc.Text);
                    comando.Parameters.AddWithValue("@fecha_inicio_Descuento", fechaInicio);
                    comando.Parameters.AddWithValue("@fecha_fin_Descuento", fechaFin);
                    comando.Parameters.AddWithValue("@precio_Descuento", Convert.ToDouble(txtPrecio.Text));
                    comando.Parameters.AddWithValue("@nuevo_precio_Descuento", Convert.ToDouble(txtPor.Text));
                    comando.Parameters.AddWithValue("@estado_Descuento", 1);
                    comando.Parameters.AddWithValue("@fecha_agrego_Descuento", DateTime.Now);
                    comando.Parameters.AddWithValue("@agrego_Descuento", usuarioActivo);
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    //Cargar(1);
                    Restablecer(2);
                    valor = 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR: " + ex.Message);
                }

            }
        }

        // GIMENA: ACCION QUE PERMITE REFRESCAR LA PANTALLA UNA VEZ REALIZADA UNA ACCION
        private void Restablecer(int x)
        {
            if (x == 1)
            {
                // GIMENA: Opcion cuando se trata de un nuevo registro
                txtCodigo.Text = "";
                txtPrecio.Text = "0.00";
                cmbDesc.Text = "0";
                cmbProducto.Text = "Seleccione un producto de la lista";
                txtPor.Text = "0.00";
                txtFechaIni.Text = DateTime.Now.ToString();
                txtFechaFin.Text = DateTime.Now.ToString();

                txtCodigo.Enabled = false;
                cmbDesc.Enabled = false;
                txtPrecio.Enabled = false;
                txtPor.Enabled = false;
                txtFechaIni.Enabled = true;
                txtFechaFin.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = false;
            }
            else if (x == 2)
            {
                // GIMENA: Opcion cuando hemos guardado informacion
                txtCodigo.Enabled = true;
                cmbDesc.Enabled = false;
                txtPrecio.Enabled = false;
                txtPor.Enabled = false;
                txtFechaIni.Enabled = false;
                txtFechaFin.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
                btnEstado.Text = "Habilitar";
            }
            else if (x == 3)
            {
                // GIMENA: Opcion cuando se trata de editar un registro
                txtCodigo.Enabled = false;
                cmbProducto.Enabled = false;
                txtPrecio.Enabled = false;
                cmbDesc.Enabled = true;
                txtPor.Enabled = false;
                txtFechaIni.Enabled = true;
                txtFechaFin.Enabled = true;

                btnNuevo.Enabled = false;
                btnEditar.Enabled = false;
                btnGuardar.Enabled = true;
                btnCancelar.Enabled = true;
                btnEstado.Enabled = true;
            }
            else if (x == 4)
            {
                // GIMENA: Opcion cuando se cancela todo
                txtCodigo.Text = "";
                txtPrecio.Text = "0.00";
                cmbDesc.Text = "0";
                cmbProducto.Text = "Seleccione un producto de la lista";
                txtPor.Text = "0.00";
                txtFechaIni.Text = DateTime.Now.ToString();
                txtFechaFin.Text = DateTime.Now.ToString();

                txtCodigo.Enabled = true;
                cmbDesc.Enabled = false;
                txtPrecio.Enabled = false;
                txtPor.Enabled = false;
                txtFechaIni.Enabled = false;
                txtFechaFin.Enabled = false;

                btnNuevo.Enabled = true;
                btnEditar.Enabled = true;
                btnGuardar.Enabled = false;
                btnCancelar.Enabled = false;
                btnEstado.Enabled = false;
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            // GIMENA: Se inicia variable , Reestablecer(Cancelar)
            valor = 0;
            Restablecer(4);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (txtIdDesc.Text == "")
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

        private void btnEstado_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Esta seguro que desea cambiar el estado de este registro", "ADVERTENCIA", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                ConexionBD conexion = new();
                conexion.Abrir();
                string cadena = "UPDATE Ventas.Descuentos SET estado_Descuento = @estado_Descuento WHERE id_Descuento = " + txtIdDesc.Text;
                try
                {
                    SqlCommand comando = new SqlCommand(cadena, conexion.conectarBD);
                    if (btnEstado.Text == "Habilitar")
                    {
                        comando.Parameters.AddWithValue("@estado_Descuento", 1);
                    }
                    else
                    {
                        comando.Parameters.AddWithValue("@estado_Descuento", 0);
                    }
                    comando.ExecuteNonQuery();
                    conexion.Cerrar();
                    // GIMENA: Se llama a la funcion cargar como una manera de actualizar los registros.
                    //Cargar(1);
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
                //Cargar(1);
                Restablecer(4);
                valor = 0;
            }

        }

        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo.Text != "")
            {
                ConexionBD conexion = new();
                string sql = "SELECT id_Descuento, id_producto_Descuento, procentaje_Descuento, fecha_inicio_Descuento ,fecha_fin_Descuento,nuevo_precio_Descuento,precio_Descuento FROM Ventas.Descuentos WHERE estado_Descuento = 1 and id_producto_Descuento= " + txtCodigo.Text;
                SqlCommand cmd = new SqlCommand(sql, conexion.conectarBD);
                SqlDataReader myreader;
                try
                {
                    conexion.Abrir();
                    myreader = cmd.ExecuteReader();
                    while (myreader.Read())
                    {
                        txtIdDesc.Text = myreader["id_Descuento"].ToString();
                        cmbDesc.Text = myreader["procentaje_Descuento"].ToString();
                        txtFechaIni.Text = myreader["fecha_inicio_Descuento"].ToString();
                        txtFechaFin.Text = myreader["fecha_fin_Descuento"].ToString();
                        txtPor.Text = myreader["nuevo_precio_Descuento"].ToString();
                        txtPrecio.Text = myreader["precio_Descuento"].ToString();
                        cmbProducto.SelectedValue = myreader["id_producto_Descuento"].ToString();
                    }
                }
                catch (Exception)
                {                    
                    conexion.Cerrar();
                }
            }
            else
            {
                txtCodigo.Text = "";
                txtPrecio.Text = "0.00";
                cmbDesc.Text = "0";
                cmbProducto.Text = "Seleccione un producto de la lista";
                txtPor.Text = "0.00";
                txtFechaIni.Text = DateTime.Now.ToString();
                txtFechaFin.Text = DateTime.Now.ToString();
            }
        }
    }
}
