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
    public partial class FormaddSocio1 : Form
    {
        private SqlConnection cn;
        private int nif;

        public FormaddSocio1(int nif, SqlConnection cn)
        {
            this.cn = cn;
            this.nif = nif;
            InitializeComponent();
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
            Socio S = new Socio();
            String commandText = "select * FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@getnif", SqlDbType.Int).Value = nif;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                S.PessoaID = reader["id_pessoa"].ToString();
            }

            cn.Close();
            AddSocio(S);
        }

        private void AddSocio(Socio S)
        {
            {
                if (!verifySGBDConnection())
                    return;

                String d = dateTimePicker2.Text.Substring(6, 4) + dateTimePicker2.Text.Substring(3, 2) + dateTimePicker2.Text.Substring(0, 2);
                var date = DateTime.ParseExact(d, "yyyymmdd", null);
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO Clube.Socio ([id_pessoa],[data_inscricao], [id_claque]) VALUES (@id_pessoa,@data_inscricao,@id_claque);";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@id_pessoa", SqlDbType.Int).Value = S.PessoaID;
                cmd.Parameters.Add("@data_inscricao", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("@id_claque", SqlDbType.Int).Value = textBox3.Text;
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

        }
    }
}
