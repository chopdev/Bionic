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
        private Receivers PossibleReceivers;
        User selectedUserInPossibleTree; //Kostul.
        public NewMessage()
        {
            ActualReceivers = new Receivers();
            
            InitializeComponent();
            ReceiversList.ItemsSource = ActualReceivers;
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            StoreDB store = new StoreDB();
            PossibleReceivers = new Receivers(SurnameTextBox.Text, NameTextBox.Text);
            PossibleList.ItemsSource = PossibleReceivers; 
        }

        private void AddReceiver_Click(object sender, RoutedEventArgs e)
        {

           
        }

        private void PossibleList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            selectedUserInPossibleTree = (User)e.NewValue;
            if (selectedUserInPossibleTree != null)
            {
                ActualReceivers.Add(selectedUserInPossibleTree);
                PossibleReceivers.Remove(selectedUserInPossibleTree);
            }
        }

        private void ReceiversList_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            selectedUserInPossibleTree = (User)e.NewValue;
            if (selectedUserInPossibleTree != null)
            {
                PossibleReceivers.Add(selectedUserInPossibleTree);
                ActualReceivers.Remove(selectedUserInPossibleTree);
            }
        }


    }
}
