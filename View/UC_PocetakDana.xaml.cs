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

            Debug.WriteLine(kursnaListaNBS.GetValutaAtIndex(0).prodajniKurs);
        }
    }
}
