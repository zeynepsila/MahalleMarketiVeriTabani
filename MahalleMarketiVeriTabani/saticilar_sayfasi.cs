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

namespace MahalleMarketiVeriTabani
{
    public partial class saticilar_sayfasi : Form
    {
        public saticilar_sayfasi()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=ZEYNEPSILA;Initial Catalog=MahalleMarketiVeriTabani;Integrated Security=True");

        private void listele()//veritabanındaki kayıtların görüntülenmesi
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from saticilar", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void saticilar_sayfasi_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zaten Satıcılar Sayfasındasınız!");
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            this.Hide();
            ürünler_sayfasi ürünler = new ürünler_sayfasi();
            ürünler.ShowDialog();
            this.Close();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            this.Hide();
            müşteriler_sayfasi müşteriler = new müşteriler_sayfasi();
            müşteriler.ShowDialog();
            this.Close();
        }

        private void pictureBox29_Click(object sender, EventArgs e)
        {
            this.Hide();
            müşteriler_sayfasi musteriler = new müşteriler_sayfasi();
            musteriler.ShowDialog();
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String SaticiKullaniciAdi = textBox3.Text;
            String SaticiSifre = textBox1.Text;

            if(textBox1.Text == "" || textBox3.Text == "")
            {
                MessageBox.Show("Eksik veri girdiniz!");
            }
            else
            {
                try
                {
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO saticilar (SaticiKullaniciAdi, SaticiSifre) VALUES ('" + SaticiKullaniciAdi + "', '" + SaticiSifre + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show(this, "Satıcı Kaydedildi!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String SaticiKullaniciAdi = textBox3.Text;
            String SaticiSifre = textBox1.Text;

            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE saticilar SET SaticiKullaniciAdi ='" + SaticiKullaniciAdi + "', SaticiSifre = '" + SaticiSifre + "'", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String SaticiKullaniciAdi = textBox3.Text; //seçilen satırın kullanici adini alıyor
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM saticilar WHERE SaticiKullaniciAdi=('" + SaticiKullaniciAdi + "') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void pictureBox27_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zaten Satıcılar Sayfasındasınız!");
        }

        private void pictureBox28_Click(object sender, EventArgs e)
        {
            this.Hide();
            ürünler_sayfasi ürünler = new ürünler_sayfasi();
            ürünler.ShowDialog();
            this.Close();
        }

        private void pictureBox30_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
