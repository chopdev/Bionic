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
       
        string s_GetUsersOnCourse = "SELECT * FROM `UserCourses` JOIN `User` on UserCourses.UserID=User.UserID WHERE CourseID=@ID";  
        //Выборка всех юзеров на определенном курсе
        string s_UpUserStatus = "UPDATE `UserCourses` SET `CourseStatus` = @St  WHERE `UserCourses`.`CourseID` =  @c_id AND `UserCourses`.`UserID` = @u_id;";
        //Поменять роль телу на курсе
        
        //Удалить тело с курса
        string s_deleteOnCourse = "DELETE FROM `UserCourses` WHERE `UserID`=@U_id and `CourseID`=@C_id";


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

        public bool UpdateStatusOnCourse(Course cr, int ID, CourseStatus CS)
        {
            MySqlCommand cmd = Connection.CreateCommand();

            cmd.CommandText = s_UpUserStatus;
            cmd.Parameters.AddWithValue("@St", CS);
            cmd.Parameters.AddWithValue("@c_id", cr.CourseId);
            cmd.Parameters.AddWithValue("@u_id", ID);
            bool result = true;
            try
            {
                Connection.Open();

                cmd.ExecuteNonQuery();
                
          
            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");
                result = false;
            }
            finally
            {

                Connection.Close();
            }
            return result;
        }

        public bool DeleteUserOnCurse(Course cr, int ID)
        {
            MySqlCommand cmd = Connection.CreateCommand();

            cmd.CommandText = s_deleteOnCourse;
            cmd.Parameters.AddWithValue("@C_id", cr.CourseId);
            cmd.Parameters.AddWithValue("@U_id", ID);
            bool result = true;
            try
            {
                Connection.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");
                result = false;
            }
            finally
            {

                Connection.Close();
            }
            return result;
        }
    }

    public class AdminSelection:INotifyPropertyChanged
    {
        public string FIO { get { return Lname + " " + Fname; } }
        int userid;
        public int UserID { get { return userid; } set{SetField(ref userid, value, "UserID");}  }
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

        public AdminSelection Clone()
        {
            return new AdminSelection(this.userid, (int)this.status, this.Fname, this.Lname, this.email);
        }

    }

    public class AdminSelections : ObservableCollection<AdminSelection>
    {
        StoreDB store = new StoreDB();

        public AdminSelections AllusersInCourse;

        public AdminSelections(Course cr)
        {

            AllusersInCourse = new AdminSelections();

            foreach (var m in store.GetUsersOnCourse(cr))
            { Add(m); AllusersInCourse.Add(m.Clone()); }
        }


        AdminSelections()
        {

        }

        public AdminSelection[] CloneCollection()
        {
            AdminSelection[] n_select = new AdminSelection[this.Count];
            int i = 0;
            foreach (var m in this)
            {
                n_select[i++] = m.Clone(); 
            }
            return n_select;
        }
    }

}
