using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class FormAgregar : Form
    {
        // Detecta el modo de conexión
        private bool ModoLocal => Properties.Settings.Default.ModoConexion == "Local";

        public FormAgregar()
        {
            InitializeComponent();
            cmbCuatrimestre.DropDownStyle = ComboBoxStyle.DropDownList;
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
                var settings = ConfigurationManager.ConnectionStrings["MiConexion"];
                if (settings == null)
                    throw new Exception("No se encontró la cadena de conexión 'MiConexion' en el archivo App.config.");

                return new MySqlConnection(settings.ConnectionString);
            }
        }

        private void CrearParametro(IDbCommand cmd, string nombre, object valor)
        {
            IDbDataParameter param = cmd.CreateParameter();
            param.ParameterName = nombre;
            param.Value = valor ?? DBNull.Value;
            cmd.Parameters.Add(param);
        }

        // --- EVENTOS Y LÓGICA ---

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Índice 0: Maestro, Índice 1: Cuatrimestre, Índice 2: Materia
            if (tabControl.SelectedIndex == 2)
            {
                CargarComboMaestros();
                CargarComboCuatrimestres(); // Este es el método que creamos anteriormente
            }
        }

        private void CargarComboMaestros()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT IdMaestro, Nombre FROM Maestros ORDER BY Nombre";

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        cmbMaestro.Items.Clear();
                        while (reader.Read())
                            cmbMaestro.Items.Add(new ComboItem(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
                if (cmbMaestro.Items.Count > 0) cmbMaestro.SelectedIndex = 0;
            }
            catch (Exception ex) { MessageBox.Show("Error al cargar maestros: " + ex.Message); }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0) GuardarMaestro();
            else if (tabControl.SelectedIndex == 1) GuardarCuatrimestre();
            else if (tabControl.SelectedIndex == 2) GuardarMateria();
        }

        private void GuardarCuatrimestre()
        {
            if (string.IsNullOrWhiteSpace(txtNombreCuatrimestre.Text)) return;

            try
            {
                using (IDbConnection con = CrearConexion()) // Cambiado a CrearConexion()
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Cuatrimestres (Nombre, FechaInicio, FechaFin) VALUES (@nom, @ini, @fin)";

                    CrearParametro(cmd, "@nom", txtNombreCuatrimestre.Text);
                    CrearParametro(cmd, "@ini", dtpInicio.Value);
                    CrearParametro(cmd, "@fin", dtpFin.Value);

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Cuatrimestre guardado con éxito.");
                txtNombreCuatrimestre.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void GuardarMaestro()
        {
            if (string.IsNullOrWhiteSpace(txtNombreMaestro.Text)) return;

            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "INSERT INTO Maestros (Nombre) VALUES (@nombre)";
                    CrearParametro(cmd, "@nombre", txtNombreMaestro.Text.Trim());
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Maestro agregado correctamente.");
                txtNombreMaestro.Clear();
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar maestro: " + ex.Message); }
        }

        private List<HorarioSimple> ObtenerHorariosExistentes()
        {
            List<HorarioSimple> lista = new List<HorarioSimple>();
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = @"SELECT t2.Nombre as Maestro, t1.DiasImparte, t1.Hora 
                                      FROM Materias t1 
                                      INNER JOIN Maestros t2 ON t1.IdMaestro = t2.IdMaestro";

                    using (IDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string horaDb = r["Hora"].ToString();
                            int horaI = 0;
                            try
                            {
                                string primeraParte = horaDb.Contains("-") ? horaDb.Split('-')[0].Trim() : horaDb;
                                horaI = TimeSpan.Parse(primeraParte).Hours;
                            }
                            catch { }

                            lista.Add(new HorarioSimple
                            {
                                Maestro = r["Maestro"].ToString(),
                                Dia = r["DiasImparte"].ToString(),
                                HoraInicio = horaI,
                                HoraFin = horaI + 1
                            });
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error de validación: " + ex.Message); }
            return lista;
        }

        private void GuardarMateria()
        {
            if (cmbMaestro.SelectedItem == null || cmbCuatrimestre.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un maestro y un cuatrimestre antes de guardar.");
                return;
            }

            try
            {
                int idMaestro = ((ComboItem)cmbMaestro.SelectedItem).Id;
                int idCuatrimestre = ((ComboItem)cmbCuatrimestre.SelectedItem).Id;

                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();

                    // Agregamos IdCuatrimestre a la consulta y a los valores
                    cmd.CommandText = @"INSERT INTO Materias 
                (IdMateria, IdMaestro, IdCuatrimestre, Nombre, DiasImparte, Hora, HD_Credito, DiasMes, TotalCredito, Inscritos, Aula, Seccion, Credito)
                VALUES (@idMateria, @idMaestro, @idCuatrimestre, @nombre, @dias, @hora, @hdCredito, @diasMes, @totalCredito, @inscritos, @aula, @seccion, @credito)";

                    CrearParametro(cmd, "@idMateria", ParseInt(txtIdMateria.Text));
                    CrearParametro(cmd, "@idMaestro", idMaestro);
                    CrearParametro(cmd, "@idCuatrimestre", idCuatrimestre); // Nuevo parámetro
                    CrearParametro(cmd, "@nombre", txtNombreMateria.Text.Trim());
                    CrearParametro(cmd, "@dias", txtDias.Text.Trim());
                    CrearParametro(cmd, "@hora", txtHora.Text.Trim());
                    CrearParametro(cmd, "@hdCredito", ParseInt(txtHDCredito.Text));
                    CrearParametro(cmd, "@diasMes", ParseInt(txtDiasMes.Text));
                    CrearParametro(cmd, "@totalCredito", ParseInt(txtTotalCredito.Text));
                    CrearParametro(cmd, "@inscritos", ParseInt(txtInscritos.Text));
                    CrearParametro(cmd, "@aula", txtAula.Text.Trim());
                    CrearParametro(cmd, "@seccion", txtSeccion.Text.Trim());
                    CrearParametro(cmd, "@credito", ParseInt(txtCredito.Text));

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Materia agregada correctamente.");
                LimpiarTabMateria();
            }
            catch (Exception ex) { MessageBox.Show("Error al guardar materia: " + ex.Message); }
        }

        private void BtnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void LimpiarTabMateria()
        {
            txtIdMateria.Clear(); txtNombreMateria.Clear(); txtDias.Clear(); txtHora.Clear();
            txtHDCredito.Clear(); txtDiasMes.Clear(); txtTotalCredito.Clear(); txtInscritos.Clear();
            txtAula.Clear(); txtSeccion.Clear(); txtCredito.Clear();
        }

        private object ParseInt(string val) => int.TryParse(val, out int i) ? (object)i : DBNull.Value;

        private void tabMaestro_Click(object sender, EventArgs e)
        {

        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {

        }

        private void txtSeccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblNombreMaestro_Click(object sender, EventArgs e)
        {

        }

        private void txtNombreCuatrimestre_TextChanged(object sender, EventArgs e)
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

        private void CargarComboCuatrimestres()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT IdCuatrimestre, Nombre FROM Cuatrimestres ORDER BY IdCuatrimestre DESC";

                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        cmbCuatrimestre.Items.Clear(); // Asegúrate de que el control se llame cmbCuatrimestre
                        while (reader.Read())
                        {
                            cmbCuatrimestre.Items.Add(new ComboItem(reader.GetInt32(0), reader.GetString(1)));
                        }
                    }
                }
                if (cmbCuatrimestre.Items.Count > 0) cmbCuatrimestre.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cuatrimestres: " + ex.Message);
            }
        }

    }



    public class ComboItem
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ComboItem(int id, string nombre)
        {
            Id = id;
            Nombre = nombre;
        }
        public override string ToString() => Nombre;
    }
}