using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class SearchEmployee
    {
        public void ShowEmployeesByOption(DataGrid dataGrid_Employee, ComboBox SearchEmployeeCombo, TextBox SearchEmployeeBox, DatePicker birth)
        {
            if (SearchEmployeeCombo.SelectedIndex == 0)
            {
                ShowEmployeesByName(dataGrid_Employee, SearchEmployeeBox);
            }
            else if (SearchEmployeeCombo.SelectedIndex == 1)
            {
                ShowEmployeesByBirthdate(dataGrid_Employee, birth);
            }
            else if (SearchEmployeeCombo.SelectedIndex == 2)
            {
                ShowEmployeesByAdress(dataGrid_Employee, SearchEmployeeBox);
            }
            else if (SearchEmployeeCombo.SelectedIndex == 3)
            {
                ShowEmployeesByPhone(dataGrid_Employee, SearchEmployeeBox);
            }
            else if (SearchEmployeeCombo.SelectedIndex == 4)
            {
                ShowEmployeesByPassport(dataGrid_Employee, SearchEmployeeBox);
            }
            else if (SearchEmployeeCombo.SelectedIndex == 5)
            {
                ShowEmployeesByPosition(dataGrid_Employee, SearchEmployeeBox);
            }
        }

        private void ShowEmployeesByName(DataGrid dataGrid_Employee, TextBox SearchEmployeeBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where employee.Name.Contains(SearchEmployeeBox.Text)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesByBirthdate(DataGrid dataGrid_Employee, DatePicker MyDate)
        {
            string formatteddate = null;
            DateTime? Date = MyDate.SelectedDate;
            if (Date.HasValue)
            {
                formatteddate = Date.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where employee.Birth_date.ToString().Contains(formatteddate)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesByAdress(DataGrid dataGrid_Employee, TextBox SearchEmployeeBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where employee.Residence_place.Contains(SearchEmployeeBox.Text)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesByPhone(DataGrid dataGrid_Employee, TextBox SearchEmployeeBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where employee.Phone.Contains(SearchEmployeeBox.Text)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesByPassport(DataGrid dataGrid_Employee, TextBox SearchEmployeeBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where employee.Passport_number.Contains(SearchEmployeeBox.Text)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }

        private void ShowEmployeesByPosition(DataGrid dataGrid_Employee, TextBox SearchEmployeeBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var employees = from employee in db.Employee
                                join position in db.Position on employee.Position_Ref equals position.Position_ID
                                where position.Position_name.Contains(SearchEmployeeBox.Text)
                                select new
                                {
                                    employee.Name,
                                    Birthdate = employee.Birth_date,
                                    Adress = employee.Residence_place,
                                    employee.Phone,
                                    Passport = employee.Passport_number,
                                    Position = position.Position_name
                                };
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }
    }
}
