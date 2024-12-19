using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace kurs_v2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Info_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Курсовая работа.");
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MainClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsWindow clientsWindow = new ClientsWindow();
            clientsWindow.ShowDialog();
        }

        private void MainPropertys_Click(object sender, RoutedEventArgs e)
        {
            PropertysWindow propertysWindow = new PropertysWindow();
            propertysWindow.ShowDialog();
        }

        private void MainAgencys_Click(object sender, RoutedEventArgs e)
        {
            AgencysWindow agencysWindow = new AgencysWindow();
            agencysWindow.ShowDialog();
        }

        private void MainTransactions_Click(object sender, RoutedEventArgs e)
        {
            TransactionsWindow transactionsWindow = new TransactionsWindow();
            transactionsWindow.ShowDialog();
        }

        private void MainType_Click(object sender, RoutedEventArgs e)
        {
            TypeTransactionsWindow typeTransactionsWindow = new TypeTransactionsWindow();
            typeTransactionsWindow.ShowDialog();
        }

        private void zapros1_Click(object sender, RoutedEventArgs e)
        {
            Zapros1Window zapros1Window = new Zapros1Window();
            zapros1Window.ShowDialog();
        }

        private void zapros2_Click(object sender, RoutedEventArgs e)
        {
            Zapros2Window zapros2Window = new Zapros2Window();
            zapros2Window.ShowDialog();
        }

        private void zapros3_Click(object sender, RoutedEventArgs e)
        {
            Zapros3Window zapros3Window = new Zapros3Window();
            zapros3Window.ShowDialog();
        }

        private void zapros4_Click(object sender, RoutedEventArgs e)
        {
            Zapros4Window zapros4Window = new Zapros4Window();
            zapros4Window.ShowDialog();
        }

        private void zapros5_Click(object sender, RoutedEventArgs e)
        {
            Zapros5Window zapros5Window = new Zapros5Window();
            zapros5Window.ShowDialog();
        }
    }
}