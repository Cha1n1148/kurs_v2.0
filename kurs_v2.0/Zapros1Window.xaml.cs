using Microsoft.EntityFrameworkCore;
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
using System.Windows.Shapes;

namespace kurs_v2._0
{
    /// <summary>
    /// Логика взаимодействия для Zapros1Window.xaml
    /// </summary>
    public partial class Zapros1Window : Window
    {
        public Zapros1Window()
        {
            InitializeComponent();
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = DGzapros1.SelectedIndex;
                _db.TransactionsDetails.Load();
                DGzapros1.ItemsSource = _db.TransactionsDetails.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DGzapros1.Items.Count)
                    {
                        selectedIndex--;
                    }
                    DGzapros1.SelectedIndex = selectedIndex;
                    DGzapros1.ScrollIntoView(DGzapros1.SelectedItem);
                }
                DGzapros1.Focus();
            }
        }

        private void info_zapros1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Представление 'Сделки с деталями'.\nЭто представление будет содержать подробную информацию о каждой сделке, включая данные о недвижимости, клиенте и агентстве.");
        }

        private void zapros1_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void Zapros1Load_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
