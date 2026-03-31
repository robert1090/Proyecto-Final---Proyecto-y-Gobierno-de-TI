using System;
using System.Configuration;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.IO;

namespace Gestor_de_Horarios_de_Maestros
{
    public partial class FormModificar : Form
    {
        // Propiedad para detectar el modo de conexión
        private bool ModoLocal => Properties.Settings.Default.ModoConexion == "Local";

        public FormModificar()
        {
            InitializeComponent();
            CargarCombosIniciales();
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
                    throw new Exception("No se encontró la cadena de conexión 'MiConexion' en App.config.");
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

        // --- LÓGICA DE CARGA ---

        private void CargarCombosIniciales()
        {
            CargarComboMaestros(cmbMaestros);
            CargarComboMaestros(cmbMaestroAsoc);
            CargarComboMaterias();
        }

        private void CargarComboMaestros(ComboBox cb)
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT IdMaestro, Nombre FROM Maestros ORDER BY Nombre";
                    using (IDataReader r = cmd.ExecuteReader())
                    {
                        cb.Items.Clear();
                        while (r.Read()) cb.Items.Add(new ComboItem(r.GetInt32(0), r.GetString(1)));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error maestros: " + ex.Message); }
        }

        private void CargarComboMaterias()
        {
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT IdMateria, Nombre FROM Materias ORDER BY Nombre";
                    using (IDataReader r = cmd.ExecuteReader())
                    {
                        cmbMaterias.Items.Clear();
                        while (r.Read()) cmbMaterias.Items.Add(new ComboItem(r.GetInt32(0), r.GetString(1)));
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error materias: " + ex.Message); }
        }

        private List<HorarioSimple> ObtenerHorariosExistentes(int idMateriaActual)
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
                                     INNER JOIN Maestros t2 ON t1.IdMaestro = t2.IdMaestro
                                     WHERE t1.IdMateria != @idActual";
                    CrearParametro(cmd, "@idActual", idMateriaActual);

                    using (IDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            string horaDb = r["Hora"].ToString();
                            int horaI = 0;
                            try
                            {
                                string parte = horaDb.Contains("-") ? horaDb.Split('-')[0].Trim() : horaDb.Trim();
                                horaI = TimeSpan.Parse(parte).Hours;
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
            catch (Exception ex) { MessageBox.Show("Error validación: " + ex.Message); }
            return lista;
        }

        private void CmbMaterias_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!(cmbMaterias.SelectedItem is ComboItem item)) return;
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "SELECT * FROM Materias WHERE IdMateria = @id";
                    CrearParametro(cmd, "@id", item.Id);

                    using (IDataReader r = cmd.ExecuteReader())
                    {
                        if (r.Read())
                        {
                            txtIdMateria.Text = r["IdMateria"].ToString();
                            txtNombreM.Text = r["Nombre"].ToString();
                            txtDias.Text = r["DiasImparte"].ToString();
                            txtHora.Text = r["Hora"].ToString();
                            txtHDCredito.Text = r["HD_Credito"].ToString();
                            txtDiasMes.Text = r["DiasMes"].ToString();
                            txtTotalCredito.Text = r["TotalCredito"].ToString();
                            txtInscritos.Text = r["Inscritos"].ToString();
                            txtAula.Text = r["Aula"].ToString();
                            txtSeccion.Text = r["Seccion"].ToString();
                            txtCredito.Text = r["Credito"].ToString();

                            int idM = Convert.ToInt32(r["IdMaestro"]);
                            foreach (ComboItem m in cmbMaestroAsoc.Items)
                            {
                                if (m.Id == idM) { cmbMaestroAsoc.SelectedItem = m; break; }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (tabControl.SelectedIndex == 0) GuardarMaestro();
            else GuardarMateria();
        }

        private void GuardarMaestro()
        {
            if (!(cmbMaestros.SelectedItem is ComboItem item)) return;
            try
            {
                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE Maestros SET Nombre=@n WHERE IdMaestro=@id";
                    CrearParametro(cmd, "@n", txtNuevoNombre.Text.Trim());
                    CrearParametro(cmd, "@id", item.Id);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Maestro actualizado.");
                    CargarCombosIniciales();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void GuardarMateria()
        {
            if (!(cmbMaterias.SelectedItem is ComboItem selector) || !(cmbMaestroAsoc.SelectedItem is ComboItem maestro)) return;

            try
            {
                List<HorarioSimple> listaHorarios = ObtenerHorariosExistentes(selector.Id);
                string horaTexto = txtHora.Text.Contains("-") ? txtHora.Text.Split('-')[0].Trim() : txtHora.Text.Trim();
                int horaDigitada = TimeSpan.Parse(horaTexto).Hours;

                var nuevo = new HorarioSimple
                {
                    Maestro = maestro.Nombre,
                    Dia = txtDias.Text,
                    HoraInicio = horaDigitada,
                    HoraFin = horaDigitada + 1
                };

                if (ValidadorHorarios.HayChoque(nuevo, listaHorarios, out string mensaje))
                {
                    MessageBox.Show(mensaje, "Choque de horario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (IDbConnection con = CrearConexion())
                {
                    con.Open();
                    IDbCommand cmd = con.CreateCommand();
                    cmd.CommandText = @"UPDATE Materias SET IdMateria=@newId, IdMaestro=@idM, Nombre=@nom, DiasImparte=@d, 
                                       Hora=@h, HD_Credito=@hd, DiasMes=@dm, TotalCredito=@tot, Inscritos=@ins, 
                                       Aula=@a, Seccion=@s, Credito=@c WHERE IdMateria=@oldId";

                    CrearParametro(cmd, "@newId", txtIdMateria.Text);
                    CrearParametro(cmd, "@idM", maestro.Id);
                    CrearParametro(cmd, "@nom", txtNombreM.Text);
                    CrearParametro(cmd, "@d", txtDias.Text);
                    CrearParametro(cmd, "@h", txtHora.Text.Trim());
                    CrearParametro(cmd, "@hd", ParseInt(txtHDCredito.Text));
                    CrearParametro(cmd, "@dm", ParseInt(txtDiasMes.Text));
                    CrearParametro(cmd, "@tot", ParseInt(txtTotalCredito.Text));
                    CrearParametro(cmd, "@ins", ParseInt(txtInscritos.Text));
                    CrearParametro(cmd, "@a", txtAula.Text);
                    CrearParametro(cmd, "@s", txtSeccion.Text);
                    CrearParametro(cmd, "@c", ParseInt(txtCredito.Text));
                    CrearParametro(cmd, "@oldId", selector.Id);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Materia actualizada correctamente.");
                    this.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private object ParseInt(string v) => int.TryParse(v, out int i) ? (object)i : DBNull.Value;
        private void BtnCancelar_Click(object sender, EventArgs e) => this.Close();
        private void CmbMaestros_SelectedIndexChanged(object sender, EventArgs e) { if (cmbMaestros.SelectedItem is ComboItem i) txtNuevoNombre.Text = i.Nombre; }
    }
}