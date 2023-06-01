using MenjacnicaProjekat.Models;
using MenjacnicaProjekat.SharedData;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

using Google.Cloud.Firestore;

namespace MenjacnicaProjekat.View.SmallerWindows
{
    /// <summary>
    /// Interaction logic for KursnaListaMenjaca.xaml
    /// </summary>
    public partial class KursnaListaMenjaca : Window
    {
        KursnaLista kursnaLista = new KursnaLista();
        ObservableCollection<ValutaKursneListe> observableCollection = null;

        //Database connection and user models
        public KursnaListaService kursnaListaService = new KursnaListaService();
        public FirestoreDb db;

        public List<UserModel> users = new List<UserModel>();

        //public async void SetUserList()
        //{
        //    try
        //    {
        //        users = await userService.GetUsersList(db);
        //    }
        //    finally
        //    {
        //        userSelectLabel_firstUser.Text = users[0].UserName;
        //        userSelectLabel_secondUser.Text = users[1].UserName;
        //        userSelectLabel_thirdUser.Text = users[2].UserName;
        //        userSelectLabel_fourthUser.Text = users[3].UserName;
        //    }
        //}

        public KursnaListaMenjaca()
        {
            InitializeComponent();

            //Dodati ucitavanje kursne liste sa baze
            db = kursnaListaService.GetConnection();

            //Ovo ukoliko na bazi nema nova kursna lista
            kursnaLista = GlobalKursneListe.kursnaListaNBS;
            GlobalKursneListe.kursnaListaMenjaca = kursnaLista;

            //Ovo ide u svakom slucaju kada se na prvi ili drugi nacin napuni kurnsa lista
            //Kreira se observable collection kako bi se bindovala sa tabelom
            if(kursnaLista != null)
            {
                observableCollection = new ObservableCollection<ValutaKursneListe>(kursnaLista);
                DataGrid_Kurs.ItemsSource = observableCollection;
            }
        }
        //Save Cancle
        private void Btn_SaveChanges(object sender, RoutedEventArgs e)
        {
            if(observableCollection == null)
            {
                return;
            }

            //Convert observable to kursna lista
            foreach (var item in observableCollection)
            {
                kursnaLista.DodajValutu(item);
            }
            GlobalKursneListe.kursnaListaMenjaca = kursnaLista;

            
            //Save to database
            kursnaListaService.AddValutaWithCustomId(db, kursnaLista);

            Close();
        }
        private void Btn_Cancle(object sender, RoutedEventArgs e)
        {
            Close();
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
