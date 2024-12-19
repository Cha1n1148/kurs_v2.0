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
    /// Логика взаимодействия для PropertysWindow.xaml
    /// </summary>
    public partial class PropertysWindow : Window
    {
        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = PropertyDG.SelectedIndex;
                _db.Clients.Load();
                _db.Propertys.Load();
                _db.Agencys.Load();
                _db.Transactions.Load();
                PropertyDG.ItemsSource = _db.Propertys.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == PropertyDG.Items.Count)
                    {
                        selectedIndex--;
                    }
                    PropertyDG.SelectedIndex = selectedIndex;
                    PropertyDG.ScrollIntoView(PropertyDG.SelectedItem);
                }
                PropertyDG.Focus();
            }
        }
        public PropertysWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void PropertysLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void PropertyBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void PropertysAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassData.DataProperty.property = null;
            AddPropertysWindow addPropertysWindow = new AddPropertysWindow();
            addPropertysWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void PropertysEdit_Click(object sender, RoutedEventArgs e)
        {
            if (PropertyDG.SelectedItem != null)
            {
                ClassData.DataProperty.property = (Property)PropertyDG.SelectedItem;
                AddPropertysWindow addPropertysWindow = new AddPropertysWindow();
                addPropertysWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void PropertysDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Property row = (Property)PropertyDG.SelectedItem;
                    if (row != null)
                    {
                        using (KursContext _db = new KursContext())
                        {
                            _db.Propertys.Remove(row);
                            _db.SaveChanges();
                        }
                        LoadDBInDataGrid();
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка.", "Ошибка удаления.", MessageBoxButton.OKCancel, MessageBoxImage.Error);
                }
            }
            else
            {
                PropertyDG.Focus();
            }
        }

        private void PropertysSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_ClientsSearch.Text))
            {
                MessageBox.Show("Пожалуйста, введите значение для поиска.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Property> listItem = (List<Property>)PropertyDG.ItemsSource;
                var filtered = listItem.Where(p => p.PropertyId.ToString().Contains(Tb_ClientsSearch.Text));
                if (filtered.Count() > 0)
                {
                    var item = filtered.First();
                    PropertyDG.SelectedItem = item;
                    PropertyDG.ScrollIntoView(item);
                    PropertyDG.Focus();
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
