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

namespace HYS.User_Control
{
    public partial class UC_vardiyalar : UserControl
    {
        public UC_vardiyalar()
        {
            InitializeComponent();
        }

        private void UC_vardiyalar_Load(object sender, EventArgs e)
        {
            vardiyalarYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void vardiyalarYazdir()
        {
            string sorgu = "SELECT * FROM vardiya";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO vardiya (calisan_id, tarih, baslangic_saati, bitis_saati) " +
                "VALUES (@calisan, @tarih::DATE, @baslangic::TIME, @bitis::TIME)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@calisan", int.Parse(calisam.Text));
            command1.Parameters.AddWithValue("@tarih", tarih.Value.Date);
            command1.Parameters.AddWithValue("@baslangic", baslangic.Value);
            command1.Parameters.AddWithValue("@bitis", bitis.Value);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir vardiya eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            vardiyalarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM vardiya WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir vardiya silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            vardiyalarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE vardiya SET calisan_id = @calisan, tarih = @tarih," +
                " baslangic_saati = @baslangic, bitis_saati = @bitis WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@calisan", int.Parse(calisam.Text));
            command1.Parameters.AddWithValue("@tarih", tarih.Value.Date);
            command1.Parameters.AddWithValue("@baslangic", baslangic.Value);
            command1.Parameters.AddWithValue("@bitis", bitis.Value);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir vardiya düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            vardiyalarYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM vardiya WHERE calisan_id = @id", veriTabini.baglanti);

            command1.Parameters.AddWithValue("@id", int.Parse(calisam.Text));

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
                calisam.Enabled = true;
                tarih.Enabled = true;
                baslangic.Enabled = true;
                bitis.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                calisam.Enabled = false;
                tarih.Enabled = false;
                baslangic.Enabled = false;
                bitis.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                calisam.Enabled = true;
                tarih.Enabled = true;
                baslangic.Enabled = true;
                bitis.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = false;
                calisam.Enabled = true;
                tarih.Enabled = false;
                baslangic.Enabled = false;
                bitis.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
