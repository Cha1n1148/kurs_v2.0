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
    /// Логика взаимодействия для TypeTransactionsWindow.xaml
    /// </summary>
    public partial class TypeTransactionsWindow : Window
    {
        public static class Data
        {
            public static TypeTransaction typeTransaction;
        }
        public TypeTransactionsWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        void LoadDBInDataGrid()
        {
            using (KursContext _db = new KursContext())
            {
                int selectedIndex = TypeTransactionsDG.SelectedIndex;
                _db.Transactions.Load();
                _db.TypeTransactions.Load();
                TypeTransactionsDG.ItemsSource = _db.TypeTransactions.ToList();
                if (selectedIndex != -1)
                {
                    if (selectedIndex == TypeTransactionsDG.Items.Count)
                    {
                        selectedIndex--;
                    }
                    TypeTransactionsDG.SelectedIndex = selectedIndex;
                    TypeTransactionsDG.ScrollIntoView(TypeTransactionsDG.SelectedItem);
                }
                TypeTransactionsDG.Focus();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void TypeTransactionsLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadDBInDataGrid();
        }

        private void TypeTransactionsBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void TypeTransactionsAdd_Click(object sender, RoutedEventArgs e)
        {
            ClassData.DataTypeTransaction.typeTransaction = null;
            AddTypeTransactionWindow addTypeTransactionWindow = new AddTypeTransactionWindow();
            addTypeTransactionWindow.ShowDialog();
            LoadDBInDataGrid();
        }

        private void TypeTransactionsEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TypeTransactionsDG.SelectedItem != null)
            {
                ClassData.DataTypeTransaction.typeTransaction = (TypeTransaction)TypeTransactionsDG.SelectedItem;
                AddTypeTransactionWindow addTypeTransactionWindow = new AddTypeTransactionWindow();
                addTypeTransactionWindow.ShowDialog();
                LoadDBInDataGrid();
            }
        }

        private void TypeTransactionsDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;
            result = MessageBox.Show("Удалить запись?", "Удаление записи.", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    TypeTransaction row = (TypeTransaction)TypeTransactionsDG.SelectedItem;
                    if (row != null)
                    {
                        using (KursContext _db = new KursContext())
                        {
                            _db.TypeTransactions.Remove(row);
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

        private void TypeTransactionSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tb_TypeTransactionsSearch.Text))
            {
                MessageBox.Show("Пожалуйста, введите значение для поиска.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                List<TypeTransaction> listItem = (List<TypeTransaction>)TypeTransactionsDG.ItemsSource;
                var filtered = listItem.Where(p => p.TransactionTypeId.ToString().Contains(Tb_TypeTransactionsSearch.Text));
                if (filtered.Count() > 0)
                {
                    var item = filtered.First();
                    TypeTransactionsDG.SelectedItem = item;
                    TypeTransactionsDG.ScrollIntoView(item);
                    TypeTransactionsDG.Focus();
                }
                else
                {
                    MessageBox.Show("Ничего не найдено.", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
    }
}
