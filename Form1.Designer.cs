namespace trabalhobd
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
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(937, 573);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
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
    }
}

