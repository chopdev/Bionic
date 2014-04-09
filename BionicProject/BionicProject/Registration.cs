using System;
using System.Collections.Generic;
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
    }
}
