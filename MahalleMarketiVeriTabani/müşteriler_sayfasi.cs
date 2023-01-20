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
using System.Data.SqlClient;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MahalleMarketiVeriTabani
{
    public partial class müşteriler_sayfasi : Form
    {
        public müşteriler_sayfasi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ZEYNEPSILA;Initial Catalog=MahalleMarketiVeriTabani;Integrated Security=True");

        private void listele()//veritabanındaki kayıtların görüntülenmesi
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from müsteriler", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            saticilar_sayfasi saticilar = new saticilar_sayfasi();
            saticilar.ShowDialog();
            this.Close();
        }

        private void pictureBox22_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zaten Müşteriler Sayfasındasınız!");
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            this.Hide();
            ürünler_sayfasi ürünler = new ürünler_sayfasi();
            ürünler.ShowDialog();
            this.Close();
        }

        private void pictureBox26_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void müşteriler_sayfasi_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String musteriAdi = textBox5.Text;
            String musteriKodu = textBox3.Text;
            String urunFiyat = textBox2.Text;
            String odedigiMiktar = textBox1.Text;
            String satinAldigiUrunKodu = textBox7.Text;
            String musteriAdresi = textBox4.Text;
            DateTime odemeYaptigiTarih = dateTimePicker1.Value.Date;
            DateTime satinAldigiTarih = dateTimePicker2.Value.Date;

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Eksik veri girdiniz!");
            }
            else
            {
                int Borc = Convert.ToInt32(urunFiyat) - Convert.ToInt32(odedigiMiktar);

                try
                {
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO müsteriler (MusteriAdi, MusteriKodu, SatinAldigiUrunFiyat, OdedigiMiktar, SatinAldigiTarih, Borc, MusteriAdresi, OdemeYaptigiTarih, SatinAldigiUrunKodu) VALUES ('" + musteriAdi + "', '" + musteriKodu + "', '" + urunFiyat + "', '" + odedigiMiktar + "', '" + satinAldigiTarih + "', '" + Borc + "', '" + musteriAdresi + "', '" + odemeYaptigiTarih + "', '" + satinAldigiUrunKodu + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show(this, "Müsteri Kaydedildi!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {//düzenleme islemi
            String musteriAdi = textBox5.Text;
            String musteriKodu = textBox3.Text;
            String urunFiyat = textBox2.Text;
            String odedigiMiktar = textBox1.Text;
            String satinAldigiUrunKodu = textBox7.Text;
            String musteriAdresi = textBox4.Text;
            DateTime odemeYaptigiTarih = dateTimePicker1.Value.Date;
            DateTime satinAldigiTarih = dateTimePicker2.Value.Date;

            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE ürünler SET MusteriAdi ='" + musteriAdi + "', MusteriKodu = '" + musteriKodu + "',SatinAldigiUrunFiyat = '" + urunFiyat + "',OdedigiMiktar = '" + odedigiMiktar + "',SatinAldigiTarih = '" + satinAldigiTarih + "', MusteriAdresi ='" + musteriAdresi + "', OdemeYaptigiTarih = '" + odemeYaptigiTarih + "', SatinAldigiUrunKodu = '" + satinAldigiUrunKodu + "' WHERE MusteriKodu = '" + musteriKodu + "'  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void button3_Click(object sender, EventArgs e)
        {//silme islemi
            String musteriKodu = textBox3.Text; //seçilen satırın müsteri kodunu alıyor
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM müsteriler WHERE MusteriKodu=('" + musteriKodu + "') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }
    }
}
