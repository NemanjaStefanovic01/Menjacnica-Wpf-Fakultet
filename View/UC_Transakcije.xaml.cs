using MenjacnicaProjekat.Models;
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
using MenjacnicaProjekat.SharedData;
using System.Diagnostics;

namespace MenjacnicaProjekat.View
{
    /// <summary>
    /// Interaction logic for UC_Transakcije.xaml
    /// </summary>
    public partial class UC_Transakcije : UserControl
    {
        public List<ValutaKursneListe> valute = new List<ValutaKursneListe>();
        public List<string> oznakePotebnihValuti = new List<string>();
        
        public string tipTransakcije = "";
        public ValutaKursneListe valutaTransakcije;
        public UC_Transakcije()
        {
            InitializeComponent();

            //Valute potrebne za transakcije
            oznakePotebnihValuti.Add("EUR");
            oznakePotebnihValuti.Add("USD");
            oznakePotebnihValuti.Add("CHF");
            oznakePotebnihValuti.Add("GBP");

            valute = DobaviOdabraneValute(oznakePotebnihValuti);
        }

        //Header buttons
        private void Btn_Valuta(object sender, RoutedEventArgs e)
        {
            //Sacuvaj selektovanu vlautu transakcije
            int index = int.Parse(((Button)e.Source).Uid);
            valutaTransakcije = valute[index];

            //Promeni stilizaciju
            Button clickedButton = (Button)sender;

            foreach (Button button in FindVisualChildren<Button>(this))
            {
                if (button.Tag != null && button.Tag.ToString() == "ValutaButton")
                {
                    button.BorderBrush = Brushes.WhiteSmoke;
                    button.Foreground = Brushes.WhiteSmoke;

                    if (button == clickedButton)
                    {
                        Color goldenYellow = (Color)ColorConverter.ConvertFromString("#FFC000");
                        button.BorderBrush = new SolidColorBrush(goldenYellow);
                        button.Foreground = new SolidColorBrush(goldenYellow);
                    }
                }
            }

            Debug.WriteLine("Prva odabrana valuta: " + valute[0].valuta);
            Debug.WriteLine("Druga odabrana valuta: " + valute[1].valuta);
            Debug.WriteLine("Treca odabrana valuta: " + valute[2].valuta);
            Debug.WriteLine("Cetvrta odabrana valuta: " + valute[3].valuta);

            Debug.WriteLine("--------------------");
            Debug.WriteLine("Tip tranakscije: " + tipTransakcije);
            Debug.WriteLine("Valuta transakcije: " + valutaTransakcije.valuta);
            Debug.WriteLine("Kupovni kurs valute tranakscije: " + valutaTransakcije.kupovniKurs);
        }

        private void Btn_Otkup_Click(object sender, RoutedEventArgs e)
        {
            //Postavi selektovanu boju
            Button button = (Button)sender;
            Color goldenYellow = (Color)ColorConverter.ConvertFromString("#FFC000");
            button.BorderBrush = new SolidColorBrush(goldenYellow);
            button.Foreground = new SolidColorBrush(goldenYellow);

            //Zameni boju drugog
            Button prodajaBtn = (Button)FindName("btn_Prodaja");
            prodajaBtn.BorderBrush = Brushes.WhiteSmoke;
            prodajaBtn.Foreground = Brushes.WhiteSmoke;

            tipTransakcije = "otkup";
        }

        private void Btn_Prodaja_Click(object sender, RoutedEventArgs e)
        {
            //Postavi selektovanu boju
            Button button = (Button)sender;
            Color goldenYellow = (Color)ColorConverter.ConvertFromString("#FFC000");
            button.BorderBrush = new SolidColorBrush(goldenYellow);
            button.Foreground = new SolidColorBrush(goldenYellow);

            //Zameni boju drugog
            Button otkupBtn = (Button)FindName("btn_Otkup");
            otkupBtn.BorderBrush = Brushes.WhiteSmoke;
            otkupBtn.Foreground = Brushes.WhiteSmoke;

            tipTransakcije = "prodaja";
        }

        //Other functions
        private List<ValutaKursneListe> DobaviOdabraneValute(List<string> valute)
        {
            List<ValutaKursneListe> odabraneValute = new List<ValutaKursneListe>();

            foreach(string valuta in valute)
            {
                foreach(ValutaKursneListe val in GlobalKursneListe.kursnaListaMenjaca)
                {
                    if (valuta == val.oznaka)
                    {
                        odabraneValute.Add(val);
                    }
                }
            }

            return odabraneValute;
        }

        //
        private IEnumerable<T> FindVisualChildren<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                {
                    var child = VisualTreeHelper.GetChild(dependencyObject, i);

                    if (child is T t)
                    {
                        yield return t;
                    }

                    foreach (var childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
    }
}
