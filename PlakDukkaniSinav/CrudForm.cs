using PlakDukkaniSinav.Class;
using PlakDukkaniSinav.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlakDukkaniSinav
{
    public partial class CrudForm : Form
    {
        UygulamaDbContext _db;
        public CrudForm()
        {
            InitializeComponent();
            _db = new UygulamaDbContext();
            ComboEkle();
            cmbSatis.Text = "Seçiniz";
            dgvListe1.DataSource = _db.Albumler.ToList();
            Temizlik();

        }
        private void ComboEkle()
        {
            cmbSatis.Items.Add("Satışı Devam Ediyor");
            cmbSatis.Items.Add("Satışı Durduruldu");

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (!(String.IsNullOrEmpty(txtAlbumAdi.Text) && String.IsNullOrEmpty(txtFiyat.Text) && String.IsNullOrEmpty(txtIndirim.Text) && String.IsNullOrEmpty(txtTarih.Text) && String.IsNullOrEmpty(txtSanatci.Text) && cmbSatis.SelectedIndex == -1))
                {
                    Album guncel = new Album();
                    guncel.AlbumAdi = txtAlbumAdi.Text;
                    guncel.Sanatci = txtSanatci.Text;
                    guncel.CikisTarihi = Convert.ToInt32(txtTarih.Text);
                    guncel.IndirimOrani = Convert.ToDecimal(txtIndirim.Text);
                    guncel.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                    if (cmbSatis.SelectedItem == "Satışı Devam Ediyor")
                    {
                        guncel.SatisDevamMi = true;
                    }
                    else if (cmbSatis.SelectedItem == "Satışı Durduruldu")
                    {
                        guncel.SatisDevamMi = false;
                    }
                    _db.Albumler.Add(guncel);
                    _db.SaveChanges();
                    MessageBox.Show("Ekleme Yapıldı");
                    Temizlik();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("HATA" + ex.Message);
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                var guncel = _db.Albumler.Find(Convert.ToInt32(txtId.Text));
                if (guncel != null) 
                {
                    guncel.AlbumAdi = txtAlbumAdi.Text;
                    guncel.Sanatci = txtSanatci.Text;
                    guncel.CikisTarihi = Convert.ToInt32(txtTarih.Text);
                    guncel.IndirimOrani = Convert.ToDecimal(txtIndirim.Text);
                    guncel.Fiyat = Convert.ToDecimal(txtFiyat.Text);
                    if (cmbSatis.SelectedItem == "Satışı Devam Ediyor")
                    {
                        guncel.SatisDevamMi = true;
                    }
                    else if (cmbSatis.SelectedItem == "Satışı Durduruldu")
                    {
                        guncel.SatisDevamMi = false;
                    }
                    _db.SaveChanges();
                    Temizlik();
                    MessageBox.Show("Guncellendi");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA"+ex.Message);
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                var silinecek = _db.Albumler.Find(Convert.ToInt32(txtId.Text));
                _db.Albumler.Remove(silinecek);
                _db.SaveChanges();
                Temizlik();
                MessageBox.Show("Silme işlemi gerceklesti");
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA"+ex.Message);
            }
        }
        private void Temizlik()
        {
            txtAlbumAdi.Text = "";
            txtFiyat.Text = "";
            txtId.Text = "";
            txtIndirim.Text = "";
            txtSanatci.Text = "";
            txtTarih.Text = "";
            cmbSatis.Text = "Seçiniz";
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            try
            {
                dgvListe1.DataSource = _db.Albumler.ToList();
                dgvListe2.DataSource = _db.Albumler.Where(x => x.SatisDevamMi == false).Select(x => new
                {
                    AlbumAdi = x.AlbumAdi,
                    Grup = x.Sanatci
                }).ToList();
                dgvListe3.DataSource = _db.Albumler.Where(x => x.SatisDevamMi == true).Select(x => new
                {
                    AlbumAdi = x.AlbumAdi,
                    Grup = x.Sanatci
                }).ToList();
                dgvListe4.DataSource = _db.Albumler.OrderBy(x => x.CikisTarihi).Take(10).Select(x => new
                {
                    AlbumAdi = x.AlbumAdi,
                    Grup = x.Sanatci
                }).ToList();
                dgvListe5.DataSource = _db.Albumler.OrderBy(x => x.IndirimOrani).Select(x => new
                {
                    AlbumAdi = x.AlbumAdi,
                    Grup = x.Sanatci
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("HATA"+ex.Message);
            }

        }
       
    }
}
