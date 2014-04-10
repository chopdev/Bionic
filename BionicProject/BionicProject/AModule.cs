using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


///////////////////////////////////
using MySql.Data.MySqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
///////////////////////////////////

namespace BionicProject
{
    partial class StoreDB
    {
        string s_GetUsersOnCourse = "SELECT * FROM `UserCourses` JOIN `User` on UserCourses.UserID=User.UserID WHERE CourseID=@ID";  //Выборка всех юзеров на определенном курсе
        public List<AdminSelection> GetUsersOnCourse(Course cr)
        {
            List<AdminSelection> selection = new List<AdminSelection>();
            MySqlCommand cmd = Connection.CreateCommand();

            cmd.CommandText = s_GetUsersOnCourse;
            cmd.Parameters.AddWithValue("@ID", cr.CourseId);

            try
            {
                Connection.Open();

                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    selection.Add(new AdminSelection(Int32.Parse(data["UserID"].ToString()),Int32.Parse(data["CourseStatus"].ToString()),
                        data["FirstName"].ToString(),data["LastName"].ToString(),data["Email"].ToString()));
                }

            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");

            }
            finally
            {

               Connection.Close();
            }
            return selection;
        }
    }

    public class AdminSelection:INotifyPropertyChanged
    {
        public string FIO { get { return Lname + " " + Fname; } }
        int userid;
        public int UserID { get { return userid; }  }
        CourseStatus status;
        public CourseStatus Status { get { return status; } set { SetField(ref status, value, "Status"); } }
        string Fname;
        public string FirstName { get { return Fname; } }
        string Lname;
        public string LastName { get { return Lname; } }
        string email;
        public string Email { get { return email; } }
        public AdminSelection(int userid, int status, string Fname, string Lname, string email)
        {
          this.userid = userid;
          this.status = (CourseStatus)status;
          this.Fname = Fname;
          this.Lname = Lname;
          this.email = email;
        }

        #region
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        #endregion


    }

    public class AdminSelections : ObservableCollection<AdminSelection>
    {
        StoreDB store = new StoreDB();

        public List<AdminSelection> AllusersInCourse = new List<AdminSelection>();

        public AdminSelections(Course cr)
        {
            foreach (var m in store.GetUsersOnCourse(cr))
            { Add(m); AllusersInCourse.Add(m); }
        }
    }

}
