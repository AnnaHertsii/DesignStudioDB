using DesignStudioCoursework.Structure;
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

namespace DesignStudioCoursework.NewProject
{
    /// <summary>
    /// Логика взаимодействия для EmployeesWindow.xaml
    /// </summary>
    public partial class EmployeesWindow : Window
    {
        SearchEmployee search = new SearchEmployee();
        TextBox employee;

        public EmployeesWindow(TextBox employee1)
        {
            InitializeComponent();
            employee = employee1;
        }

        private void FindEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            search.ShowEmployeesByOption(DataGridEmployee, combobox_option, search_text, birthdate);
        }

        private void combobox_option_DropDownClosed(object sender, EventArgs e)
        {
            if (combobox_option.SelectedIndex == 0)
            {
                birthdate.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 1)
            {
                birthdate.Visibility = System.Windows.Visibility.Visible;
            }
            else if (combobox_option.SelectedIndex == 2)
            {
                birthdate.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 3)
            {
                birthdate.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 4)
            {
                birthdate.Visibility = System.Windows.Visibility.Hidden;
            }
            else if (combobox_option.SelectedIndex == 5)
            {
                birthdate.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            ShowEmployees();
        }

        public void ShowEmployees()
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                DataGridEmployee.ItemsSource = employees.ToList();
            }
        }

        private void ChooseEmployeeButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = GetSelectedCellValue(0);
            employee.Text = Name;
            this.Close();
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridEmployee.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }
    }
}
