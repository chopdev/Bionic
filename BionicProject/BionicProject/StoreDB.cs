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
        string connection = "Server=mysql.cyberhost.net.ua;Database=Bionic;Uid=Bionic;Pwd=BionicGroup;";
        string SelectAllcourses = "Select * from `Course`";
        string SelectCourses = "SELECT * FROM `UserCourses` usrc JOIN `Course` c ON usrc.CourseID = c.CourseID where usrc.UserID =@ID";
        string GetUser = "SELECT * FROM `User` WHERE Email = @Email and Pass = @pass";
        

        MySqlConnection database;

        
        public StoreDB()
        {
            database = new MySqlConnection(connection);

        }

        public List<Course> PossibleCourses(User user)
        {
            List<Course> Allcourses = new List<Course>();
            MySqlCommand cmd = database.CreateCommand();
            if (user.IsAdmin) cmd.CommandText = SelectAllcourses;
            else { cmd.CommandText = SelectCourses; cmd.Parameters.AddWithValue("@ID", user.UserID); }

            try
            {
                database.Open();


                MySqlDataReader data = cmd.ExecuteReader();
                int c_id;
                int t_id;
                int status;

                while (data.Read())
                {

                    if (!user.IsAdmin)
                    {
                        if (!Int32.TryParse(data["courseid"].ToString(), out c_id) ||
                            !Int32.TryParse(data["teacherid"].ToString(), out t_id) ||
                            !Int32.TryParse(data["coursestatus"].ToString(), out status) || status > 2) continue;

                        Allcourses.Add(new Course(c_id, data["name"].ToString(), t_id, status));
                    }
                    else
                    {
                        if (!Int32.TryParse(data["courseid"].ToString(), out c_id) ||
                           !Int32.TryParse(data["teacherid"].ToString(), out t_id)) continue;
                            Allcourses.Add(new Course(c_id, data["name"].ToString(), t_id));
                    }
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


            return Allcourses;
        } //Выборка возможных курсов для юзера

        public User GetUserOnLogin(string Email, string Password)
        {
            User user=null;
            MySqlCommand cmd = database.CreateCommand();

            cmd.CommandText = GetUser;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Pass", Password);

            try
            {
                database.Open();


                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    user = new User(Int32.Parse(data["UserID"].ToString()), data["FirstName"].ToString(), data["LastName"].ToString(),
                           DateTime.Now, Email);
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
        }//Выборка Юзера по логину 
         
        public void ConnectionExample()
        {
            MySqlConnection database = new MySqlConnection(connection);
            MySqlCommand cmd = database.CreateCommand();
            string str = "";
            cmd.CommandText = @"Select * from Bionic.User";
            try
            {
                database.Open();
                MySqlDataReader data = cmd.ExecuteReader();

                while (data.Read())
                {

                    str += data["Birthdate"].ToString() + "\n";

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
        }

    }
}
