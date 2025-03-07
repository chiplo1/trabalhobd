﻿using System;
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
    public partial class Form1 : Form
    {

        private SqlConnection cn;
        private String currentselected; //jogador, socio, estadio, centrotreinos, staff, claque
        private DataGridViewSelectedRowCollection selection;

        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'p1g10DataSet1.jogadores' table. You can move, or remove it, as needed.
            //this.jogadoresTableAdapter.Fill(this.p1g10DataSet1.jogadores);
            cn = getSGBDConnection();
            loadJogadores();
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

        private void button4_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de jogadores do clube:";
            FiltrosJogadores.Visible = true;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            checkBox1.Enabled = true;
            button15_Click(sender, e);
            loadJogadores();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de sócios do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = true;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            checkBox1.Enabled = true;
            button15_Click(sender, e);
            loadSocios();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista de claques do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = true;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            checkBox1.Enabled = false;
            button15_Click(sender, e);
            loadClaques();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista do Staff do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = true;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = false;
            checkBox1.Enabled = true;
            button15_Click(sender, e);
            loadStaff();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Centros de Treino do clube:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = true;
            FiltrosEstadios.Visible = false;
            checkBox1.Enabled = false;
            button15_Click(sender, e);
            loadCentrosTreino();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            label2.Text = "Lista dos Estádios:";
            FiltrosJogadores.Visible = false;
            FiltrosSocios.Visible = false;
            FiltrosClaques.Visible = false;
            FiltrosStaff.Visible = false;
            FiltrosCentroTreinos.Visible = false;
            FiltrosEstadios.Visible = true;
            checkBox1.Enabled = false;
            button15_Click(sender, e);
            loadEstadios();
        }



        private void loadJogadores()
        {
            string filtro = " where 1=1 ";

            currentselected = "jogador";
            if (!verifySGBDConnection())
                return;

            if (comboBox1.Items.Count < 1)
            {
                SqlCommand cmd1 = new SqlCommand("select distinct posicao from jogadores where posicao is not null ", cn);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                comboBox1.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    comboBox1.Items.Add(dr1["posicao"].ToString());
                }
                cmd1.Dispose();
            }

            if (comboBox2.Items.Count < 1)
            {
                SqlCommand cmd2 = new SqlCommand("select distinct equipa from jogadores where equipa is not null ", cn);
                cmd2.ExecuteNonQuery();
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                comboBox2.Items.Clear();
                foreach (DataRow dr2 in dt2.Rows)
                {
                    comboBox2.Items.Add(dr2["equipa"].ToString());
                }
                cmd2.Dispose();
            }

            if (comboBox3.Items.Count < 1)
            {
                SqlCommand cmd3 = new SqlCommand("select distinct liga from jogadores where liga is not null ", cn);
                cmd3.ExecuteNonQuery();
                DataTable dt3 = new DataTable();
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);
                da3.Fill(dt3);
                comboBox3.Items.Clear();
                foreach (DataRow dr3 in dt3.Rows)
                {
                    comboBox3.Items.Add(dr3["liga"].ToString());
                }
                cmd3.Dispose();
            }

            if (!String.IsNullOrEmpty(textBox1.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (comboBox1.SelectedIndex > -1)
                filtro += " and posicao = '" + comboBox1.SelectedItem + "'";
            if (comboBox2.SelectedIndex > -1)
                filtro += " and equipa = '" + comboBox2.SelectedItem + "'";
            if (comboBox3.SelectedIndex > -1)
                filtro += " and liga = '" + comboBox3.SelectedItem + "'";
            filtro += " and data_termino <= '" + dateTimePicker3.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc <= '" + dateTimePicker2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc >= '" + dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand("select * from jogadores" + filtro, cn);
            if (!String.IsNullOrEmpty(textBox1.Text))
                cmd.Parameters.AddWithValue("nome", textBox1.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Posição");
            dt.Columns.Add("Termino Contrato");
            dt.Columns.Add("Equipa");
            dt.Columns.Add("Liga");
            dt.Columns.Add("Data Nascimento");

            while (reader.Read())
            {
                Jogador J = new Jogador();
                J.JogadorID = reader["id_jogador"].ToString();
                J.Pos = reader["posicao"].ToString();
                J.Nome = reader["nome"].ToString();
                J.Dataterm = reader["data_termino"].ToString().Substring(0, 10);
                J.Equipa = reader["equipa"].ToString();
                J.Liga = reader["liga"].ToString();
                J.Datanasc = reader["data_nasc"].ToString().Substring(0, 10);


                dt.Rows.Add(J.JogadorID, J.Nome, J.Pos, J.Dataterm, J.Equipa, J.Liga, J.Datanasc);

            }

            dataGridView1.DataSource = dt;
            
            reader.Close();
            cmd.Dispose();
            cn.Close();
           
        }

        private void loadSocios()
        {
            string filtro = " where 1=1 ";

            currentselected = "socio";
            if (!verifySGBDConnection())
                return;

            if (comboBox6.Items.Count < 1)
            {
                SqlCommand cmd1 = new SqlCommand("select distinct claque from socios where claque is not null ", cn);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                comboBox6.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    comboBox6.Items.Add(dr1["claque"].ToString());
                }
                cmd1.Dispose();
            }

            if (!String.IsNullOrEmpty(textBox2.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (comboBox6.SelectedIndex > -1)
                filtro += " and claque = '" + comboBox6.SelectedItem + "'";
            filtro += " and data_inscricao >= '" + dateTimePicker7.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_inscricao <= '" + dateTimePicker6.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc >= '" + dateTimePicker5.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_nasc <= '" + dateTimePicker4.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand("select * from socios" + filtro, cn);
            if (!String.IsNullOrEmpty(textBox2.Text))
                cmd.Parameters.AddWithValue("nome", textBox2.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inscrição");
            dt.Columns.Add("Data Nascimento");
            dt.Columns.Add("Claque");

            while (reader.Read())
            {
                Socio S = new Socio();
                S.SocioID = reader["id_socio"].ToString();
                S.Nome = reader["nome"].ToString();
                S.DataInsc = reader["data_inscricao"].ToString().Substring(0, 10);
                S.DataNasc = reader["data_nasc"].ToString().Substring(0, 10);
                S.Claque = reader["claque"].ToString();

                dt.Rows.Add(S.SocioID, S.Nome, S.DataInsc, S.DataNasc, S.Claque);
            }

            dataGridView1.DataSource = dt;
            reader.Close();
            cmd.Dispose();
            cn.Close();
          
        }

        private void loadClaques()
        {
            string filtro = " where 1=1 ";

            currentselected = "claque";
            if (!verifySGBDConnection())
                return;

            if (comboBox8.Items.Count < 1)
            {
                SqlCommand cmd1 = new SqlCommand("select distinct bancada from clube.claque where bancada is not null ", cn);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                comboBox8.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    comboBox8.Items.Add(dr1["bancada"].ToString());
                }
                cmd1.Dispose();
            }

            if (!String.IsNullOrEmpty(textBox3.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (!String.IsNullOrEmpty(textBox9.Text))
                filtro += " and localizacao_sede like '%'+@loc+'%'";
            if (comboBox8.SelectedIndex > -1)
                filtro += " and bancada = '" + comboBox8.SelectedItem + "'";

            SqlCommand cmd = new SqlCommand("SELECT * FROM clube.claque " + filtro, cn);
            if (!String.IsNullOrEmpty(textBox3.Text))
                cmd.Parameters.AddWithValue("nome", textBox3.Text);
            if (!String.IsNullOrEmpty(textBox9.Text))
                cmd.Parameters.AddWithValue("loc", textBox9.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Localização Sede");
            dt.Columns.Add("Bancada");

            while (reader.Read())
            {
                Claque C = new Claque();
                C.ClaqueID = reader["id_claque"].ToString();
                C.Nome = reader["nome"].ToString();
                C.Loc = reader["localizacao_sede"].ToString();
                C.Bancada = reader["bancada"].ToString();

                dt.Rows.Add(C.ClaqueID, C.Nome, C.Loc, C.Bancada);
            }

            dataGridView1.DataSource = dt;
            reader.Close();
            cmd.Dispose();
            cn.Close();
         

        }

        private void loadStaff()
        {
            string filtro = " where 1=1 ";

            currentselected = "staff";
            if (!verifySGBDConnection())
                return;

            if (comboBox12.Items.Count < 1)
            {
                SqlCommand cmd1 = new SqlCommand("select distinct tipo from staff where tipo not like '' ", cn);
                cmd1.ExecuteNonQuery();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                da1.Fill(dt1);
                comboBox12.Items.Clear();
                foreach (DataRow dr1 in dt1.Rows)
                {
                    comboBox12.Items.Add(dr1["tipo"].ToString());
                }
                cmd1.Dispose();
            }

            if (!String.IsNullOrEmpty(textBox4.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (comboBox12.SelectedIndex > -1)
                filtro += " and tipo = '" + comboBox12.SelectedItem + "'";
            filtro += " and data_termino <= '" + dateTimePicker8.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            SqlCommand cmd = new SqlCommand("select * from staff" + filtro, cn);
            if (!String.IsNullOrEmpty(textBox4.Text))
                cmd.Parameters.AddWithValue("nome", textBox4.Text);
            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Tipo");
            dt.Columns.Add("Data Término");

            while (reader.Read())
            {
                Staff S = new Staff();
                S.Id_staff = reader["id_staff"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Tipo = reader["tipo"].ToString();
                S.Data_termino = reader["data_termino"].ToString().Substring(0, 10);
                S.Data_nascimento = reader["data_nasc"].ToString().Substring(0, 10);

                dt.Rows.Add(S.Id_staff, S.Nome, S.Tipo, S.Data_termino);

            }

            dataGridView1.DataSource = dt;
            cmd.Dispose();
           
            reader.Close();
            cn.Close();
            
        }

        private void loadCentrosTreino()
        {
            string filtro = " where 1=1 ";

            currentselected = "centrotreinos";
            if (!verifySGBDConnection())
                return;

            if (!String.IsNullOrEmpty(textBox5.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (!String.IsNullOrEmpty(textBox7.Text))
                filtro += " and localizacao like '%'+@loc+'%'";

            filtro += " and data_inauguracao >= '" + dateTimePicker10.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_inauguracao <= '" + dateTimePicker9.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            SqlCommand cmd = new SqlCommand("select * from Clube.CentroTreinos" + filtro, cn);

            if (!String.IsNullOrEmpty(textBox5.Text))
                cmd.Parameters.AddWithValue("nome", textBox5.Text);
            if (!String.IsNullOrEmpty(textBox7.Text))
                cmd.Parameters.AddWithValue("loc", textBox7.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inauguração");
            dt.Columns.Add("Localização");

            while (reader.Read())
            {
                Centro_de_treino S = new Centro_de_treino();
                S.Id_centro_treinos = reader["id_centro_treinos"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Data_inauguracao = reader["data_inauguracao"].ToString().Substring(0, 10);
                S.Localizacao = reader["localizacao"].ToString();

                dt.Rows.Add(S.Id_centro_treinos, S.Nome, S.Data_inauguracao, S.Localizacao);

            }
           

            dataGridView1.DataSource = dt;
            cmd.Dispose();
            reader.Close();
            cn.Close();
            
        }

        private void loadEstadios()
        {
            string filtro = " where 1=1 ";

            currentselected = "estadio";
            if (!verifySGBDConnection())
                return;

            if (!String.IsNullOrEmpty(textBox6.Text))
                filtro += " and nome like '%'+@nome+'%'";
            if (!String.IsNullOrEmpty(textBox8.Text))
                filtro += " and localizacao like '%'+@loc+'%'";
            if (!String.IsNullOrEmpty(textBox10.Text))
                filtro += " and lotacao > @lotacao";

            filtro += " and data_inauguracao >= '" + dateTimePicker12.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            filtro += " and data_inauguracao <= '" + dateTimePicker11.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";

            SqlCommand cmd = new SqlCommand("select * from Clube.Estadio" + filtro, cn);

            if (!String.IsNullOrEmpty(textBox6.Text))
                cmd.Parameters.AddWithValue("nome", textBox6.Text);
            if (!String.IsNullOrEmpty(textBox8.Text))
                cmd.Parameters.AddWithValue("loc", textBox8.Text);
            if (!String.IsNullOrEmpty(textBox10.Text))
                cmd.Parameters.AddWithValue("lotacao", textBox10.Text);

            SqlDataReader reader = cmd.ExecuteReader();

            DataTable dt = new DataTable();

            dt.Columns.Add("ID");
            dt.Columns.Add("Nome");
            dt.Columns.Add("Data Inauguração");
            dt.Columns.Add("Arquiteto");
            dt.Columns.Add("Lotação");
            dt.Columns.Add("Localização");

            while (reader.Read())
            {
                Estadio S = new Estadio();
                S.Id_estadio = reader["id_estadio"].ToString();
                S.Nome = reader["nome"].ToString();
                S.Data_inauguracao = reader["data_inauguracao"].ToString().Substring(0, 10);
                S.Arquiteto = reader["arquiteto"].ToString();
                S.Lotacao = reader["lotacao"].ToString();
                S.Localizacao = reader["localizacao"].ToString();

                dt.Rows.Add(S.Id_estadio, S.Nome, S.Data_inauguracao, S.Arquiteto, S.Lotacao, S.Localizacao);

            }

            dataGridView1.DataSource = dt;
            cmd.Dispose();
            reader.Close();
            cn.Close();
           
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //jogador, socio, estadio, centrotreinos, staff, claque
            switch (currentselected)
            {
                case "jogador":
                    Form f = new FormaddNIF(currentselected, cn);
                    f.TopMost = true;
                    f.ShowDialog();
                    break;
                case "staff":
                    Form f2 = new FormaddNIF(currentselected, cn);
                    f2.TopMost = true;
                    f2.ShowDialog();
                    break;
                case "socio":
                    Form f3 = new FormaddNIF(currentselected, cn);
                    f3.TopMost = true;
                    f3.ShowDialog();
                    break;
                case "estadio":
                    Form f4 = new FormaddEstadio(cn);
                    f4.TopMost = true;
                    f4.ShowDialog();
                    break;
                case "centrotreinos":
                    Form f5 = new FormaddCentroTreinos(cn);
                    f5.TopMost = true;
                    f5.ShowDialog();
                    break;
                case "claque":
                    Form f6 = new FormaddClaque(cn);
                    f6.TopMost = true;
                    f6.ShowDialog();
                    break;
                default:
                    break;
            }


        }

        private void button8_Click(object sender, EventArgs e) // BOTÂO EDITAR
        {

            selection = dataGridView1.SelectedRows;

            if (selection.Count == 0)
            {
                MessageBox.Show("Para editar tem que ser selecionar uma linha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (selection.Count > 1)
            {
                MessageBox.Show("Para editar só pode selecionar uma linha.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            String toUpdate = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

            DialogResult dialogResult = MessageBox.Show("Pretende actualizar os dados do elemento com o id " + toUpdate + " da tabela " + currentselected + "?", "Actualizar elemento.", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.No)
            {
                return;
            }

            switch (currentselected)
            {
                case "jogador":
                    Form f1 = new UpdateJogador(toUpdate, cn);
                    f1.TopMost = true;
                    f1.ShowDialog();
                    break;
                case "staff":
                    Form f2 = new UpdateStaff(toUpdate, cn);
                    f2.TopMost = true;
                    f2.ShowDialog();
                    break;
                case "socio":
                    Form f3 = new UpdateSocio(toUpdate, cn);
                    f3.TopMost = true;
                    f3.ShowDialog();
                    break;
                case "estadio":
                    Form f4 = new UpdateEstadio(toUpdate, cn);
                    f4.TopMost = true;
                    f4.ShowDialog();
                    break;
                case "centrotreinos":
                    Form f5 = new UpdateCentroTreinos(toUpdate, cn);
                    f5.TopMost = true;
                    f5.ShowDialog();
                    break;
                case "claque":
                    Form f6 = new UpdateClaque(toUpdate, cn);
                    f6.TopMost = true;
                    f6.ShowDialog();
                    break;
                default:
                    break;
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            selection = dataGridView1.SelectedRows;

            if (selection.Count <= 0)
            {
                MessageBox.Show("É necessario selecionar pelo menos uma linha a remover!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lista com todos os ids a remover na respetiva tabela
            String toRemove = "( ";
            for (int i = selection.Count - 1; i > 0; i--)
            {
                toRemove = toRemove + dataGridView1.SelectedRows[i].Cells[0].Value.ToString() + ",";
            }

            toRemove = toRemove + dataGridView1.SelectedRows[0].Cells[0].Value.ToString() + " )";

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;

            DialogResult dialogResult = MessageBox.Show("Tem a certeza que quer apagar da tabela " + currentselected  + " os elementos com os ids " + toRemove , "Apagar elementos permanentemente", MessageBoxButtons.YesNo);
            
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            switch (currentselected)
            {
                case "jogador":
                    if (checkBox1.Checked)
                    {
                        if (!verifySGBDConnection())
                            return;
                        cmd.CommandText = "delete from clube.jogador where id_jogador in " + toRemove;
                        cmd.ExecuteNonQuery();
                        loadJogadores();
                        break;
                    }
                    deletePessoasToo(toRemove, "jogadores");
                    break;

                case "staff":
                    if (checkBox1.Checked)
                    {
                        if (!verifySGBDConnection())
                            return;
                        cmd.CommandText = "delete from clube.staff where id_staff in " + toRemove;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        loadStaff();
                        break;
                    }
                    deletePessoasToo(toRemove, "staff");
                    break;

                case "socio":
                    if (checkBox1.Checked)
                    {
                        if (!verifySGBDConnection())
                            return;
                        cmd.CommandText = "delete from clube.socio where id_socio in " + toRemove;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();
                        loadSocios();
                        break;
                    }
                    deletePessoasToo(toRemove, "socios");
                    break;

                case "estadio":
                    cmd.CommandText = "delete from clube.estadio where id_estadio in " + toRemove;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    loadEstadios();
                    break;
                case "centrotreinos":
                    cmd.CommandText = "delete from clube.centrotreinos where id_centro_treinos in " + toRemove;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    loadCentrosTreino();
                    break;
                case "claque":
                    cmd.CommandText = "delete from clube.claque where id_claque in " + toRemove;
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    loadClaques();
                    break;
                default:
                    break;
            }

            cn.Close();
               

        }

        private void deletePessoasToo(string toRemove, string v)
        {

            if (!verifySGBDConnection())
                return;

            SqlTransaction transaction;
            transaction = cn.BeginTransaction("SampleTransaction");
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.Transaction = transaction;
            String pessoaToRemove = "( ";

            try
            {
                if (v.Equals("jogadores"))
                {
                    //guardar os id's das pessoas a remover
                    cmd.CommandText = "select id_pessoa from clube.jogador where id_jogador in " + toRemove;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        pessoaToRemove += reader["id_pessoa"].ToString() + ", ";
                    }

                    reader.Close();

                    pessoaToRemove += "null)";

                    //delete from tabela clube.jogador
                    cmd.CommandText = "delete from clube.jogador where id_jogador in " + toRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.socio e clube.staff para o caso de esta pessoa também ser socio e/ou staff
                    cmd.CommandText = "delete from clube.socio where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from clube.staff where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.pessoa
                    cmd.CommandText = "delete from clube.pessoa where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery(); 

                    transaction.Commit();
                    loadJogadores();
                }
                if (v.Equals("staff"))
                {
                    //guardar os id's das pessoas a remover
                    cmd.CommandText = "select id_pessoa from clube.staff where id_staff in " + toRemove;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        pessoaToRemove += reader["id_pessoa"].ToString() + ", ";
                    }

                    reader.Close();

                    pessoaToRemove += "null)";

                    //delete from tabela clube.staff
                    cmd.CommandText = "delete from clube.staff where id_staff in " + toRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.socio e clube.jogador para o caso de esta pessoa também ser socio e/ou jogador
                    cmd.CommandText = "delete from clube.socio where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from clube.jogador where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.pessoa
                    cmd.CommandText = "delete from clube.pessoa where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    loadStaff();
                }
                if (v.Equals("socios"))
                {
                    //guardar os id's das pessoas a remover
                    cmd.CommandText = "select id_pessoa from clube.socio where id_socio in " + toRemove;
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        pessoaToRemove += reader["id_pessoa"].ToString() + ", ";
                    }

                    reader.Close();

                    pessoaToRemove += "null)";

                    //delete from tabela clube.jogador
                    cmd.CommandText = "delete from clube.socio where id_socio in " + toRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.jogador e clube.staff para o caso de esta pessoa também ser jogador e/ou staff
                    cmd.CommandText = "delete from clube.jogador where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "delete from clube.staff where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();

                    //delete from tabela clube.pessoa
                    cmd.CommandText = "delete from clube.pessoa where id_pessoa in " + pessoaToRemove;
                    cmd.ExecuteNonQuery();

                    transaction.Commit();
                    loadSocios();
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show("Remoção Falhou!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        }

        private void button14_Click(object sender, EventArgs e) // Botão filtrar
        {
            if (!verifySGBDConnection())
                return;

            switch (currentselected)
            {
                case "jogador":
                    loadJogadores();
                    break;
                case "staff":
                    loadStaff();
                    break;
                case "socio":
                    loadSocios();
                    break;
                case "estadio":
                    loadEstadios();
                    break;
                case "centrotreinos":
                    loadCentrosTreino();
                    break;
                case "claque":
                    loadClaques();
                    break;
                default:
                    break;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            // FILTROS JOGADOR
            textBox1.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            dateTimePicker3.Value = new DateTime(2030, 01, 01);
            dateTimePicker1.Value = new DateTime(1900, 01, 01);
            dateTimePicker2.Value = DateTime.Today;

            // FILTROS SOCIOS
            textBox2.Text = "";
            comboBox6.SelectedIndex = -1;
            dateTimePicker7.Value = new DateTime(1900, 01, 01);
            dateTimePicker6.Value = DateTime.Today;
            dateTimePicker5.Value = new DateTime(1900, 01, 01);
            dateTimePicker4.Value = DateTime.Today;

            // FILTROS CLAQUES
            textBox3.Text = "";
            textBox9.Text = "";
            comboBox8.SelectedIndex = -1;

            // FILTROS STAFF
            textBox4.Text = "";
            comboBox12.SelectedIndex = -1;
            dateTimePicker8.Value = new DateTime(2030, 01, 01);

            // FILTROS CENTROS TREINO
            textBox5.Text = "";
            textBox7.Text = "";
            dateTimePicker10.Value = new DateTime(1900, 01, 01);
            dateTimePicker9.Value = DateTime.Today;

            // FILTROS ESTADIOS
            textBox6.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            dateTimePicker12.Value = new DateTime(1900, 01, 01);
            dateTimePicker11.Value = DateTime.Today;

            button14_Click(sender, e);
        }
    }

}
