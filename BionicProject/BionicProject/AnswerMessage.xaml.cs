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
    /// Логика взаимодействия для AnswerMessage.xaml
    /// </summary>
    public partial class AnswerMessage : Window
    {
        private Message message;
        public AnswerMessage(Message message)
        {
            InitializeComponent();
            this.message = message;
            NameLabel.Content = message.Owner;
            MessageBlock.Text = message.Text;
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            StoreDB store = new StoreDB();
            store.SendMessage(MessageBox.Text, MainWindow.user.UserID, message.Owner.UserID);
            this.Close();
        }
    }
}
