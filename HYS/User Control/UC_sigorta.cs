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
    public partial class UC_sigorta : UserControl
    {
        public UC_sigorta()
        {
            InitializeComponent();
        }

        private void UC_sigorta_Load(object sender, EventArgs e)
        {
            sigortalarYazdir();

            islem.Items.Add("Ekle");
            islem.Items.Add("Sil");
            islem.Items.Add("Düzenle");
            islem.Items.Add("Ara");
        }

        public void sigortalarYazdir()
        {
            string sorgu = "SELECT * FROM sigorta";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }

        private void ekle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("INSERT INTO sigorta (police_detaylari) VALUES (@p1)", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@p1", police_det.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir sigorta eklendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            sigortalarYazdir();
        }

        private void sil_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("DELETE FROM sigorta WHERE id = @p1", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@p1", int.Parse(id.Text));

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir sigorta silindi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }

            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            sigortalarYazdir();
        }

        private void duzenle_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("UPDATE sigorta SET police_detaylari = @police WHERE id = @id\r\n", veriTabini.baglanti);
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));
            command1.Parameters.AddWithValue("@police", police_det.Text);

            try
            {
                command1.ExecuteNonQuery();
                MessageBox.Show("Bir sigorta düzenlendi");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}");
            }
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Open)
                veriTabini.baglanti.Close();

            sigortalarYazdir();
        }

        private void ara_Click(object sender, EventArgs e)
        {
            if (veriTabini.baglanti.State == System.Data.ConnectionState.Closed)
                veriTabini.baglanti.Open();

            NpgsqlCommand command1 = new NpgsqlCommand("SELECT * FROM sigorta WHERE id = @id", veriTabini.baglanti);
            
            command1.Parameters.AddWithValue("@id", int.Parse(id.Text));

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
                police_det.Enabled = true;
                ekle.Enabled = true;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Sil")
            {
                id.Enabled = true;
                police_det.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = false;
                sil.Enabled = true;
            }
            else if (islem.Text == "Düzenle")
            {
                id.Enabled = true;
                police_det.Enabled = true;
                ekle.Enabled = false;
                duzenle.Enabled = true;
                ara.Enabled = false;
                sil.Enabled = false;
            }
            else if (islem.Text == "Ara")
            {
                id.Enabled = true;
                police_det.Enabled = false;
                ekle.Enabled = false;
                duzenle.Enabled = false;
                ara.Enabled = true;
                sil.Enabled = false;
            }
        }
    }
}
