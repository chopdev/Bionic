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
    /// Логика взаимодействия для Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string Email = EmailTextBox.Text;
            if (!Email.Contains("@") || PasswordTextBox.Text.Length == 0)
            {
                System.Windows.MessageBox.Show("Email is not valide");
                EmailTextBox.Focus();
                return;
            }
            string Password = PasswordTextBox.Text;
            string FirstName = FirstNameTextBox.Text;
            string LastName = LastNameTextBox.Text;
            DateTime birthDate;
            if (BirthDate.SelectedDate.HasValue) birthDate = BirthDate.SelectedDate.Value;
            else birthDate = DateTime.Now;
            StoreDB store = new StoreDB();
            store.RegisterUser(Email, Password, FirstName, LastName, birthDate, DateTime.Now);
            this.Close();

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
