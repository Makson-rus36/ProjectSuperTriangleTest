using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;

namespace ProjectSuperTriangleTest
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public SqlDataAdapter adapter;
        public SqlCommand cmd;

        public RegWindow()
        {
            InitializeComponent();
        }
        public void addSomeDannie(string name, string surname, string Otchestvo, string _class)
        {
            try
            {
                cmd = new SqlCommand("insert into Schoolguys (Name, Surname,LastName, _Class) values(@name, @Surname, @LastName, @class)", connection);
                connection.Open();
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@Surname", surname);
                cmd.Parameters.AddWithValue("@LastName", Otchestvo);
                cmd.Parameters.AddWithValue("@class", _class);
                cmd.ExecuteNonQuery();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
                
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxName.Text == "" || textBoxLastName.Text == "" || textBoxPatronymic.Text == "" || textBoxClass.Text == "")
                MessageBox.Show("Не все поля заполнены!!!");
            else
            {
                addSomeDannie(textBoxName.Text, textBoxPatronymic.Text, textBoxLastName.Text, textBoxClass.Text);
                this.Close();
                TestWindow testWindow = new TestWindow();
                testWindow.Show();
                this.Close();
            }
        }

        private void BtnClickExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("Подтвердите выход", "Выход",
                                                                                                         System.Windows.Forms.MessageBoxButtons.YesNo,
                                                                                                         System.Windows.Forms.MessageBoxIcon.Warning);
            if (result == System.Windows.Forms.DialogResult.Yes)
                this.Close();
        }
    }
   
    
}
