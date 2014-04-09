using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace BionicProject
{
    public enum QuestionType
    {
        Radiobutton, 
        Checkbox,
        Text
    }

    public class TeacherModule
    {
        public void AddQuestion(string questionText, QuestionType questionType, int difficulty)
        {
             using (MySqlConnection connection = )
            {
                connection.Open();
                string query = string.Format(actionQuery, _tableName);
                StringBuilder queryBuilder = new StringBuilder(query);
                using (MySqlCommand command = (MySqlCommand)GetCommand())
                {
                    _pack(result, this);
                    int count = 0;
                    foreach (KeyValuePair<string, object> item in result)
                    {
                        if (item.Key != "Id")
                        {
                            count++;
                            queryBuilder.Append(item.Key + " = @" + item.Key);
                            if (count < result.Count - 1)
                            {
                                queryBuilder.Append(", ");
                            }
                        }
                    }
                    queryBuilder.Append(" " + whereStatement);
                    command.CommandText = queryBuilder.ToString();
                    command.Connection = connection;
                    foreach (KeyValuePair<string, object> item in result)
                    {
                        command.Parameters.AddWithValue(item.Key, item.Value);
                    }
                    command.ExecuteNonQuery();
                    if (Id == default(int))
                    {
                        Id = (int) command.LastInsertedId;
                    }
                }
        }
    }
}
