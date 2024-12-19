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
    /// Логика взаимодействия для AddAgencysWindow.xaml
    /// </summary>
    public partial class AddAgencysWindow : Window
    {
        KursContext _db = new KursContext();

        Agency _agency;
        public AddAgencysWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClassData.DataAgency.agency == null)
            {
                Title = "Добавить запись.";
                Add_edit_agency.Content = "Добавить.";
                _agency = new Agency();
            }
            else
            {
                Title = "Изменение записи.";
                Add_edit_agency.Content = "Изменить";
                _agency = _db.Agencys.Find(ClassData.DataAgency.agency.AgencyId);
            }
            DataContext = _agency;
        }

        private void Tb_add_number_agency_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Add_edit_agency_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Tb_add_name_agency.Text.Length == 0) errors.AppendLine("Введите название");
            if (Tb_add_address_agency.Text.Length == 0) errors.AppendLine("Введите адрес");
            if (Tb_add_number_agency.Text.Length == 0) errors.AppendLine("Введите цену");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (ClassData.DataAgency.agency == null)
                {
                    _db.Agencys.Add(_agency);
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

        private void Add_agency_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
