using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalhobd
{
    public partial class FormaddJogador1 : Form
    {
        private SqlConnection cn;
        private int nif;

        public FormaddJogador1(int nif, SqlConnection cn)
        {
            this.cn = cn;
            this.nif = nif;
            InitializeComponent();
            dataInitialize();
        }

        private void dataInitialize()
        {
            if (!verifySGBDConnection())
                return;

            comboBox1.Items.Add("GK");
            comboBox1.Items.Add("DC");
            comboBox1.Items.Add("RB");
            comboBox1.Items.Add("LB");
            comboBox1.Items.Add("DM");
            comboBox1.Items.Add("CM");
            comboBox1.Items.Add("RM");
            comboBox1.Items.Add("LM");
            comboBox1.Items.Add("MO");
            comboBox1.Items.Add("RW");
            comboBox1.Items.Add("LW");
            comboBox1.Items.Add("FW");

            if (comboBox2.Items.Count < 1)
            {
                SqlCommand cmd2 = new SqlCommand("select distinct nome from Clube.Equipa where id_centro_treinos is not NULL", cn);
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                comboBox2.Items.Clear();
                foreach (DataRow dr2 in dt2.Rows)
                {
                    comboBox2.Items.Add(dr2["nome"].ToString());
                }
            }

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            cn.Close();
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;
            Jogador J = new Jogador();
            String commandText = "select * FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@getnif", SqlDbType.Int).Value = nif;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                J.PessoaID = reader["id_pessoa"].ToString();
            }

            cn.Close();
            AddJogador(J);
        }

        private void AddJogador(Jogador J)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            String d = dateTimePicker1.Text.Substring(6, 4) + dateTimePicker1.Text.Substring(3, 2) + dateTimePicker1.Text.Substring(0, 2);
            var date = DateTime.ParseExact(d, "yyyymmdd", null);

            //get id_equipa
            cmd.CommandText = "Select id_equipa from Clube.equipa where nome=@nome";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = comboBox2.Text;
            cmd.Connection = cn;
            int id_equipa = (int)cmd.ExecuteScalar();

            //insert jogador
            cmd.CommandText = "INSERT INTO Clube.Jogador ([id_pessoa], [posicao], [data_termino], [id_equipa]) VALUES (@id_pessoa,@posicao,@data_termino,@id_equipa);";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id_pessoa", SqlDbType.Int).Value = J.PessoaID;
            cmd.Parameters.Add("@posicao", SqlDbType.VarChar,30).Value = comboBox1.Text;
            cmd.Parameters.Add("@id_equipa", SqlDbType.Int).Value = id_equipa;
            cmd.Parameters.Add("@data_termino", SqlDbType.Date).Value = date;
            cmd.Connection = cn;

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to update contact in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }

            this.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FormaddJogador1_Load(object sender, EventArgs e)
        {

        }
    }
}
