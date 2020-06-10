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
    public partial class FormaddStaff2 : Form
    {

        private SqlConnection cn;
        private int nif;

        public FormaddStaff2(int nif, SqlConnection cn)
        {
            InitializeComponent();
            this.nif = nif;
            this.cn = cn;


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

        

        private void AddPessoa()
        {
            if (!verifySGBDConnection())
                return;
            String d = dateTimePicker1.Text.Substring(6, 4) + dateTimePicker1.Text.Substring(3, 2) + dateTimePicker1.Text.Substring(0, 2);
            var date = DateTime.ParseExact(d,"yyyymmdd",null);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Clube.Pessoa ([nif], [fname], [lname], [data_nasc]) VALUES (@nif, @fname, @lname, @data_nasc);";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@nif", SqlDbType.Int).Value = nif;
            cmd.Parameters.Add("@fname", SqlDbType.VarChar, 100).Value = textBox1.Text;
            cmd.Parameters.Add("@lname", SqlDbType.VarChar, 100).Value = textBox2.Text;
            cmd.Parameters.Add("@data_nasc", SqlDbType.Date).Value = date;
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

        }
        private void AddStaff(Staff S)
        {
            if (!verifySGBDConnection())
                return;

            String d = dateTimePicker2.Text.Substring(6, 4) + dateTimePicker2.Text.Substring(3, 2) + dateTimePicker2.Text.Substring(0, 2);
            var date = DateTime.ParseExact(d, "yyyymmdd", null);
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "INSERT INTO Clube.Staff ([id_pessoa], [tipo], [data_termino]) VALUES (@id_pessoa,@tipo,@data_termino);";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@id_pessoa", SqlDbType.Int).Value = S.PessoaID;
            cmd.Parameters.Add("@tipo", SqlDbType.VarChar, 100).Value = textBox4.Text;
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            
            AddPessoa();
            if (!verifySGBDConnection())
                return;
            Staff S = new Staff();
            String commandText = "select * FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.Add("@getnif", SqlDbType.Int).Value = nif;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                S.PessoaID = reader["id_pessoa"].ToString();
            }

            cn.Close();
            AddStaff(S);
        }
    }
}
