namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormAsignar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.ComboBox cmbMaestros;
        private System.Windows.Forms.ComboBox cmbMaterias; // Nuevo ComboBox
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;

        private void InitializeComponent()
        {
            cmbMaestros = new ComboBox();
            cmbMaterias = new ComboBox();
            btnGuardar = new Button();
            lbl1 = new Label();
            lbl2 = new Label();
            cmbCuatrimestre = new ComboBox();
            label1 = new Label();
            SuspendLayout();
            // 
            // cmbMaestros
            // 
            cmbMaestros.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaestros.Location = new Point(20, 40);
            cmbMaestros.Name = "cmbMaestros";
            cmbMaestros.Size = new Size(240, 23);
            cmbMaestros.TabIndex = 0;
            // 
            // cmbMaterias
            // 
            cmbMaterias.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMaterias.Location = new Point(20, 100);
            cmbMaterias.Name = "cmbMaterias";
            cmbMaterias.Size = new Size(240, 23);
            cmbMaterias.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(72, 249);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 30);
            btnGuardar.TabIndex = 2;
            btnGuardar.Text = "Realizar Asignación";
            btnGuardar.Click += btnGuardar_Click;
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new Point(20, 20);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(116, 15);
            lbl1.TabIndex = 3;
            lbl1.Text = "Seleccionar Maestro:";
            // 
            // lbl2
            // 
            lbl2.AutoSize = true;
            lbl2.Location = new Point(20, 80);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(113, 15);
            lbl2.TabIndex = 4;
            lbl2.Text = "Seleccionar Materia:";
            // 
            // cmbCuatrimestre
            // 
            cmbCuatrimestre.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCuatrimestre.Location = new Point(20, 163);
            cmbCuatrimestre.Name = "cmbCuatrimestre";
            cmbCuatrimestre.Size = new Size(240, 23);
            cmbCuatrimestre.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(20, 143);
            label1.Name = "label1";
            label1.Size = new Size(141, 15);
            label1.TabIndex = 6;
            label1.Text = "Seleccionar Cuatrimestre:";
            // 
            // FormAsignar
            // 
            ClientSize = new Size(284, 291);
            Controls.Add(cmbCuatrimestre);
            Controls.Add(label1);
            Controls.Add(cmbMaestros);
            Controls.Add(cmbMaterias);
            Controls.Add(btnGuardar);
            Controls.Add(lbl1);
            Controls.Add(lbl2);
            Name = "FormAsignar";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Asignar Maestro";
            ResumeLayout(false);
            PerformLayout();
        }

        private ComboBox cmbCuatrimestre;
        private Label label1;
    }
}