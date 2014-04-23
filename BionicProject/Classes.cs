using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.ComponentModel;  //Binding
using System.Collections.ObjectModel;

namespace BionicProject
{

    public enum CourseStatus
    {
        Abiturient=0, Student=1, Teacher = 2, Admin=6
    };

    public class User
    {
        int userId;
        public int UserID { get { return userId; } }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public DateTime Bdate { get; set; }
        public string Email { get; set; }
        string pass;
        public string Password { get { return pass; } }
        public bool IsAdmin { get; private set; }

        public Courses MyCourses;

        public EntryTests MyEntryTests;

        public User(int userId, string Fname, string Lname, DateTime Bdate, string Email, bool IsAdmin = false)
        {
          this.userId = userId;
          this.Fname = Fname;
          this.Lname = Lname;
          this.Bdate = Bdate;
          this.Email = Email;
          this.IsAdmin = IsAdmin;

          MyCourses = new Courses(this);
          MyEntryTests = new EntryTests();

        }
    }

    class UserCourses
    {
        public int CourseId { get; private set; }
        public int UserId { get; private set; }
        public CourseStatus Status { get; set; }
    }

   public class Course : INotifyPropertyChanged
    {
        int courseid;
        public int CourseId { get { return courseid; } }
        string name;
        public string CourseName { get { return name; } set { SetField(ref name, value, "CourseName"); } }
        int teachid;
        public int TeacherId { get { return teachid; } }
        public CourseStatus Status { get; set; }

        public Course(int courseid, string name, int teachid, int status=6)
        {
           this.courseid = courseid;
           this.name = name;
           this.teachid = teachid;
           Status = (CourseStatus)status;
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

        public override string ToString()
        {
            return CourseName;
        }

    }

   public class Courses : ObservableCollection<Course>
    {
       StoreDB store = new StoreDB();
        public Courses(User user)
        {
            foreach (var m in store.PossibleCourses(user))
            { Add(m); }
        }

    }
   public class EntryTest : INotifyPropertyChanged
   {
       int entrytestid;
       public int EntryTestid { get { return entrytestid; } }
       string entrytestname;
       public string EntryTestName { get { return entrytestname; } set { SetField(ref entrytestname, value, "EntryTestName"); } }

       public EntryTest(int entrytestid, string entrytestname)
       {
           this.entrytestid = entrytestid;
           this.entrytestname = entrytestname;
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
       public override string ToString()
       {
           return EntryTestName;
       }
   }

   public class EntryTests : ObservableCollection<EntryTest>
   {
       StoreDB store = new StoreDB();
       public EntryTests()
       {
           foreach (var m in store.AllEntrytests())
           { Add(m); }
       }
   }
   public class Users : ObservableCollection<User>
    {
       
    }

    public class Question
    {
        public int QuestionId { get; set; }
        public int CourseId { get; private set; }
        public string QuestionText { get; set;}
        public QuestionType QuestionType { get; set; }
        public int Difficulty { get; set; }

        public Question(string questionText,QuestionType questionType, int difficulty, int courseId )
        {
            QuestionText = questionText;
            QuestionType = questionType;
            Difficulty = difficulty;
            CourseId = courseId;
            
        }
    }

    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int QuestionsId { get; set; }
        public bool IsCorrect { get; set; }

        public Answer(string answerText, int questionId, bool isCorrect)
        {
            AnswerText = answerText;
            QuestionsId = questionId;
            IsCorrect = isCorrect;
           
        }
    }
}
