using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telefon.Entities;

namespace Telefon.Core
{
    public class DatabaseLogicLayer
    {
        List<RehberKayit> kayitlarim;


        public DatabaseLogicLayer()
        {
            VeriTabaniControl();
            kayitlarim = new List<RehberKayit>();
        }

        private void VeriTabaniControl()
        {
            //c içinde TelefonRehberiDB
            bool KlasorKontrol =  Directory.Exists(@"C:\TelefonRehberiDB");
            if (!KlasorKontrol) //Yoksa
            {//Oluştur
                Directory.CreateDirectory(@"C:\TelefonRehberiDB");
                //Kullanici örnekle admin/admin olştur
                Kullanici _kullanici = new Kullanici()
                {
                    ID = Guid.NewGuid(),
                    KullaniciAdi = "admin",
                    Sifre = "admin"
                };
                //Json olarak 
               string _jsonKullanici =  Newtonsoft.Json.JsonConvert.SerializeObject(_kullanici);
                //kaydet
                File.WriteAllText(@"C:\TelefonRehberiDB\kullanici.json", _jsonKullanici);

            }
        }

        public int YeniKayit(RehberKayit K)
        {
            int _sonuc = 0;
            try
            {
                RehberKayitlariGetir(); //varsa getirir yoksa bellekte degere eklenmek üzere bekler

                kayitlarim.Add(K); //parametyerede geleni kayitlara yazar
                JsonDBGuncelle(); //varsa override eder yoksa yeni json oluşturur.
            }
            catch (Exception ex)
            {
                _sonuc = 0;
            }
            return _sonuc;
        }


        public List<RehberKayit> RehberKayitlariGetir()
        {
            //oku db
            if (File.Exists(@"C:\TelefonRehberiDB\Rehber.json"))
            {//al dbiyi
               string _jsonFromDbText =  File.ReadAllText(@"C:\TelefonRehberiDB\Rehber.json");
                //Deserializ et kayitlarim listesine
                kayitlarim = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RehberKayit>>(_jsonFromDbText);
            }

            //geri bas
            return kayitlarim;
        }


        public int KullaniciKontrol(Kullanici kullanici)
        {
            int KullaniciSonuc = 0;
            //eğer kullanıcı databasede varsa
            if (File.Exists(@"C:\TelefonRehberiDB\kullanici.json"))
            {
                //kullanıcıları oku
                string JsonKullanici = File.ReadAllText(@"C:\TelefonRehberiDB\kullanici.json");
                //okudugun kullanıcıları deserialize et
                List<Kullanici> _kullaniciListesi = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Kullanici>>(JsonKullanici);
                //bu deserialize içinde parametreden gelen ile eşleştirip countunu al KullanıcıSonuca ver
               KullaniciSonuc = _kullaniciListesi.FindAll(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.ID == kullanici.ID).ToList().Count();
            }
            //geriye dön
            return KullaniciSonuc;
        }


        private void JsonDBGuncelle()
        {
            //eger kayitlar boş değilse ya da 0 dan buyukse
            if (kayitlarim != null && kayitlarim.Count > 0)
            {
                //serialize et
                string JsonDB = Newtonsoft.Json.JsonConvert.SerializeObject(kayitlarim);
                //ettiğin JsonDB yi yaz
                File.WriteAllText(@"C:\TelefonRehberiDB\Rehber.json", JsonDB);
            }
        }

    }
}
