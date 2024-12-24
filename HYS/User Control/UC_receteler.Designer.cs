namespace HYS.User_Control
{
    partial class UC_receteler
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
            this.dozaj = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.islem = new System.Windows.Forms.ComboBox();
            this.id = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ara = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.duzenle = new System.Windows.Forms.Button();
            this.sil = new System.Windows.Forms.Button();
            this.ekle = new System.Windows.Forms.Button();
            this.tibbi_kayit_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.talimat = new System.Windows.Forms.TextBox();
            this.Talimatlar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dozaj
            // 
            this.dozaj.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dozaj.Location = new System.Drawing.Point(129, 121);
            this.dozaj.Name = "dozaj";
            this.dozaj.Size = new System.Drawing.Size(327, 21);
            this.dozaj.TabIndex = 184;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(125, 97);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 21);
            this.label3.TabIndex = 183;
            this.label3.Text = "Dozaj";
            // 
            // islem
            // 
            this.islem.FormattingEnabled = true;
            this.islem.Location = new System.Drawing.Point(171, 26);
            this.islem.Name = "islem";
            this.islem.Size = new System.Drawing.Size(180, 21);
            this.islem.TabIndex = 180;
            this.islem.SelectedIndexChanged += new System.EventHandler(this.islem_SelectedIndexChanged);
            // 
            // id
            // 
            this.id.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.id.Location = new System.Drawing.Point(129, 75);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(180, 21);
            this.id.TabIndex = 178;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.Location = new System.Drawing.Point(125, 51);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(27, 21);
            this.label11.TabIndex = 177;
            this.label11.Text = "ID";
            // 
            // ara
            // 
            this.ara.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.ara.Location = new System.Drawing.Point(639, 164);
            this.ara.Name = "ara";
            this.ara.Size = new System.Drawing.Size(120, 32);
            this.ara.TabIndex = 176;
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
            this.dataGridView1.TabIndex = 175;
            // 
            // duzenle
            // 
            this.duzenle.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.duzenle.Location = new System.Drawing.Point(493, 164);
            this.duzenle.Name = "duzenle";
            this.duzenle.Size = new System.Drawing.Size(120, 32);
            this.duzenle.TabIndex = 173;
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
            this.sil.TabIndex = 172;
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
            this.ekle.TabIndex = 171;
            this.ekle.Text = "Ekle";
            this.ekle.UseVisualStyleBackColor = true;
            this.ekle.Click += new System.EventHandler(this.ekle_Click);
            // 
            // tibbi_kayit_id
            // 
            this.tibbi_kayit_id.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tibbi_kayit_id.Location = new System.Drawing.Point(512, 75);
            this.tibbi_kayit_id.Name = "tibbi_kayit_id";
            this.tibbi_kayit_id.Size = new System.Drawing.Size(180, 21);
            this.tibbi_kayit_id.TabIndex = 182;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(508, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 21);
            this.label1.TabIndex = 181;
            this.label1.Text = "Tıbbı Kayıt ID";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.Location = new System.Drawing.Point(167, 2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(49, 21);
            this.label12.TabIndex = 179;
            this.label12.Text = "İşlem";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 26);
            this.label6.TabIndex = 174;
            this.label6.Text = "Reçeteler";
            // 
            // talimat
            // 
            this.talimat.Location = new System.Drawing.Point(512, 121);
            this.talimat.Name = "talimat";
            this.talimat.Size = new System.Drawing.Size(350, 20);
            this.talimat.TabIndex = 185;
            // 
            // Talimatlar
            // 
            this.Talimatlar.AutoSize = true;
            this.Talimatlar.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Talimatlar.Location = new System.Drawing.Point(508, 99);
            this.Talimatlar.Name = "Talimatlar";
            this.Talimatlar.Size = new System.Drawing.Size(87, 21);
            this.Talimatlar.TabIndex = 186;
            this.Talimatlar.Text = "Talimatlar";
            // 
            // UC_receteler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Talimatlar);
            this.Controls.Add(this.talimat);
            this.Controls.Add(this.dozaj);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.islem);
            this.Controls.Add(this.id);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ara);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.duzenle);
            this.Controls.Add(this.sil);
            this.Controls.Add(this.ekle);
            this.Controls.Add(this.tibbi_kayit_id);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label6);
            this.Name = "UC_receteler";
            this.Size = new System.Drawing.Size(984, 561);
            this.Load += new System.EventHandler(this.UC_receteler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox dozaj;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox islem;
        private System.Windows.Forms.TextBox id;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button ara;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button duzenle;
        private System.Windows.Forms.Button sil;
        private System.Windows.Forms.Button ekle;
        private System.Windows.Forms.TextBox tibbi_kayit_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox talimat;
        private System.Windows.Forms.Label Talimatlar;
    }
}
