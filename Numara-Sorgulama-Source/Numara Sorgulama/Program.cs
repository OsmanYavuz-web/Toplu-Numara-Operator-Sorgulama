using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//Eklenenler
using System.Threading; 

namespace Numara_Sorgulama
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /*[STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form3());
        }*/

        [STAThread]
        static void Main()
        {
            bool kontrol;

            Mutex mutex = new Mutex(true, "Program", out kontrol); //Örnek Mutex nesnesi oluşturalım. 
            if (kontrol == false)
            {
                MessageBox.Show("Numara Operatör Sorgulama programı zaten çalışıyor..","Hata",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            Application.EnableVisualStyles();
            Application.Run(new Form3());
            GC.KeepAlive(mutex); //Nesneyi kaldırıyoruz. 
        }
    }
}
