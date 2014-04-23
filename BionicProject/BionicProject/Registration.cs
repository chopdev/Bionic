using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BionicProject
{
    partial class StoreDB
    {
        string InsertUser = "INSERT INTO `Bionic`.`User` (`UserID`, `FirstName`, `LastName`, `BirthDate`, `Email`, `Pass`, `CreatedDate`, `ModifiedDate`, `LastLiginDate`) VALUES (NULL, @FirstName, @LastName, @BirthDate, @Email, @Password, @CreatedDate, @ModifiedDate, @LastLoginDate);";
        public void RegisterUser(string Email, string Password, string FirstName, string LastName, DateTime BirthDate, DateTime CreatedDate)
        {
            
            MySqlCommand cmd = database.CreateCommand();
            cmd.CommandText = InsertUser;
            cmd.Parameters.AddWithValue("@Email", Email);
            cmd.Parameters.AddWithValue("@Password", Password);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            cmd.Parameters.AddWithValue("@BirthDate", BirthDate.ToShortDateString());
            cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate.ToShortDateString());


            cmd.Parameters.AddWithValue("@ModifiedDate", CreatedDate.ToShortDateString());
            cmd.Parameters.AddWithValue("@LastLoginDate", null);
            try
            {
                database.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Не удалось подключиться к БД");
            }

            finally
            {
                database.Close();
                
            }
            
        }
        public List<User> PossibleReceivers(string FirstName, string LastName)
        {
            MySqlCommand cmd = database.CreateCommand();
            cmd.CommandText = "Select * from User where FirstName = @FirstName and LastName = @LastName";
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@LastName", LastName);
            //cmd.CommandText = "Select * from User";
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds,"User");

            List<User> l = new List<User>();
            var o = ds.Tables["User"].Rows;
            foreach(DataRow row in ds.Tables["User"].Rows)
                l.Add(GetUserOnLogin(row.ItemArray[4].ToString(),row.ItemArray[5].ToString()));
            return l;
        }
    }
}
