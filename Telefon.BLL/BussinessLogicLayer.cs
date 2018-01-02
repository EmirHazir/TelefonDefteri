using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telefon.Core;
using Telefon.Entities;

namespace Telefon.BLL
{
    public class BussinessLogicLayer
    {
        DatabaseLogicLayer DLL;
        public BussinessLogicLayer()
        {
            DLL = new DatabaseLogicLayer();
        }

        public int  KullaniciKontrol(string KullaniciAdi, string Sifre)
        {
            int Sonuc = 0;
            if (!string.IsNullOrEmpty(KullaniciAdi)&& !string.IsNullOrEmpty(Sifre))
            {
                Kullanici kullanici = new Kullanici
                {
                    KullaniciAdi = KullaniciAdi,
                    Sifre = Sifre
                };

                Sonuc = DLL.KullaniciKontrol(kullanici);
            }
            else
            {
                Sonuc = -100;
            }
            return Sonuc;
        }



        public int YeniKayit(Guid ID, string Isim, string Soyisim, string Telefon1, string Telefon2, string Telefon3, string Email, string Adres, string Aciklama, string Website)
        {
            int Sonuc = 0;
            if (ID != Guid.Empty && !string.IsNullOrEmpty(Isim) && !string.IsNullOrEmpty(Soyisim) && !string.IsNullOrEmpty(Telefon1))
            {
                RehberKayit kayit = new RehberKayit
                {
                    ID = ID,
                    Isim = Isim,
                    Soyisim = Soyisim,
                    Telefon1 = Telefon1,
                    Telefon2 = Telefon2,
                    Telefon3 = Telefon3,
                    Adres = Adres,
                    Email = Email,
                    Aciklama = Aciklama,
                    Website = Website
                };

                DLL.YeniKayit(kayit);
            }
            else
            {
                Sonuc =  -100; // sadece böyle bir parametre verdim espirisi yok
            }
            return Sonuc;
        }

        
    }
}
