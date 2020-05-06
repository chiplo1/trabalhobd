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
    public partial class Form2 : Form
    {
        public Form2(String currentselected, SqlConnection cn)
        {
            this.currentselected = currentselected;
            this.cn = cn;
            InitializeComponent();
        }

        private SqlConnection cn;
        private String currentselected;
        private int getnif;

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
                Form f = new Form3(getnif, cn);
                this.Close();
                f.TopMost = true;
                f.Show();
            }
            else
            {
                Form f2 = new Form4(getnif, cn);
                this.Close();
                f2.TopMost = true;
                f2.Show();
            }
            
        }

        private bool HasPerson()
        {
            getnif = Int32.Parse(textBox1.Text);
            verifySGBDConnection();
            String commandText = "select COUNT(*) FROM Clube.Pessoa WHERE nif=@getnif";
            SqlCommand cmd = new SqlCommand(commandText, cn);
            cmd.Parameters.AddWithValue("@getnif", getnif);
            Int32 b = Convert.ToInt32(cmd.ExecuteScalar());
            cn.Close();
            if (b == 0)
                return false;
            else
                return true;
            
        }
    }
}
