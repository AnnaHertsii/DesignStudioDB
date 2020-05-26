using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class SearchOrder
    {
        public void ShowOrdersByOption(DataGrid dataGrid_Order, ComboBox SearchOrderCombo, TextBox SearchOrderBox)
        {
            if (SearchOrderCombo.SelectedIndex == 0)
            {
                ShowOrdersByDescription(dataGrid_Order, SearchOrderBox);
            }
            else if (SearchOrderCombo.SelectedIndex == 1)
            {
                ShowOrdersByPrice(dataGrid_Order, SearchOrderBox);
            }
            else if (SearchOrderCombo.SelectedIndex == 2)
            {
                ShowOrdersByCustomer(dataGrid_Order, SearchOrderBox);
            }
        }

        private void ShowOrdersByDescription(DataGrid dataGrid_Order, TextBox SearchOrderBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var orders = from order in db.Order
                             join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                             join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                             join position in db.Position on employee.Position_Ref equals position.Position_ID
                             where order.Description.Contains(SearchOrderBox.Text)
                             select new
                             {
                                 order.Description,
                                 order.Start_date,
                                 order.End_date,
                                 order.Total_price,
                                 customer.Name,
                                 //employee.Name,
                                 position.Position_name

                             };
                dataGrid_Order.ItemsSource = orders.ToList();
            }
        }

        private void ShowOrdersByPrice(DataGrid dataGrid_Order, TextBox SearchOrderBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var orders = from order in db.Order
                             join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                             join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                             join position in db.Position on employee.Position_Ref equals position.Position_ID
                             where order.Total_price.ToString().Contains(SearchOrderBox.Text)
                             select new
                             {
                                 order.Description,
                                 order.Start_date,
                                 order.End_date,
                                 order.Total_price,
                                 customer.Name,
                                 //employee.Name,
                                 position.Position_name

                             };
                dataGrid_Order.ItemsSource = orders.ToList();
            }
        }

        private void ShowOrdersByCustomer(DataGrid dataGrid_Order, TextBox SearchOrderBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var orders = from order in db.Order
                             join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                             join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                             join position in db.Position on employee.Position_Ref equals position.Position_ID
                             where customer.Name.Contains(SearchOrderBox.Text)
                             select new
                             {
                                 order.Description,
                                 order.Start_date,
                                 order.End_date,
                                 order.Total_price,
                                 customer.Name,
                                 //employee.Name,
                                 position.Position_name

                             };
                dataGrid_Order.ItemsSource = orders.ToList();
            }
        }
    }
}
