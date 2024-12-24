namespace HYS.User_Control
{
    partial class UC_ilac
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
            this.sd = new System.Windows.Forms.Label();
            this.maliyt = new System.Windows.Forms.TextBox();
            this.ilacAdi = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.baslangic = new System.Windows.Forms.DateTimePicker();
            this.islem = new System.Windows.Forms.ComboBox();
            this.id = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ara = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.duzenle = new System.Windows.Forms.Button();
            this.sil = new System.Windows.Forms.Button();
            this.ekle = new System.Windows.Forms.Button();
            this.receteId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bitis = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // sd
            // 
            this.sd.AutoSize = true;
            this.sd.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sd.Location = new System.Drawing.Point(635, 99);
            this.sd.Name = "sd";
            this.sd.Size = new System.Drawing.Size(57, 21);
            this.sd.TabIndex = 170;
            this.sd.Text = "Maliyt";
            // 
            // maliyt
            // 
            this.maliyt.Location = new System.Drawing.Point(639, 121);
            this.maliyt.Name = "maliyt";
            this.maliyt.Size = new System.Drawing.Size(180, 20);
            this.maliyt.TabIndex = 169;
            // 
            // ilacAdi
            // 
            this.ilacAdi.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ilacAdi.Location = new System.Drawing.Point(639, 75);
            this.ilacAdi.Name = "ilacAdi";
            this.ilacAdi.Size = new System.Drawing.Size(180, 21);
            this.ilacAdi.TabIndex = 168;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(635, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 21);
            this.label3.TabIndex = 167;
            this.label3.Text = "İlaç Adı";
            // 
            // baslangic
            // 
            this.baslangic.CustomFormat = "dd-MM-yyyy";
            this.baslangic.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.baslangic.Location = new System.Drawing.Point(129, 121);
            this.baslangic.Name = "baslangic";
            this.baslangic.ShowUpDown = true;
            this.baslangic.Size = new System.Drawing.Size(180, 20);
            this.baslangic.TabIndex = 166;
            // 
            // islem
            // 
            this.islem.FormattingEnabled = true;
            this.islem.Location = new System.Drawing.Point(129, 27);
            this.islem.Name = "islem";
            this.islem.Size = new System.Drawing.Size(180, 21);
            this.islem.TabIndex = 162;
            this.islem.SelectedIndexChanged += new System.EventHandler(this.islem_SelectedIndexChanged);
            // 
            // id
            // 
            this.id.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.id.Location = new System.Drawing.Point(129, 75);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(180, 21);
            this.id.TabIndex = 160;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(125, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 21);
            this.label11.TabIndex = 159;
            this.label11.Text = "ID";
            // 
            // ara
            // 
            this.ara.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ara.Location = new System.Drawing.Point(639, 164);
            this.ara.Name = "ara";
            this.ara.Size = new System.Drawing.Size(120, 32);
            this.ara.TabIndex = 158;
            this.ara.Text = "Ara";
            this.ara.UseVisualStyleBackColor = true;
            this.ara.Click += new System.EventHandler(this.ara_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 221);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(984, 340);
            this.dataGridView1.TabIndex = 157;
            // 
            // duzenle
            // 
            this.duzenle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.duzenle.Location = new System.Drawing.Point(493, 164);
            this.duzenle.Name = "duzenle";
            this.duzenle.Size = new System.Drawing.Size(120, 32);
            this.duzenle.TabIndex = 155;
            this.duzenle.Text = "Düzenle";
            this.duzenle.UseVisualStyleBackColor = true;
            this.duzenle.Click += new System.EventHandler(this.duzenle_Click);
            // 
            // sil
            // 
            this.sil.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sil.Location = new System.Drawing.Point(349, 164);
            this.sil.Name = "sil";
            this.sil.Size = new System.Drawing.Size(120, 32);
            this.sil.TabIndex = 154;
            this.sil.Text = "Sil";
            this.sil.UseVisualStyleBackColor = true;
            this.sil.Click += new System.EventHandler(this.sil_Click);
            // 
            // ekle
            // 
            this.ekle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ekle.Location = new System.Drawing.Point(200, 164);
            this.ekle.Name = "ekle";
            this.ekle.Size = new System.Drawing.Size(120, 32);
            this.ekle.TabIndex = 153;
            this.ekle.Text = "Ekle";
            this.ekle.UseVisualStyleBackColor = true;
            this.ekle.Click += new System.EventHandler(this.ekle_Click);
            // 
            // receteId
            // 
            this.receteId.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.receteId.Location = new System.Drawing.Point(393, 75);
            this.receteId.Name = "receteId";
            this.receteId.Size = new System.Drawing.Size(180, 21);
            this.receteId.TabIndex = 164;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(389, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 21);
            this.label1.TabIndex = 163;
            this.label1.Text = "Reçete ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(125, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 21);
            this.label2.TabIndex = 165;
            this.label2.Text = "Başiangiç Tarihi";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(125, 3);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 21);
            this.label12.TabIndex = 161;
            this.label12.Text = "İşlem";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(79, 26);
            this.label6.TabIndex = 156;
            this.label6.Text = "İlaçlar";
            // 
            // bitis
            // 
            this.bitis.CustomFormat = "dd-MM-yyyy";
            this.bitis.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.bitis.Location = new System.Drawing.Point(393, 121);
            this.bitis.Name = "bitis";
            this.bitis.ShowUpDown = true;
            this.bitis.Size = new System.Drawing.Size(180, 20);
            this.bitis.TabIndex = 172;
            this.bitis.Value = new System.DateTime(2024, 12, 23, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(389, 99);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 21);
            this.label5.TabIndex = 171;
            this.label5.Text = "Bitiş Tarihi";
            // 
            // UC_ilac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bitis);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.sd);
            this.Controls.Add(this.maliyt);
            this.Controls.Add(this.ilacAdi);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.baslangic);
            this.Controls.Add(this.islem);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ara);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.duzenle);
            this.Controls.Add(this.sil);
            this.Controls.Add(this.ekle);
            this.Controls.Add(this.receteId);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Name = "UC_ilac";
            this.Size = new System.Drawing.Size(984, 561);
            this.Load += new System.EventHandler(this.UC_ilac_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label sd;
        private System.Windows.Forms.TextBox maliyt;
        private System.Windows.Forms.TextBox ilacAdi;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker baslangic;
        private System.Windows.Forms.ComboBox islem;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ara;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button duzenle;
        private System.Windows.Forms.Button sil;
        private System.Windows.Forms.Button ekle;
        private System.Windows.Forms.TextBox receteId;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker bitis;
        private System.Windows.Forms.Label label5;
    }
}
