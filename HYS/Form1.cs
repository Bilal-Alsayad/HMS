using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HYS.User_Control;

namespace HYS
{
    public partial class HYS : Form
    {
        public HYS()
        {
            InitializeComponent();
        }

        private void userControlEkle(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            panelContainer.Controls.Clear();
            panelContainer.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void hastalarMenu_Click(object sender, EventArgs e)
        {
            UC_hastalar uc = new UC_hastalar();
            userControlEkle(uc);
            
        }

        private void calisanlar_Click(object sender, EventArgs e)
        {
            UC_calisan uc = new UC_calisan();
            userControlEkle(uc);
        }

        private void musteriIslemler_Click(object sender, EventArgs e)
        {
            UC_doktor uc = new UC_doktor();
            userControlEkle(uc);
        }

        private void hizmetler_Click(object sender, EventArgs e)
        {
            UChemsire uc = new UChemsire();
            userControlEkle(uc);
        }

        private void sigorta_Click(object sender, EventArgs e)
        {
            UC_sigorta uc = new UC_sigorta();
            userControlEkle(uc);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UC_bolum uc = new UC_bolum();
            userControlEkle(uc);
        }

        private void harcamaTakibi_Click(object sender, EventArgs e)
        {
            UC_vardiyalar uc = new UC_vardiyalar();
            userControlEkle(uc);
        }

        private void randevuYon_Click(object sender, EventArgs e)
        {
            UC_randevu uc = new UC_randevu();
            userControlEkle(uc);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UC_tibbi_kayit uc = new UC_tibbi_kayit();
            userControlEkle(uc);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UC_receteler uc = new UC_receteler();
            userControlEkle(uc);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            UC_fatura uc = new UC_fatura();
            userControlEkle(uc);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UC_ilac uc = new UC_ilac();
            userControlEkle(uc);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            UC_lab_test uc = new UC_lab_test();
            userControlEkle(uc);
        }
    }
}
