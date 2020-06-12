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
    
    public partial class UpdateStaff : Form
    {
        private String toUpdate;
        private SqlConnection cn;
        public UpdateStaff(String toUpdate, SqlConnection cn)
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
            String commandText = "select nome FROM staff WHERE id_staff=@id";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = toUpdate;
            SqlDataReader reader = cmd.ExecuteReader();
            String nome = " ";
            while (reader.Read())
            {
                nome = reader["nome"].ToString();
            }
            label1.Text = "Actualização do Staff " + nome + ", com o ID " + toUpdate + ":";
            reader.Close();
            cmd.Dispose();
            cn.Close();

        }


        private void dataInitialize()
        {
            if (!verifySGBDConnection())
                return;


            SqlCommand cmd4 = new SqlCommand("select data_termino from Clube.Staff where id_staff = @id ", cn);
            cmd4.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
            cmd4.ExecuteNonQuery();
            SqlDataReader reader = cmd4.ExecuteReader();
            String date = " ";
            while (reader.Read())
            {
                date = reader["data_termino"].ToString();
            }
            int day = Int32.Parse(date.Substring(0, 2));
            int month = Int32.Parse(date.Substring(3, 2));
            int year = Int32.Parse(date.Substring(6, 4));
            dateTimePicker2.Value = new DateTime(year, month, day);

            reader.Close();
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

                //update data termino
                String d = dateTimePicker2.Text.Substring(6, 4) + dateTimePicker2.Text.Substring(3, 2) + dateTimePicker2.Text.Substring(0, 2);
                var date = DateTime.ParseExact(d, "yyyymmdd", null);
                cmd.CommandText = "UPDATE Clube.Staff SET data_termino = @data where id_staff = @id";
                cmd.Parameters.Add("data", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
                cmd.ExecuteNonQuery();

                //update tipo
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    String tipo = textBox1.Text;
                    cmd.CommandText = "UPDATE Clube.Staff SET  tipo = @tipo where id_staff = @id2";
                    cmd.Parameters.Add("tipo", SqlDbType.VarChar, 100).Value = tipo;
                    cmd.Parameters.Add("id2", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();

                MessageBox.Show("Staff Actualizado");

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
