using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    /// Логика взаимодействия для AgencysWindow.xaml
    /// </summary>
    public partial class AgencysWindow : Window
    {
        public AgencysWindow()
        {
            InitializeComponent();
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = AgencyDG.SelectedIndex;
                _db.Agencys.Load();
                AgencyDG.ItemsSource = _db.Agencys.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == AgencyDG.Items.Count)
                    {
                        selectedIndex--;
                    }
                    AgencyDG.SelectedIndex = selectedIndex;
                    AgencyDG.ScrollIntoView(AgencyDG.SelectedItem);
                }
                AgencyDG.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void AgencysLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void AgencysBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void AgencysAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassData.DataAgency.agency = null;
            AddAgencysWindow addAgencysWindow = new AddAgencysWindow();
            addAgencysWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void AgencysEdit_Click(object sender, RoutedEventArgs e)
        {
            if (AgencyDG.SelectedItem != null)
            {
                ClassData.DataAgency.agency = (Agency)AgencyDG.SelectedItem;
                AddAgencysWindow addAgencysWindow = new AddAgencysWindow();
                addAgencysWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void AgencysDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Agency row = (Agency)AgencyDG.SelectedItem;
                    if (row != null)
                    {
                        using (KursContext _db = new KursContext())
                        {
                            _db.Agencys.Remove(row);
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
        }

        private void AgencysSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_AgencysSearch.Text))
            {
                MessageBox.Show("Пожалуйста, введите значение для поиска.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Agency> listItem = (List<Agency>)AgencyDG.ItemsSource;
                var filtered = listItem.Where(p => p.AgencyId.ToString().Contains(Tb_AgencysSearch.Text));
                if (filtered.Count() > 0)
                {
                    var item = filtered.First();
                    AgencyDG.SelectedItem = item;
                    AgencyDG.ScrollIntoView(item);
                    AgencyDG.Focus();
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
