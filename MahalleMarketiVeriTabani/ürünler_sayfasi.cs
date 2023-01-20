using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace MahalleMarketiVeriTabani
{
    public partial class ürünler_sayfasi : Form
    {
        public ürünler_sayfasi()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=ZEYNEPSILA;Initial Catalog=MahalleMarketiVeriTabani;Integrated Security=True");
        private void ürünler_sayfasi_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void listele()//veritabanındaki kayıtların görüntülenmesi
        {
            baglanti.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select *from ürünler", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            String miktari = textBox1.Text;
            String gelisFiyati = textBox2.Text;
            String irsaliyeNumarasi = textBox3.Text;
            String firmaKodu = textBox4.Text;
            String firmaAdi = textBox5.Text;
            String urunKodu = textBox6.Text;
            String ulkeKodu = textBox7.Text;
            String satisFiyati = textBox8.Text;
            DateTime gelisTarihi = dateTimePicker1.Value.Date;

            int Kar = Convert.ToInt32(satisFiyati) - Convert.ToInt32(gelisFiyati);
            int Zarar = Convert.ToInt32(gelisFiyati) - Convert.ToInt32(satisFiyati);

            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "")
            {
                MessageBox.Show("Eksik veri girdiniz!");
            }
            else
            {
                int[] BarkodNumarasiOlusturma = new int[13];
                BarkodNumarasiOlusturma[0] = ulkeKodu[0];
                BarkodNumarasiOlusturma[1] = ulkeKodu[1];
                BarkodNumarasiOlusturma[2] = ulkeKodu[2];
                BarkodNumarasiOlusturma[3] = firmaKodu[0];
                BarkodNumarasiOlusturma[4] = firmaKodu[1];
                BarkodNumarasiOlusturma[5] = firmaKodu[2];
                BarkodNumarasiOlusturma[6] = firmaKodu[3];
                BarkodNumarasiOlusturma[7] = urunKodu[0];
                BarkodNumarasiOlusturma[8] = urunKodu[1];
                BarkodNumarasiOlusturma[9] = urunKodu[2];
                BarkodNumarasiOlusturma[10] = urunKodu[3];
                BarkodNumarasiOlusturma[11] = urunKodu[4];

                int ÇiftHanelerinToplamı = 0, TekHanelerinToplamı = 0;
                for (int i = 0; i < 12; i++)
                {
                    if (BarkodNumarasiOlusturma[i] % 2 == 0)
                    {
                        ÇiftHanelerinToplamı += i;
                    }
                    else
                    {
                        TekHanelerinToplamı += i;
                    }

                }

                int KontrolHanesiOluşturma = (ÇiftHanelerinToplamı * 3) + TekHanelerinToplamı;

                int KontrolHanesi = 0;
                for (int i = 10; i < 100; i++)
                {
                    if (KontrolHanesiOluşturma <= i)
                        KontrolHanesi = i - KontrolHanesiOluşturma;
                }

                int[] BarkodNumarası = new int[13];
                BarkodNumarası = BarkodNumarasiOlusturma;
                Convert.ToString(KontrolHanesi);
                BarkodNumarası[12] = KontrolHanesi;

                try
                {
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("INSERT INTO ürünler (TedarikciFirmaAdi, TedarikciFirmaKodu, UrunKodu, UlkeKodu, IrsaliyeNumarasi, GelisFiyati, satisFiyati, Kar, Zarar, Miktari, BarkodNumarasi, GelisTarihi) VALUES ('" + firmaAdi + "', '" + firmaKodu + "', '" + urunKodu + "', '" + ulkeKodu + "', '" + irsaliyeNumarasi + "', '" + gelisFiyati + "', '" + satisFiyati + "', '" + Kar + "', '" + Zarar + "', '" + miktari + "', '" + BarkodNumarası + "', '" + gelisTarihi + "')", baglanti);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    listele();
                    MessageBox.Show(this, "Ürün Kaydedildi!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }  
           }


        private void pictureBox26_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Zaten Ürünler Sayfasındasınız!");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            saticilar_sayfasi saticilar = new saticilar_sayfasi();
            saticilar.ShowDialog();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String urunKodu = textBox6.Text; //seçilen satırın ürün kodunu alıyor
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM ürünler WHERE UrunKodu=('" + urunKodu + "') ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String miktari = textBox1.Text;
            String gelisFiyati = textBox2.Text;
            String irsaliyeNumarasi = textBox3.Text;
            String firmaKodu = textBox4.Text;
            String firmaAdi = textBox5.Text;
            String urunKodu = textBox6.Text;
            String ulkeKodu = textBox7.Text;
            DateTime gelisTarihi = dateTimePicker1.Value.Date;

            baglanti.Close();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("UPDATE ürünler SET TedarikciFirmaAdi ='" + firmaAdi + "', TedarikciFirmaKodu = '" + firmaKodu + "',UrunKodu = '" + urunKodu + "',UlkeKodu = '" + ulkeKodu + "',IrsaliyeNumarasi = '" + irsaliyeNumarasi + "', GelisFiyati ='" + gelisFiyati + "', Miktari = '" + miktari + "', GelisTarihi = '" + gelisTarihi + "' WHERE UrunKodu = '" + urunKodu + "'  ", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }


        private void pictureBox22_Click(object sender, EventArgs e)
        {
            this.Hide();
            müşteriler_sayfasi musteriler = new müşteriler_sayfasi();
            musteriler.ShowDialog();
            this.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream fs = new FileStream(@"C:\Users\zynps\source\repos\MahalleMarketiVeriTabani\MahalleMarketiVeriTabani\veritabani.txt", FileMode.Open);
                StreamReader oku = new StreamReader(fs);
                richTextBox1.Text = oku.ReadToEnd();
                oku.Close();
                fs.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Dosya açılamadı!");
            }

        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
