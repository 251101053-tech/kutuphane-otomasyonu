using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace nesne_tbanaloı_programlama_odev_1
{
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Server=.\\SQLEXPRESS;Database=Ray;Trusted_Connection=True;TrustServerCertificate=True;");
        string secilen;
        void TabloyuYenile()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RAYS", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            TabloyuYenile();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("INSERT INTO RAYS (Ad, Soyad, numara) VALUES (@ad, @soyad, @numara)", baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@numara", txtNumara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            TabloyuYenile();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM RAYS WHERE numara = @numara", baglanti);
            komut.Parameters.AddWithValue("@numara", secilen);
            komut.ExecuteNonQuery();
            baglanti.Close();

            TabloyuYenile();
        }

        private void btnGuncele_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE RAYS SET Ad = @ad, Soyad = @soyad, numara = @yeniNumara WHERE numara = @eskiNumara", baglanti);
            komut.Parameters.AddWithValue("@ad", txtAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            komut.Parameters.AddWithValue("@yeniNumara", txtNumara.Text); // Kutudaki yeni numara
            komut.Parameters.AddWithValue("@eskiNumara", secilen);        // Tablodan seçilen eski numara
            komut.ExecuteNonQuery();
            baglanti.Close();

            TabloyuYenile();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM RAYS WHERE Ad LIKE @isim", baglanti);
            da.SelectCommand.Parameters.AddWithValue("@isim", "%" + txtAd.Text + "%");
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                txtAd.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                txtSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                txtNumara.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                secilen = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
        }
    }
}
