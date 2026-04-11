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
    public partial class FormModificar : Form
    {
        // Propiedad para detectar el modo de conexión
        private bool ModoLocal => Properties.Settings.Default.ModoConexion == "Local";

        public FormModificar()
        {
            InitializeComponent();
            // Ejecutamos la configuración manual para que el Designer no la borre
            ConfigurarDisenoPersonalizado();
            CargarCombosIniciales();
        }

        private void ConfigurarDisenoPersonalizado()
        {
            // TAB MAESTRO - Labels manuales
            AgregarL(tabMaestro, "Seleccionar maestro:", 15, 45);
            AgregarL(tabMaestro, "Nuevo nombre:", 15, 105);

            // TAB MATERIA - Labels y Posicionamiento
            AgregarL(tabMateria, "1. Elija Materia a editar:", 15, 15);

            // Aquí usamos tu método Config para reubicar los TXT y crear sus Labels
            Config(txtIdMateria, "ID Materia:", 15, 75, 110, 80, tabMateria);
            Config(cmbMaestroAsoc, "Maestro:", 210, 75, 270, 125, tabMateria);

            Config(txtNombreM, "Materia:", 15, 110, 110, 285, tabMateria);
            Config(txtDias, "Días:", 15, 145, 110, 285, tabMateria);

            Config(txtHora, "Hora:", 15, 180, 110, 90, tabMateria);
            Config(txtAula, "Aula:", 220, 180, 280, 115, tabMateria);

            Config(txtHDCredito, "H/D Cred:", 15, 215, 110, 90, tabMateria);
            Config(txtSeccion, "Sección:", 220, 215, 280, 115, tabMateria);

            Config(txtDiasMes, "Días/Mes:", 15, 250, 110, 90, tabMateria);
            Config(txtCredito, "Créditos:", 220, 250, 280, 115, tabMateria);

            Config(txtTotalCredito, "Total Cred:", 15, 285, 110, 90, tabMateria);
            Config(txtInscritos, "Inscritos:", 220, 285, 280, 115, tabMateria);

            // Forzamos que los controles estén dentro de sus pestañas (por si el designer los sacó)
            if (!tabMaestro.Controls.Contains(cmbMaestros)) tabMaestro.Controls.Add(cmbMaestros);
            if (!tabMaestro.Controls.Contains(txtNuevoNombre)) tabMaestro.Controls.Add(txtNuevoNombre);
        }

        // Asegúrate de que estos métodos existan en FormModificar.cs (puedes moverlos desde el designer)
        private void Config(System.Windows.Forms.Control c, string t, int lx, int ty, int tx, int tw, System.Windows.Forms.Control p)
        {
            System.Windows.Forms.Label l = new System.Windows.Forms.Label { Text = t, Location = new System.Drawing.Point(lx, ty + 3), AutoSize = true, ForeColor = System.Drawing.Color.White };
            c.Location = new System.Drawing.Point(tx, ty);
            c.Width = tw;
            p.Controls.Add(l);
            p.Controls.Add(c);
        }

        private void AgregarL(System.Windows.Forms.Control p, string t, int x, int y)
        {
            p.Controls.Add(new System.Windows.Forms.Label { Text = t, Location = new System.Drawing.Point(x, y), AutoSize = true, ForeColor = System.Drawing.Color.White });
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

        private void tabMaestro_Click(object sender, EventArgs e)
        {

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

        private void cerrarToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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