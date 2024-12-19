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
    /// Логика взаимодействия для TransactionsWindow.xaml
    /// </summary>
    public partial class TransactionsWindow : Window
    {
        public static class Data
        {
            public static Transaction transaction;
        }
        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = TransactionDG.SelectedIndex;
                _db.Clients.Load();
                _db.Propertys.Load();
                _db.Agencys.Load();
                _db.Transactions.Load();
                TransactionDG.ItemsSource = _db.Transactions.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == TransactionDG.Items.Count)
                    {
                        selectedIndex--;
                    }
                    TransactionDG.SelectedIndex = selectedIndex;
                    TransactionDG.ScrollIntoView(TransactionDG.SelectedItem);
                }
                TransactionDG.Focus();
            }
        }
        public TransactionsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void TransactionsLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void TransactionsBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void TransactionsAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassData.DataTransaction.transaction = null;
            AddTransactionsWindow addTransactionsWindow = new AddTransactionsWindow();
            addTransactionsWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void TransactionsEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TransactionDG.SelectedItem != null)
            {
                ClassData.DataTransaction.transaction = (Transaction)TransactionDG.SelectedItem;
                AddTransactionsWindow addTransactionsWindow = new AddTransactionsWindow();
                addTransactionsWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void TransactionsDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    Transaction row = (Transaction)TransactionDG.SelectedItem;
                    if (row != null)
                    {
                        using (KursContext _db = new KursContext())
                        {
                            _db.Transactions.Remove(row);
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

        private void TransactionsSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_TransactionsSearch.Text))
            {
                MessageBox.Show("Пожалуйста, введите значение для поиска.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<Transaction> listItem = (List<Transaction>)TransactionDG.ItemsSource;
                var filtered = listItem.Where(p => p.AgencyId.ToString().Contains(Tb_TransactionsSearch.Text));
                if (filtered.Count() > 0)
                {
                    var item = filtered.First();
                    TransactionDG.SelectedItem = item;
                    TransactionDG.ScrollIntoView(item);
                    TransactionDG.Focus();
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
