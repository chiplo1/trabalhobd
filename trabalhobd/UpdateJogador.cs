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
    public partial class UpdateJogador : Form
    {
        private String toUpdate;
        private SqlConnection cn;
        public UpdateJogador(String toUpdate, SqlConnection cn)
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
            String commandText = "select nome FROM jogadores WHERE id_jogador=@id";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = toUpdate;
            SqlDataReader reader = cmd.ExecuteReader();
            String nome = " ";
            while (reader.Read())
            {
                nome = reader["nome"].ToString();
            }
            label1.Text = "Actualização do Jogador " + nome + ", com o ID " + toUpdate + ":";
            reader.Close();
            cmd.Dispose();
            cn.Close();

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


            SqlCommand cmd4 = new SqlCommand("select data_termino from jogadores where id_jogador = @id ", cn);
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

            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            reader.Close();
            cmd4.Dispose();
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
                cmd.CommandText = "UPDATE Clube.Jogador SET data_termino = @data where id_jogador = @id";
                cmd.Parameters.Add("data", SqlDbType.Date).Value = date;
                cmd.Parameters.Add("id", SqlDbType.Int).Value = toUpdate;
                cmd.ExecuteNonQuery();

                //update posicao
                if (!string.IsNullOrEmpty(comboBox1.Text))
                {
                    String posicao = comboBox1.Text;
                    cmd.CommandText = "UPDATE Clube.Jogador SET  posicao = @posicao where id_jogador = @id2";
                    cmd.Parameters.Add("posicao", SqlDbType.VarChar, 30).Value = posicao;
                    cmd.Parameters.Add("id2", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                //update equipa
                if (!string.IsNullOrEmpty(comboBox2.Text))
                {
                    cmd.CommandText = "Select id_equipa from Clube.Equipa where nome = @nome";
                    cmd.Parameters.Add("nome", SqlDbType.VarChar, 100).Value = comboBox2.Text;
                    var id_equipa = cmd.ExecuteScalar();
                    Console.WriteLine(id_equipa);
                    cmd.CommandText = "UPDATE Clube.Jogador SET  id_equipa = @id_equipa where id_jogador = @id3";
                    cmd.Parameters.Add("id_equipa", SqlDbType.Int).Value = (int)id_equipa;
                    cmd.Parameters.Add("id3", SqlDbType.Int).Value = toUpdate;
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit();
                
                MessageBox.Show("Jogador Actualizado");

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

        private void UpdateJogador_Load(object sender, EventArgs e)
        {

        }
    }
}
