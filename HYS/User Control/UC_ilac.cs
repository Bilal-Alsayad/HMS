using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HYS.User_Control
{
    public partial class UC_ilac : UserControl
    {
        public UC_ilac()
        {
            InitializeComponent();
        }

        private void UC_ilac_Load(object sender, EventArgs e)
        {
            ilaclarYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void ilaclarYazdir()
        {
            string sorgu = "SELECT * FROM ilac\r\n";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO ilac (recete_id, adi, baslangic_tarihi, bitis_tarihi, maliyet)"+
                "VALUES (@recete, @adi, @baslangic::DATE, @bitis::DATE, @maliyet)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@recete", int.Parse(receteId.Text));
            command1.Parameters.AddWithValue("@adi", ilacAdi.Text);
            command1.Parameters.AddWithValue("@baslangic", baslangic.Value.Date);
            command1.Parameters.AddWithValue("@bitis", bitis.Value.Date);
            command1.Parameters.AddWithValue("@maliyet", int.Parse(maliyt.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir ilaç eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            ilaclarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM ilac WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir ilaç silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            ilaclarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE ilac SET adi = @adi, baslangic_tarihi = @baslangic, bitis_tarihi = @bitis ,maliyet = @maliyet WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@adi", ilacAdi.Text);
            command1.Parameters.AddWithValue("@baslangic", baslangic.Value.Date);
            command1.Parameters.AddWithValue("@bitis", bitis.Value.Date);
            command1.Parameters.AddWithValue("@maliyet", int.Parse(maliyt.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir ilaç düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            ilaclarYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM ilac WHERE recete_id = @recete", veriTabini.baglanti);

            command1.Parameters.AddWithValue("@recete", int.Parse(receteId.Text));

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(command1);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);
            dataGridView1.DataSource = dataSet.Tables[0];

            try
            {
                command1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();
        }

        private void islem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (islem.Text == "Ekle")
            {
                id.Enabled = false;
                receteId.Enabled = true;
                ilacAdi.Enabled = true;
                baslangic.Enabled = true;
                bitis.Enabled = true;
                maliyt.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                receteId.Enabled = false;
                ilacAdi.Enabled = false;
                baslangic.Enabled = false;
                bitis.Enabled = false;
                maliyt.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                receteId.Enabled = false;
                ilacAdi.Enabled = true;
                baslangic.Enabled = true;
                bitis.Enabled = true;
                maliyt.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = false;
                receteId.Enabled = true;
                ilacAdi.Enabled = false;
                baslangic.Enabled = false;
                bitis.Enabled = false;
                maliyt.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
