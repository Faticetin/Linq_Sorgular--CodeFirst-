using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityOrnek
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DbOgrenciSinavEntities1 db = new DbOgrenciSinavEntities1();
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                var dger = db.Notlar.Where(n => n.Sınav1 < 50);
                dataGridView1.DataSource = dger.ToList();
            }
            if (radioButton2.Checked == true)
            {
                var dger2 = db.Ogrenci.Select(o => new { Ad = o.Ad });
                dataGridView1.DataSource = dger2.ToList();
            }
            if (radioButton3.Checked == true)
            {
                var dger3 = db.Ogrenci.Select(o => new { Soyad = o.Soyad });
                dataGridView1.DataSource = dger3.ToList();
            }
            if (radioButton4.Checked == true)
            {
                var dger4 = db.Ogrenci.Select(o => new { Ad = o.Ad.ToUpper(), soyad = o.Soyad.ToLower() });
                dataGridView1.DataSource = dger4.ToList();
            }
            if (radioButton5.Checked == true)
            {
                var dger5 = db.Notlar.Select(n => new { Ad = n.OgrenciId, Ortalaması = n.Ortalama, Durum = n.Durum == true ? "Geçti" : "Kaldı" });
                dataGridView1.DataSource = dger5.ToList();
            }
            if (radioButton6.Checked == true)
            {
                var dger6 = db.Notlar.SelectMany(n => db.Ogrenci.Where(o => o.Id == n.OgrenciId), (n, o) => new
                {
                    o.Ad,
                    o.Soyad,
                    n.Ortalama
                });
                dataGridView1.DataSource = dger6.ToList();
            }
            if (radioButton7.Checked == true)
            {
                var dger7 = db.Ogrenci.OrderBy(o => o.Id).Skip(3);
                dataGridView1.DataSource = dger7.ToList();
            }
            if (radioButton8.Checked == true)
            {
                var dger8 = db.Notlar.OrderBy(n => n.Durum).GroupBy(x => x.Durum).Select(z => new { Durum = z.Key == true ? "Geçen Kişi Sayısı" : "Kalan Kişi Syaısı", Toplam = z.Count() });
                dataGridView1.DataSource = dger8.ToList();
            }
            label1.Text = (from x in db.Notlar
                           orderby x.Sınav1 ascending
                           select x.Durum).First();
        }
    }
}
