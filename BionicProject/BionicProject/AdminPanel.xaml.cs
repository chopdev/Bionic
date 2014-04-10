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
    }

}
