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
    /// Логика взаимодействия для Zapros5Window.xaml
    /// </summary>
    public partial class Zapros5Window : Window
    {
        public Zapros5Window()
        {
            InitializeComponent();
        }
        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = DGzapros5.SelectedIndex;
                _db.TotalTransactionAmountByMonthAndAgencies.Load();
                DGzapros5.ItemsSource = _db.TotalTransactionAmountByMonthAndAgencies.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DGzapros5.Items.Count)
                    {
                        selectedIndex--;
                    }
                    DGzapros5.SelectedIndex = selectedIndex;
                    DGzapros5.ScrollIntoView(DGzapros5.SelectedItem);
                }
                DGzapros5.Focus();
            }
        }

        private void Zapros5Load_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void info_zapros5_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Представление 'Общая сумма сделок по месяцам и агентствам'\nЭто представление показывает общую сумму сделок, совершенных каждым агентством в каждом месяце.");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void zapros5_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
