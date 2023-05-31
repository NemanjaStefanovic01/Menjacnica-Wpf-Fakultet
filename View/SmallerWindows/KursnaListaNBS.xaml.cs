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
using System.Windows.Shapes;
using MenjacnicaProjekat.Models;
using MenjacnicaProjekat.SharedData;
using System.Collections.Generic;
using System.Diagnostics;

namespace MenjacnicaProjekat.View.SmallerWindows
{
    /// <summary>
    /// Interaction logic for KursnaListaNBS.xaml
    /// </summary>
    public partial class KursnaListaNBS : Window
    {
        KursnaLista kursnaListaNBS = new KursnaLista();
        public KursnaListaNBS()
        {
            InitializeComponent();

            //Popuni tabelu
            kursnaListaNBS = GlobalKursneListe.kursnaListaNBS;
            DataGrid_Kurs.ItemsSource = kursnaListaNBS;
        }

        //Win control functions
        private void BtnClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void BtnMinimize(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void DragMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();  
        }
    }
}
