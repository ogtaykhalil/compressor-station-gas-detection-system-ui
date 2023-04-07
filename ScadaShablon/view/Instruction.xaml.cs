using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ScadaShablon.view
{
    /// <summary>
    /// Interaction logic for Instruction.xaml
    /// </summary>
    public partial class Instruction : Window
    {
        DispatcherTimer tm = new DispatcherTimer ( );
        string s = "";
        public Instruction ( )
        {
            InitializeComponent ( );
            tm.Tick += Timer_Tick_Exhaust_Start;
            tm.Interval = TimeSpan.FromMilliseconds ( 500 );
        }

        private void Timer_Tick_Exhaust_Start ( object sender, EventArgs e )
        {
            if ( !IsMouseOver )
                this.Close ( );
            tm.Stop ( );
        }





        private void Window_LostFocus ( object sender, MouseEventArgs e )
        {
            tm.Start ( );
        }

        private void Star2t_Click ( object sender, RoutedEventArgs e )
        {
            //PdfReader pdf = new PdfReader("C:\\Users\\asus\\Desktop\\DELTA_IA-PLC_PLC-Link_AN_EN_20160127.pdf");

            //for (int i = 1; i <= pdf.NumberOfPages; i++)
            //{
            //    s = s + PdfTextExtractor.GetTextFromPage(pdf, i);
            //}
            //dd.Text = s;
            try
            {
                System.Diagnostics.Process.Start("E:\\OQTAY\\csharp master cLass\\Csharp_Master_CLass\\Content\\1\\ders1");
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        //private void Operxro_Click(object sender, RoutedEventArgs e)
        //{
        //    dd.Text = "Operator müdaxilələrinin tarixləri."+
        //        "                                                                                                                                                                                                         " +
        //        "Operator tərəfindən idarə etmə ekranı üzərindən icra edilmiş əmrlər, habelə əmirlərin icra edimə tarix və zamanını izləmək üçün ekranın yuxarısında yerləşən" +
        //        " “HADİSƏLƏR” düyməsinə basmaqla açılan pəncərədə olan “Başlanğıc Tarixi” və “Son Tarix” bölmələrində yerləşən təqvimdən itədiyimiz zaman aralığının " +
        //        "bir gün əvvəlini və bir gün sonrasını seçmək lazımdır";
        //}

        //private void Alarmxro_Click(object sender, RoutedEventArgs e)
        //{
        //    dd.Text = "Hadisələrin tarixləri" +
        //                    "                                                                                                                                                                                                                       " +
        //                    "1, Zona 2, Zona 3, Zona 4, Zona 5, Zona 6, Zona 7, Zona 8, Zona 9, Zona 10, Zona 11 və Zona 12 - də baş vermiş istənilən Yanğın, Qaz Sızması və Tüstülənmə" +
        //                    " ilə bağlı həyəcan signalları və baş vermə tarix və zamanı, habelə yuxarıda göstərilən ərazilərdə yerləşən detektorlarda əmələ gələn xətalar və baş vermə tarix və zamanını izləmək mümkündür" +
        //                 "                                                                                                                                                                                                                                                                 " +
        //                 "Bunun  üçün ekranın yuxarısında olan “ALARMLAR” düyməsinə basmaqla açılan pəncərədə yerləşən “Başlanğıc Tarixi” və “Son Tarix”" +
        //                 " bölmələrində yerləşən təqvimdən itədiyimiz zaman aralığının bir gün əvvəlini və bir gün sonrasını seçmək lazımdır.";
        //}

        //private void Nav_Click(object sender, RoutedEventArgs e)
        //{
        //    dd.Text = "Zonalar və Obyektlər üzrə navigasiya."+
        //                     "                                                                                                                                                                                                                      " +
        //                     "İdarə etmə sisteminin nəzarət etdiyi Zonalar üzrə qrafik pəncərələrin açılması və ya zonalarda hər hansı bir həyəcan signalının olub olmamasına nəzarət etmək" +
        //                     " üçün siçanın sol düyməsi ilə ekranın yuxarısında yerləşən “ƏSAS EKRAN” düyməsinə basmaq lazımdır.Açılan pəncərədə hər bir zonaya aid düymələr yerləşdirilmişdir." +
        //                     "                                                                                                                                                                                                                                                                         " +
        //                     " İstənilən düymə basmaqla aidiyyəti zonaya aid qrafik dəyərlərin saxlandığı pəncərəyə keçid etmək olar və ya düymənin üzərində yerləşən vəziyyət göstəricisinin(indigator)" +
        //                     " statusuna görə həmin zonanın hazırkı vəziyyəti izlənə bilər."+
        //                     "                                                                                                                                                                                                                         "+
        //                     "Obyektlər üzrə nəzarətin həyata keçirilməsi üçün ekranın yuxarı hissəsində yerləşən düymələrdən istifadə etmək lazımdır. Hər hansı bir obyektə aid qrafik statusları izləmək " +
        //                     "üçün siçanın sol düyməsi ilə aidiyyəti obyektin adı yazılan düyməni basmaq lazımdır."+
        //                    "                                                                                                                                                                                                                             " +
        //                    "Həmçinin düymələrin üzərində yerləşən vəziyyət göstəricisi(indigator) ilə aid oldugu obyektin hazırkı statusu izlənə bilər.";
        //}
    }
}
