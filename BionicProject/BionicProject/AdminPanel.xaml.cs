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
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : UserControl
    {
        Course course;

        AdminSelections selection;

        public AdminPanel(Course SelectedCourse)
        {
            InitializeComponent();
            course = SelectedCourse;
            selection = new AdminSelections(course);
            MainPanel.ItemsSource = selection;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (Lname_Field.Text.Length==0&&Fname_Field.Text.Length==0)
            {
                selection.Clear();
                foreach (var n in selection.AllusersInCourse) selection.Add(n);
                return;
            }


            IEnumerable<AdminSelection> from_array;
           

            if (selection.Count == 0 && selection.AllusersInCourse.Count != 0) from_array = selection.AllusersInCourse;
            else from_array = selection.AllusersInCourse;
            IEnumerable<AdminSelection> to = from_array;

            if (Lname_Field.Text.Length > 0)
            {
                to = from record in from_array
                     where record.LastName.StartsWith(Lname_Field.Text, true, new System.Globalization.CultureInfo("uk-UA"))
                     select record;
                from_array = to;
            }

            if (Fname_Field.Text.Length > 0)
            {
                to = from record in from_array
                     where record.FirstName.StartsWith(Fname_Field.Text, true, new System.Globalization.CultureInfo("uk-UA"))
                     select record;
            }

            selection.Clear();
            foreach (var g in to) selection.Add(g);
            
        }
      
    }

}
