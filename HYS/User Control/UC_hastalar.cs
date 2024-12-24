using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace HYS
{
    public partial class UC_hastalar : UserControl
    {
        public UC_hastalar()
        {
            InitializeComponent();
        }

        private void UC_hastalar_Load(object sender, EventArgs e)
        {
            hastalarYazdir();

            cinsiyet.Items.Add("Erkek");
            cinsiyet.Items.Add("Kadın");

            kanGrubu.Items.Add("AB+");
            kanGrubu.Items.Add("AB-");
            kanGrubu.Items.Add("A+");
            kanGrubu.Items.Add("A-");
            kanGrubu.Items.Add("B+");
            kanGrubu.Items.Add("B-");
            kanGrubu.Items.Add("O+");
            kanGrubu.Items.Add("O-");

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void hastalarYazdir()
        {
            string sorgu = "SELECT * FROM hastalar_yazdir()";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL hasta_ekle(@ad, @soyad, @dogum::DATE, @cinsiyet::CHAR(5)," +
                " @kan::VARCHAR(3), @sigorta, @tel::CHAR(11), @adres, @epsta)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@ad", adi.Text);
            command1.Parameters.AddWithValue("@soyad", soyadi.Text);
            command1.Parameters.AddWithValue("@dogum", dogumTarihi.Value.Date);
            command1.Parameters.AddWithValue("@cinsiyet", cinsiyet.Text);
            command1.Parameters.AddWithValue("@kan", kanGrubu.Text);
            command1.Parameters.AddWithValue("@sigorta", string.IsNullOrEmpty(sigortaNo.Text) ? 0 : int.Parse(sigortaNo.Text));
            command1.Parameters.AddWithValue("@tel", telefon.Text);
            command1.Parameters.AddWithValue("@adres", adres.Text);
            command1.Parameters.AddWithValue("@epsta", eposta.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir hasta eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            hastalarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL hasta_silme(@id)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir hasta silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            hastalarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("CALL hasta_guncelleme(@id, @ad, @soyad, @dogum::DATE, @cinsiyet::CHAR(5)," +
                " @kan::VARCHAR(3), @tel::CHAR(11), @adres, @epsta, @sigorta)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@ad", adi.Text);
            command1.Parameters.AddWithValue("@soyad", soyadi.Text);
            command1.Parameters.AddWithValue("@dogum", dogumTarihi.Value.Date);
            command1.Parameters.AddWithValue("@cinsiyet", cinsiyet.Text);
            command1.Parameters.AddWithValue("@kan", kanGrubu.Text);
            command1.Parameters.AddWithValue("@tel", telefon.Text);
            command1.Parameters.AddWithValue("@adres", adres.Text);
            command1.Parameters.AddWithValue("@epsta", eposta.Text);
            command1.Parameters.AddWithValue("@sigorta", string.IsNullOrEmpty(sigortaNo.Text) ? 0 : int.Parse(sigortaNo.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir hasta düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            hastalarYazdir();
        }
        
        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM hasta_arama(@id, @ad)", veriTabini.baglanti);
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
                kanGrubu.Enabled = true;
                sigortaNo.Enabled = true;
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
                kanGrubu.Enabled = false;
                sigortaNo.Enabled = false;
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
                kanGrubu.Enabled = true;
                sigortaNo.Enabled = true;
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
                kanGrubu.Enabled = false;
                sigortaNo.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }

    }
}
