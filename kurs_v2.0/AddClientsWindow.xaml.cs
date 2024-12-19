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
    /// Логика взаимодействия для AddClientsWindow.xaml
    /// </summary>
    public partial class AddClientsWindow : Window
    {
        KursContext _db = new KursContext();

        Client _client;
        public AddClientsWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Add_edit_clients_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Tb_add_fname_clients.Text.Length == 0) errors.AppendLine("Введите имя");
            if (Tb_add_mname_clients.Text.Length == 0) errors.AppendLine("Введите отчество");
            if (Tb_add_lname_clients.Text.Length == 0) errors.AppendLine("Введите фамилию");
            if (Tb_add_number_clients.Text.Length == 0) errors.AppendLine("Введите номер телефона");
            if (Tb_add_email_clients.Text.Length == 0) errors.AppendLine("Введите почту");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (ClassData.DataClient.client == null)
                {
                    _db.Clients.Add(_client);
                    _db.SaveChanges();
                }
                else
                {
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

        private void Add_clients_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClassData.DataClient.client == null)
            {
                Title = "Добавить запись.";
                Add_edit_clients.Content = "Добавить.";
                _client = new Client();
            }
            else
            {
                Title = "Изменение записи.";
                Add_edit_clients.Content = "Изменить";
                _client = _db.Clients.Find(ClassData.DataClient.client.ClientsId);
            }
            DataContext = _client;
        }

        private void Tb_add_fname_clients_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Tb_add_mname_clients_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Tb_add_lname_clients_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Tb_add_number_clients_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
    }
}
