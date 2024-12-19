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
    /// Логика взаимодействия для AddTypeTransactionWindow.xaml
    /// </summary>
    public partial class AddTypeTransactionWindow : Window
    {
        KursContext _db = new KursContext();

        TypeTransaction _typeTransaction;
        public AddTypeTransactionWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Add_edit_TypeTransactions_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Tb_add_type_TypeTransactions.Text != "Покупка" && Tb_add_type_TypeTransactions.Text != "Продажа" && Tb_add_type_TypeTransactions.Text != "Аренда") errors.AppendLine("Введите тип сделки");
            if (cb_add_transactions_TypeTransactions.SelectedItem == null) errors.AppendLine("Выберите id сделки");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (ClassData.DataTypeTransaction.typeTransaction == null)
                {
                    _db.TypeTransactions.Add(_typeTransaction);
                    _db.SaveChanges();
                }
                else
                {
                    _db.Entry(_typeTransaction).State = EntityState.Modified;
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

        private void Add_TypeTransactions_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cb_add_transactions_TypeTransactions.ItemsSource = _db.Transactions.ToList();
            cb_add_transactions_TypeTransactions.DisplayMemberPath = "TransactionsId";
            if (ClassData.DataTypeTransaction.typeTransaction == null)
            {
                Title = "Добавить запись.";
                Add_edit_TypeTransactions.Content = "Добавить.";
                _typeTransaction = new TypeTransaction();
            }
            else
            {
                Title = "Изменение записи.";
                Add_edit_TypeTransactions.Content = "Изменить";
                _typeTransaction = _db.TypeTransactions.Find(ClassData.DataTypeTransaction.typeTransaction.TransactionTypeId);
            }
            DataContext = _typeTransaction;
        }

        private void Tb_add_type_TypeTransactions_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char ch in e.Text)
            {
                if (!char.IsDigit(ch))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                    MessageBox.Show("Проверьте, правильно ли вы ввели данные.", "Ошибка ввода в поле 'Имя'.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                }
            }
        }
    }
}
