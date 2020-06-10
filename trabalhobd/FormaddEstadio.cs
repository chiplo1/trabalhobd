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
    public partial class FormaddEstadio : Form
    {
        private SqlConnection cn;
        public FormaddEstadio(SqlConnection cn)
        {
            InitializeComponent();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            String d = dateTimePicker1.Text.Substring(6, 4) + dateTimePicker1.Text.Substring(3, 2) + dateTimePicker1.Text.Substring(0, 2);
            var date = DateTime.ParseExact(d, "yyyymmdd", null);



            cmd.CommandText = "INSERT INTO Clube.Estadio ([nome], [data_inauguracao], [arquiteto], [lotacao], [localizacao]) VALUES (@nome,@data_inauguracao,@arquiteto,@lotacao,@localizacao);";
            cmd.Parameters.Clear();
            cmd.Parameters.Add("@nome", SqlDbType.VarChar, 100).Value = textBox1.Text;
            cmd.Parameters.Add("@arquiteto", SqlDbType.VarChar,100).Value = textBox2.Text;
            cmd.Parameters.Add("@lotacao", SqlDbType.Int).Value = textBox3.Text;
            cmd.Parameters.Add("@localizacao", SqlDbType.VarChar,100).Value = textBox4.Text;
            cmd.Parameters.Add("@data_inauguracao", SqlDbType.Date).Value = date;
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
