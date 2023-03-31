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

namespace MenjacnicaProjekat.View
{
    /// <summary>
    /// Interaction logic for LogInWin.xaml
    /// </summary>
    public partial class LogInWin : Window
    {
        public List<String> users = new List<String>();
        public string selectedUser;

        List<Button> buttonList = new List<Button>();
        string[] uidList = { "btn0", "btn1", "btn2", "btn3" };

        public LogInWin()
        {
            InitializeComponent();

            //Popunjavanje liste korisnika
            users.Add("Nemanja");
            users.Add("Milovan");
            users.Add("Jovana");
            users.Add("Radovan");

            //Update UI po listi korisnika
            userSelectLabel_firstUser.Text = users[0];
            userSelectLabel_secondUser.Text = users[1];
            userSelectLabel_thirdUser.Text = users[2];
            userSelectLabel_fourthUser.Text = users[3];

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

            login_userLabel.Text = selectedUser;

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
            //Napravi custom prozor koji se otvori umesto ogavnog message boxa
            MessageBox.Show("Na vas broj telefona će vam stici privremena lozinka!");
        }
    }
}
