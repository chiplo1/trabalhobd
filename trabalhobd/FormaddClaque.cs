﻿using System;
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
    public partial class FormaddClaque : Form
    {
        private SqlConnection cn;
        public FormaddClaque(SqlConnection cn)
        {
            this.cn = cn;
            InitializeComponent();
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

            SqlCommand cmd = new SqlCommand();


            cmd.CommandText = "INSERT INTO Clube.Claque ([nome], [localizacao_sede], [bancada]) VALUES (@nome,@localizacao_sede,@bancada);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nome", textBox1.Text);
            cmd.Parameters.AddWithValue("@localizacao_sede", textBox2.Text);
            cmd.Parameters.AddWithValue("@bancada", textBox3.Text);
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
