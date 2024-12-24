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
    public partial class UC_lab_test : UserControl
    {
        public UC_lab_test()
        {
            InitializeComponent();
        }

        private void UC_lab_test_Load(object sender, EventArgs e)
        {
            testlerYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }
        public void testlerYazdir()
        {
            string sorgu = "SELECT * FROM lab_testi\r\n";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO lab_testi (hasta_id, doktor_id, test_turu, tarih, sonucu, maliyet) " +
                "VALUES (@hasta_id, @doktor_id, @test_turu, @tarih::DATE, @sonucu, @maliyet)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@hasta_id", int.Parse(hastaId.Text));
            command1.Parameters.AddWithValue("@doktor_id", int.Parse(doktorId.Text));
            command1.Parameters.AddWithValue("@test_turu", test_turu.Text);
            command1.Parameters.AddWithValue("@tarih", tarih.Value.Date);
            command1.Parameters.AddWithValue("@sonucu", sonuc.Text);
            command1.Parameters.AddWithValue("@maliyet", int.Parse(maliyet.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir lab testi eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            testlerYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM lab_testi WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir randevu silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            testlerYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE lab_testi SET test_turu = @test_turu, tarih = @tarih::DATE, sonucu = @sonucu, maliyet = @maliyet WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@test_turu", test_turu.Text);
            command1.Parameters.AddWithValue("@tarih", tarih.Value.Date);
            command1.Parameters.AddWithValue("@sonucu", sonuc.Text);
            command1.Parameters.AddWithValue("@maliyet", int.Parse(maliyet.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir lab testi düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            testlerYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM lab_testi WHERE hasta_id = @hasta_id", veriTabini.baglanti);

            command1.Parameters.AddWithValue("@hasta_id", int.Parse(hastaId.Text));

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
                hastaId.Enabled = true;
                doktorId.Enabled = true;
                tarih.Enabled = true;
                test_turu.Enabled = true;
                sonuc.Enabled = true;
                maliyet.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                hastaId.Enabled = false;
                doktorId.Enabled = false;
                tarih.Enabled = false;
                test_turu.Enabled = false;
                sonuc.Enabled = false;
                maliyet.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                hastaId.Enabled = false;
                doktorId.Enabled = false;
                tarih.Enabled = true;
                test_turu.Enabled = true;
                sonuc.Enabled = true;
                maliyet.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = false;
                hastaId.Enabled = true;
                doktorId.Enabled = false;
                tarih.Enabled = false;
                test_turu.Enabled = false;
                sonuc.Enabled = false;
                maliyet.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
