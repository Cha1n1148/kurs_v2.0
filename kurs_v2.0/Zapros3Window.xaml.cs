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
    /// Логика взаимодействия для Zapros3Window.xaml
    /// </summary>
    public partial class Zapros3Window : Window
    {
        public Zapros3Window()
        {
            InitializeComponent();
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = DGzapros3.SelectedIndex;
                _db.MostProfitablePropertyTypes.Load();
                DGzapros3.ItemsSource = _db.MostProfitablePropertyTypes.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == DGzapros3.Items.Count)
                    {
                        selectedIndex--;
                    }
                    DGzapros3.SelectedIndex = selectedIndex;
                    DGzapros3.ScrollIntoView(DGzapros3.SelectedItem);
                }
                DGzapros3.Focus();
            }
        }

        private void Zapros3Load_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void info_zapros3_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Представление 'Самый прибыльный тип недвижимости'\nЭто представление определяет тип недвижимости, который принес наибольшую общую прибыль.");
        }

        private void zapros3_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }
    }
}
