namespace Gestor_de_Horarios_de_Maestros
{
    partial class FormBuscar
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtSeccion;
        private System.Windows.Forms.TextBox txtDia;
        private System.Windows.Forms.TextBox txtCredito;
        private System.Windows.Forms.TextBox txtHora;
        private System.Windows.Forms.Label lbl1;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.Button btnCancelar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) { components.Dispose(); }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtSeccion = new TextBox();
            txtDia = new TextBox();
            txtCredito = new TextBox();
            txtHora = new TextBox();
            lbl1 = new Label();
            lbl2 = new Label();
            lbl3 = new Label();
            lbl4 = new Label();
            btnAceptar = new Button();
            btnCancelar = new Button();
            SuspendLayout();
            // 
            // txtSeccion
            // 
            txtSeccion.Location = new Point(100, 17);
            txtSeccion.Name = "txtSeccion";
            txtSeccion.Size = new Size(150, 23);
            txtSeccion.TabIndex = 0;
            // 
            // txtDia
            // 
            txtDia.Location = new Point(100, 47);
            txtDia.Name = "txtDia";
            txtDia.Size = new Size(150, 23);
            txtDia.TabIndex = 1;
            // 
            // txtCredito
            // 
            txtCredito.Location = new Point(100, 77);
            txtCredito.Name = "txtCredito";
            txtCredito.Size = new Size(150, 23);
            txtCredito.TabIndex = 2;
            // 
            // txtHora
            // 
            txtHora.Location = new Point(100, 107);
            txtHora.Name = "txtHora";
            txtHora.Size = new Size(150, 23);
            txtHora.TabIndex = 3;
            // 
            // lbl1
            // 
            lbl1.Location = new Point(20, 20);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(100, 23);
            lbl1.TabIndex = 4;
            lbl1.Text = "Sección:";
            // 
            // lbl2
            // 
            lbl2.Location = new Point(20, 50);
            lbl2.Name = "lbl2";
            lbl2.Size = new Size(100, 23);
            lbl2.TabIndex = 5;
            lbl2.Text = "Día:";
            // 
            // lbl3
            // 
            lbl3.Location = new Point(20, 80);
            lbl3.Name = "lbl3";
            lbl3.Size = new Size(100, 23);
            lbl3.TabIndex = 6;
            lbl3.Text = "Crédito:";
            // 
            // lbl4
            // 
            lbl4.Location = new Point(20, 110);
            lbl4.Name = "lbl4";
            lbl4.Size = new Size(100, 23);
            lbl4.TabIndex = 7;
            lbl4.Text = "Hora:";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(40, 150);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.Size = new Size(75, 23);
            btnAceptar.TabIndex = 8;
            btnAceptar.Text = "Buscar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(140, 150);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(75, 23);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // FormBuscar
            // 
            ClientSize = new Size(280, 200);
            Controls.Add(txtSeccion);
            Controls.Add(txtDia);
            Controls.Add(txtCredito);
            Controls.Add(txtHora);
            Controls.Add(lbl1);
            Controls.Add(lbl2);
            Controls.Add(lbl3);
            Controls.Add(lbl4);
            Controls.Add(btnAceptar);
            Controls.Add(btnCancelar);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormBuscar";
            StartPosition = FormStartPosition.CenterParent;
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
