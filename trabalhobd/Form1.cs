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

        public Form1()
        {
            InitializeComponent();
        }

    

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'p1g10DataSet1.jogadores' table. You can move, or remove it, as needed.
            this.jogadoresTableAdapter.Fill(this.p1g10DataSet1.jogadores);
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
            loadJogadores();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de sócios do clube:";
            loadSocios();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de claques do clube:";
            loadClaques();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista do Staff do clube:";
            loadStaff();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Centros de Treino do clube:";
            loadCentrosTreino();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Estádios:";
            loadEstadios();
        }

     

        private void loadJogadores()
        {
            currentselected = "jogador";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from jogadores", cn);
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
            currentselected = "socio";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM socios", cn);
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
            currentselected = "claque";
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM clube.claque", cn);
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
            currentselected = "staff";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clube.Staff", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("ID_Pessoa");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Data Término");

            while (reader.Read())
            {
                Staff S = new Staff();
                S.Id_staff = reader["id_staff"].ToString();
                S.Id_pessoa = reader["id_pessoa"].ToString();
                S.Tipo = reader["tipo"].ToString();
                S.Data_termino = reader["data_termino"].ToString().Substring(0, 10);

                dt.Rows.Add(S.Id_staff, S.Id_pessoa, S.Tipo, S.Data_termino);

            }

            dataGridView1.DataSource = dt;
            cn.Close();
        }

        private void loadCentrosTreino()
        {
            currentselected = "centrotreinos";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clube.CentroTreinos", cn);
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
            currentselected = "estadio";
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM Clube.Estadio", cn);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inauguração");
            dt.Columns.Add("Arquiteto");
            dt.Columns.Add("Lotação");
            dt.Columns.Add("Data Término");

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
    }
}
