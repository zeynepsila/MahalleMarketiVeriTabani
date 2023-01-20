using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MahalleMarketiVeriTabani
{
    public partial class giris_sayfasi : Form
    {
        public giris_sayfasi()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            var username = textBox1.Text;
            var password = textBox2.Text;
            var errorMessage = "";
            var isError = false;

            if (string.IsNullOrEmpty(username))
            {
                errorMessage += ("Kullanici adini bos birakamazsiniz!\r\n");
                isError = true;
            }

            if (string.IsNullOrEmpty(password))
            {
                errorMessage += ("Sifre kismini bos birakamazsiniz!\r\n");
                isError = true;
            }

            if (isError)
            {
                MessageBox.Show(errorMessage, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            var loginState = Login(username, password);

            if (loginState)
            {
                ürünler_sayfasi ürünler = new ürünler_sayfasi();
                ürünler.ShowDialog();
                this.Close();

            }
            else
            {
                MessageBox.Show("Giris Basarisiz");
                this.Close();

            }
        }

        public bool Login(string username, string password)
        {

            var SaticiKulaniciAdi = "yönetici";
            var SaticiSifre = "yönetici";


            if (username == SaticiKulaniciAdi && password == SaticiSifre)
                return true;

            return false;

        }

        private void giris_sayfasi_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}
