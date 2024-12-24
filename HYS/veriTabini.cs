using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace HYS
{
    static class veriTabini
    {
        public static NpgsqlConnection baglanti = new NpgsqlConnection("server=localhost; port=5432; Database=hys; user ID=postgres; password=123123");

        public static DataSet yazdir(string sorgu)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet dataSet = new DataSet();
            da.Fill(dataSet);
            return dataSet;
        }
    }
}
