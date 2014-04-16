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
using System.Windows.Controls.Primitives;

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
            Refresh(SelectedCourse);
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


            IEnumerable<AdminSelection> from_array = selection.AllusersInCourse.CloneCollection();
           
          //  if (selection.Count == 0 && selection.AllusersInCourse.Count != 0) from_array = selection.AllusersInCourse;
           // else from_array = selection.CloneCollection();

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

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            IEnumerable<AdminSelection> from_array;
            if (Lname_Field.Text.Length == 0 && Fname_Field.Text.Length == 0)
            {
                from_array = selection.AllusersInCourse;
            }
            else
            {
                TextBox_TextChanged_1(null, null);
                from_array = selection.CloneCollection();
            }

            ComboBox cmd = sender as ComboBox;
            CourseStatus status = (CourseStatus)cmd.SelectedItem;
            int a = 5;

            var n  = from record in from_array
                         where record.Status == status
                         select record;

            selection.Clear();
            foreach (var m in n) selection.Add(m);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //Save Changes
        {
            int Id = (int)(((Button)sender).Tag);

            var m = from man in selection
                    where man.UserID == Id
                    select man;

            AdminSelection n_man=null;
            foreach (var n in m)
            {
                n_man = n;
                break;
            }

            if (n_man != null)
            {
                
                bool sign = new StoreDB().UpdateStatusOnCourse(course, n_man.UserID, n_man.Status);
                ShowResult(sign);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            int Id = (int)(((Button)sender).Tag);

            var m = from man in selection
                    where man.UserID == Id
                    select man;

            AdminSelection n_man = null;
            foreach (var n in m)
            {
                n_man = n;
                break;
            }

            if (n_man != null)
            {
                bool sign = new StoreDB().DeleteUserOnCurse(course, n_man.UserID);
               if(sign) Refresh(course);
               ShowResult(sign);
            }

        }

        void Refresh(Course SelectedCourse)
        {
            course = SelectedCourse;
            selection = new AdminSelections(course);
            MainPanel.ItemsSource = selection;
            Lname_Field.Text = "";
            Fname_Field.Text = "";
            StatusPanel.SelectedIndex = -1;
        }


        void ShowResult(bool result)
        {
            Popup window = new Popup();
            window.AllowsTransparency = true;
            TextBlock txt = new TextBlock();
            txt.Foreground = result ? Brushes.Blue : Brushes.Red;
            txt.Text = result ? "Зміни успішно внесені" : "Виникла помилка. Спробуйте ще";
            txt.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            txt.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            txt.FontFamily = new System.Windows.Media.FontFamily("Calibri");
            txt.FontSize = 18;
            txt.FontWeight = FontWeights.Bold;
            txt.Background = Brushes.Transparent;
            Border brd = new Border();
            brd.BorderThickness = new Thickness(3);
            brd.CornerRadius = new CornerRadius(15);
            brd.BorderBrush = Brushes.LightGray;
            brd.Background = Brushes.Transparent;
            brd.Child = txt;
            brd.Background = Brushes.White;

            txt.TextWrapping = TextWrapping.Wrap;
            txt.Width = 100;
            txt.TextAlignment = TextAlignment.Center;
            window.Child = brd;

            window.Opened += (object sender, EventArgs e) =>
            {
                Popup wnd = sender as Popup;

                System.Windows.Media.Animation.DoubleAnimation anim = new System.Windows.Media.Animation.DoubleAnimation();
                anim.From = 1;
                anim.To = 0.0;
                anim.Duration = new Duration(new TimeSpan(0, 0, 0, 1,200));
                anim.Completed += (object sender1, EventArgs e1) => { window.IsOpen = false; };
                wnd.Child.BeginAnimation(OpacityProperty, anim);

            };
            window.IsOpen = true;
        }

    }

}
