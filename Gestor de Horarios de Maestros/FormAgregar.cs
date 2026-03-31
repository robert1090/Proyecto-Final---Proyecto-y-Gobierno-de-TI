using System;
using System.Data;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
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
            if (tabControl.SelectedIndex == 1)
                CargarComboMaestros();
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
            if (cmbMaestro.SelectedItem == null)
            {
                MessageBox.Show("Por favor, seleccione un maestro antes de guardar.");
                return;
            }

            try
            {
                int idMaestro = ((ComboItem)cmbMaestro.SelectedItem).Id;

                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = @"INSERT INTO Materias 
                        (IdMateria, IdMaestro, Nombre, DiasImparte, Hora, HD_Credito, DiasMes, TotalCredito, Inscritos, Aula, Seccion, Credito)
                        VALUES (@idMateria, @idMaestro, @nombre, @dias, @hora, @hdCredito, @diasMes, @totalCredito, @inscritos, @aula, @seccion, @credito)";

                    CrearParametro(cmd, "@idMateria", ParseInt(txtIdMateria.Text));
                    CrearParametro(cmd, "@idMaestro", idMaestro);
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