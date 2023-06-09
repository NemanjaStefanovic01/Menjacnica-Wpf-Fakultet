using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

namespace MenjacnicaProjekat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string StanjeDana = "";
        public MainWindow()
        {
            InitializeComponent();
            this.mainContentControl.Content = new View.UC_PocetakDana();
        }

        //Win control functions
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        //User Controle
        private void TabButton_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            if(index != 0)
            {
                MoveCursorMenu(index);
            }
            else
            {
                CursorForProfile();
            }

            //User Cotnroles
            switch (index)
            {
                case 0:
                    GridUserContol.Children.Clear();
                    GridUserContol.Children.Add(new View.UC_Izrada());
                    break;
                case 1:
                    GridUserContol.Children.Clear();
                    GridUserContol.Children.Add(new View.UC_PocetakDana());
                    break;
                case 2:
                    //GridUserContol.Children.Clear();
                    //GridUserContol.Children.Add(new View.UC_PocetakDana());
                    break;
                case 3:
                    GridUserContol.Children.Clear();
                    GridUserContol.Children.Add(new View.UC_Izrada());
                    break;
                case 4:
                    GridUserContol.Children.Clear();
                    GridUserContol.Children.Add(new View.UC_ZavrsetakDana());
                    break;
                default:
                    break;
            }
        }
        //Functions for user controls to call
        public void ZapocniDan()
        {
            Debug.WriteLine("Dan zapocet");
            StanjeDana = "Zapocet";

            //Disable pocetak dana
            Button btn_PocetakDana = FindButtonByUid(this, "1");
            btn_PocetakDana.IsEnabled = false;

            //Switch to Transakcije
            GridUserContol.Children.Clear();
            GridUserContol.Children.Add(new View.UC_Izrada());
        }
        public Button FindButtonByUid(DependencyObject parent, string uid)
        {
            if (parent == null)
                return null;

            if (parent is Button button && button.Uid == uid)
                return button;

            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                Button foundButton = FindButtonByUid(child, uid);
                if (foundButton != null)
                    return foundButton;
            }

            return null;
        }

        //Menu functionality
        private void MenuItem_OpenNBS(object sender, RoutedEventArgs e) 
        { 
            Window existingWindow = Application.Current.MainWindow;
            Window kursnaListaNBS = new View.SmallerWindows.KursnaListaNBS();
            kursnaListaNBS.Owner = existingWindow;
            kursnaListaNBS.Show();
        }
        private void MenuItem_OpenKL(object sender, RoutedEventArgs e)
        {
            Window existingWindow = Application.Current.MainWindow;
            Window kursnaLista = new View.SmallerWindows.KursnaListaMenjaca();
            kursnaLista.Owner = existingWindow;
            kursnaLista.Show();
        }


        //Misc
        private void MoveCursorMenu(int index)
        {
            Color goldenYellow = (Color)ColorConverter.ConvertFromString("#FFC000");
            GridCursor.Background = new SolidColorBrush(goldenYellow);

            GridCursor.Margin = new Thickness(195, 170 + (227 * (index-1)), 0, 0);
        }
        private void CursorForProfile()
        {
            GridCursor.Background = Brushes.Transparent;
        }
    }
}
