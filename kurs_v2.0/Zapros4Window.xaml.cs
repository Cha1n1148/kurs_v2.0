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
    /// Логика взаимодействия для Zapros4Window.xaml
    /// </summary>
    public partial class Zapros4Window : Window
    {
        public Zapros4Window()
        {
            InitializeComponent();
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = DGzapros4.SelectedIndex;
                _db.TransactionCountAndTotalAmountByTransactionTypes.Load();
                DGzapros4.ItemsSource = _db.TransactionCountAndTotalAmountByTransactionTypes.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DGzapros4.Items.Count)
                    {
                        selectedIndex--;
                    }
                    DGzapros4.SelectedIndex = selectedIndex;
                    DGzapros4.ScrollIntoView(DGzapros4.SelectedItem);
                }
                DGzapros4.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void Zapros4Load_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void info_zapros4_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Представление 'Количество сделок и сумма сделок по типам сделок'\nЭто представление показывает количество и общую сумму сделок для каждого типа сделки.");
        }

        private void zapros4_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
