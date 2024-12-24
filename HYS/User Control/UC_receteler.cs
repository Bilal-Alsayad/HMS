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
    public partial class UC_receteler : UserControl
    {
        public UC_receteler()
        {
            InitializeComponent();
        }

        private void UC_receteler_Load(object sender, EventArgs e)
        {
            receteYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void receteYazdir()
        {
            string sorgu = "SELECT * FROM recete";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO recete (tibbi_kayit_id, dozaj, talimatlar) " +
                "VALUES (@tibbi_kayit, @dozaj, @talimat)\r\n", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@tibbi_kayit", int.Parse(tibbi_kayit_id.Text));
            command1.Parameters.AddWithValue("@dozaj", dozaj.Text);
            command1.Parameters.AddWithValue("@talimat", talimat.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir reçete eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            receteYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM recete WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir reçete silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            receteYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE recete SET dozaj = @dozaj, talimatlar = @talimat WHERE id = @id\r\n", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@dozaj", dozaj.Text);
            command1.Parameters.AddWithValue("@talimat", talimat.Text);

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

            receteYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM recete WHERE tibbi_kayit_id = @tibbi_kayit_id", veriTabini.baglanti);

            command1.Parameters.AddWithValue("@tibbi_kayit_id", int.Parse(tibbi_kayit_id.Text));

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
                tibbi_kayit_id.Enabled = true;
                dozaj.Enabled = true;
                talimat.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                tibbi_kayit_id.Enabled = false;
                dozaj.Enabled = false;
                talimat.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                tibbi_kayit_id.Enabled = false;
                dozaj.Enabled = true;
                talimat.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = false;
                tibbi_kayit_id.Enabled = true;
                dozaj.Enabled = false;
                talimat.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
