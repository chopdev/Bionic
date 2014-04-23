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
    /// Логика взаимодействия для NewMessage.xaml
    /// </summary>
    public partial class NewMessage : Window
    {
        public NewMessage()
        {
            InitializeComponent();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            StoreDB store = new StoreDB();
            store.RegisterUser("343", "34", "23", "2323", DateTime.Now, DateTime.Now);
           // PossibleList.ItemsSource = store.PossibleReceivers("1", "1");
        }
    }
}
