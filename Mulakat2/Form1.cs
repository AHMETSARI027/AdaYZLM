﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mulakat2
{
    public partial class Form1 : Form
    {
        // Bağlatı oluşturma
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        public Form1()
        {
            InitializeComponent();
        }
        //Musteri tablosunu datagirdWiewde görüntüleme
        void musteri()
        {
            baglanti = new SqlConnection("Data Source = AHMETSARI; Initial Catalog = mulakat; Integrated Security = True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM musteri", baglanti);
            DataTable tabloMusteri = new DataTable();
            da.Fill(tabloMusteri);
            dgv_musteri.DataSource = tabloMusteri;
            baglanti.Close();
        }

        //Sepet tablosunu datagirdWiewde görüntüleme
        void sepet()
        {
            baglanti = new SqlConnection("Data Source = AHMETSARI; Initial Catalog = mulakat; Integrated Security = True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM sepet", baglanti);
            DataTable tabloMusteri = new DataTable();
            da.Fill(tabloMusteri);
            dgv_sepet.DataSource = tabloMusteri;
            baglanti.Close();
        }
        //SepetUrun tablosunu datagirdWiewde görüntüleme
        void sepetUrun()
        {
            baglanti = new SqlConnection("Data Source = AHMETSARI; Initial Catalog = mulakat; Integrated Security = True");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM sepeturun", baglanti);
            DataTable tabloMusteri = new DataTable();
            da.Fill(tabloMusteri);
            dgv_sepetUrun.DataSource = tabloMusteri;
            baglanti.Close();
        }
        //Tblolarda Sıfırlama işlemi için
        void IdSifirla()
        {
            baglanti = new SqlConnection("Data Source = AHMETSARI; Initial Catalog = mulakat; Integrated Security = True");
            baglanti.Open();
            komut = new SqlCommand("TRUNCATE TABLE musteri", baglanti);
            komut.ExecuteNonQuery();
            komut = new SqlCommand("TRUNCATE TABLE sepet", baglanti);
            komut.ExecuteNonQuery();
            komut = new SqlCommand("TRUNCATE TABLE sepeturun", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
        }

        void TestVerisiOlustur(int musteriAdet, int Sepetadet)
        {

            //Müşteri Oluşturma
            Random rastgele = new Random();

            for (int i = 0; i < musteriAdet; i++)
            {
                string[] isimler = { "ahmet", "ersan", "veli", "cevdet" };
                int no = rastgele.Next(isimler.Length);
                string ad = isimler[no];
                string[] soyisimler = { "yıldırım", "sarı", "taşkın", "zoroğlu" };              
                int no1 = rastgele.Next(soyisimler.Length);
                string soyad = soyisimler[no1];
                string[] sehirler = { "Ankara", "İstanbul", "İzmir", "Bursa", "Edirne", "Konya", "Antalya", "Diyarbakır", "Van", "Rize" };
                int no2 = rastgele.Next(sehirler.Length);
                string sehir = sehirler[no2];

                string[,] musteri = new string[musteriAdet, 3];

                musteri[i, 0] = ad;
                musteri[i, 1] = soyad;
                musteri[i, 2] = sehir;

                baglanti = new SqlConnection("Data Source = AHMETSARI; Initial Catalog = mulakat; Integrated Security = True");
                string sorgu = "INSERT INTO musteri (Ad,Soyad,Sehir) Values (@Ad,@Soyad,@Sehir)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@Ad", (ad));
                komut.Parameters.AddWithValue("@Soyad", (soyad));
                komut.Parameters.AddWithValue("@Sehir", sehir);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            }

            // Sepet Oluşturma


            for (int i = 0; i < Sepetadet; i++)
            {
                
                int rastgeleMusteriId = rastgele.Next(musteriAdet);
                string sorgu = "INSERT INTO sepet (MusteriId) Values (@MusteriId)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@MusteriId", (rastgeleMusteriId));
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
            }


            //Ürün Oluşturma
            for (int k = 1; k < Sepetadet+1; k++)
            {
               
                int rdsayi = rastgele.Next(1, 5);
                for (int i = 1; i < rdsayi; i++)
                {

                    int rastgeleSepetId = rastgele.Next(Sepetadet);

                    int rastgeleTutar = rastgele.Next(100, 1000);

                    
                    string harfler = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZabcçdefgğhıijklmnoöprsştuüvyz";
                    string aciklamaUret = "";
                    for (int j = 0; j < 6; j++)
                    {
                        aciklamaUret += harfler[rastgele.Next(harfler.Length)];
                    }

                    string sorgu = "INSERT INTO sepeturun (SepetId,Tutar,Aciklama) Values (@SepetId,@Tutar,@Aciklama)";
                    komut = new SqlCommand(sorgu, baglanti);
                    komut.Parameters.AddWithValue("@SepetId", (k));
                    komut.Parameters.AddWithValue("@Tutar", (rastgeleTutar));
                    komut.Parameters.AddWithValue("@Aciklama", (aciklamaUret));
                    baglanti.Open();
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                }
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            IdSifirla();

            TestVerisiOlustur(50, 100);

            musteri();

            sepet();

            sepetUrun();  
            






        }
    }
}
