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
        public Question CreateQuestion(string questionText, QuestionType questionType, int difficulty, Course course)
        {
            StoreDB storeDb = new StoreDB();
            using (MySqlConnection connection =storeDb.Connection)
            {
                connection.Open();
                string query = string.Format("insert into {0} set ", "Questions");
                StringBuilder queryBuilder = new StringBuilder(query);
                using (MySqlCommand command = new MySqlCommand())
                {

                    queryBuilder.Append(" QuestionText=@QuestionText, QuestionType=@QuestionType, Difficulty=@Difficulty, CourseId=@CourseId");
                    command.CommandText = queryBuilder.ToString();
                    command.Connection = connection;

                    command.Parameters.AddWithValue("QuestionText", questionText);
                    command.Parameters.AddWithValue("QuestionType", questionType);
                    command.Parameters.AddWithValue("Difficulty", difficulty);
                    command.Parameters.AddWithValue("CourseId", course.CourseId);
                    
                    command.ExecuteNonQuery();  
                    Question question = new Question(questionText,questionType,difficulty,course.CourseId);
                    question.QuestionId = (int)command.LastInsertedId;
                    return question;
                }
            }
        }

        public Answer CreateAnswer(Question question, string answerText)
        {
            StoreDB storeDb = new StoreDB();
            using (MySqlConnection connection = storeDb.Connection)
            {
                connection.Open();
                string query = string.Format("insert into {0} set ", "Answer");
                StringBuilder queryBuilder = new StringBuilder(query);
                using (MySqlCommand command = new MySqlCommand())
                {

                    queryBuilder.Append(" AnswerText=@AnswerText, QuestionId=@QuestionId");
                    command.CommandText = queryBuilder.ToString();
                    command.Connection = connection;

                    command.Parameters.AddWithValue("AnswerText", answerText);
                    command.Parameters.AddWithValue("QuestionType", question.QuestionId);

                    command.ExecuteNonQuery();
                    Answer answer = new Answer(answerText,question.QuestionId);
                    answer.AnswerId = (int)command.LastInsertedId;
                    return  answer;
                }
            }
        }
    }
}