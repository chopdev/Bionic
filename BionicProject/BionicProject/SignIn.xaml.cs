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

namespace BionicProject
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Window
    {
        public static User user;
        public SignIn()
        {
            InitializeComponent();
        }

        private void SignInButton_Click(object sender, RoutedEventArgs e)
        {
            StoreDB storeDB = new StoreDB();
            user = storeDB.GetUserOnLogin(EmailTextBox.Text, PasswordTextBox.Text);
            if (user == null)
            {
                System.Windows.MessageBox.Show("Incorrect email and password!");
            }
            else this.Close();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.ShowDialog();
        }






    }
}
