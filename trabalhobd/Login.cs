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
using System.Windows.Input;

namespace trabalhobd
{
    public partial class Login : Form
    {
        private SqlConnection cn;
        public Login()
        {
            InitializeComponent();
        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("Data Source=tcp:mednat.ieeta.pt\\SQLSERVER,8101;Initial Catalog=p1g10;Persist Security Info=True;User ID=p1g10;Password=Bd!complexo1;" + "MultipleActiveResultSets=true;");
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
            String s1 = textBox1.Text;
            String s2 = textBox2.Text;

            cn = getSGBDConnection();

            if (!verifySGBDConnection())
                return;
            

            String commandText = "select COUNT(*) FROM Clube.AppUsers WHERE LoginName = @username";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.Add("@username", SqlDbType.VarChar,100).Value = s1;
            int num = Convert.ToInt32(cmd.ExecuteScalar());
            

            if (num == 0)
            {
                MessageBox.Show("Username Inválido", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Clear();
                textBox2.Clear();
            }
            else
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Clube.Login";
                cmd.Parameters.Clear();
                SqlParameter param1 = new SqlParameter("@LoginName", s1);
                param1.SqlDbType = SqlDbType.NVarChar;
                SqlParameter param2 = new SqlParameter("@Password", s2);
                param2.SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.Add(param1);
                cmd.Parameters.Add(param2);
                cmd.Parameters.Add("@response", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                int res = Convert.ToInt32(cmd.Parameters["@response"].Value);

                cn.Close();
                
                if (res == 0)
                {
                    MessageBox.Show("Password Inválida", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox2.Clear();
                }
                else
                {
                    Form f = new Form1();
                    this.Hide();
                    f.TopMost = true;
                    f.ShowDialog();
                    this.Close();
                }
            }
            cn.Close();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                button1_Click(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Ir para a pagina de registo
        {
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void button3_Click(object sender, EventArgs e) // Registar
        {
            

            if (!verifySGBDConnection())
                return;

            String s1 = textBox1.Text;
            String s2 = textBox2.Text;


            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Necessário introduzir um username e uma password!", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else 
            {


                String commandText = "select COUNT(*) FROM Clube.AppUsers WHERE LoginName = @username";
                SqlCommand cmd = new SqlCommand(commandText, cn);
                cmd.Parameters.Add("@username", SqlDbType.NVarChar, 40).Value = s1;
                int num = Convert.ToInt32(cmd.ExecuteScalar());


                if (num > 0)
                {
                    MessageBox.Show("Username " + s1 + " já existe.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = "";
                }
                else
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Clube.AddUser";
                    cmd.Parameters.Clear();
                    SqlParameter param1 = new SqlParameter("@Login", s1);
                    param1.SqlDbType = SqlDbType.NVarChar;
                    SqlParameter param2 = new SqlParameter("@Password", s2);
                    param2.SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.Add(param1);
                    cmd.Parameters.Add(param2);

                    cmd.Connection = cn;

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Contra criada com sucesso.");
                    textBox1.Clear();
                    textBox2.Clear();

                    label5.Visible = true;
                    label6.Visible = true;
                    label7.Visible = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = false;
                }
                cn.Close();
            }


            

        }
    }
}
