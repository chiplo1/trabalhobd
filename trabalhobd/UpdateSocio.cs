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
    
    public partial class UpdateSocio : Form
    {

        private String toUpdate;
        private SqlConnection cn;
        public UpdateSocio(String toUpdate, SqlConnection cn)
        {
            InitializeComponent();
            this.toUpdate = toUpdate;
            this.cn = cn;
            labelInitialize();
            dataInitialize();
        }

        private void labelInitialize()
        {
            if (!verifySGBDConnection())
                return;
            String commandText = "select nome FROM socios WHERE id_socio=@id";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = toUpdate;
            SqlDataReader reader = cmd.ExecuteReader();
            String nome = " ";
            while (reader.Read())
            {
                nome = reader["nome"].ToString();
            }
            label1.Text = "Actualização do Sócio " + nome + ", com o ID " + toUpdate + ":";


            cn.Close();
        }

        private void dataInitialize()
        {
            if (!verifySGBDConnection())
                return;


            if (comboBox1.Items.Count < 1)
            {
                SqlCommand cmd2 = new SqlCommand("select distinct nome from Clube.Claque", cn);
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                comboBox1.Items.Clear();
                foreach (DataRow dr2 in dt2.Rows)
                {
                    comboBox1.Items.Add(dr2["nome"].ToString());
                }

            }


            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
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

        

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;


            try
            {

                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    cmd.CommandText = "UPDATE Clube.Socio SET id_claque = NULL where id_socio = @id";
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    cmd.CommandText = "Select id_claque from Clube.Claque where nome = @nome";
                    cmd.Parameters.Add("nome", SqlDbType.VarChar, 100).Value = comboBox1.Text;
                    var id_claque = cmd.ExecuteScalar();
                    cmd.CommandText = "UPDATE Clube.Socio SET id_claque = @id_claque where id_socio = @id";
                    cmd.Parameters.Add("id_claque", SqlDbType.Int).Value = id_claque;
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Sócio Actualizado");

            }
            catch (Exception ex)
            {
                MessageBox.Show("Actualização Falhou!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Failed to update sócio in database. \n ERROR MESSAGE: \n" + ex.Message);
            }
            finally
            {
                cn.Close();
            }


            this.Close();
        }
    }
}
