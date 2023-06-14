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
using System.Windows.Media.Converters;

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
        public ValutaKursneListe valutaTransakcije = new ValutaKursneListe();

        public float iznosValuteOtkupa = 0;
        public float iznosValuteProdaje = 0;

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
            Debug.WriteLine("Prodajni kurs valute tranakscije: " + valutaTransakcije.prodajniKurs);

            EnableTextBoxes();
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

            EnableTextBoxes();
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

            EnableTextBoxes();
        }

        private void EnableTextBoxes()
        {
            if (tipTransakcije == "")
                return;
            if (valutaTransakcije.valuta == null)
                return;

            //TextBoxovi
            FrameworkElement parentContainer = this;

            UIElement ivo = parentContainer.FindName("Input_IznosValuteOtkupa") as UIElement;
            TextBox tb_IznosValuteOtkupa = (TextBox)ivo;
            tb_IznosValuteOtkupa.IsEnabled = true;

            UIElement ivp = parentContainer.FindName("Input_IznosValuteProdaje") as UIElement;
            TextBox tb_IznosValuteProdaje = (TextBox)ivp;
            tb_IznosValuteProdaje.IsEnabled = true;
        }

        //Transakcija
        private void RacunanjeTransakcije(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            bool unosValidan = ProveriUnosIznosa(sender, textBox.Text);

            if (!unosValidan)
                return;

            switch (textBox.Name)
            {
                case "Input_IznosValuteOtkupa":
                    iznosValuteOtkupa = float.Parse(textBox.Text);
                    Debug.WriteLine("Iznos valute otkupa: " + iznosValuteOtkupa);
                    IzracunajVrednosti("otkupa");
                    break;
                case "Input_IznosValuteProdaje":
                    iznosValuteProdaje = float.Parse(textBox.Text);
                    Debug.WriteLine("Iznos valute prodaje: " + iznosValuteProdaje);
                    IzracunajVrednosti("prodaje");
                    break;
                default: break;
            }
        }
        private bool ProveriUnosIznosa(object sender, string iznos)
        {
            float iznosFloat;
            bool canConvert = float.TryParse(iznos, out iznosFloat);

            if(!canConvert)
            {
                TextBox tb = (TextBox)sender;
                tb.BorderBrush = Brushes.Red;
            }
            else
            {
                TextBox tb = (TextBox)sender;
                tb.BorderBrush = Brushes.Teal;
            }

            return canConvert;
        }
        private void IzracunajVrednosti(string pozvanoIz)
        {
            Debug.WriteLine("IzracunajVrednosti, prodaja: " + iznosValuteProdaje.ToString());
            if (tipTransakcije == "otkup")
            {
                Debug.WriteLine("1");
                if (pozvanoIz == "otkupa")
                {
                    Debug.WriteLine("1.1");
                    iznosValuteProdaje = iznosValuteOtkupa * valutaTransakcije.kupovniKurs;
                }
                if(pozvanoIz == "prodaje")
                {
                    Debug.WriteLine("1.2");
                    iznosValuteOtkupa = iznosValuteProdaje / valutaTransakcije.kupovniKurs;
                }
            }
            if (tipTransakcije == "prodaja")
            {
                Debug.WriteLine("2");
                if (pozvanoIz == "otkupa")
                {
                    Debug.WriteLine("2.1");
                    iznosValuteProdaje = iznosValuteOtkupa / valutaTransakcije.prodajniKurs;
                }
                if (pozvanoIz == "prodaje")
                {
                    Debug.WriteLine("2.2");
                    iznosValuteOtkupa = iznosValuteProdaje * valutaTransakcije.prodajniKurs;
                }
            }

            //Postavi vrednosti u textboxove
            FrameworkElement parentContainer = this;

            UIElement ivo = parentContainer.FindName("Input_IznosValuteOtkupa") as UIElement;
            TextBox tb_IznosValuteOtkupa = (TextBox)ivo;
            tb_IznosValuteOtkupa.Text = iznosValuteOtkupa.ToString();

            UIElement ivp = parentContainer.FindName("Input_IznosValuteProdaje") as UIElement;
            TextBox tb_IznosValuteProdaje = (TextBox)ivp;
            tb_IznosValuteProdaje.Text = iznosValuteProdaje.ToString();

            Debug.WriteLine("Izmene zavrsene, otkup: " + iznosValuteOtkupa.ToString());
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

        //Misc
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
