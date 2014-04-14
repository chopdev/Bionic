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
        User user;
        StoreDB store = new StoreDB();
        SignIn SignInDialog;
        public MainWindow()
        {
            InitializeComponent();

            SignInDialog = new SignIn();
            SignInDialog.ShowDialog();
            user = SignIn.user;
            CoursesTree.ItemsSource = user.MyCourses;

            MessageControl control = new MessageControl();
            Grid.SetColumn(control, 1);
            Grid.SetRow(control, 1);
            Programulina.Children.Add(control);

        }


    }
}
