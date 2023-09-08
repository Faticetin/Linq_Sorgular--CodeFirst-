using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Core.Mapping;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        DbOgrenciSinavEntities1 db = new DbOgrenciSinavEntities1();
        private void btnOgrenciListe_Click(object sender, EventArgs e)
        {
            var query = from item in db.Ogrenci
                        select new { item.Id, item.Ad, item.Soyad };

            dgwListe.DataSource = query.ToList();
        }

        private void btnDersListesi_Click(object sender, EventArgs e)
        {

            dgwListe.DataSource = db.Dersler.ToList();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            Ogrenci o = new Ogrenci();
            o.Ad = txtOgrAd.Text;
            o.Soyad = txtOgrSoyad.Text;
            db.Ogrenci.Add(o);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Kaydedildi");
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrId.Text);
            var x = db.Ogrenci.Find(id);
            db.Ogrenci.Remove(x);
            db.SaveChanges();
            MessageBox.Show("Öğrenci Silindi");
        }

        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txtOgrId.Text);
            var x = db.Ogrenci.Find(id);
            x.Ad = txtOgrAd.Text;
            x.Soyad = txtOgrSoyad.Text;
            x.Fotograf = txtOgrFoto.Text;
            db.SaveChanges();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi");
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {

        }

        private void btnProsedur_Click(object sender, EventArgs e)
        {
            dgwListe.DataSource = db.NotListe2();
        }


        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            string aranan = txtAra.Text;
            var deger = from item in db.Ogrenci
                        where item.Ad.Contains(aranan)
                        select item;
            dgwListe.DataSource = deger.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                List<Ogrenci> liste1 = db.Ogrenci.OrderBy(o => o.Ad).ToList();
                dgwListe.DataSource = liste1;
            }
            if (radioButton2.Checked == true)
            {
                List<Ogrenci> liste2 = db.Ogrenci.OrderByDescending(o => o.Ad).ToList();
                dgwListe.DataSource = liste2;
            }
            if (radioButton3.Checked == true)
            {
                List<Ogrenci> list3 = db.Ogrenci.OrderBy(o => o.Ad).Take(3).ToList();
                dgwListe.DataSource = list3;
            }
            if (radioButton4.Checked == true)
            {
                List<Ogrenci> list4 = db.Ogrenci.Where(o => o.Ad.StartsWith("a")).ToList();
                dgwListe.DataSource = list4;
            }
            if (radioButton5.Checked == true)
            {
                List<Ogrenci> list5 = db.Ogrenci.Where(o => o.Ad.EndsWith("i")).ToList();
                dgwListe.DataSource = list5;
            }
            if (radioButton6.Checked == true)
            {
                int list6 = db.Ogrenci.Count();
                MessageBox.Show( " Toplam öğrenci sayısı : " + list6);
            }
            if (radioButton7.Checked == true)
            {
                var list7 = db.Notlar.Sum(n => n.Sınav1);
                MessageBox.Show(" Toplam Sınav1 Puanı :  " + list7.ToString());
            }
            if (radioButton8.Checked == true)
            {
                var list8 = db.Notlar.Average(n => n.Sınav1);
                MessageBox.Show(" Toplam Sınav1 Puanı :  " + list8.ToString());
            }
            if (radioButton9.Checked == true)
            {
                var list9 = db.Notlar.Max(n => n.Sınav1);
                MessageBox.Show(" Toplam Sınav1 En Yüksek Notu :  " + list9.ToString());
            }
            if (radioButton10.Checked == true)
            {
                var list10 = db.Notlar.Min(n => n.Sınav1);
                MessageBox.Show(" Toplam Sınav1 En Düşük Notu :  " + list10.ToString());
            }
        }

        private void btnJoin_Click(object sender, EventArgs e)
        {
            var sorgu = from d1 in db.Notlar
                        join d2 in db.Ogrenci
                        on d1.Id equals d2.Id
                        select new
                        {
                            Öğrenci = d2.Ad + " " + d2.Soyad,
                            //Soyad=d2.Soyad
                            Sınav1 = d1.Sınav1,
                            Sınav2 = d1.Sınav2,
                            Sınav3 = d1.Sınav3,
                            Ortalama = d1.Ortalama
                        };
            dgwListe.DataSource = sorgu.ToList();

        }
    }
}
