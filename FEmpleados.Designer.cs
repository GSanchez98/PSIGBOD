
namespace SIGBOD
{
    partial class FEmpleados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.DGListadoEmpleados = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIdentidad = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.txtSalario = new System.Windows.Forms.TextBox();
            this.txtCEmpleado = new System.Windows.Forms.TextBox();
            this.btnEstado = new System.Windows.Forms.Button();
            this.cmbCargo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PBEmpleado = new System.Windows.Forms.PictureBox();
            this.txtRuta = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.TABEmpleados = new System.Windows.Forms.TabControl();
            this.TPDatosG = new System.Windows.Forms.TabPage();
            this.txtCorreo = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.TPListado = new System.Windows.Forms.TabPage();
            this.CBEstado = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.RBNombre = new System.Windows.Forms.RadioButton();
            this.RBCodigo = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.DGListadoEmpleados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBEmpleado)).BeginInit();
            this.TABEmpleados.SuspendLayout();
            this.TPDatosG.SuspendLayout();
            this.TPListado.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancelar
            // 
            this.btnCancelar.Enabled = false;
            this.btnCancelar.Location = new System.Drawing.Point(89, 339);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 33);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnNuevo
            // 
            this.btnNuevo.Location = new System.Drawing.Point(281, 339);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(72, 33);
            this.btnNuevo.TabIndex = 16;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = true;
            this.btnNuevo.Click += new System.EventHandler(this.btnNuevo_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Location = new System.Drawing.Point(365, 339);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(72, 33);
            this.btnEditar.TabIndex = 14;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnGuardar
            // 
            this.btnGuardar.Enabled = false;
            this.btnGuardar.Location = new System.Drawing.Point(11, 339);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(72, 33);
            this.btnGuardar.TabIndex = 13;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // DGListadoEmpleados
            // 
            this.DGListadoEmpleados.AllowUserToAddRows = false;
            this.DGListadoEmpleados.AllowUserToDeleteRows = false;
            this.DGListadoEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGListadoEmpleados.Location = new System.Drawing.Point(15, 94);
            this.DGListadoEmpleados.Name = "DGListadoEmpleados";
            this.DGListadoEmpleados.ReadOnly = true;
            this.DGListadoEmpleados.RowTemplate.Height = 25;
            this.DGListadoEmpleados.Size = new System.Drawing.Size(702, 293);
            this.DGListadoEmpleados.TabIndex = 11;
            this.DGListadoEmpleados.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGListadoCargos_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Datos del empleado";
            // 
            // txtIdentidad
            // 
            this.txtIdentidad.Enabled = false;
            this.txtIdentidad.Location = new System.Drawing.Point(332, 45);
            this.txtIdentidad.Name = "txtIdentidad";
            this.txtIdentidad.Size = new System.Drawing.Size(260, 23);
            this.txtIdentidad.TabIndex = 19;
            // 
            // txtNombre
            // 
            this.txtNombre.Enabled = false;
            this.txtNombre.Location = new System.Drawing.Point(332, 74);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(377, 23);
            this.txtNombre.TabIndex = 20;
            // 
            // txtTelefono
            // 
            this.txtTelefono.Enabled = false;
            this.txtTelefono.Location = new System.Drawing.Point(332, 103);
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.Size = new System.Drawing.Size(260, 23);
            this.txtTelefono.TabIndex = 21;
            // 
            // txtDireccion
            // 
            this.txtDireccion.Enabled = false;
            this.txtDireccion.Location = new System.Drawing.Point(332, 161);
            this.txtDireccion.Multiline = true;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.Size = new System.Drawing.Size(377, 77);
            this.txtDireccion.TabIndex = 22;
            // 
            // txtSalario
            // 
            this.txtSalario.Enabled = false;
            this.txtSalario.Location = new System.Drawing.Point(332, 310);
            this.txtSalario.Name = "txtSalario";
            this.txtSalario.Size = new System.Drawing.Size(260, 23);
            this.txtSalario.TabIndex = 24;
            // 
            // txtCEmpleado
            // 
            this.txtCEmpleado.Location = new System.Drawing.Point(669, 45);
            this.txtCEmpleado.Name = "txtCEmpleado";
            this.txtCEmpleado.Size = new System.Drawing.Size(40, 23);
            this.txtCEmpleado.TabIndex = 25;
            this.txtCEmpleado.Visible = false;
            // 
            // btnEstado
            // 
            this.btnEstado.Location = new System.Drawing.Point(443, 339);
            this.btnEstado.Name = "btnEstado";
            this.btnEstado.Size = new System.Drawing.Size(72, 33);
            this.btnEstado.TabIndex = 26;
            this.btnEstado.UseVisualStyleBackColor = true;
            this.btnEstado.Click += new System.EventHandler(this.btnEstado_Click);
            // 
            // cmbCargo
            // 
            this.cmbCargo.FormattingEnabled = true;
            this.cmbCargo.Location = new System.Drawing.Point(332, 244);
            this.cmbCargo.Name = "cmbCargo";
            this.cmbCargo.Size = new System.Drawing.Size(375, 23);
            this.cmbCargo.TabIndex = 27;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 208);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 45);
            this.button1.TabIndex = 28;
            this.button1.Text = "Cargar imagen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PBEmpleado
            // 
            this.PBEmpleado.Enabled = false;
            this.PBEmpleado.Image = global::SIGBOD.Properties.Resources.Perfil1;
            this.PBEmpleado.Location = new System.Drawing.Point(18, 31);
            this.PBEmpleado.Name = "PBEmpleado";
            this.PBEmpleado.Size = new System.Drawing.Size(164, 171);
            this.PBEmpleado.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PBEmpleado.TabIndex = 29;
            this.PBEmpleado.TabStop = false;
            // 
            // txtRuta
            // 
            this.txtRuta.Location = new System.Drawing.Point(18, 282);
            this.txtRuta.Name = "txtRuta";
            this.txtRuta.Size = new System.Drawing.Size(164, 23);
            this.txtRuta.TabIndex = 30;
            this.txtRuta.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Location = new System.Drawing.Point(101, 60);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(616, 23);
            this.txtBuscar.TabIndex = 31;
            this.txtBuscar.TextChanged += new System.EventHandler(this.txtBuscar_TextChanged);
            // 
            // TABEmpleados
            // 
            this.TABEmpleados.Controls.Add(this.TPDatosG);
            this.TABEmpleados.Controls.Add(this.TPListado);
            this.TABEmpleados.Location = new System.Drawing.Point(12, 13);
            this.TABEmpleados.Name = "TABEmpleados";
            this.TABEmpleados.SelectedIndex = 0;
            this.TABEmpleados.Size = new System.Drawing.Size(738, 524);
            this.TABEmpleados.TabIndex = 32;
            // 
            // TPDatosG
            // 
            this.TPDatosG.Controls.Add(this.txtCorreo);
            this.TPDatosG.Controls.Add(this.label10);
            this.TPDatosG.Controls.Add(this.label9);
            this.TPDatosG.Controls.Add(this.label8);
            this.TPDatosG.Controls.Add(this.label7);
            this.TPDatosG.Controls.Add(this.label6);
            this.TPDatosG.Controls.Add(this.label5);
            this.TPDatosG.Controls.Add(this.PBEmpleado);
            this.TPDatosG.Controls.Add(this.btnEstado);
            this.TPDatosG.Controls.Add(this.button1);
            this.TPDatosG.Controls.Add(this.btnNuevo);
            this.TPDatosG.Controls.Add(this.btnCancelar);
            this.TPDatosG.Controls.Add(this.btnEditar);
            this.TPDatosG.Controls.Add(this.label2);
            this.TPDatosG.Controls.Add(this.cmbCargo);
            this.TPDatosG.Controls.Add(this.txtDireccion);
            this.TPDatosG.Controls.Add(this.btnGuardar);
            this.TPDatosG.Controls.Add(this.txtSalario);
            this.TPDatosG.Controls.Add(this.txtIdentidad);
            this.TPDatosG.Controls.Add(this.txtCEmpleado);
            this.TPDatosG.Controls.Add(this.txtNombre);
            this.TPDatosG.Controls.Add(this.txtRuta);
            this.TPDatosG.Controls.Add(this.txtTelefono);
            this.TPDatosG.Location = new System.Drawing.Point(4, 24);
            this.TPDatosG.Name = "TPDatosG";
            this.TPDatosG.Padding = new System.Windows.Forms.Padding(3);
            this.TPDatosG.Size = new System.Drawing.Size(730, 496);
            this.TPDatosG.TabIndex = 0;
            this.TPDatosG.Text = "Datos Generales";
            this.TPDatosG.UseVisualStyleBackColor = true;
            // 
            // txtCorreo
            // 
            this.txtCorreo.Enabled = false;
            this.txtCorreo.Location = new System.Drawing.Point(332, 132);
            this.txtCorreo.Name = "txtCorreo";
            this.txtCorreo.Size = new System.Drawing.Size(260, 23);
            this.txtCorreo.TabIndex = 37;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(207, 247);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 15);
            this.label10.TabIndex = 36;
            this.label10.Text = "Cargo :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(207, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 15);
            this.label9.TabIndex = 35;
            this.label9.Text = "Dirección :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(207, 135);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 15);
            this.label8.TabIndex = 34;
            this.label8.Text = "Correo :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(207, 106);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 15);
            this.label7.TabIndex = 33;
            this.label7.Text = "Teléfono :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(207, 77);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 15);
            this.label6.TabIndex = 32;
            this.label6.Text = "Nombre completo :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(207, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 15);
            this.label5.TabIndex = 31;
            this.label5.Text = "Identidad :";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // TPListado
            // 
            this.TPListado.Controls.Add(this.CBEstado);
            this.TPListado.Controls.Add(this.label4);
            this.TPListado.Controls.Add(this.label3);
            this.TPListado.Controls.Add(this.label1);
            this.TPListado.Controls.Add(this.RBNombre);
            this.TPListado.Controls.Add(this.RBCodigo);
            this.TPListado.Controls.Add(this.DGListadoEmpleados);
            this.TPListado.Controls.Add(this.txtBuscar);
            this.TPListado.Location = new System.Drawing.Point(4, 24);
            this.TPListado.Name = "TPListado";
            this.TPListado.Padding = new System.Windows.Forms.Padding(3);
            this.TPListado.Size = new System.Drawing.Size(730, 496);
            this.TPListado.TabIndex = 1;
            this.TPListado.Text = "Listado";
            this.TPListado.UseVisualStyleBackColor = true;
            // 
            // CBEstado
            // 
            this.CBEstado.FormattingEnabled = true;
            this.CBEstado.Location = new System.Drawing.Point(532, 28);
            this.CBEstado.Name = "CBEstado";
            this.CBEstado.Size = new System.Drawing.Size(185, 23);
            this.CBEstado.TabIndex = 39;
            this.CBEstado.SelectedIndexChanged += new System.EventHandler(this.CBEstado_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 15);
            this.label4.TabIndex = 38;
            this.label4.Text = "Buscar :";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(462, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 35;
            this.label3.Text = "Registros : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 34;
            this.label1.Text = "Filtrar por : ";
            // 
            // RBNombre
            // 
            this.RBNombre.AutoSize = true;
            this.RBNombre.Location = new System.Drawing.Point(171, 26);
            this.RBNombre.Name = "RBNombre";
            this.RBNombre.Size = new System.Drawing.Size(69, 19);
            this.RBNombre.TabIndex = 33;
            this.RBNombre.Text = "Nombre";
            this.RBNombre.UseVisualStyleBackColor = true;
            // 
            // RBCodigo
            // 
            this.RBCodigo.AutoSize = true;
            this.RBCodigo.Checked = true;
            this.RBCodigo.Location = new System.Drawing.Point(101, 26);
            this.RBCodigo.Name = "RBCodigo";
            this.RBCodigo.Size = new System.Drawing.Size(64, 19);
            this.RBCodigo.TabIndex = 32;
            this.RBCodigo.TabStop = true;
            this.RBCodigo.Text = "Codigo";
            this.RBCodigo.UseVisualStyleBackColor = true;
            // 
            // FEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 549);
            this.Controls.Add(this.TABEmpleados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FEmpleados";
            this.Text = "FEmpleados";
            this.Load += new System.EventHandler(this.FEmpleados_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGListadoEmpleados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PBEmpleado)).EndInit();
            this.TABEmpleados.ResumeLayout(false);
            this.TPDatosG.ResumeLayout(false);
            this.TPDatosG.PerformLayout();
            this.TPListado.ResumeLayout(false);
            this.TPListado.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.DataGridView DGListadoEmpleados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIdentidad;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.TextBox txtTelefono;
        private System.Windows.Forms.TextBox txtDireccion;
        private System.Windows.Forms.TextBox txtSalario;
        private System.Windows.Forms.TextBox txtCEmpleado;
        private System.Windows.Forms.Button btnEstado;
        private System.Windows.Forms.ComboBox cmbCargo;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox PBEmpleado;
        private System.Windows.Forms.TextBox txtRuta;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.TabControl TABEmpleados;
        private System.Windows.Forms.TabPage TPDatosG;
        private System.Windows.Forms.TabPage TPListado;
        private System.Windows.Forms.RadioButton RBNombre;
        private System.Windows.Forms.RadioButton RBCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CBEstado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtCorreo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
    }
}