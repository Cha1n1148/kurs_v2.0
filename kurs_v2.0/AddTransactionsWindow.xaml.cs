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
    /// Логика взаимодействия для AddTransactionsWindow.xaml
    /// </summary>
    public partial class AddTransactionsWindow : Window
    {
        KursContext _db = new KursContext();

        Transaction _transaction;
        public AddTransactionsWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_add_client_transaction.ItemsSource = _db.Clients.ToList();
            cb_add_client_transaction.DisplayMemberPath = "ClientsId";
            cb_add_agency_transaction.ItemsSource = _db.Agencys.ToList();
            cb_add_agency_transaction.DisplayMemberPath = "AgencyId";
            cb_add_property_transaction.ItemsSource = _db.Propertys.ToList();
            cb_add_property_transaction.DisplayMemberPath = "PropertyId";
            if (ClassData.DataTransaction.transaction == null)
            {
                Title = "Добавить запись.";
                Add_edit_transaction.Content = "Добавить.";
                _transaction = new Transaction();
            }
            else
            {
                Title = "Изменение записи.";
                Add_edit_transaction.Content = "Изменить";
                _transaction = _db.Transactions.Find(ClassData.DataTransaction.transaction.TransactionsId);
            }
            DataContext = _transaction;
        }
    

        private void Tb_add_price_transaction_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    MessageBox.Show("Проверьте корректность вводимых значений!", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Add_edit_transaction_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Tb_add_price_transaction.Text.Length == 0) errors.AppendLine("Введите сумму сделки");
            if (cb_add_agency_transaction.SelectedItem == null) errors.AppendLine("Выберите id агенства");
            if (cb_add_property_transaction.SelectedItem == null) errors.AppendLine("Выберите id недвижимости");
            if (cb_add_client_transaction.SelectedItem == null) errors.AppendLine("Выберите id клиента");
            if (Dp_add_data_transaction.SelectedDate == null) errors.AppendLine("Введите дату сделки");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (ClassData.DataTransaction.transaction == null)
                {
                    _transaction.AmountTransaction = System.Decimal.Parse(Tb_add_price_transaction.Text);
                    _transaction.TransactionDate = System.DateOnly.Parse(Dp_add_data_transaction.Text);
                    _db.Transactions.Add(_transaction);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(_transaction).State = EntityState.Modified;
                    _db.SaveChanges();
                }
                MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при сохранении изменений. Подробности: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Add_transaction_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
