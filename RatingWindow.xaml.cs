using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;


namespace ProjectSuperTriangleTest
{
    class Item
    {
        public String Place { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Surname { get; set; }
        public String Score { get; set; }
        public String RightAnswers { get; set; }
        public String WrongAnswers { get; set; }
        public String _Class { get; set; }
        public DateTime DataTest { get; set; }
        
    }
    /// <summary>
    /// Логика взаимодействия для RatingWindow.xaml
    /// </summary>
    public partial class RatingWindow : Window
    {
        public SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        public SqlDataAdapter adapter;

        public RatingWindow()
        {
            InitializeComponent();
            LoadDataTabel();

            
        }
        private void LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
        public DataTable Table { get; private set; }

        public void LoadDataTabel()
        {
            string sql = "Select r.Score,  r.RightAnswers, r.WrongAnswers, (select Name from Schoolguys where r.Id_Guy = Id_Schoolguy) as Name,"+
 " (select LastName from Schoolguys where r.Id_Guy = Id_Schoolguy) as LastName,"+
 " (select Surname from Schoolguys where r.Id_Guy = Id_Schoolguy) as Surname," +
 " (select _Class from Schoolguys where r.Id_Guy = Id_Schoolguy) as _Class,r.DataTest" +
 " from Result r" +
 " order by r.Score desc; ";
           Table = new DataTable();
           Table= loadfromDB(sql, ref connection);
            for (int i = 0; i < Table.Rows.Count; i++)
            {
                dataGrid.Items.Add(new Item()
                {
                    Place = Convert.ToString(i + 1),
                    Score = Table.Rows[i].ItemArray[0].ToString(),
                    RightAnswers = Table.Rows[i].ItemArray[1].ToString(),
                    WrongAnswers = Table.Rows[i].ItemArray[2].ToString(),
                    Name = Table.Rows[i].ItemArray[3].ToString(),
                    LastName = Table.Rows[i].ItemArray[4].ToString(),
                    Surname = Table.Rows[i].ItemArray[5].ToString(),
                    _Class = Table.Rows[i].ItemArray[6].ToString(),
                    DataTest = DateTime.Parse(Table.Rows[i].ItemArray[7].ToString()).Date


                });
            }

        }
        
        private DataTable loadfromDB(string sql, ref SqlConnection connection)
        {
            try
            {
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(Table);
                return Table;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void testStartClick(object sender, RoutedEventArgs e)
        {
            this.Close();
            RegWindow window1 = new RegWindow();
            window1.Show();
        }
    }
}
