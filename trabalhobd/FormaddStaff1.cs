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
    public partial class FormaddStaff1 : Form
    {

        private SqlConnection cn;
        private int nif;

        public FormaddStaff1(int nif, SqlConnection cn)
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(!verifySGBDConnection())
                return;
            Staff S = new Staff();
            String commandText = "select * FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@getnif", nif);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                S.Id_pessoa = reader["id_pessoa"].ToString();
            }

            cn.Close();
            AddStaff(S);

        }

        private void AddStaff(Staff S)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            String d = dateTimePicker1.Text.Substring(6,4) + dateTimePicker1.Text.Substring(3, 2) + dateTimePicker1.Text.Substring(0, 2);


            cmd.CommandText = "INSERT INTO Clube.Staff ([id_pessoa], [tipo], [data_termino]) VALUES (@id_pessoa,@tipo,@data_termino);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id_pessoa", S.Id_pessoa);
            cmd.Parameters.AddWithValue("@tipo", textBox1.Text);
            cmd.Parameters.AddWithValue("@data_termino",d);
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

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
