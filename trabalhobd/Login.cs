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
            
            String commandText = "select COUNT(*) FROM Clube.AppUsers WHERE username = @username";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.Add("@username", SqlDbType.VarChar,100).Value = s1;
            int num = Convert.ToInt32(cmd.ExecuteScalar());
            

            if (num == 0)
            {
                MessageBox.Show("Username Inválido", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                commandText = "select pass FROM Clube.AppUsers WHERE username = @username";
                cmd = new SqlCommand(commandText, cn);
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = s1;
                String pass = (String)cmd.ExecuteScalar();
                cn.Close();
                if (!s2.Equals(pass))
                {
                    MessageBox.Show("Password Inválida", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            textBox1.Text = "";
            textBox2.Text = "";

            String commandText = "select COUNT(*) FROM Clube.AppUsers WHERE username = @username";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.Add("@username", SqlDbType.VarChar, 100).Value = s1;
            int num = Convert.ToInt32(cmd.ExecuteScalar());


            if (num > 0)
            {
                MessageBox.Show("Username " + s1 + " já existe.", "Erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
            }
            else
            {
                cmd.CommandText = "INSERT INTO Clube.Appusers ([username], [pass]) VALUES (@username,@pass);";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@username", SqlDbType.VarChar, 30).Value = s1;
                cmd.Parameters.Add("@pass", SqlDbType.VarChar, 30).Value = s2;
                cmd.Connection = cn;

                cmd.ExecuteNonQuery();

                MessageBox.Show("Contra criada com sucesso.");

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
