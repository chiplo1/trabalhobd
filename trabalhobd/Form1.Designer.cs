﻿namespace trabalhobd
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.p1g10DataSet = new trabalhobd.p1g10DataSet();
            this.p1g10DataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.p1g10DataSet1 = new trabalhobd.p1g10DataSet1();
            this.jogadoresBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.jogadoresTableAdapter = new trabalhobd.p1g10DataSet1TableAdapters.jogadoresTableAdapter();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.jogadoresBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 12);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(146, 69);
            this.button4.TabIndex = 0;
            this.button4.Text = "Jogadores";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(164, 12);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(146, 69);
            this.button5.TabIndex = 1;
            this.button5.Text = "Sócios";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(316, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(146, 69);
            this.button6.TabIndex = 2;
            this.button6.Text = "Claques";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Lista de jogadores do clube :";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(797, 120);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(122, 31);
            this.button7.TabIndex = 5;
            this.button7.Text = "Adicionar";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(797, 166);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(122, 31);
            this.button8.TabIndex = 6;
            this.button8.Text = "Editar";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(797, 212);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(122, 31);
            this.button9.TabIndex = 7;
            this.button9.Text = "Remover";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 120);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(763, 441);
            this.dataGridView1.TabIndex = 8;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // p1g10DataSet
            // 
            this.p1g10DataSet.DataSetName = "p1g10DataSet";
            this.p1g10DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // p1g10DataSetBindingSource
            // 
            this.p1g10DataSetBindingSource.DataSource = this.p1g10DataSet;
            this.p1g10DataSetBindingSource.Position = 0;
            // 
            // p1g10DataSet1
            // 
            this.p1g10DataSet1.DataSetName = "p1g10DataSet1";
            this.p1g10DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // jogadoresBindingSource
            // 
            this.jogadoresBindingSource.DataMember = "jogadores";
            this.jogadoresBindingSource.DataSource = this.p1g10DataSet1;
            // 
            // jogadoresTableAdapter
            // 
            this.jogadoresTableAdapter.ClearBeforeFill = true;
            // 
            // button10
            // 
            this.button10.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.button10.Location = new System.Drawing.Point(468, 12);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(146, 69);
            this.button10.TabIndex = 9;
            this.button10.Text = "Staff";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(620, 12);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(146, 69);
            this.button11.TabIndex = 10;
            this.button11.Text = "Centros de Treino";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(773, 12);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(146, 69);
            this.button12.TabIndex = 11;
            this.button12.Text = "Estádios";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.button12_Click);
            // 
            // button13
            // 
            this.button13.BackColor = System.Drawing.SystemColors.Highlight;
            this.button13.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.button13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button13.Location = new System.Drawing.Point(797, 501);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(122, 60);
            this.button13.TabIndex = 12;
            this.button13.Text = "Refresh";
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(937, 573);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MinimumSize = new System.Drawing.Size(953, 612);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.p1g10DataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.jogadoresBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource p1g10DataSetBindingSource;
        private p1g10DataSet p1g10DataSet;
        private p1g10DataSet1 p1g10DataSet1;
        private System.Windows.Forms.BindingSource jogadoresBindingSource;
        private p1g10DataSet1TableAdapters.jogadoresTableAdapter jogadoresTableAdapter;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
    }
}

