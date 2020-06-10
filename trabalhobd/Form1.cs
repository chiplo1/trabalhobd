using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalhobd
{
    public partial class Form1 : Form
    {

        private SqlConnection cn;
        private String currentselected; //jogador, socio, estadio, centrotreinos, staff, claque
        private DataGridViewSelectedRowCollection selection;

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'p1g10DataSet1.jogadores' table. You can move, or remove it, as needed.
            //this.jogadoresTableAdapter.Fill(this.p1g10DataSet1.jogadores);
            cn = getSGBDConnection();
            loadJogadores();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g10;Persist Security Info=True;User ID=p1g10;Password=Bd!complexo1");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de jogadores do clube:";
            FiltrosJogadores.Visible = true;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            button15_Click(sender, e);
            loadJogadores();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de sócios do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = true;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            button15_Click(sender, e);
            loadSocios();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de claques do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = true;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            button15_Click(sender, e);
            loadClaques();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista do Staff do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = true;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            button15_Click(sender, e);
            loadStaff();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Centros de Treino do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = true;
            FiltrosEstadios.Visible = false;
            button15_Click(sender, e);
            loadCentrosTreino();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Estádios:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = true;
            button15_Click(sender, e);
            loadEstadios();
        }



        private void loadJogadores()
        {
            string filtro = " where 1=1 ";

            currentselected = "jogador";
            if (!verifySGBDConnection())
                return;

            if (comboBox1.Items.Count < 1)
            {
                SqlCommand cmd1 = new SqlCommand("select distinct posicao from jogadores", cn);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                comboBox1.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    comboBox1.Items.Add(dr1["posicao"].ToString());
                }
            }

            if (comboBox2.Items.Count < 1)
            {
                SqlCommand cmd2 = new SqlCommand("select distinct equipa from jogadores", cn);
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                comboBox2.Items.Clear();
                foreach (DataRow dr2 in dt2.Rows)
                {
                    comboBox2.Items.Add(dr2["equipa"].ToString());
                }
            }

            if (comboBox3.Items.Count < 1)
            {
                SqlCommand cmd3 = new SqlCommand("select distinct liga from jogadores", cn);
                cmd3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(dt3);
                comboBox3.Items.Clear();
                foreach (DataRow dr3 in dt3.Rows)
                {
                    comboBox3.Items.Add(dr3["liga"].ToString());
                }
            }

            if (!String.IsNullOrEmpty(textBox1.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (comboBox1.SelectedIndex > -1)
                filtro += " and posicao = '" + comboBox1.SelectedItem + "'";
            if (comboBox2.SelectedIndex > -1)
                filtro += " and equipa = '" + comboBox2.SelectedItem + "'";
            if (comboBox3.SelectedIndex > -1)
                filtro += " and liga = '" + comboBox3.SelectedItem + "'";
            filtro += " and data_termino <= '" + dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand("select * from jogadores" + filtro, cn);
            if (!String.IsNullOrEmpty(textBox1.Text))
                cmd.Parameters.AddWithValue("nome", textBox1.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Posição");
            dt.Columns.Add("Termino Contrato");
            dt.Columns.Add("Equipa");
            dt.Columns.Add("Liga");
            dt.Columns.Add("Data Nascimento");

            while (reader.Read())
            {
                Jogador J = new Jogador();
                J.JogadorID = reader["id_jogador"].ToString();
                J.Pos = reader["posicao"].ToString();
                J.Nome = reader["nome"].ToString();
                J.Dataterm = reader["data_termino"].ToString().Substring(0, 10);
                J.Equipa = reader["equipa"].ToString();
                J.Liga = reader["liga"].ToString();
                J.Datanasc = reader["data_nasc"].ToString().Substring(0, 10);


                dt.Rows.Add(J.JogadorID, J.Nome, J.Pos, J.Dataterm, J.Equipa, J.Liga, J.Datanasc);

            }

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void loadSocios()
        {
            string filtro = " where 1=1 ";

            currentselected = "socio";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM socios" + filtro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inscrição");
            dt.Columns.Add("Data Nascimento");
            dt.Columns.Add("Claque");

            while (reader.Read())
            {
                Socio S = new Socio();
                S.SocioID = reader["id_socio"].ToString();
                S.Nome = reader["nome"].ToString();
                S.DataInsc = reader["data_inscricao"].ToString().Substring(0, 10);
                S.DataNasc = reader["data_nasc"].ToString().Substring(0, 10);
                S.Claque = reader["claque"].ToString();

                dt.Rows.Add(S.SocioID, S.Nome, S.DataInsc, S.DataNasc, S.Claque);
            }

            dataGridView1.DataSource = dt;

            cn.Close();
        }

        private void loadClaques()
        {
            string filtro = " where 1=1 ";

            currentselected = "claque";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM clube.claque" + filtro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Localização Sede");
            dt.Columns.Add("Bancada");

            while (reader.Read())
            {
                Claque C = new Claque();
                C.ClaqueID = reader["id_claque"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Loc = reader["localizacao_sede"].ToString();
                C.Bancada = reader["bancada"].ToString();

                dt.Rows.Add(C.ClaqueID, C.Nome, C.Loc, C.Bancada);
            }

            dataGridView1.DataSource = dt;

            cn.Close();

        }

        private void loadStaff()
        {
            string filtro = " where 1=1 ";

            currentselected = "staff";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM staff" + filtro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Data Término");

            while (reader.Read())
            {
                Staff S = new Staff();
                S.Id_staff = reader["id_staff"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Tipo = reader["tipo"].ToString();
                S.Data_termino = reader["data_termino"].ToString().Substring(0, 10);
                S.Data_nascimento = reader["data_nasc"].ToString().Substring(0, 10);

                dt.Rows.Add(S.Id_staff, S.Nome, S.Tipo, S.Data_termino);

            }

            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void loadCentrosTreino()
        {
            string filtro = " where 1=1 ";

            currentselected = "centrotreinos";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clube.CentroTreinos" + filtro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inauguração");
            dt.Columns.Add("Localização");

            while (reader.Read())
            {
                Centro_de_treino S = new Centro_de_treino();
                S.Id_centro_treinos = reader["id_centro_treinos"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Data_inauguracao = reader["data_inauguracao"].ToString().Substring(0, 10);
                S.Localizacao = reader["localizacao"].ToString();

                dt.Rows.Add(S.Id_centro_treinos, S.Nome, S.Data_inauguracao, S.Localizacao);

            }

            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void loadEstadios()
        {
            string filtro = " where 1=1 ";

            currentselected = "estadio";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clube.Estadio" + filtro, cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inauguração");
            dt.Columns.Add("Arquiteto");
            dt.Columns.Add("Lotação");
            dt.Columns.Add("Localização");

            while (reader.Read())
            {
                Estadio S = new Estadio();
                S.Id_estadio = reader["id_estadio"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Data_inauguracao = reader["data_inauguracao"].ToString().Substring(0, 10);
                S.Arquiteto = reader["arquiteto"].ToString();
                S.Lotacao = reader["lotacao"].ToString();
                S.Localizacao = reader["localizacao"].ToString();

                dt.Rows.Add(S.Id_estadio, S.Nome, S.Data_inauguracao, S.Arquiteto, S.Lotacao, S.Localizacao);

            }

            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //jogador, socio, estadio, centrotreinos, staff, claque
            switch (currentselected)
            {
                case "jogador":
                    Form f = new FormaddNIF(currentselected, cn);
                    f.TopMost = true;
                    f.ShowDialog();
                    break;
                case "staff":
                    Form f2 = new FormaddNIF(currentselected, cn);
                    f2.TopMost = true;
                    f2.ShowDialog();
                    break;
                case "socio":
                    Form f3 = new FormaddNIF(currentselected, cn);
                    f3.TopMost = true;
                    f3.ShowDialog();
                    break;
                case "estadio":
                    Form f4 = new FormaddEstadio(cn);
                    f4.TopMost = true;
                    f4.ShowDialog();
                    break;
                case "centrotreinos":
                    Form f5 = new FormaddCentroTreinos(cn);
                    f5.TopMost = true;
                    f5.ShowDialog();
                    break;
                case "claque":
                    Form f6 = new FormaddClaque(cn);
                    f6.TopMost = true;
                    f6.ShowDialog();
                    break;
                default:
                    break;
            }


        }

        private void button13_Click(object sender, EventArgs e)
        {
            switch (currentselected)
            {
                case "jogador":
                    label2.Text = "Lista de jogadores do clube:";
                    loadJogadores();
                    break;
                case "staff":
                    label2.Text = "Lista do Staff do clube:";
                    loadStaff();
                    break;
                case "socio":
                    label2.Text = "Lista de sócios do clube:";
                    loadSocios();
                    break;
                case "estadio":
                    label2.Text = "Lista dos Estádios:";
                    loadEstadios();
                    break;
                case "centrotreinos":
                    label2.Text = "Lista dos Centros de Treino do clube:";
                    loadCentrosTreino();
                    break;
                case "claque":
                    label2.Text = "Lista de claques do clube:";
                    loadClaques();
                    break;
                default:
                    break;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Width = this.Width - 300;
            dataGridView1.Height = this.Height - 175;
            button13.Left = dataGridView1.Width + dataGridView1.Left + 25;
            button14.Left = dataGridView1.Width + dataGridView1.Left + 25;
            button13.Top = this.Height - 110;
            FiltrosJogadores.Left = dataGridView1.Width + dataGridView1.Left + 25; 
            FiltrosSocios.Left = dataGridView1.Width + dataGridView1.Left + 25;
            FiltrosClaques.Left = dataGridView1.Width + dataGridView1.Left + 25;
            FiltrosStaff.Left = dataGridView1.Width + dataGridView1.Left + 25; 
            FiltrosCentroTreinos.Left = dataGridView1.Width + dataGridView1.Left + 25; 
            FiltrosEstadios.Left = dataGridView1.Width + dataGridView1.Left + 25; 
        }

        private void button8_Click(object sender, EventArgs e) // TO DO - BOTÂO EDITAR
        {
            if (!verifySGBDConnection())
                return;

            selection = dataGridView1.SelectedRows;

            if (selection.Count == 0)
            {
                MessageBox.Show("Para editar tem que ser selecionar uma linha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selection.Count > 1)
            {
                MessageBox.Show("Para editar só pode selecionar uma linha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (currentselected)
            {
                case "jogador":
                    break;
                case "staff":
                    break;
                case "socio":
                    break;
                case "estadio":
                    break;
                case "centrotreinos":
                    break;
                case "claque":
                    break;
                default:
                    break;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            selection = dataGridView1.SelectedRows;

            if (selection.Count <= 0)
            {
                MessageBox.Show("É necessario selecionar pelo menos uma linha a remover!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lista com todos os ids a remover na respetiva tabela
            String toRemove = "( ";
            for (int i = selection.Count - 1; i > 0; i--)
            {
                toRemove = toRemove + dataGridView1.SelectedRows[i].Cells[0].Value.ToString() + ",";
            }

            toRemove = toRemove + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + " )";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            DialogResult dialogResult = MessageBox.Show("Tem a certeza que quer apagar da tabela " + currentselected  + " os elementos com os ids " + toRemove , "Apagar elementos permanentemente", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            switch (currentselected)
            {
                case "jogador":
                    cmd.CommandText = "delete from clube.jogador where id_jogador in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadJogadores();
                    break;
                case "staff":
                    cmd.CommandText = "delete from clube.staff where id_staff in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadStaff();
                    break;
                case "socio":
                    cmd.CommandText = "delete from clube.socio where id_socio in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadSocios();
                    break;
                case "estadio":
                    cmd.CommandText = "delete from clube.estadio where id_estadio in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadEstadios();
                    break;
                case "centrotreinos":
                    cmd.CommandText = "delete from clube.centrotreinos where id_centro_treinos in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadCentrosTreino();
                    break;
                case "claque":
                    cmd.CommandText = "delete from clube.claque where id_claque in " + toRemove;
                    cmd.ExecuteNonQuery();
                    loadClaques();
                    break;
                default:
                    break;
            }

                cn.Close();

        }

        private void button14_Click(object sender, EventArgs e) // TO DO - BOTÂO FILTRAR
        {
            if (!verifySGBDConnection())
                return;

            switch (currentselected)
            {
                case "jogador":
                    loadJogadores();
                    break;
                case "staff":
                    loadStaff();
                    break;
                case "socio":
                    loadSocios();
                    break;
                case "estadio":
                    loadEstadios();
                    break;
                case "centrotreinos":
                    loadCentrosTreino();
                    break;
                case "claque":
                    loadClaques();
                    break;
                default:
                    break;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // FILTROS JOGADOR
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker3.Value = new DateTime(2030, 01, 01);
            dateTimePicker1.Value = new DateTime(1900, 01, 01);
            dateTimePicker2.Value = DateTime.Today;

            // FILTROS SOCIOS
            textBox2.Text = "";
            comboBox6.SelectedIndex = -1;
            dateTimePicker7.Value = new DateTime(1900, 01, 01);
            dateTimePicker6.Value = DateTime.Today;
            dateTimePicker5.Value = new DateTime(1900, 01, 01);
            dateTimePicker4.Value = DateTime.Today;

            // FILTROS CLAQUES
            textBox3.Text = "";
            textBox9.Text = "";
            comboBox8.SelectedIndex = -1;

            // FILTROS STAFF
            textBox4.Text = "";
            comboBox12.SelectedIndex = -1;
            dateTimePicker8.Value = new DateTime(2030, 01, 01);

            // FILTROS CENTROS TREINO
            textBox5.Text = "";
            textBox7.Text = "";
            dateTimePicker10.Value = new DateTime(1900, 01, 01);
            dateTimePicker9.Value = DateTime.Today;

            // FILTROS ESTADIOS
            textBox6.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            dateTimePicker12.Value = new DateTime(1900, 01, 01);
            dateTimePicker11.Value = DateTime.Today;

            button14_Click(sender, e);
        }
    }

}
