using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


///////////////////////////////////
using MySql.Data.MySqlClient;
///////////////////////////////////


namespace BionicProject
{
    partial class StoreDB
    {
        string s_GetUsersOnCourse = "SELECT * FROM `UserCourses` JOIN `User` on UserCourses.UserID=User.UserID WHERE CourseID=@ID";  //Выборка всех юзеров на определенном курсе

        public List<User> GetUsersOnCourse(Course cr)
        {
            User user = null;
            MySqlCommand cmd = database.CreateCommand();

            cmd.CommandText = s_GetUsersOnCourse;
            cmd.Parameters.AddWithValue("@ID", cr.CourseId);

            try
            {
                database.Open();

                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    
                }

            }
            catch
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");

            }
            finally
            {

                database.Close();
            }
            return user;
        }

    }


    public class AdminSelection
    {
        Course course;

        int userid;
        CourseStatus status;
        string Fname;
        string Lname;
        string email;

        public AdminSelection(Course SelectedCourse)
        {
            course = SelectedCourse;
        }
    }


}
