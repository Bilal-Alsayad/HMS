namespace HYS.User_Control
{
    partial class UChemsire
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.bolum = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ise_alma = new System.Windows.Forms.DateTimePicker();
            this.islem = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.id = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Ünvan = new System.Windows.Forms.Label();
            this.unvan = new System.Windows.Forms.TextBox();
            this.ara = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.eposta = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.adres = new System.Windows.Forms.TextBox();
            this.dogumTarihi = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.cinsiyet = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.duzenle = new System.Windows.Forms.Button();
            this.sil = new System.Windows.Forms.Button();
            this.ekle = new System.Windows.Forms.Button();
            this.telefon = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.soyadi = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.adi = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // bolum
            // 
            this.bolum.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.bolum.Location = new System.Drawing.Point(420, 237);
            this.bolum.Name = "bolum";
            this.bolum.Size = new System.Drawing.Size(180, 21);
            this.bolum.TabIndex = 175;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(416, 213);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 21);
            this.label10.TabIndex = 174;
            this.label10.Text = "Bölüm ID";
            // 
            // ise_alma
            // 
            this.ise_alma.CustomFormat = "dd-MM-yyyy";
            this.ise_alma.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.ise_alma.Location = new System.Drawing.Point(420, 188);
            this.ise_alma.Name = "ise_alma";
            this.ise_alma.ShowUpDown = true;
            this.ise_alma.Size = new System.Drawing.Size(180, 20);
            this.ise_alma.TabIndex = 171;
            // 
            // islem
            // 
            this.islem.FormattingEnabled = true;
            this.islem.Location = new System.Drawing.Point(124, 28);
            this.islem.Name = "islem";
            this.islem.Size = new System.Drawing.Size(180, 21);
            this.islem.TabIndex = 170;
            this.islem.SelectedIndexChanged += new System.EventHandler(this.islem_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(120, 4);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 21);
            this.label12.TabIndex = 169;
            this.label12.Text = "İşlem";
            // 
            // id
            // 
            this.id.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.id.Location = new System.Drawing.Point(420, 24);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(180, 21);
            this.id.TabIndex = 168;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(416, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 21);
            this.label11.TabIndex = 167;
            this.label11.Text = "ID";
            // 
            // Ünvan
            // 
            this.Ünvan.AutoSize = true;
            this.Ünvan.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Ünvan.Location = new System.Drawing.Point(678, 164);
            this.Ünvan.Name = "Ünvan";
            this.Ünvan.Size = new System.Drawing.Size(62, 21);
            this.Ünvan.TabIndex = 166;
            this.Ünvan.Text = "Ünvan";
            // 
            // unvan
            // 
            this.unvan.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.unvan.Location = new System.Drawing.Point(682, 187);
            this.unvan.Name = "unvan";
            this.unvan.Size = new System.Drawing.Size(180, 21);
            this.unvan.TabIndex = 165;
            // 
            // ara
            // 
            this.ara.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ara.Location = new System.Drawing.Point(654, 264);
            this.ara.Name = "ara";
            this.ara.Size = new System.Drawing.Size(120, 32);
            this.ara.TabIndex = 164;
            this.ara.Text = "Ara";
            this.ara.UseVisualStyleBackColor = true;
            this.ara.Click += new System.EventHandler(this.ara_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(416, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 21);
            this.label9.TabIndex = 163;
            this.label9.Text = "İşe Tarihi Alma";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(120, 165);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 21);
            this.label8.TabIndex = 162;
            this.label8.Text = "E_Posta";
            // 
            // eposta
            // 
            this.eposta.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.eposta.Location = new System.Drawing.Point(124, 188);
            this.eposta.Name = "eposta";
            this.eposta.Size = new System.Drawing.Size(180, 21);
            this.eposta.TabIndex = 161;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(120, 109);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 21);
            this.label7.TabIndex = 160;
            this.label7.Text = "Adres";
            // 
            // adres
            // 
            this.adres.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.adres.Location = new System.Drawing.Point(124, 133);
            this.adres.Name = "adres";
            this.adres.Size = new System.Drawing.Size(180, 21);
            this.adres.TabIndex = 159;
            // 
            // dogumTarihi
            // 
            this.dogumTarihi.CustomFormat = "dd-MM-yyyy";
            this.dogumTarihi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dogumTarihi.Location = new System.Drawing.Point(682, 73);
            this.dogumTarihi.Name = "dogumTarihi";
            this.dogumTarihi.ShowUpDown = true;
            this.dogumTarihi.Size = new System.Drawing.Size(180, 20);
            this.dogumTarihi.TabIndex = 158;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(678, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 21);
            this.label5.TabIndex = 157;
            this.label5.Text = "Doğum Tarihi";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 310);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(984, 251);
            this.dataGridView1.TabIndex = 156;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(3, 1);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(124, 26);
            this.label6.TabIndex = 155;
            this.label6.Text = "Hemşireler";
            // 
            // cinsiyet
            // 
            this.cinsiyet.FormattingEnabled = true;
            this.cinsiyet.Location = new System.Drawing.Point(420, 133);
            this.cinsiyet.Name = "cinsiyet";
            this.cinsiyet.Size = new System.Drawing.Size(180, 21);
            this.cinsiyet.TabIndex = 154;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(416, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 21);
            this.label4.TabIndex = 153;
            this.label4.Text = "Cinsiyet";
            // 
            // duzenle
            // 
            this.duzenle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.duzenle.Location = new System.Drawing.Point(508, 264);
            this.duzenle.Name = "duzenle";
            this.duzenle.Size = new System.Drawing.Size(120, 32);
            this.duzenle.TabIndex = 152;
            this.duzenle.Text = "Düzenle";
            this.duzenle.UseVisualStyleBackColor = true;
            this.duzenle.Click += new System.EventHandler(this.duzenle_Click);
            // 
            // sil
            // 
            this.sil.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sil.Location = new System.Drawing.Point(364, 264);
            this.sil.Name = "sil";
            this.sil.Size = new System.Drawing.Size(120, 32);
            this.sil.TabIndex = 151;
            this.sil.Text = "Sil";
            this.sil.UseVisualStyleBackColor = true;
            this.sil.Click += new System.EventHandler(this.sil_Click);
            // 
            // ekle
            // 
            this.ekle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ekle.Location = new System.Drawing.Point(215, 264);
            this.ekle.Name = "ekle";
            this.ekle.Size = new System.Drawing.Size(120, 32);
            this.ekle.TabIndex = 150;
            this.ekle.Text = "Ekle";
            this.ekle.UseVisualStyleBackColor = true;
            this.ekle.Click += new System.EventHandler(this.ekle_Click);
            // 
            // telefon
            // 
            this.telefon.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.telefon.Location = new System.Drawing.Point(682, 132);
            this.telefon.Name = "telefon";
            this.telefon.Size = new System.Drawing.Size(180, 21);
            this.telefon.TabIndex = 149;
            this.telefon.Text = "05";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(678, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 21);
            this.label3.TabIndex = 148;
            this.label3.Text = "Telefon";
            // 
            // soyadi
            // 
            this.soyadi.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.soyadi.Location = new System.Drawing.Point(420, 72);
            this.soyadi.Name = "soyadi";
            this.soyadi.Size = new System.Drawing.Size(180, 21);
            this.soyadi.TabIndex = 147;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(416, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 21);
            this.label2.TabIndex = 146;
            this.label2.Text = "Soyadı";
            // 
            // adi
            // 
            this.adi.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.adi.Location = new System.Drawing.Point(124, 72);
            this.adi.Name = "adi";
            this.adi.Size = new System.Drawing.Size(180, 21);
            this.adi.TabIndex = 145;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(120, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 21);
            this.label1.TabIndex = 144;
            this.label1.Text = "Adı";
            // 
            // UChemsire
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bolum);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ise_alma);
            this.Controls.Add(this.islem);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.Ünvan);
            this.Controls.Add(this.unvan);
            this.Controls.Add(this.ara);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.eposta);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.adres);
            this.Controls.Add(this.dogumTarihi);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cinsiyet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.duzenle);
            this.Controls.Add(this.sil);
            this.Controls.Add(this.ekle);
            this.Controls.Add(this.telefon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.soyadi);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.adi);
            this.Controls.Add(this.label1);
            this.Name = "UChemsire";
            this.Size = new System.Drawing.Size(984, 561);
            this.Load += new System.EventHandler(this.UChemsire_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox bolum;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DateTimePicker ise_alma;
        private System.Windows.Forms.ComboBox islem;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label Ünvan;
        private System.Windows.Forms.TextBox unvan;
        private System.Windows.Forms.Button ara;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox eposta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox adres;
        private System.Windows.Forms.DateTimePicker dogumTarihi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cinsiyet;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button duzenle;
        private System.Windows.Forms.Button sil;
        private System.Windows.Forms.Button ekle;
        private System.Windows.Forms.TextBox telefon;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox soyadi;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox adi;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}
