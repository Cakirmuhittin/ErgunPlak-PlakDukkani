using PlakDukkaniSinav.Class;
using PlakDukkaniSinav.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlakDukkaniSinav
{
    public partial class KayitForm : Form
    {
        UygulamaDbContext _db;
        public KayitForm()
        {
            InitializeComponent();
            _db = new UygulamaDbContext();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {

                if (_db.Adminler.Any(x => x.KullaniciAdi == txtKullaniciAd.Text))
                {
                    MessageBox.Show("Bu kullanıcı Adı daha önce alınmıştır.Lütfen yeni bir Kullanıcı Adı deneyiniz");
                    return;
                }
                else
                {
                    string password = txtSifre.Text;
                    if (IsValidPassword(password))
                    {
                        if (txtSifre.Text == txtSifreTekrar.Text)
                        {
                            Admin admin = new Admin();
                            admin.KullaniciAdi = txtKullaniciAd.Text;
                            string ySifre = sha256_hash(txtSifre.Text);
                            admin.Sifre = ySifre;
                            _db.Adminler.Add(admin);
                            _db.SaveChanges();
                            Temizlik();
                            MessageBox.Show("Yönetici Kaydı Yapılmıştır!");
                        }
                        else
                        {
                            MessageBox.Show("Şifre ve şifre doğrulama uyuşmadı lütfen şifreleri kontrol ediniz");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Geçersiz şifre türü lütfen kriterlere uygun bir şifre yazınız!");
                        return;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA!" + ex.Message);
            }

        }
        private void Temizlik()
        {
            txtSifre.Text = "";
            txtKullaniciAd.Text = "";
            txtSifreTekrar.Text = "";
        }
        private bool IsValidPassword(string password)
        {
            int passwordLength = 8; // şifre uzunluğu
            int numUpper = 2; // en az kaç tane büyük harf olmalı
            int numLower = 3; // en az kaç tane küçük harf olmalı
            int numSpecial = 2; // en az kaç tane özel karakter olmalı

            // özel karakterler listesi
            char[] specialCharacters = new char[] { '!', ':', '+', '*' };

            // şifrenin uzunluğunu kontrol et
            if (password.Length <= passwordLength)
            {
                return false;
            }

            // büyük harflerin sayısını kontrol et
            int numUpperFound = 0;
            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    numUpperFound++;
                }
            }
            if (numUpperFound < numUpper)
            {
                return false;
            }

            // küçük harflerin sayısını kontrol et
            int numLowerFound = 0;
            foreach (char c in password)
            {
                if (char.IsLower(c))
                {
                    numLowerFound++;
                }
            }
            if (numLowerFound < numLower)
            {
                return false;
            }

            // özel karakterlerin sayısını kontrol et
            int numSpecialFound = 0;
            foreach (char c in password)
            {
                if (specialCharacters.Contains(c))
                {
                    numSpecialFound++;
                }
            }
            if (numSpecialFound < numSpecial)
            {
                return false;
            }

            // tüm kriterler uygunsa, şifre geçerli
            return true;
        }
        private string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            { return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2"))); }
        }
    }
}
