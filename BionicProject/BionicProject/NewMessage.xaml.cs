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
        private Receivers ActualReceivers;
        public NewMessage()
        {
            InitializeComponent();
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            StoreDB store = new StoreDB();

            PossibleList.ItemsSource = new Receivers(SurnameTextBox.Text, NameTextBox.Text);

        }

        private void AddReceiver_Click(object sender, RoutedEventArgs e)
        {
            ActualReceivers.Add((User)ReceiversList.SelectedValue);
            ReceiversList.ItemsSource = ActualReceivers;
            
            //PossibleList.SelectedItem
        }
    }
}
