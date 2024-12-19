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
    /// Логика взаимодействия для ClientsWindow.xaml
    /// </summary>
    public partial class ClientsWindow : Window
    {
        public ClientsWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = ClientsDG.SelectedIndex;
                _db.Clients.Load();
                _db.Propertys.Load();
                _db.Agencys.Load();
                _db.Transactions.Load();
                _db.TypeTransactions.Load();
                ClientsDG.ItemsSource = _db.Clients.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == ClientsDG.Items.Count)
                    {
                        selectedIndex--;
                    }
                    ClientsDG.SelectedIndex = selectedIndex;
                    ClientsDG.ScrollIntoView(ClientsDG.SelectedItem);
                }
                ClientsDG.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void ClientsLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void ClientBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void ClientsAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassData.DataClient.client = null;
            AddClientsWindow addClientsWindow = new AddClientsWindow();
            addClientsWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void ClientsEdit_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsDG.SelectedItem != null)
            {
                ClassData.DataClient.client = (Client)ClientsDG.SelectedItem;
                AddClientsWindow addClientsWindow = new AddClientsWindow();
                addClientsWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void ClientsDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Client row = (Client)ClientsDG.SelectedItem;
                    if (row != null)
                    {
                        using (KursContext _db = new KursContext())
                        {
                            _db.Clients.Remove(row);
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
                ClientsDG.Focus();
            }
        }

        private void ClientsSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_ClientsSearch.Text))
            {
                MessageBox.Show("Пожалуйста, введите значение для поиска.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Client> listItem = (List<Client>)ClientsDG.ItemsSource;
                var filtered = listItem.Where(p => p.ClientsId.ToString().Contains(Tb_ClientsSearch.Text));
                if (filtered.Count() > 0)
                {
                    var item = filtered.First();
                    ClientsDG.SelectedItem = item;
                    ClientsDG.ScrollIntoView(item);
                    ClientsDG.Focus();
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
