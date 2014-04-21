using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BionicProject
{
    /// <summary>
    /// Interaction logic for TeacherQuestionAddingControl.xaml
    /// </summary>
    public partial class TeacherQuestionAddingControl : UserControl
    {
        private Course currentCourse;
        public List<Answer> answers;
        public Question question;
        

        public TeacherQuestionAddingControl(Course currentCourse)
        {
            InitializeComponent();
            this.currentCourse = currentCourse;
            Refresh();
        }

        private void Refresh()
        {
            answers = null;
            lvAnswersList.ItemsSource = null;
            cbDifficulty.SelectedIndex = 0;
            cbCountOfAnswers.SelectedIndex = -1;
            tbQuestionsText.Text = "";
            question = new Question("",QuestionType.Checkbox,1,currentCourse.CourseId);
            gridMain.DataContext = question;
            
        }

        private void cbCountOfAnswers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int countOfAnswers = cbCountOfAnswers.SelectedIndex +1;
             answers = new List<Answer>();
            for (int i = 0; i < countOfAnswers; i++)
            {
                answers.Add(new Answer("",question.QuestionId,false));
            }
            lvAnswersList.ItemsSource = answers;
            //lvAnswersList
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //проверки
            if (string.IsNullOrEmpty(tbQuestionsText.Text))
            {
                MessageBox.Show("Заполните текст вопроса", "Error", MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }
            if (answers==null)
            {
                MessageBox.Show("Заполните ответы на вопрос", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            int countOfCorrectAnswers = 0;
            int countOfAnswers = cbCountOfAnswers.SelectedIndex + 1;
            for (int i = 0; i < countOfAnswers; i++)
            {
                if(answers[i].IsCorrect)
                    countOfCorrectAnswers++;    
                if (string.IsNullOrEmpty(answers[i].AnswerText))
                {
                    MessageBox.Show("Заполните все ответы", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
            }
            if (countOfCorrectAnswers == 0)
            {
                MessageBox.Show("Хотя бы один ответ должен быть правильным", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            //проверки

            TeacherModule.SaveQuestion(question);
            countOfCorrectAnswers = 0;
            for (int i = 0; i < countOfAnswers; i++)
            {
                answers[i].QuestionsId = question.QuestionId;
                if (answers[i].IsCorrect)
                    countOfCorrectAnswers++;
                TeacherModule.SaveAnswer(answers[i]);
            }
            if (countOfAnswers == 1)
            {
                question.QuestionType = QuestionType.Text;
            }
            else if(countOfAnswers>1 && countOfCorrectAnswers==1)
            {
                question.QuestionType = QuestionType.Radiobutton;
            }
            else
            {
                question.QuestionType = QuestionType.Checkbox;
            }
            TeacherModule.SaveQuestion(question);
            Refresh();
        }
    }
}
