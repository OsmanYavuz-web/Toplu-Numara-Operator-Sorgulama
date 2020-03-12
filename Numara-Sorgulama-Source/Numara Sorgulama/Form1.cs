using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using mshtml;
using System.IO;
using System.Net;
using HtmlAgilityPack;

namespace Numara_Sorgulama
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string kaynakKod = null;
        string kaynakKod2 = null;

        string programYol = Application.StartupPath + @"\";

        #region FORM_LOAD
        private void Form1_Load(object sender, EventArgs e)
        {
            //Site Bağlantısı
            webBrowser2.Navigate(textBox4.Text);

            //Form Boyutu
            this.Size = new Size(1047, 637);
        }
        #endregion

        #region Manuel Sorgu
        #region Sorgu gönderme
        private void button1_Click(object sender, EventArgs e)
        {
            label6.Text = "Bekleniyor..";
            label6.ForeColor = Color.Black;

            try
            {
                #region Operatör sorgulama
                string numara = textBox1.Text;
                numara = numara.Remove(0, 1);
                webBrowser2.Document.GetElementById("txtMsisdn").InnerText = numara;
                HtmlElementCollection elc2 = this.webBrowser2.Document.GetElementsByTagName("input");
                foreach (HtmlElement el2 in elc2)
                {
                    if (el2.GetAttribute("value").Equals("Sorgula"))
                    {
                        el2.InvokeMember("Click");
                    }
                }
                #endregion
            }
            catch { }
        }
        #endregion

        #region Yeni Sorgu
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                webBrowser2.Navigate(textBox4.Text);
                textBox1.Text = "0";
                label6.Text = "Bekleniyor..";
                label6.ForeColor = Color.Black;
            }
            catch { }
        }
        #endregion

        #region Kontrol İşlemleri
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
              #region Operatör sorgulama
                if (kaynakKod2.IndexOf("hizmet aldığı işletmeci") != -1)
                {
                    label6.ForeColor = Color.DarkBlue;
                    string gelen = kaynakKod2;
                    int titleIndexBaslangici = gelen.IndexOf(textBox6.Text) + textBox6.TextLength;
                    int titleIndexBitisi = gelen.Substring(titleIndexBaslangici).IndexOf("</DIV>");
                    string cikti = gelen.Substring(titleIndexBaslangici, titleIndexBitisi).Remove(0, 11).Replace("numarası", "Numara");
                    label6.Text = cikti.Replace("hizmet aldığı işletmeci: ","");
                }
                else
                {
                    label6.Text = "Bekleniyor..";
                    label6.ForeColor = Color.Black;
                }

                if (kaynakKod2.IndexOf("numarası bir işletmeciye ait değildir.") != -1)
                {
                    label6.Text = "Bu numara herhangi bir işletmeciye ait değildir";
                }
              #endregion
            }
            catch { }
        }
        #endregion

        #region Operatör sorgulama site kaynak kod
        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                kaynakKod2 = webBrowser2.Document.Body.InnerHtml.ToString(); // site kaynak kod
                textBox5.Text = kaynakKod2;
            }
            catch { }
        }
        #endregion
        #endregion

        #region Programı Kapat
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void programıKapatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Otomatik Sorgu

        #region Numara Listesi Aktarma
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                OpenFileDialog open = new OpenFileDialog();
                open.FileName = "";
                open.Filter = "Txt Dosyası|*.txt";
                DialogResult dosya = open.ShowDialog();
                if (dosya == DialogResult.OK)
                {
                    textBox13.Text = open.FileName;
                    StreamReader oku;
                    oku = File.OpenText(open.FileName);
                    string yaz;
                    while ((yaz = oku.ReadLine()) != null)
                    {
                        yaz = yaz.Replace("+90", "");
                        yaz = yaz.Replace("90", "");
                        string varmı = yaz.Substring(0, 1);
                        if (varmı != "0")
                        {
                            yaz = 0 + yaz;
                        }
                        yaz = yaz.Remove(0, 1);
                        listBox1.Items.Add(yaz.ToString());
                    }
                    oku.Close();

                    lbl_Durum.ForeColor = Color.DarkGreen;
                    lbl_Durum.Text = listBox1.Items.Count.ToString() + " adet numara yüklendi..";
                    label3.Text = "Toplam: [" + listBox1.Items.Count.ToString() + "]";
                    MessageBox.Show("İşlem başarılı, " + listBox1.Items.Count.ToString() + " adet numara yüklendi..", "İşlem", MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else
                {
                    lbl_Durum.ForeColor = Color.Blue;
                    lbl_Durum.Text = "Numara yüklemeyi iptal ettiniz.";
                }

            }
            catch { }
        }
        #endregion

        #region Başlat
        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count > 1)
            {
                textBox3.Clear();
                textBox7.Clear();
                textBox8.Clear();
                textBox9.Clear();
                listView1.Items.Clear();
                listView2.Items.Clear();
                listView3.Items.Clear();
                listView4.Items.Clear();
                label19.Text = "Toplam: [0]";
                label20.Text = "Toplam: [0]";
                label21.Text = "Toplam: [0]";
                label23.Text = "Toplam: [0]";
                kalan = 0;
                taranan = 0;

                lbl_Durum.ForeColor = Color.DarkGreen;
                lbl_Durum.Text = "Otomatik numara sorgulama başladı...";

                button3.Enabled = false;
                button4.Enabled = false;
                listBox1.Enabled = false;
                listBox1.SelectedIndex = -1;
                button5.Enabled = true;
                timer2.Enabled = true;
                gecenSure = 0;

            }
            else
            {
                MessageBox.Show("Taramayı başlatabilmek için en az 2 numara gerek! Tek numaras orgularını Manuel Sorgudan yapabilirsiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }     
        #endregion

        #region Durdur
        private void button5_Click(object sender, EventArgs e)
        {
            lbl_Durum.ForeColor = Color.DarkRed;
            lbl_Durum.Text = "Otomatik numara sorgulama durduruldu...";

            button5.Enabled = false;
            timer2.Enabled = false;
            limit = 0;
            webBrowser1.Stop();
            try
            {
                Proxy.VarsayılanProxy();
            }
            catch { }
            button3.Enabled = true;
            button4.Enabled = true;
            listBox1.Enabled = true;
            listBox1.SelectedIndex = -1;

        }
        #endregion

        #region Otomatik Kayıt
        void kaydet2()
        {
            string yol = Application.StartupPath + @"\";
            string klasorAdi = "Operatör Sorgulama Sonuçları";


            if (Directory.Exists(yol + klasorAdi) == true)
            {
                //Sonuç Klasörleri
                DateTime dt = DateTime.Now;
                string tarih = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt) + "] Sonuçları";

                //Dosyaları Kaydet
                //sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");

                if (Directory.Exists(yol + klasorAdi + @"\" + tarih) == true)
                {
                    sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");
                }
                else
                {
                    //Sonuç Klasörleri
                    DateTime dt2 = DateTime.Now;
                    string tarih2 = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt2) + "] Sonuçları";
                    Directory.CreateDirectory(yol + klasorAdi + @"\" + tarih2);

                    //Dosyaları Kaydet
                    sonucOlustur(yol + klasorAdi + @"\" + tarih2 + @"\");
                }
            }
            else
            {
                //Klasör Oluştur
                Directory.CreateDirectory(yol + klasorAdi);

                //Sonuç Klasörleri
                DateTime dt = DateTime.Now;
                string tarih = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt) + "] Sonuçları";
                Directory.CreateDirectory(yol + klasorAdi + @"\" + tarih);

                //Dosyaları Kaydet
                sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");
            }
        }
        #endregion

        #region İşlemler
        int limit = 0;
        int taranan = 0;
        int gecenSure = 0;
        int kalan = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (listBox1.Items.Count - 1 == listBox1.SelectedIndex)
                {
                    lbl_Durum.ForeColor = Color.DarkGreen;
                    lbl_Durum.Text = "Otomatik numara sorgulama tamamlandı. Taranan numara [" + listBox1.Items.Count.ToString() + "]";

                    button5.Enabled = false;
                    timer2.Enabled = false;
                    limit = 0;
                    webBrowser1.Stop();
                    Proxy.VarsayılanProxy();
                    button3.Enabled = true;
                    button4.Enabled = true;
                    listBox1.Enabled = true;
                    listBox1.SelectedIndex = -1;
                    kaydet2();
                    MessageBox.Show("Otomatik numara sorgulama tamamlandı. Taranan numara [" + listBox1.Items.Count.ToString() + "]", "İşlem Sonucu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    limit = limit + 1;
                    label14.Text = limit.ToString();


                    gecenSure = gecenSure + 1;
                    //label16.Text = "Geçen süre: " + gecenSure.ToString() + " sn";

                    double saniye;

                    saniye = Convert.ToDouble(gecenSure);
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                    dateTime = dateTime.AddSeconds(saniye);
                    string saatidakikasaniye = dateTime.ToLongTimeString();
                    label16.Text = "Geçen Süre: " + saatidakikasaniye;
 
                   
                    if (limit == int.Parse(textBox10.Text))
                    {
                       //Rastgele Proxy 
                       /*Random rnd = new Random();
                       listBox_Proxy.SelectedIndex = rnd.Next(0, listBox_Proxy.Items.Count);
                       listBox_Proxy2.SelectedIndex = listBox_Proxy.SelectedIndex;
                       Proxy.ProxyAyarla(listBox_Proxy.Text+":"+listBox_Proxy2.Text);*/
                        
                        webBrowser1.Navigate(textBox4.Text);

                        listBox1.SelectedIndex = listBox1.SelectedIndex + 1;

                        textBox14.Text = listBox1.SelectedIndex.ToString();

                        lbl_Durum.ForeColor = Color.DarkGreen;
                        lbl_Durum.Text = listBox1.Text + " numarası sorgulanıyor..";

                        textBox3.Text = listBox1.Text;


                        if (textBox3.Text.Length == 10)
                        {
                            //webBrowser1.Navigate(textBox4.Text);
                        }
                        else
                        {
                            string[] veri4 = { textBox3.Text, "Geçersiz" };
                            ListViewItem numara4 = new ListViewItem(veri4);
                            listView4.Items.Add(numara4);
                            label23.Text = "Toplam: [" + listView4.Items.Count.ToString() + "]";

                            lbl_Durum.ForeColor = Color.DarkRed;
                            lbl_Durum.Text = listBox1.Text + " geçersiz numara diğer numaraya atlandı.";
                            limit = 0;
                        }

                        taranan = taranan + 1;
                        textBox8.Text = taranan.ToString();
                        kalan = listBox1.Items.Count - taranan;
                        textBox9.Text = kalan.ToString();
                    }

                    if (limit == 1)
                    {
                        proxyList();
                    }

                    if (limit == int.Parse(textBox12.Text))
                    {
                        try
                        {
                            listBox1.SelectedIndex = listBox1.SelectedIndex - 1;
                            limit = 0;
                            taranan = taranan - 1;
                            textBox8.Text = taranan.ToString();
                            kalan = listBox1.Items.Count - taranan;
                            textBox9.Text = kalan.ToString();
                        }
                        catch { }
                    }
                }
            }
            catch { }
        }
        #endregion

        #region Operator Yakalama
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                kaynakKod = webBrowser1.Document.Body.InnerHtml.ToString();

                if (webBrowser1.StatusText == "Bitti" || webBrowser1.StatusText == "Done")
                {
                    if (kaynakKod.IndexOf("Güvenlik Resmi") != -1)
                    {
                        lbl_Durum.ForeColor = Color.DarkRed;
                        lbl_Durum.Text = listBox1.Text + " numarası güvenlik koduna takıldı..Proxy değiştiriliyor..";
                        limit = 0;
                    }
                    else
                    {
                        #region Operatör sorgulama
                        
                        #region Sorgula Butonu
                        if (kaynakKod.IndexOf("Telefon Numarası") != -1)
                        {
                            webBrowser1.Document.GetElementById("txtMsisdn").InnerText = textBox3.Text;
                            HtmlElementCollection elc2 = this.webBrowser1.Document.GetElementsByTagName("input");
                            foreach (HtmlElement el2 in elc2)
                            {
                                if (el2.GetAttribute("value").Equals("Sorgula"))
                                {
                                    el2.InvokeMember("Click");
                                }
                            }
                        }
                        #endregion

                        if (kaynakKod.IndexOf("reminderContainer") != -1)
                        {
                            try
                            {
                                #region Sonuç Çıktısı
                                if (kaynakKod.IndexOf("hizmet aldığı işletmeci") != -1)
                                {
                                    string gelen = kaynakKod;
                                    int titleIndexBaslangici = gelen.IndexOf(textBox6.Text) + textBox6.TextLength;
                                    int titleIndexBitisi = gelen.Substring(titleIndexBaslangici).IndexOf("</DIV>");
                                    string cikti = gelen.Substring(titleIndexBaslangici, titleIndexBitisi).Remove(0, 11).Replace("numarası", "Numara");
                                    textBox7.Text = cikti.Replace("hizmet aldığı işletmeci: ", "");
                                    lbl_Durum.ForeColor = Color.DarkGreen;
                                    lbl_Durum.Text = listBox1.Text + " - " + textBox7.Text;
                                }
                                else if (kaynakKod.IndexOf("numarası bir işletmeciye ait değildir.") != -1)
                                {
                                    lbl_Durum.ForeColor = Color.DarkGreen;
                                    lbl_Durum.Text = listBox1.Text + " numaranın operatörü bilinmiyor..";
                                    textBox7.Text = "Bilinmiyor";
                                }
                                else
                                {
                                    lbl_Durum.ForeColor = Color.DarkGreen;
                                    lbl_Durum.Text = listBox1.Text + " numaranın operatörü bilinmiyor..";
                                    textBox7.Text = "Bilinmiyor";
                                }
                                #endregion

                                #region Çıktı İşleminin Yansıtılması
                                if (textBox7.Text.IndexOf("TURKCELL") != -1 || textBox7.Text.IndexOf("Turkcell") != -1)
                                {
                                        string[] veri = { textBox3.Text, textBox7.Text };
                                        ListViewItem numara = new ListViewItem(veri);
                                        listView1.Items.Add(numara);
                                        label19.Text = "Toplam: [" + listView1.Items.Count.ToString() + "]";
                                    
                                }
                                else if (textBox7.Text.IndexOf("AVEA") != -1 || textBox7.Text.IndexOf("Avea") != -1)
                                {
                                        string[] veri2 = { textBox3.Text, textBox7.Text };
                                        ListViewItem numara2 = new ListViewItem(veri2);
                                        listView2.Items.Add(numara2);
                                        label20.Text = "Toplam: [" + listView2.Items.Count.ToString() + "]";
                                    
                                }
                                else if (textBox7.Text.IndexOf("VODAFONE") != -1 || textBox7.Text.IndexOf("Vodafone") != -1)
                                {
                                    
                                        string[] veri3 = { textBox3.Text, textBox7.Text };
                                        ListViewItem numara3 = new ListViewItem(veri3);
                                        listView3.Items.Add(numara3);
                                        label21.Text = "Toplam: [" + listView3.Items.Count.ToString() + "]";
                                    
                                }
                                else if (textBox7.Text.IndexOf("Bilinmiyor") != -1)
                                {
                                        string[] veri4 = { textBox3.Text, "Geçersiz" };
                                        ListViewItem numara4 = new ListViewItem(veri4);
                                        listView4.Items.Add(numara4);
                                        label23.Text = "Toplam: [" + listView4.Items.Count.ToString() + "]";
                                    
                                }
                                #endregion

                                limit = 0;

                            }
                            catch { }
                        }
                        #endregion
                    }
                }
                else
                {

                    lbl_Durum.ForeColor = Color.DarkRed;
                    lbl_Durum.Text = "Kaynak siteye erişilemiyor.. İnternet bağlantınızı kontrol edin ve taramayı yeniden başlatın..";
                    button5.Enabled = false;
                    timer2.Enabled = false;
                    limit = 0;
                    webBrowser1.Stop();

                    button3.Enabled = true;
                    button4.Enabled = true;
                    listBox1.Enabled = true;
                    listBox1.SelectedIndex = -1;
                    MessageBox.Show("Kaynak siteye erişilemiyor.. İnternet bağlantınızı kontrol edin ve taramayı yeniden başlatın..", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { }
        }
        #endregion

        #region Kaydet Fonksiyonu
        void kaydet()
        {
            DialogResult folder = folderBrowserDialog1.ShowDialog();

            if (folder == DialogResult.OK) 
            {
                string yol = folderBrowserDialog1.SelectedPath + @"\";
                string klasorAdi = "Operatör Sorgulama Sonuçları";


                if (Directory.Exists(yol + klasorAdi) == true)
                {
                    //Sonuç Klasörleri
                    DateTime dt = DateTime.Now;
                    string tarih = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt) + "] Sonuçları";

                    //Dosyaları Kaydet
                    //sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");

                    if (Directory.Exists(yol + klasorAdi + @"\" + tarih) == true) 
                    {
                        sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");
                    }
                    else
                    {
                        //Sonuç Klasörleri
                        DateTime dt2 = DateTime.Now;
                        string tarih2 = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt2) + "] Sonuçları";
                        Directory.CreateDirectory(yol + klasorAdi + @"\" + tarih2);

                        //Dosyaları Kaydet
                        sonucOlustur(yol + klasorAdi + @"\" + tarih2 + @"\");
                    }
                }
                else
                {
                    //Klasör Oluştur
                    Directory.CreateDirectory(yol + klasorAdi);

                    //Sonuç Klasörleri
                    DateTime dt = DateTime.Now;
                    string tarih = "[" + String.Format("{0:dd-mm-yyyy HH.mm}", dt) + "] Sonuçları";
                    Directory.CreateDirectory(yol + klasorAdi + @"\" + tarih);

                    //Dosyaları Kaydet
                    sonucOlustur(yol + klasorAdi + @"\" + tarih + @"\");
                }
            }
            else
            {
                MessageBox.Show("Sonuçları kaydetmeyi iptal ettiniz!","İşlem",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
        void sonucOlustur(string kayitYol)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(kayitYol + "Turkcell.txt"))
                {
                    #region Turkcell
                    try
                    {
                        if (listView1.Items.Count > 0) // listview boş değil ise 
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (ColumnHeader baslik in listView1.Columns) // columns
                            {
                                sb.Append(string.Format("{0}\t", baslik.Text));
                            }
                            sw.WriteLine(sb.ToString());
                            foreach (ListViewItem lvi in listView1.Items)
                            {
                                sb = new StringBuilder();
                                foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                                {
                                    sb.Append(string.Format("{0}\t", listViewSubItem.Text));
                                }
                                sw.WriteLine(sb.ToString());
                            }
                            sw.WriteLine();

                            lbl_Durum.ForeColor = Color.DarkGreen;
                            lbl_Durum.Text = "Turkcell numaraların sonuçları başarıyla kaydedildi.";

                        }
                    }
                    catch
                    {
                        lbl_Durum.ForeColor = Color.DarkRed;
                        lbl_Durum.Text = "Turkcell numaraların sonuçları kaydedilemedi.";
                    }
                    #endregion
                }

                using (StreamWriter sw2 = new StreamWriter(kayitYol + "Avea.txt"))
                {
                    #region Avea
                    try
                    {
                        if (listView2.Items.Count > 0) // listview boş değil ise 
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (ColumnHeader baslik in listView2.Columns) // columns
                            {
                                sb.Append(string.Format("{0}\t", baslik.Text));
                            }
                            sw2.WriteLine(sb.ToString());
                            foreach (ListViewItem lvi in listView2.Items)
                            {
                                sb = new StringBuilder();
                                foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                                {
                                    sb.Append(string.Format("{0}\t", listViewSubItem.Text));
                                }
                                sw2.WriteLine(sb.ToString());
                            }
                            sw2.WriteLine();

                            lbl_Durum.ForeColor = Color.DarkGreen;
                            lbl_Durum.Text = "Avea numaraların sonuçları başarıyla kaydedildi.";
                        }
                    }
                    catch
                    {
                        lbl_Durum.ForeColor = Color.DarkRed;
                        lbl_Durum.Text = "Avea numaraların sonuçları kaydedilemedi.";
                    }
                    #endregion
                }

                using (StreamWriter sw3 = new StreamWriter(kayitYol + "Vodafone.txt"))
                {
                    #region Vodafone
                    try
                    {
                        if (listView3.Items.Count > 0) // listview boş değil ise 
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (ColumnHeader baslik in listView3.Columns) // columns
                            {
                                sb.Append(string.Format("{0}\t", baslik.Text));
                            }
                            sw3.WriteLine(sb.ToString());
                            foreach (ListViewItem lvi in listView3.Items)
                            {
                                sb = new StringBuilder();
                                foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                                {
                                    sb.Append(string.Format("{0}\t", listViewSubItem.Text));
                                }
                                sw3.WriteLine(sb.ToString());
                            }
                            sw3.WriteLine();

                            lbl_Durum.ForeColor = Color.DarkGreen;
                            lbl_Durum.Text = "Vodafone numaraların sonuçları başarıyla kaydedildi.";
                        }
                    }
                    catch
                    {
                        lbl_Durum.ForeColor = Color.DarkRed;
                        lbl_Durum.Text = "Vodafone numaraların sonuçları kaydedilemedi.";
                    }
                    #endregion
                }

                using (StreamWriter sw4 = new StreamWriter(kayitYol + "Geçersiz.txt"))
                {
                    #region Geçersiz Numaralar
                    try
                    {
                        if (listView4.Items.Count > 0) // listview boş değil ise 
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (ColumnHeader baslik in listView4.Columns) // columns
                            {
                                sb.Append(string.Format("{0}\t", baslik.Text));
                            }
                            sw4.WriteLine(sb.ToString());
                            foreach (ListViewItem lvi in listView4.Items)
                            {
                                sb = new StringBuilder();
                                foreach (ListViewItem.ListViewSubItem listViewSubItem in lvi.SubItems)
                                {
                                    sb.Append(string.Format("{0}\t", listViewSubItem.Text));
                                }
                                sw4.WriteLine(sb.ToString());
                            }
                            sw4.WriteLine();

                            lbl_Durum.ForeColor = Color.DarkGreen;
                            lbl_Durum.Text = "Geçersiz numaraların sonuçları başarıyla kaydedildi.";
                        }
                    }
                    catch
                    {
                        lbl_Durum.ForeColor = Color.DarkRed;
                        lbl_Durum.Text = "Geçersiz numaraların sonuçları kaydedilemedi.";
                    }
                    #endregion
                }

                MessageBox.Show("Sonuçlar başarıyla kaydedildi.\nKayıt yolu: " + kayitYol, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch 
            {
                MessageBox.Show("Kaydetmek istediğiniz sonuçları, daha önce kaydettiniz.\nYol: " + kayitYol, "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Sonuçları Kaydet
        private void sonuçlarıKaydetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            kaydet();
        }
        #endregion

        #region Proxy List Çek
        private void proxyList()
        {
            try
            {
                listBox_Proxy.Items.Clear();
                listBox_Proxy2.Items.Clear();

                Uri url = new Uri("https://www.socks-proxy.net/");
                WebClient client = new WebClient() { Encoding = Encoding.UTF8 };
                string html = client.DownloadString(url);
                HtmlAgilityPack.HtmlDocument dokuman = new HtmlAgilityPack.HtmlDocument();
                dokuman.LoadHtml(html);

                HtmlNodeCollection XPath2 = dokuman.DocumentNode.SelectNodes("//table[@id='proxylisttable']/tbody/tr/td[1]");
                foreach (var veri2 in XPath2)
                {
                    listBox_Proxy.Items.Add(veri2.InnerText);
                }


                HtmlNodeCollection XPath3 = dokuman.DocumentNode.SelectNodes("//table[@id='proxylisttable']/tbody/tr/td[2]");
                foreach (var veri3 in XPath3)
                {
                    listBox_Proxy2.Items.Add(veri3.InnerText);
                }

                textBox11.Text = listBox_Proxy.Items.Count.ToString();
            }
            catch { }
        }
        #endregion


        #endregion


    }
}
