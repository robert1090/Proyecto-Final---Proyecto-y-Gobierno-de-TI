namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormModificar
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabMaestro = new TabPage();
            cmbMaestros = new ComboBox();
            txtNuevoNombre = new TextBox();
            tabMateria = new TabPage();
            cmbMaterias = new ComboBox();
            cmbMaestroAsoc = new ComboBox();
            txtIdMateria = new TextBox();
            txtNombreM = new TextBox();
            txtDias = new TextBox();
            txtHora = new TextBox();
            txtAula = new TextBox();
            txtHDCredito = new TextBox();
            txtSeccion = new TextBox();
            txtDiasMes = new TextBox();
            txtCredito = new TextBox();
            txtTotalCredito = new TextBox();
            txtInscritos = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            menuStrip2 = new MenuStrip();
            cerrarToolStripMenuItem6 = new ToolStripMenuItem();
            maximizarToolStripMenuItem = new ToolStripMenuItem();
            minimizarToolStripMenuItem = new ToolStripMenuItem();
            tabControl.SuspendLayout();
            tabMaestro.SuspendLayout();
            tabMateria.SuspendLayout();
            menuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabMaestro);
            tabControl.Controls.Add(tabMateria);
            tabControl.Location = new Point(12, 31);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(430, 371);
            tabControl.TabIndex = 0;
            // 
            // tabMaestro
            // 
            tabMaestro.BackColor = Color.FromArgb(45, 45, 48);
            tabMaestro.Controls.Add(cmbMaestros);
            tabMaestro.Controls.Add(txtNuevoNombre);
            tabMaestro.Location = new Point(4, 24);
            tabMaestro.Name = "tabMaestro";
            tabMaestro.Size = new Size(422, 343);
            tabMaestro.TabIndex = 0;
            tabMaestro.Text = "Maestro";
            tabMaestro.Click += tabMaestro_Click;
            // 
            // cmbMaestros
            // 
            cmbMaestros.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestros.Location = new Point(15, 62);
            cmbMaestros.Name = "cmbMaestros";
            cmbMaestros.Size = new Size(350, 23);
            cmbMaestros.TabIndex = 0;
            cmbMaestros.SelectedIndexChanged += CmbMaestros_SelectedIndexChanged;
            // 
            // txtNuevoNombre
            // 
            txtNuevoNombre.Location = new Point(15, 122);
            txtNuevoNombre.Name = "txtNuevoNombre";
            txtNuevoNombre.Size = new Size(350, 23);
            txtNuevoNombre.TabIndex = 1;
            // 
            // tabMateria
            // 
            tabMateria.BackColor = Color.FromArgb(45, 45, 48);
            tabMateria.Controls.Add(cmbMaterias);
            tabMateria.Location = new Point(4, 24);
            tabMateria.Name = "tabMateria";
            tabMateria.Size = new Size(422, 343);
            tabMateria.TabIndex = 1;
            tabMateria.Text = "Materia";
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.Location = new Point(15, 35);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(380, 23);
            cmbMaterias.TabIndex = 0;
            cmbMaterias.SelectedIndexChanged += CmbMaterias_SelectedIndexChanged;
            // 
            // cmbMaestroAsoc
            // 
            cmbMaestroAsoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestroAsoc.Location = new Point(0, 0);
            cmbMaestroAsoc.Name = "cmbMaestroAsoc";
            cmbMaestroAsoc.Size = new Size(121, 23);
            cmbMaestroAsoc.TabIndex = 0;
            // 
            // txtIdMateria
            // 
            txtIdMateria.Location = new Point(0, 0);
            txtIdMateria.Name = "txtIdMateria";
            txtIdMateria.Size = new Size(100, 23);
            txtIdMateria.TabIndex = 0;
            // 
            // txtNombreM
            // 
            txtNombreM.Location = new Point(0, 0);
            txtNombreM.Name = "txtNombreM";
            txtNombreM.Size = new Size(100, 23);
            txtNombreM.TabIndex = 0;
            // 
            // txtDias
            // 
            txtDias.Location = new Point(0, 0);
            txtDias.Name = "txtDias";
            txtDias.Size = new Size(100, 23);
            txtDias.TabIndex = 0;
            // 
            // txtHora
            // 
            txtHora.Location = new Point(0, 0);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(100, 23);
            txtHora.TabIndex = 0;
            // 
            // txtAula
            // 
            txtAula.Location = new Point(0, 0);
            txtAula.Name = "txtAula";
            txtAula.Size = new Size(100, 23);
            txtAula.TabIndex = 0;
            // 
            // txtHDCredito
            // 
            txtHDCredito.Location = new Point(0, 0);
            txtHDCredito.Name = "txtHDCredito";
            txtHDCredito.Size = new Size(100, 23);
            txtHDCredito.TabIndex = 0;
            // 
            // txtSeccion
            // 
            txtSeccion.Location = new Point(0, 0);
            txtSeccion.Name = "txtSeccion";
            txtSeccion.Size = new Size(100, 23);
            txtSeccion.TabIndex = 0;
            // 
            // txtDiasMes
            // 
            txtDiasMes.Location = new Point(0, 0);
            txtDiasMes.Name = "txtDiasMes";
            txtDiasMes.Size = new Size(100, 23);
            txtDiasMes.TabIndex = 0;
            // 
            // txtCredito
            // 
            txtCredito.Location = new Point(0, 0);
            txtCredito.Name = "txtCredito";
            txtCredito.Size = new Size(100, 23);
            txtCredito.TabIndex = 0;
            // 
            // txtTotalCredito
            // 
            txtTotalCredito.Location = new Point(0, 0);
            txtTotalCredito.Name = "txtTotalCredito";
            txtTotalCredito.Size = new Size(100, 23);
            txtTotalCredito.TabIndex = 0;
            // 
            // txtInscritos
            // 
            txtInscritos.Location = new Point(0, 0);
            txtInscritos.Name = "txtInscritos";
            txtInscritos.Size = new Size(100, 23);
            txtInscritos.TabIndex = 0;
            // 
            // btnGuardar
            // 
            btnGuardar.ForeColor = SystemColors.ButtonHighlight;
            btnGuardar.Location = new Point(215, 415);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 35);
            btnGuardar.TabIndex = 1;
            btnGuardar.Text = "💾 Guardar";
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.ForeColor = SystemColors.ButtonHighlight;
            btnCancelar.Location = new Point(345, 415);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 35);
            btnCancelar.TabIndex = 2;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // menuStrip2
            // 
            menuStrip2.BackColor = Color.FromArgb(20, 20, 20);
            menuStrip2.ImageScalingSize = new Size(20, 20);
            menuStrip2.Items.AddRange(new ToolStripItem[] { cerrarToolStripMenuItem6, maximizarToolStripMenuItem, minimizarToolStripMenuItem });
            menuStrip2.Location = new Point(0, 0);
            menuStrip2.Name = "menuStrip2";
            menuStrip2.Size = new Size(465, 28);
            menuStrip2.TabIndex = 3;
            menuStrip2.Text = "menuStrip2";
            menuStrip2.MouseDown += toolStrip1_MouseDown;
            // 
            // cerrarToolStripMenuItem6
            // 
            cerrarToolStripMenuItem6.Alignment = ToolStripItemAlignment.Right;
            cerrarToolStripMenuItem6.Image = Properties.Resources.icon_icons__1_;
            cerrarToolStripMenuItem6.Name = "cerrarToolStripMenuItem6";
            cerrarToolStripMenuItem6.Size = new Size(32, 24);
            cerrarToolStripMenuItem6.Click += cerrarToolStripMenuItem6_Click;
            // 
            // maximizarToolStripMenuItem
            // 
            maximizarToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            maximizarToolStripMenuItem.Image = Properties.Resources.maximizethewindow_theapplication_maximizar_2873;
            maximizarToolStripMenuItem.Name = "maximizarToolStripMenuItem";
            maximizarToolStripMenuItem.Size = new Size(32, 24);
            maximizarToolStripMenuItem.Click += maximizarToolStripMenuItem_Click;
            // 
            // minimizarToolStripMenuItem
            // 
            minimizarToolStripMenuItem.Alignment = ToolStripItemAlignment.Right;
            minimizarToolStripMenuItem.Image = Properties.Resources.minimize_thewindow_theapplication_2872;
            minimizarToolStripMenuItem.Name = "minimizarToolStripMenuItem";
            minimizarToolStripMenuItem.Size = new Size(32, 24);
            minimizarToolStripMenuItem.Click += minimizarToolStripMenuItem_Click;
            // 
            // FormModificar
            // 
            BackColor = Color.FromArgb(20, 20, 20);
            ClientSize = new Size(465, 465);
            Controls.Add(menuStrip2);
            Controls.Add(tabControl);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FormModificar";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Modificar Registros";
            tabControl.ResumeLayout(false);
            tabMaestro.ResumeLayout(false);
            tabMaestro.PerformLayout();
            tabMateria.ResumeLayout(false);
            menuStrip2.ResumeLayout(false);
            menuStrip2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabMaestro, tabMateria;
        private System.Windows.Forms.ComboBox cmbMaestros, cmbMaterias, cmbMaestroAsoc;
        private System.Windows.Forms.TextBox txtNuevoNombre, txtIdMateria, txtNombreM, txtDias, txtHora, txtHDCredito, txtDiasMes, txtInscritos, txtAula, txtSeccion, txtCredito, txtTotalCredito;
        private System.Windows.Forms.Button btnGuardar, btnCancelar;
        private MenuStrip menuStrip2;
        private ToolStripMenuItem cerrarToolStripMenuItem6;
        private ToolStripMenuItem maximizarToolStripMenuItem;
        private ToolStripMenuItem minimizarToolStripMenuItem;
    }
}
