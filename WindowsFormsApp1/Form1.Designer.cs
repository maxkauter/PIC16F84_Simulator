namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Programcounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Program = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.PORTB = new System.Windows.Forms.GroupBox();
            this.PORTA = new System.Windows.Forms.GroupBox();
            this.STATUS_REGISTER = new System.Windows.Forms.GroupBox();
            this.SPECIAL_REGISTER = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Datei öffnen";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(720, 175);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 20);
            this.button2.TabIndex = 1;
            this.button2.Text = "Go";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(720, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(95, 20);
            this.button3.TabIndex = 2;
            this.button3.Text = "Step In";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(720, 227);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(95, 20);
            this.button4.TabIndex = 3;
            this.button4.Text = "Stop";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(720, 253);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 19);
            this.button5.TabIndex = 4;
            this.button5.Text = "Reset";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(724, 12);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "WatchDogTMR";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(720, 56);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(97, 21);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(720, 104);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(95, 21);
            this.comboBox2.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(721, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Laufzeit";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Programcounter,
            this.Program});
            this.dataGridView1.Location = new System.Drawing.Point(12, 40);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(223, 379);
            this.dataGridView1.TabIndex = 9;
            // 
            // Programcounter
            // 
            this.Programcounter.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Programcounter.HeaderText = "Programcounter";
            this.Programcounter.Name = "Programcounter";
            // 
            // Program
            // 
            this.Program.HeaderText = "Program";
            this.Program.Name = "Program";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(271, 331);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(253, 114);
            this.dataGridView2.TabIndex = 10;
            this.dataGridView2.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellContentClick);
            // 
            // dataGridView3
            // 
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(562, 331);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.Size = new System.Drawing.Size(253, 111);
            this.dataGridView3.TabIndex = 11;
            // 
            // PORTB
            // 
            this.PORTB.Location = new System.Drawing.Point(427, 253);
            this.PORTB.Name = "PORTB";
            this.PORTB.Size = new System.Drawing.Size(287, 58);
            this.PORTB.TabIndex = 12;
            this.PORTB.TabStop = false;
            this.PORTB.Text = "PORTB";
            // 
            // PORTA
            // 
            this.PORTA.Location = new System.Drawing.Point(427, 178);
            this.PORTA.Name = "PORTA";
            this.PORTA.Size = new System.Drawing.Size(287, 55);
            this.PORTA.TabIndex = 13;
            this.PORTA.TabStop = false;
            this.PORTA.Text = "PORTA";
            // 
            // STATUS_REGISTER
            // 
            this.STATUS_REGISTER.Location = new System.Drawing.Point(427, 120);
            this.STATUS_REGISTER.Name = "STATUS_REGISTER";
            this.STATUS_REGISTER.Size = new System.Drawing.Size(287, 55);
            this.STATUS_REGISTER.TabIndex = 14;
            this.STATUS_REGISTER.TabStop = false;
            this.STATUS_REGISTER.Text = "STATUS_REGISTER";
            // 
            // SPECIAL_REGISTER
            // 
            this.SPECIAL_REGISTER.Location = new System.Drawing.Point(251, 15);
            this.SPECIAL_REGISTER.Name = "SPECIAL_REGISTER";
            this.SPECIAL_REGISTER.Size = new System.Drawing.Size(144, 218);
            this.SPECIAL_REGISTER.TabIndex = 15;
            this.SPECIAL_REGISTER.TabStop = false;
            this.SPECIAL_REGISTER.Text = "Special Register";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 457);
            this.Controls.Add(this.SPECIAL_REGISTER);
            this.Controls.Add(this.STATUS_REGISTER);
            this.Controls.Add(this.PORTA);
            this.Controls.Add(this.PORTB);
            this.Controls.Add(this.dataGridView3);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Programcounter;
        private System.Windows.Forms.DataGridViewTextBoxColumn Program;
        private System.Windows.Forms.GroupBox PORTB;
        private System.Windows.Forms.GroupBox PORTA;
        private System.Windows.Forms.GroupBox STATUS_REGISTER;
        private System.Windows.Forms.GroupBox SPECIAL_REGISTER;
    }
}

