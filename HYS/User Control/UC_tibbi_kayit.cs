using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HYS.User_Control
{
    public partial class UC_tibbi_kayit : UserControl
    {
        public UC_tibbi_kayit()
        {
            InitializeComponent();
        }

        private void UC_tibbi_kayit_Load(object sender, EventArgs e)
        {
            kayitlarYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void kayitlarYazdir()
        {
            string sorgu = "SELECT * FROM tibbi_kayit";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO tibbi_kayit (hasta_id, olustuma_tarihi, teshis, tedavi_plani) " +
                "VALUES (@hasta, now()::DATE, @teshis, @tedavi_plani)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@hasta", int.Parse(hastaId.Text));
            command1.Parameters.AddWithValue("@teshis", teshis.Text);
            command1.Parameters.AddWithValue("@tedavi_plani", tedavi_plani.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir tıbbı kayıt eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            kayitlarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM tibbi_kayit WHERE id = @id", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir tıbbı kayıt silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            kayitlarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE tibbi_kayit SET hasta_id = @hasta, teshis = @teshis, " +
                "tedavi_plani = @tedavi_plani WHERE id = @id\r\n", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@hasta", int.Parse(hastaId.Text));
            command1.Parameters.AddWithValue("@teshis", teshis.Text);
            command1.Parameters.AddWithValue("@tedavi_plani", tedavi_plani.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir tıbbı kayıt düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            kayitlarYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM tibbi_kayit WHERE hasta_id = @hasta", veriTabini.baglanti);

            command1.Parameters.AddWithValue("@hasta", int.Parse(hastaId.Text));

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
                teshis.Enabled = true;
                tedavi_plani.Enabled = true;
                tedavi_plani.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                hastaId.Enabled = false;
                teshis.Enabled = false;
                tedavi_plani.Enabled = false;
                tedavi_plani.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                hastaId.Enabled = false;
                teshis.Enabled = true;
                tedavi_plani.Enabled = true;
                tedavi_plani.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = false;
                hastaId.Enabled = true;
                teshis.Enabled = false;
                tedavi_plani.Enabled = false;
                tedavi_plani.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
