using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telefon.BLL;

namespace TelefonWFUI
{
    public partial class Form1 : Form
    {

        BussinessLogicLayer BLL;

        public Form1()
        {
            InitializeComponent();
            BLL = new BussinessLogicLayer();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
           int Sonuc = BLL.KullaniciKontrol(txtKullaniciAdi.Text, txtSifre.Text );
            if (Sonuc > 0)
            {
                AnaForm form = new AnaForm();
                form.Show();
            }
            else if(Sonuc == -100)
            {
                MessageBox.Show("Form Alanlarını Eksiksiz Doldurunuz");
            }
            else
            {
                MessageBox.Show("Hatalı kullanıcı");
            }
        }
    }
}
