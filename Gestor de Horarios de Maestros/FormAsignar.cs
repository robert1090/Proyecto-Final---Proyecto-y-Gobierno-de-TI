using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class FormAsignar : Form
    {
        // Propiedad para detectar el modo de conexión desde los settings del proyecto
        private bool ModoLocal => Properties.Settings.Default.ModoConexion == "Local";

        public FormAsignar()
        {
            InitializeComponent();
            CargarDatos();
            CargarCuatrimestres();
        }

        // --- MÉTODOS DE CONEXIÓN HÍBRIDA ---

        private IDbConnection CrearConexion()
        {
            if (ModoLocal)
            {
                string dbPath = Path.Combine(Application.StartupPath, "LocalData.db");
                return new SQLiteConnection($"Data Source={dbPath};");
            }
            else
            {
                string remoteString = ConfigurationManager.ConnectionStrings["MiConexion"]?.ConnectionString;
                return new MySqlConnection(remoteString);
            }
        }

        private void LlenarDataTable(DataTable dt, string query, IDbConnection con)
        {
            if (ModoLocal)
            {
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(query, (SQLiteConnection)con))
                    da.Fill(dt);
            }
            else
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(query, (MySqlConnection)con))
                    da.Fill(dt);
            }
        }

        private void AñadirParametro(IDbCommand cmd, string nombre, object valor)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }

        // --- LÓGICA DEL FORMULARIO ---

        private void CargarDatos()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();

                    // Cargar Maestros
                    DataTable dtM = new DataTable();
                    LlenarDataTable(dtM, "SELECT IdMaestro, Nombre FROM Maestros ORDER BY Nombre", con);
                    cmbMaestros.DataSource = dtM;
                    cmbMaestros.DisplayMember = "Nombre";
                    cmbMaestros.ValueMember = "IdMaestro";

                    // Cargar Materias
                    DataTable dtMat = new DataTable();
                    LlenarDataTable(dtMat, "SELECT IdMateria, Nombre FROM Materias ORDER BY Nombre", con);
                    cmbMaterias.DataSource = dtMat;
                    cmbMaterias.DisplayMember = "Nombre";
                    cmbMaterias.ValueMember = "IdMateria";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar maestros/materias: " + ex.Message, "Error de Carga");
            }
        }

        private void CargarCuatrimestres()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    LlenarDataTable(dt, "SELECT IdCuatrimestre, Nombre FROM Cuatrimestres ORDER BY IdCuatrimestre DESC", con);

                    cmbCuatrimestre.DataSource = dt;
                    cmbCuatrimestre.DisplayMember = "Nombre";
                    cmbCuatrimestre.ValueMember = "IdCuatrimestre";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error cargando cuatrimestres: " + ex.Message, "Error");
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            // Validación simple
            if (cmbMaestros.SelectedValue == null || cmbMaterias.SelectedValue == null || cmbCuatrimestre.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione todos los campos.", "Validación");
                return;
            }

            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();

                    // Query de actualización (funciona igual en MySQL y SQLite)
                    cmd.CommandText = @"UPDATE Materias 
                                       SET IdMaestro = @idM, 
                                           IdCuatrimestre = @idC 
                                       WHERE IdMateria = @idMat";

                    // Añadimos parámetros usando nuestra función genérica
                    AñadirParametro(cmd, "@idM", cmbMaestros.SelectedValue);
                    AñadirParametro(cmd, "@idC", cmbCuatrimestre.SelectedValue);
                    AñadirParametro(cmd, "@idMat", cmbMaterias.SelectedValue);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Asignación guardada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al realizar la asignación: " + ex.Message, "Error de Base de Datos");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lbl2_Click(object sender, EventArgs e)
        {

        }

        private void lbl1_Click(object sender, EventArgs e)
        {

        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void maximizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void minimizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void MoverVentana()
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        private void toolStrip1_MouseDown(object sender, MouseEventArgs e)
        {
            // Solo permitimos mover si se hace clic con el botón izquierdo
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            }
        }
    }
}