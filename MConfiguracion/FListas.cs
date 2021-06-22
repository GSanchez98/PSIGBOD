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

namespace SIGBOD.MConfiguracion
{
    public partial class FListas : Form
    {
        int idLista;

        //Constructor encargado de obtener id
        public FListas(int id)
        {
            InitializeComponent();
            idLista = id;

        }


        private void FListas_Load(object sender, EventArgs e)
        {
            //DGListadoEmpleados.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            dgvListados.DefaultCellStyle.Font = new Font("Century Gothic", 10);
            switch (idLista)
            {
                case 1:
                    // Listar empleados
                    Cargar(1);
                    break;
                case 2:
                    Cargar(2);
                    break;
                case 3:
                    Cargar(3);
                    break;
                case 4:
                    MessageBox.Show("VALOR 4");
                    break;
                case 5:
                    MessageBox.Show("VALOR 5");
                    break;
                default:
                    break;
            }
        }


        private void Cargar(int estado)
        {
            ConexionBD conexion = new();
            conexion.Abrir();
            string cadena;
            switch (idLista)
            {
                case 1:
                    // Listar empleados
                    if (estado == 1)
                    {
                        // GIMENA: Esta parte del codigo se encarga de llenar el listado para los empleados
                        cadena = "Select * from Usuarios.Empleados where estado_Empleado = 1";
                        SqlCommand comando = new(cadena, conexion.conectarBD);
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        dgvListados.DataSource = tabla;
                        dgvListados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    }
                    else if (estado == 0)
                    {
                        // GIMENA: Esta parte del codigo se encarga de llenar el listado para los empleados
                        cadena = "Select * from Usuarios.Empleados where estado_Empleado = 0";
                        SqlCommand comando = new(cadena, conexion.conectarBD);
                        SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                        DataTable tabla = new DataTable();
                        adaptador.Fill(tabla);
                        dgvListados.DataSource = tabla;
                        dgvListados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    }
                    break;
                case 2:
                    // listar los cargos existentes
                    cadena = "SELECT *  FROM Usuarios.Cargos";
                    SqlCommand comandoCargos = new(cadena, conexion.conectarBD);
                    SqlDataAdapter adaptadorCargos = new SqlDataAdapter(comandoCargos);
                    DataTable tablaCargos = new DataTable();
                    adaptadorCargos.Fill(tablaCargos);
                    dgvListados.DataSource = tablaCargos;
                    dgvListados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    break;
                case 3:
                    // listar los cargos existentes
                    cadena = "SELECT *  FROM Usuarios.Usuarios";
                    SqlCommand comandoUsuarios = new(cadena, conexion.conectarBD);
                    SqlDataAdapter adaptadorUsuarios = new SqlDataAdapter(comandoUsuarios);
                    DataTable tablaUsuarios = new DataTable();
                    adaptadorUsuarios.Fill(tablaUsuarios);
                    dgvListados.DataSource = tablaUsuarios;
                    dgvListados.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader);
                    break;
                case 4:
                    MessageBox.Show("VALOR 4");
                    break;
                case 5:
                    MessageBox.Show("VALOR 5");
                    break;
                default:
                    break;
            }
            conexion.Cerrar();
        }

        private void dgvListados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (idLista)
            {
                case 1:
                    try
                    {
                        string cempleado = dgvListados.CurrentRow.Cells[0].Value.ToString();
                        string identidad = dgvListados.CurrentRow.Cells[1].Value.ToString();
                        string nombre = dgvListados.CurrentRow.Cells[2].Value.ToString();
                        string telefono = dgvListados.CurrentRow.Cells[3].Value.ToString();
                        string correo = dgvListados.CurrentRow.Cells[4].Value.ToString();
                        string direccion = dgvListados.CurrentRow.Cells[5].Value.ToString();
                        int valorIndex = Convert.ToInt32(dgvListados.CurrentRow.Cells[6].Value);
                        string FechaNac = dgvListados.CurrentRow.Cells[7].Value.ToString();
                        string FechaIng = dgvListados.CurrentRow.Cells[8].Value.ToString();
                        string Salario = dgvListados.CurrentRow.Cells[9].Value.ToString();
                        string Ruta = dgvListados.CurrentRow.Cells[10].Value.ToString();

                        FEmpleados empleado = Owner as FEmpleados;
                        empleado.txtCEmpleado.Text = cempleado;
                        empleado.txtIdentidad.Text = identidad;
                        empleado.txtNombre.Text = nombre;
                        empleado.txtTelefono.Text = telefono;
                        empleado.txtCorreo.Text = correo;
                        empleado.txtDireccion.Text = direccion;
                        empleado.txtFechaNac.Text = FechaNac;
                        empleado.txtFechaIng.Text = FechaIng;
                        empleado.txtSalario.Text = Salario;
                        empleado.txtRuta.Text = Ruta;
                        empleado.PBEmpleado.Load(Ruta);
                        empleado.cmbCargo.SelectedValue = valorIndex;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Se presento un error" + ex.Message);
                    }
                    break;
                case 2:
                    try
                    {
                        string idCargo = dgvListados.CurrentRow.Cells[0].Value.ToString();
                        string cargo = dgvListados.CurrentRow.Cells[1].Value.ToString();
                        string descripcion = dgvListados.CurrentRow.Cells[2].Value.ToString();

                        FCargos Cargos = Owner as FCargos;
                        Cargos.txtCCargo.Text = idCargo;
                        Cargos.txtCargo.Text = cargo;
                        Cargos.txtDescripcion.Text = descripcion;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Se presento un error" + ex.Message);
                    }
                    break;
                case 3:
                    try
                    {
                        string idUsuario = dgvListados.CurrentRow.Cells[0].Value.ToString();
                        string nombreUsuario = dgvListados.CurrentRow.Cells[1].Value.ToString();
                        string Clave = dgvListados.CurrentRow.Cells[2].Value.ToString();
                        int valorC = Convert.ToInt32(dgvListados.CurrentRow.Cells[0].Value);
                        

                        FUsuarios Usuarios = Owner as FUsuarios;
                        Usuarios.txtAcceso.Text = nombreUsuario;
                        Usuarios.txtxClave.Text = Clave; // u ocultar
                        Usuarios.txtIdUsuario.Text = idUsuario;
                        Usuarios.cmbEmpleado.SelectedValue = valorC;
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Se presento un error" + ex.Message);
                    }
                    break;
                case 4:
                    MessageBox.Show("VALOR 4");
                    break;
                case 5:
                    MessageBox.Show("VALOR 5");
                    break;
                default:
                    break;
            }
            
            this.Close();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
