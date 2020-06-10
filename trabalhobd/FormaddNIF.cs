using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace trabalhobd
{
    public partial class FormaddNIF : Form
    {
        public FormaddNIF(String currentselected, SqlConnection cn)
        {
            this.currentselected = currentselected;
            this.cn = cn;
            InitializeComponent();
        }

        private SqlConnection cn;
        private String currentselected;
        private int getnif;
        private int pessoaid;

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
            if (HasPerson())
            {
                String commandText;
                SqlCommand cmd;
                Int32 num;
                switch (currentselected)
                {
                    case "jogador":
                        verifySGBDConnection();
                        commandText = "select COUNT(*) FROM Clube.Jogador WHERE id_pessoa=@pessoaid";
                        cmd = new SqlCommand(commandText, cn);
                        cmd.Parameters.AddWithValue("@pessoaid", SqlDbType.Int).Value = pessoaid;
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cn.Close();
                        if (num == 0)
                        {
                            Form f = new FormaddJogador1(getnif, cn);
                            this.Close();
                            f.TopMost = true;
                            f.Show();
                        }
                        else
                            MessageBox.Show("Já existe um jogador com este NIF!");
                        break;
                    case "staff":
                        verifySGBDConnection();
                        commandText = "select COUNT(*) FROM Clube.Staff WHERE id_pessoa=@pessoaid";
                        cmd = new SqlCommand(commandText, cn);
                        cmd.Parameters.AddWithValue("@pessoaid", SqlDbType.Int).Value = pessoaid;
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cn.Close();
                        if (num == 0)
                        {
                            Form f2 = new FormaddStaff1(getnif, cn);
                            this.Close();
                            f2.TopMost = true;
                            f2.ShowDialog();
                        }
                        else
                            MessageBox.Show("Já existe um membro do staff com este NIF!");
                        break;
                    case "socio":
                        verifySGBDConnection();
                        commandText = "select COUNT(*) FROM Clube.Socio WHERE id_pessoa=@pessoaid";
                        cmd = new SqlCommand(commandText, cn);
                        cmd.Parameters.AddWithValue("@pessoaid", SqlDbType.Int).Value = pessoaid;
                        num = Convert.ToInt32(cmd.ExecuteScalar());
                        cn.Close();
                        if (num == 0)
                        {
                            Form f3 = new FormaddSocio1(getnif, cn);
                            this.Close();
                            f3.TopMost = true;
                            f3.ShowDialog();
                        }
                        else
                            MessageBox.Show("Já existe um sócio com este NIF!");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (currentselected)
                {
                    case "jogador":
                        Form f = new FormaddJogador2(getnif, cn);
                        this.Close();
                        f.TopMost = true;
                        f.Show();
                        break;
                    case "staff":
                        Form f2 = new FormaddStaff2(getnif, cn);
                        this.Close();
                        f2.TopMost = true;
                        f2.ShowDialog();
                        break;
                    case "socio":
                        Form f3 = new FormaddSocio2(getnif, cn);
                        this.Close();
                        f3.TopMost = true;
                        f3.ShowDialog();
                        break;
                    default:
                        break;
                }
            }
            
        }


        private bool HasPerson()
        {
         
            getnif = Int32.Parse(textBox1.Text);

            verifySGBDConnection();
            String commandText = "select COUNT(*) FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@getnif", SqlDbType.Int).Value = getnif;
            Int32 b = Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            if (b == 0)
                return false;
            else
                verifySGBDConnection();
                commandText = "select id_pessoa FROM Clube.Pessoa WHERE nif=@getnif";
                cmd = new SqlCommand(commandText, cn);
                cmd.Parameters.AddWithValue("@getnif", SqlDbType.Int).Value = getnif;
                pessoaid = Convert.ToInt32(cmd.ExecuteScalar());
            return true;
            
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
            var isNumeric = int.TryParse(textBox1.Text, out _);
            if (!isNumeric)
            {
                MessageBox.Show("O NIF introduzido tem de ser um valor numérico!");
                textBox1.Clear();
            }
            else if (textBox1.Text.Length != 9)
            {
                MessageBox.Show("O NIF deve ter 9 caracteres numéricos.");
                textBox1.Clear();
                
            }
        }
    }
}
