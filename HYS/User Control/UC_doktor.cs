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
    public partial class UC_doktor : UserControl
    {
        public UC_doktor()
        {
            InitializeComponent();
        }

        private void UC_doktor_Load(object sender, EventArgs e)
        {
            doktorlarYazdir();

            cinsiyet.Items.Add("Erkek");
            cinsiyet.Items.Add("Kadın");

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void doktorlarYazdir()
        {
            string sorgu = "SELECT * FROM doktorlar_yazdir()";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL doktor_ekle(@ad, @soyad, @dogum::DATE, @cinsiyet::CHAR(5)," +
                " @ise_tarihi_alma::DATE, @unvan, @uzmanlik, @bolum_id, @tel::CHAR(11), @adres, @epsta)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@ad", adi.Text);
            command1.Parameters.AddWithValue("@soyad", soyadi.Text);
            command1.Parameters.AddWithValue("@dogum", dogumTarihi.Value.Date);
            command1.Parameters.AddWithValue("@cinsiyet", cinsiyet.Text);
            command1.Parameters.AddWithValue("@ise_tarihi_alma", ise_alma.Value.Date);
            command1.Parameters.AddWithValue("@unvan", unvan.Text);
            command1.Parameters.AddWithValue("@uzmanlik", uzmanlik.Text);
            command1.Parameters.AddWithValue("@bolum_id", int.Parse(bolum.Text));
            command1.Parameters.AddWithValue("@tel", telefon.Text);
            command1.Parameters.AddWithValue("@adres", adres.Text);
            command1.Parameters.AddWithValue("@epsta", eposta.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir doktor eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            doktorlarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL calisan_silme(@id)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir doktor silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            doktorlarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL doktor_guncelleme(@id, @ad, @soyad, @dogum::DATE, @cinsiyet::CHAR(5)," +
                " @ise_tarihi_alma::DATE, @unvan, @uzmanlik, @bolum_id, @tel::CHAR(11), @adres, @epsta)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@ad", adi.Text);
            command1.Parameters.AddWithValue("@soyad", soyadi.Text);
            command1.Parameters.AddWithValue("@dogum", dogumTarihi.Value.Date);
            command1.Parameters.AddWithValue("@cinsiyet", cinsiyet.Text);
            command1.Parameters.AddWithValue("@ise_tarihi_alma", ise_alma.Value.Date);
            command1.Parameters.AddWithValue("@unvan", unvan.Text);
            command1.Parameters.AddWithValue("@uzmanlik", uzmanlik.Text);
            command1.Parameters.AddWithValue("@bolum_id", int.Parse(bolum.Text));
            command1.Parameters.AddWithValue("@tel", telefon.Text);
            command1.Parameters.AddWithValue("@adres", adres.Text);
            command1.Parameters.AddWithValue("@epsta", eposta.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir doktor düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            doktorlarYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM doktor_arama(@id, @ad)", veriTabini.baglanti);
            if (string.IsNullOrEmpty(id.Text))
                command1.Parameters.AddWithValue("@id", DBNull.Value);
            else
                command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            if (string.IsNullOrEmpty(adi.Text))
                command1.Parameters.AddWithValue("@ad", DBNull.Value);
            else
                command1.Parameters.AddWithValue("@ad", adi.Text);

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
                adi.Enabled = true;
                soyadi.Enabled = true;
                dogumTarihi.Enabled = true;
                adres.Enabled = true;
                cinsiyet.Enabled = true;
                telefon.Enabled = true;
                eposta.Enabled = true;
                ise_alma.Enabled = true;
                unvan.Enabled = true;
                uzmanlik.Enabled = true;
                bolum.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                adi.Enabled = false;
                soyadi.Enabled = false;
                dogumTarihi.Enabled = false;
                adres.Enabled = false;
                cinsiyet.Enabled = false;
                telefon.Enabled = false;
                eposta.Enabled = false;
                ise_alma.Enabled = false;
                unvan.Enabled = false;
                uzmanlik.Enabled = false;
                bolum.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                adi.Enabled = true;
                soyadi.Enabled = true;
                dogumTarihi.Enabled = true;
                adres.Enabled = true;
                cinsiyet.Enabled = true;
                telefon.Enabled = true;
                eposta.Enabled = true;
                ise_alma.Enabled = true;
                unvan.Enabled = true;
                uzmanlik.Enabled = true;
                bolum.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = true;
                adi.Enabled = true;
                soyadi.Enabled = false;
                dogumTarihi.Enabled = false;
                adres.Enabled = false;
                cinsiyet.Enabled = false;
                telefon.Enabled = false;
                eposta.Enabled = false;
                ise_alma.Enabled = false;
                unvan.Enabled = false;
                uzmanlik.Enabled = false;
                bolum.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
