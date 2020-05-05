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
        private int currentselected;

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
            label2.Text = "Lista de jogadores do clube :";
            loadJogadores();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de sócios do clube :";
            loadSocios();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de claques do clube :";
            loadClaques();
        }

        private void loadJogadores()
        {
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

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
    }
}
