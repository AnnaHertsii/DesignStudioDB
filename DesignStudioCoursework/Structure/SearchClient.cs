using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class SearchClient
    {
        public void ShowClientsByOption(DataGrid dataGrid_Client, ComboBox SearchClientCombo, TextBox SearchClientBox)
        {
            if (SearchClientCombo.SelectedIndex == 0)
            {
                ShowClientsByName(dataGrid_Client, SearchClientBox);
            }
            else if (SearchClientCombo.SelectedIndex == 1)
            {
                ShowClientsByPhone(dataGrid_Client, SearchClientBox);
            }
            else if (SearchClientCombo.SelectedIndex == 2)
            {
                ShowClientsByAdress(dataGrid_Client, SearchClientBox);
            }
            else if (SearchClientCombo.SelectedIndex == 3)
            {
                ShowClientsByMailAdress(dataGrid_Client, SearchClientBox);
            }
        }

        private void ShowClientsByName(DataGrid dataGrid_Client, TextBox SearchClientBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var clientQuery =  from customer in db.Customer                                  
                                   join type in db.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                   where customer.Name.Contains(SearchClientBox.Text)
                                   select new
                                   {
                                       customer.Customer_ID,
                                       customer.Name,
                                       customer.Phone,
                                       customer.Adress,
                                       customer.Mail_adress,
                                       type.Customer_type1
                                   };
                dataGrid_Client.ItemsSource = clientQuery.ToList();
            }
        }

        private void ShowClientsByPhone(DataGrid dataGrid_Client, TextBox SearchClientBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var clientQuery = from customer in db.Customer
                                  join type in db.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                  where customer.Phone.Contains(SearchClientBox.Text)
                                  select new
                                  {
                                      customer.Customer_ID,
                                      customer.Name,
                                      customer.Phone,
                                      customer.Adress,
                                      customer.Mail_adress,
                                      type.Customer_type1
                                  };
                dataGrid_Client.ItemsSource = clientQuery.ToList();
            }
        }

        private void ShowClientsByAdress(DataGrid dataGrid_Client, TextBox SearchClientBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var clientQuery = from customer in db.Customer
                                  join type in db.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                  where customer.Adress.Contains(SearchClientBox.Text)
                                  select new
                                  {
                                      customer.Customer_ID,
                                      customer.Name,
                                      customer.Phone,
                                      customer.Adress,
                                      customer.Mail_adress,
                                      type.Customer_type1
                                  };
                dataGrid_Client.ItemsSource = clientQuery.ToList();
            }
        }

        private void ShowClientsByMailAdress(DataGrid dataGrid_Client, TextBox SearchClientBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var clientQuery = from customer in db.Customer
                                  join type in db.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                  where customer.Mail_adress.Contains(SearchClientBox.Text)
                                  select new
                                  {
                                      customer.Customer_ID,
                                      customer.Name,
                                      customer.Phone,
                                      customer.Adress,
                                      customer.Mail_adress,
                                      type.Customer_type1
                                  };
                dataGrid_Client.ItemsSource = clientQuery.ToList();
            }
        }
    }
}
