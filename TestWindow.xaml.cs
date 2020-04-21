using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace ProjectSuperTriangleTest
{
    static class  ClassForRandomingQuest
   {
        
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
    class TestStruct
    {
        public string type { get; set; }
        public string descrp { get; set; }
        public string right { get; set; }
        public string ans1 { get; set; }
        public string ans2 { get; set; }
        public string ans3 { get; set; }
        public string koef { get; set; }


    }
    /// <summary>
    /// Логика взаимодействия для TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public SqlDataAdapter adapter;
        public SqlCommand cmd;
        int CurrentAnsw = 0;
        string UserAnswer;
        List<TestStruct> testStructs;
        string[] testUserAnsw;
        int RightAnsw = 0;
        int WrongAnsw = 0;
        float Scores = 0.0f;
        public TestWindow()
        {
            InitializeComponent();
            

           testStructs = new List<TestStruct>();
            

           
            LoadQwest();
            testUserAnsw = new string[testStructs.Count];
            ShowQwest();
        }
        
        
        public void LoadQwest()
        {
            try
            {
                string[] s = new string[7];
                XmlTextReader reader = new XmlTextReader(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//TriangleSystem//TestXML.xml");
                int i = 0;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Text:
                            {
                                i++;
                                s[i - 1] = reader.Value;
                                break;
                            }
                    }
                    if (i == 7)
                    {
                        testStructs.Add(new TestStruct { type = s[0], descrp = s[2], ans1 = s[3], ans2 = s[4], ans3 = s[5], right = s[1], koef = s[6] });
                        i = 0;
                    }

                }
                ClassForRandomingQuest.Shuffle<TestStruct>(testStructs);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void ShowQwest()
        {
            if (testStructs[CurrentAnsw].type.Contains("выбор"))
            {
                radioButton1.IsChecked = false;
                radioButton2.IsChecked = false;
                radioButton3.IsChecked = false;
                Type2.Visibility = Visibility.Collapsed;
                Type1.Visibility = Visibility.Visible;
                labelType1Quest.Content = "Вопрос № " + (CurrentAnsw + 1).ToString() + "/" + testStructs.Count;
                HeaderTask.Text = testStructs[CurrentAnsw].descrp;
                rbText1.Text = testStructs[CurrentAnsw].ans1;
                rbText2.Text = testStructs[CurrentAnsw].ans2;
                rbText3.Text = testStructs[CurrentAnsw].ans3;
            }else if (testStructs[CurrentAnsw].type.Contains("задача"))
            {
                UserAnswersTextTask.Text = null;
                Type1.Visibility = Visibility.Collapsed;
                Type2.Visibility = Visibility.Visible;
                labelType2Quest.Content = "Вопрос № " + (CurrentAnsw + 1).ToString() + "/" + testStructs.Count;
                textBoxTask.Text = testStructs[CurrentAnsw].descrp;
            }
            
        }
        private void ShowQwest(int pos)
        {
            if (testUserAnsw[CurrentAnsw] != null)
            {
                if (testStructs[CurrentAnsw].type.Contains("выбор"))
                {
                
                    if (testUserAnsw[CurrentAnsw] =="1")
                        radioButton1.IsChecked = true;
                    else if(testUserAnsw[CurrentAnsw] == "2")
                    radioButton2.IsChecked = true;
                    else if(testUserAnsw[CurrentAnsw] == "3")
                    radioButton3.IsChecked = true;
                    else { }
                    Type2.Visibility = Visibility.Collapsed;
                    Type1.Visibility = Visibility.Visible;
                    labelType1Quest.Content = "Вопрос № " + (CurrentAnsw + 1).ToString() + "/" + testStructs.Count;
                    HeaderTask.Text = testStructs[CurrentAnsw].descrp;
                    rbText1.Text = testStructs[CurrentAnsw].ans1;
                    rbText2.Text = testStructs[CurrentAnsw].ans2;
                    rbText3.Text = testStructs[CurrentAnsw].ans3;
                }
                else if (testStructs[CurrentAnsw].type.Contains("задача"))
                {
                    UserAnswersTextTask.Text = testUserAnsw[CurrentAnsw];
                    Type1.Visibility = Visibility.Collapsed;
                    Type2.Visibility = Visibility.Visible;
                    labelType2Quest.Content = "Вопрос № " + (CurrentAnsw + 1).ToString() + "/" + testStructs.Count;
                    textBoxTask.Text = testStructs[CurrentAnsw].descrp;

                }
            }
            else
            {
                ShowQwest();
            }

        }
        private void Result()
        {
            
            for (int i = 0; i < testStructs.Count; i++) {
                testStructs[i].right = testStructs[i].right.Replace(" ", "");
                testUserAnsw[i] = testUserAnsw[i].Replace(" ", "");
                if (testUserAnsw[i] == testStructs[i].right)
                {
                    RightAnsw++;
                    Scores = Scores + (1 * float.Parse(testStructs[i].koef));
                }
                else
                {
                    WrongAnsw++;
                }
            }
            MessageBox.Show("Результаты теста\nПравильно: " + RightAnsw.ToString() + " \n Неправильно: " + WrongAnsw.ToString() + " \n Ваш балл: " + Scores.ToString());
            try
            {
                String sql = "select Id_Schoolguy from Schoolguys order by Id_Schoolguy desc;";
                cmd = new SqlCommand(sql, connection);
                connection.Open();
                int id = int.Parse(cmd.ExecuteScalar().ToString());

                sql = "insert into Result (Score, DataTest, Id_Guy, RightAnswers, WrongAnswers) Values (@score, getdate(), @id, @ra, @wa)";
                cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@score", Scores);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@ra", RightAnsw);
                cmd.Parameters.AddWithValue("@wa", WrongAnsw);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                RatingWindow ratingWindow = new RatingWindow();
                ratingWindow.Show();
                this.Close();
            }
        }
        private void SaveAnswer()
        {
            if (radioButton1.IsChecked == false && radioButton2.IsChecked == false && radioButton3.IsChecked == false)
                UserAnswer = "-1";
            if (testStructs[CurrentAnsw].type == "выбор")
            {
                if (radioButton1.IsChecked == true)
                    UserAnswer = "1";
                else if (radioButton2.IsChecked == true)
                    UserAnswer = "2";
                else if (radioButton3.IsChecked == true)
                    UserAnswer = "3";
                testUserAnsw[CurrentAnsw]= UserAnswer;
            }
            else if (testStructs[CurrentAnsw].type == "задача")
                testUserAnsw[CurrentAnsw] = UserAnswersTextTask.Text;
               
        }
        private void buttonNext1_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnsw != testStructs.Count-1)
            {
                SaveAnswer();
                CurrentAnsw++;
                ShowQwest(CurrentAnsw);
            }else if (CurrentAnsw == testStructs.Count - 1)
            {
                SaveAnswer();
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Конец теста! Готовы проверить ответы?", "Проверка",
                                                                                                         System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                                                         System.Windows.Forms.MessageBoxIcon.Question);
                if (result == System.Windows.Forms.DialogResult.Yes)
                    Result();
            }
            else
            {
               
            }
        }

        private void buttonRedo1_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentAnsw > 0)
            {
                CurrentAnsw--;
                ShowQwest(CurrentAnsw);
            }
            else
            {
                MessageBox.Show("Это начало теста");
            }
        }
       
        private void textInputOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",")
              && (e.Text.Contains(",")
              && e.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }

       

        private void BtnClickExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Подтвердите выход\n ВНИМАНИЕ! Результаты теста сохранены не будут!", "Выход",
                                                                                                         System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                                                         System.Windows.Forms.MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }

        
    }
}
