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
    /// Логика взаимодействия для AddPropertysWindow.xaml
    /// </summary>
    public partial class AddPropertysWindow : Window
    {
        KursContext _db = new KursContext();

        Property _property;
        public AddPropertysWindow()
        {
            InitializeComponent();
            Height += 30;
            Width += 30;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (ClassData.DataProperty.property == null)
            {
                Title = "Добавить запись.";
                Add_edit_property.Content = "Добавить.";
                _property = new Property();
            }
            else
            {
                Title = "Изменение записи.";
                Add_edit_property.Content = "Изменить";
                _property = _db.Propertys.Find(ClassData.DataProperty.property.PropertyId);
            }
            DataContext = _property;
        }

        private void Tb_add_class_property_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
                    MessageBox.Show("Проверьте, правильно ли вы ввели данные.", "Ошибка ввода в поле 'Тип недвижимости'.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                }
            }
        }

        private void Tb_add_price_property_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Add_edit_property_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (Tb_add_address_property.Text.Length == 0) errors.AppendLine("Введите адрес");
            if (Tb_add_class_property.Text.Length == 0 && Tb_add_class_property.Text != "Дом" && Tb_add_class_property.Text != "Квартира" && Tb_add_class_property.Text != "Техническое помещение") errors.AppendLine("Введите корректное значение типа недвижимости!");
            if (Tb_add_price_property.Text.Length == 0) errors.AppendLine("Введите цену");
            if (Tb_add_property_condition.Text.Length == 0 && Tb_add_property_condition.Text != "Отличное" && Tb_add_property_condition.Text != "Хорошее" && Tb_add_property_condition.Text != "Удовлетворительное" && Tb_add_property_condition.Text != "Плохое") errors.AppendLine("Введите корректное значение типа недвижимости!");
            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                if (ClassData.DataProperty.property == null)
                {
                    _db.Propertys.Add(_property);
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

        private void Add_property_exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void Tb_add_property_condition_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
                    MessageBox.Show("Проверьте, правильно ли вы ввели данные.", "Ошибка ввода в поле 'Тип недвижимости'.", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                }
            }
        }
    }
}
