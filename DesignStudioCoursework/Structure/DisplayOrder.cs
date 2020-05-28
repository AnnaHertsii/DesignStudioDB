using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayOrder
    {
        public void ShowOrders(DataGrid dataGrid_Order)
        {
            using (var db = new DesignStudioEntities())
            {
                var orders = from order in db.Order
                             join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                             join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                             join position in db.Position on employee.Position_Ref equals position.Position_ID
                             select new
                             {
                                 order.Description,
                                 Start = order.Start_date,
                                 End = order.End_date,
                                 Price = order.Total_price,
                                 Customer = customer.Name,
                                 Employee = employee.Name,
                                 Position = position.Position_name
                             };
                dataGrid_Order.ItemsSource = orders.ToList();
            }
        }
    }
}
