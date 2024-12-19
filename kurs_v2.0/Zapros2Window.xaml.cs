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
    /// Логика взаимодействия для Zapros2Window.xaml
    /// </summary>
    public partial class Zapros2Window : Window
    {
        public Zapros2Window()
        {
            InitializeComponent();
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = DGzapros2.SelectedIndex;
                _db.AveragePriceByPropertyTypes.Load();
                DGzapros2.ItemsSource = _db.AveragePriceByPropertyTypes.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DGzapros2.Items.Count)
                    {
                        selectedIndex--;
                    }
                    DGzapros2.SelectedIndex = selectedIndex;
                    DGzapros2.ScrollIntoView(DGzapros2.SelectedItem);
                }
                DGzapros2.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void Zapros2Load_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void info_zapros2_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Представление 'Средняя цена недвижимости по типу'\nЭто представление показывает среднюю цену недвижимости в зависимости от её типа.");
        }

        private void zapros2_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
