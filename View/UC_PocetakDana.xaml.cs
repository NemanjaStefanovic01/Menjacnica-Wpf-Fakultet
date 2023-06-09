using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using MenjacnicaProjekat.Models;
using MenjacnicaProjekat.SharedData;


namespace MenjacnicaProjekat.View
{
    public partial class UC_PocetakDana : UserControl
    {
        public ScreapeService scraper = new ScreapeService();
        public KursnaLista kursnaListaNBS = new KursnaLista();
        public UC_PocetakDana()
        {
            InitializeComponent();
        }

        private void Btn_Preuzmi(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Preuzmi");
            kursnaListaNBS = scraper.ScrapeFromNBS();
            
            //Save kurnsa lista as global
            GlobalKursneListe.kursnaListaNBS = kursnaListaNBS;
        }

        private void Button_KreirajKursnuListu(object sender, RoutedEventArgs e)
        {
            Window existingWindow = Application.Current.MainWindow;
            Window kursnaLista = new View.SmallerWindows.KursnaListaMenjaca();
            kursnaLista.Owner = existingWindow;
            kursnaLista.Show();
        }

        private void Button_ZapocniDan(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            if (parentWindow is MainWindow yourWindow && kursnaListaNBS.kursnaLista.Count != 0)
            {
                yourWindow.ZapocniDan();
            }
        }
    }
}
