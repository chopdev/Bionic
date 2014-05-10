using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BionicProject
{
    partial class StoreDB
    {
        string InsertMessage = "INSERT INTO `Messages`(`MessageID`, `Text`, `CreatedDate`, `OwnerId`, `RecieverId`) VALUES (NULL,@Text,null,@OwnerId,@ReceiverId)";
        public void SendMessage(string Text, int SenderId, int ReceiverId)
        {
            MySqlConnection database = new MySqlConnection(connection);
            MySqlCommand cmd = database.CreateCommand();
            cmd.CommandText = InsertMessage;
            cmd.Parameters.AddWithValue("@Text", Text);
           // cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@OwnerId", SenderId);
            cmd.Parameters.AddWithValue("@ReceiverId", ReceiverId);
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
        public List<Message> getMessagesOnId(int id)
        {
            MySqlConnection database = new MySqlConnection(connection);
            MySqlCommand cmd = database.CreateCommand();
            cmd.CommandText = "Select * from Messages where recieverId = @ReceiverId";
            cmd.Parameters.AddWithValue("@ReceiverId", id);


            
            List<Message> result = new List<Message>();

            try
            {

                database.Open();
               
                MySqlDataReader data = cmd.ExecuteReader();
                while (data.Read())
                {
                    string d = data["CreatedDate"].ToString();
                    if (d == "") d = DateTime.Now.ToString();
                    result.Add(new Message(Int32.Parse(data["MessageID"].ToString()), data["Text"].ToString(), DateTime.Parse(d),
                           Int32.Parse(data["OwnerId"].ToString()), Int32.Parse(data["OwnerId"].ToString())));
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);

            }
            finally
            {

                database.Close();
            }
            return result;
        }

    }
}
