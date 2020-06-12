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
    public partial class UpdateEstadio : Form
    {
        private String toUpdate;
        private SqlConnection cn;
        public UpdateEstadio(String toUpdate, SqlConnection cn)
        {
            InitializeComponent();
            this.toUpdate = toUpdate;
            this.cn = cn;
            labelInitialize();
        }
        private void labelInitialize()
        {
            if (!verifySGBDConnection())
                return;
            String commandText = "select nome FROM Clube.Estadio WHERE id_estadio=@id";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = toUpdate;
            SqlDataReader reader = cmd.ExecuteReader();
            String nome = " ";
            while (reader.Read())
            {
                nome = reader["nome"].ToString();
            }
            label1.Text = "Actualização do Estádio " + nome + ", com o ID " + toUpdate + ":";
            reader.Close();
            cmd.Dispose();
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

            SqlTransaction transaction;
            transaction = cn.BeginTransaction("SampleTransaction");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = transaction;

            try
            {

                //update nome
                if (!string.IsNullOrEmpty(textBox1.Text))
                { 
                    String nome = textBox1.Text;
                    cmd.CommandText = "UPDATE Clube.Estadio SET  nome = @nome where id_estadio = @id";
                    cmd.Parameters.Add("nome", SqlDbType.VarChar, 100).Value = nome;
                    cmd.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                //update localizacao
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    String localizacao = textBox2.Text;
                    cmd.CommandText = "UPDATE Clube.Estadio SET  localizacao = @localizacao where id_estadio = @id2";
                    cmd.Parameters.Add("localizacao", SqlDbType.VarChar, 100).Value = localizacao;
                    cmd.Parameters.Add("id2", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                //update lotacao
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    int lotacao = Int32.Parse(textBox3.Text);
                    cmd.CommandText = "UPDATE Clube.Estadio SET  lotacao = @lotacao where id_estadio = @id3";
                    cmd.Parameters.Add("lotacao", SqlDbType.Int).Value = lotacao;
                    cmd.Parameters.Add("id3", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }



                transaction.Commit();

                MessageBox.Show("Estádio Actualizado");

            }
            catch (Exception ex)
            {

                MessageBox.Show("Actualização Falhou!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                Console.WriteLine("  Message: {0}", ex.Message);

                // Attempt to roll back the transaction.
                try
                {
                    transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    // This catch block will handle any errors that may have occurred
                    // on the server that would cause the rollback to fail, such as
                    // a closed connection.
                    Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                    Console.WriteLine("  Message: {0}", ex2.Message);
                }
            }



            this.Close();



        }
    }
}
