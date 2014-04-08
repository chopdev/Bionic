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

///////////////////////////////////
using MySql.Data.MySqlClient;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
///////////////////////////////////

namespace BionicProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connection = "Server=mysql.cyberhost.net.ua;Database=bionic_lab;Uid=Bionic;Pwd=BionicGroup;";
        public MainWindow()
        {
            InitializeComponent();
            ConnectionExample();
        }


        public void ConnectionExample()
        {
            MySqlConnection database = new MySqlConnection(connection);
            MySqlCommand cmd = database.CreateCommand();
            string str = "";
            cmd.CommandText = @"Select * from Bionic.User";
            try
            {
                database.Open();
                MySqlDataReader data = cmd.ExecuteReader();
               
                while (data.Read())
                {

                    str += data["Email"].ToString() + "\n";
                   
                }

            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");

            }
            finally
            {

                database.Close();
            }
            MessageBox.Show(str);
        }

    }
}
