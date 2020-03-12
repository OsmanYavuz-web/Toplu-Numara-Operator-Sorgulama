using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Management;
using System.Net;

namespace Numara_Sorgulama
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        string kaynakSite = "http://operatorsorgu.com/program";
        string version = null;
        string prog_Version = "V1.1";

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                webBrowser1.Document.GetElementById("kadi").InnerText = textBox1.Text;
                webBrowser1.Document.GetElementById("sifre").InnerText = textBox2.Text;
                webBrowser1.Document.GetElementById("a").InnerText = SystemInformation.ComputerName.ToString();
                webBrowser1.Document.GetElementById("b").InnerText = MAC();

                HtmlElementCollection elc2 = webBrowser1.Document.GetElementsByTagName("button");
                foreach (HtmlElement el2 in elc2)
                {
                    if (el2.GetAttribute("name").Equals("giris"))
                    {
                        el2.InvokeMember("Click");
                    }
                }
            }
            catch 
            { 
            
            }
        }

        private string MAC()
        {
            ManagementClass manager = new ManagementClass("Win32_NetworkAdapterConfiguration");
            foreach (ManagementObject obj in manager.GetInstances())
            {
                if ((bool)obj["IPEnabled"])
                {
                    return obj["MacAddress"].ToString();
                }
            }
            return String.Empty;
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            //webBrowser1.Navigate("http://kadinurunleri.com/program/");
            //webBrowser1.Navigate(kaynakSite);
            this.Hide();
            Form1 frm1 = new Form1();
            frm1.ShowDialog();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {


            string kaynakKod = webBrowser1.Document.Body.InnerHtml.ToString();

            button1.Enabled = true;

            if (kaynakKod.IndexOf("Tüm alanları doldurmalısınız..") != -1) 
            {
                MessageBox.Show("Tüm alanları doldurmalısınız..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (kaynakKod.IndexOf("Kullanıcı adı veya şifre hatalı..") != -1) 
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kaynakKod.IndexOf("Programı sadece bir sistemde kullanabilirsiniz.") != -1)
            {
                MessageBox.Show("Programı sadece bir sistemde kullanabilirsiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (kaynakKod.IndexOf("Kurulum başarıyla tamamlandı! Giriş yapılıyor..") != -1)
            {
                try
                {
                    webBrowser1.Document.GetElementById("kadi").InnerText = textBox1.Text;
                    webBrowser1.Document.GetElementById("sifre").InnerText = textBox2.Text;
                    webBrowser1.Document.GetElementById("a").InnerText = SystemInformation.ComputerName.ToString();
                    webBrowser1.Document.GetElementById("b").InnerText = MAC();

                    HtmlElementCollection elc2 = webBrowser1.Document.GetElementsByTagName("button");
                    foreach (HtmlElement el2 in elc2)
                    {
                        if (el2.GetAttribute("name").Equals("giris"))
                        {
                            el2.InvokeMember("Click");
                        }
                    }
                }
                catch
                {

                }
                MessageBox.Show("Kurulum başarıyla tamamlandı! Giriş yapılıyor..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (kaynakKod.IndexOf("Sisteme giriş yapıldı!") != -1)
            {
                MessageBox.Show("Başarılı bir şekilde giriş yaptınız..", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                webBrowser1.Stop();
                this.Hide();
                Form1 frm1 = new Form1();
                frm1.ShowDialog();
            }

            /*if (version == prog_Version) { }else
            {
               DialogResult soru = MessageBox.Show("Programın yeni sürümü mevcut.\nGüncellemek istiyorsanız 'Evet' butonuna tıklayınız.","Güncelleme",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
               if (soru == DialogResult.Yes)
               {
                   string indirilecek = "http://localhost/uploads/Numara%20Sorgulama.exe";
                   string klasor = Application.StartupPath;
                   string dosyaAdi = "Numara Sorgulama.exe";

                   WebClient webClient = new WebClient();
                   webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
                   webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
                   webClient.DownloadFileAsync(new Uri(indirilecek), klasor + dosyaAdi);

               }
            }*/
        }
       /* private static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
           // Console.WriteLine("Dosya indiriliyor: %{0}", e.ProgressPercentage);
        }
        private static void Completed(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("İndirme işlemi tamamlandı! Programı tekrar başlatmanız gerekiyor..", "Güncelleme", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }*/
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
