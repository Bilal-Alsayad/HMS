using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HYS.User_Control
{
    public partial class UC_fatura : UserControl
    {
        public UC_fatura()
        {
            InitializeComponent();
        }

        private void UC_fatura_Load(object sender, EventArgs e)
        {
            string sorgu = "SELECT id ,concat(kisi.ad || ' ' || kisi.soyad) AS Adi, detaylar, topmlam FROM fatura\r\nINNER JOIN hasta ON hasta.kisi_id = fatura.hasta_id\r\nNATURAL JOIN kisi;";
            DataSet dataSet = veriTabini.yazdir(sorgu);
            dataGridView1.DataSource = dataSet.Tables[0];
        }
    }
}
