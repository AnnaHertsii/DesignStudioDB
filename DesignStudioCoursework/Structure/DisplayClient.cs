using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayClient
    {
        public void ShowCustomers(DataGrid dataGrid_Client)
        {
            using (var db = new DesignStudioEntities())
            {
                var customers = from customer in db.Customer
                                join type in db.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                select new
                                {
                                    customer.Customer_ID,
                                    customer.Name,
                                    customer.Phone,
                                    customer.Adress,
                                    customer.Mail_adress,
                                    type.Customer_type1
                                };
                dataGrid_Client.ItemsSource = customers.ToList();
            }
        }
    }
}
