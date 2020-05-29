using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayEmployee
    {
        public void ShowEmployees(DataGrid dataGrid_Employee)
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
                dataGrid_Employee.ItemsSource = employees.ToList();
            }
        }
    }
}
