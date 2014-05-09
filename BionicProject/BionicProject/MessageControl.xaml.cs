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
namespace BionicProject
{
    /// <summary>
    /// Логика взаимодействия для MessageControl.xaml
    /// </summary>
    public partial class MessageControl : UserControl
    {
        public MessageControl()
        {
            InitializeComponent();
            StoreDB store = new StoreDB();
            var messages = store.getMessagesOnId(MainWindow.user.UserID);
            foreach (var m in messages)
                Messages.Items.Add(m);
            //Messages.Items.Add("3434");
        }

        private void createNewMessage_Click(object sender, RoutedEventArgs e)
        {
            NewMessage nm = new NewMessage();
            nm.ShowDialog();
            
            
        }
    }
}
