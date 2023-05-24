using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
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

using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;

using Google.Cloud.Firestore;

using MenjacnicaProjekat.Models;

namespace MenjacnicaProjekat.View
{
    public partial class LogInWin : Window
    {
        //Database connection and user models
        public UserService userService = new UserService();
        public FirestoreDb db;

        public List<UserModel> users = new List<UserModel>();
        public async void SetUserList()
        {
            try
            {
                users = await userService.GetUsersList(db);
            }
            finally
            {
                userSelectLabel_firstUser.Text = users[0].UserName;
                userSelectLabel_secondUser.Text = users[1].UserName;
                userSelectLabel_thirdUser.Text = users[2].UserName;
                userSelectLabel_fourthUser.Text = users[3].UserName;
            }
        }

        public UserModel selectedUser;

        List<Button> buttonList = new List<Button>();
        string[] uidList = { "btn0", "btn1", "btn2", "btn3" };

        public LogInWin()
        {
            InitializeComponent();

            //Povezi se na bazu
            db = userService.GetConnection();

            //Popunjavanje liste korisnika i Update UI po listi korisnika
            SetUserList();

            //Dodavanje dugmica u listu dugmica
            foreach (string uid in uidList)
            {
                Button button = FindName(uid) as Button;
                if (button != null)
                {
                    buttonList.Add(button);
                }
            }

            //Labela korisnik
            login_userLabel.Text = "Odaberite Korisnika";
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

        //Login functionality
        private void Btn_Login(object sender, RoutedEventArgs e)
        {
            if(selectedUser == null)
            {
                MessageBox.Show("Morate odabrati korisnika");
                return;
            }

            string password = Input_password.Password;
            
            if (!ValidatePassword(password))
            {
                Input_password.Password = string.Empty;
                return;
            }
            
            if(password != selectedUser.Password)
            {
                MessageBox.Show("Uneli ste pogresnu lozinku");
                Input_password.Password = string.Empty;
                return;
            }

            //Uloguj korisnika
            MainWindow win = new MainWindow();
            win.Show();
            this.Close();

            //MessageBox.Show("Usepesno ste se ulogovali");
        }
        private bool ValidatePassword(string password)
        {
            if (password.Length <= 6)
            {
                MessageBox.Show("Lozinka mora biti duza od 6 karaktera!");
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                MessageBox.Show("Lozinka mora imati velika slova!");
                return false;
            }
            if (!password.Any(char.IsDigit))
            {
                MessageBox.Show("Lozinka mora imati brojeve!");
                return false;
            }

            return true;
        }

        //Mis
        
        private void Btn_UserSelected(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string uid = button.Name;

            selectedUser = users[int.Parse(uid.Substring(3))];
            string userId = "";
            for(int i = 0; i < users.Count; i++)
            {
                if(selectedUser == users[i])
                {
                    userId = i.ToString();
                }
            }

            login_userLabel.Text = selectedUser.UserName;

            //Style
            Color goldenYellow = (Color)ColorConverter.ConvertFromString("#FFC000");

            //Dodaj zutu boju na pritisnuto dugme
            Border border = FindParentBorderOfButton(button);

            border.BorderBrush = new SolidColorBrush(goldenYellow);
            border.BorderThickness = new Thickness(2);
            
            //Skini boju sa proslog selektovanog dugmeta
            foreach (Button btn in buttonList)
            {
                if (btn.Name.Substring(3) != userId)
                {
                    Border border1 = FindParentBorderOfButton(btn);

                    if (border1.BorderThickness.Top > 0)
                    {
                        border1.BorderBrush = new SolidColorBrush(Colors.Transparent);
                        border1.BorderThickness = new Thickness(0);
                    }
                }
            }

            //Focus to password box
            Input_password.Focus();
        }
        
        public Border FindParentBorderOfButton(Button button)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(button);
            while (!(parent is Border) && parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            Border border = parent as Border;

            return border;
        }
        
        private void Btn_ZaboravljenaLozinka(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Na Vaš broj telefona će vam stici privremena lozinka!");
        } 

        //Keydown Functions
        private void KeyDown_PasswordBox(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;

                if(sender == Input_password)
                {
                    Btn_Login(sender, e);
                }
            }
        }

    }
}
//registracija usera

//DateTime now = DateTime.Now;
//string date = now.ToString("dd/MM/yyyy");
//string time = now.ToString("HH:mm");
//string dateCreated = date + " " + time;

//UserModel user = new UserModel();

//user.Ime = "Milovan";
//user.Prezime = "Bozic";
//user.UserName = "MilovanB";
//user.Password = "milovan123";
//user.Funkcija = "User";
//user.BrTelefona = "061/3394503";
//user.DateCreated = dateCreated;
//user.Status = "Active";

//Debug.WriteLine("user" + user.UserName);
//userService.AddUserWithCustomId(db, user);
