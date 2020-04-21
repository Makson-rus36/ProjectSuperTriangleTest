using System.Windows;

namespace ProjectSuperTriangleTest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnTeory_Click(object sender, RoutedEventArgs e)
        {
            TeoryWindow teoryWindow = new TeoryWindow();
            teoryWindow.ShowDialog();
        }

        private void BtnRating_Click(object sender, RoutedEventArgs e)
        {
            RatingWindow rating = new RatingWindow();
            rating.ShowDialog();
        }
       
        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            RegWindow window1 = new RegWindow();
            window1.Show();
            
        }
    }
}
