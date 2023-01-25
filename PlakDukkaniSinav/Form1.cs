using PlakDukkaniSinav.Class;
using PlakDukkaniSinav.Context;
using System.Security.Cryptography;
using System.Text;

namespace PlakDukkaniSinav
{
    public partial class Form1 : Form
    {
        UygulamaDbContext db;
        public Form1()
        {
            InitializeComponent();
            db = new UygulamaDbContext();
            txtSifre.PasswordChar = '*';
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            var frm = new KayitForm();
            frm.ShowDialog();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(String.IsNullOrEmpty(txtKullaniciAdi.Text) && String.IsNullOrEmpty(txtSifre.Text)))
                {
                    var sfir = sha256_hash(txtSifre.Text);
                    if (db.Adminler.Any(x => x.KullaniciAdi == txtKullaniciAdi.Text && x.Sifre == sfir))
                    {
                        Temizlik();
                        var frm = new CrudForm();
                        frm.ShowDialog();

                    }
                }
                else
                {
                    MessageBox.Show("Kullanýcý adý veya sifre hatalý");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA!"+ex.Message);
            }
        }

        private string sha256_hash(string sifre)
        {
            using (SHA256 hash = SHA256Managed.Create())
            { return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(sifre)).Select(l => l.ToString("X2"))); }
        }

        private void chkSifre_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSifre.Checked)
            {
                txtSifre.PasswordChar= '\0';
            }
            else
            {
                txtSifre.PasswordChar = '*';
            }
        }
        private void Temizlik()
        {
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
        }
    }
}