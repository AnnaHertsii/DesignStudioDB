﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesignStudioCoursework.Review.ClientsReview
{
    public partial class ClientsPage : Page
    {
        private Action goBack;

        public ClientsPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
           }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void UpdateCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateCustomer();       
        }

        public void UpdateCustomer()
        {
            int index = Int16.Parse(GetSelectedCellValue(0));
            UpdateClientWindow updateClient = new UpdateClientWindow(index);
            updateClient.Show();
        }

        private void ShowCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            ShowCustomers();
        }

        public void ShowCustomers()
        {
            using (var Content = new DesignStudioEntities())
            {
                var customers = from customer in Content.Customer
                                join type in Content.Customer_Type on customer.Customer_type_Ref equals type.Customer_type_ID
                                select new
                                {
                                    customer.Customer_ID,
                                    customer.Name,
                                    customer.Phone,
                                    customer.Adress,
                                    customer.Mail_adress,
                                    type.Customer_type1
                                };
                DataGridCustomer.ItemsSource = customers.ToList();
            }
        }

        private void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteCustomer();
            ShowCustomers();
        }

        public void DeleteCustomer()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            int SelectedId = Int16.Parse(GetSelectedCellValue(0));
            string strSQL = string.Format("DELETE Customer WHERE Customer_ID = '{0}'", SelectedId);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            myCommand.ExecuteNonQuery();

            MessageBox.Show("Клієнта видалено!");
        }

        public string GetSelectedCellValue(int index)
        {
            DataGridCellInfo cellInfo = DataGridCustomer.SelectedCells[index];
            if (cellInfo == null) return null;

            DataGridBoundColumn column = cellInfo.Column as DataGridBoundColumn;
            if (column == null) return null;

            FrameworkElement element = new FrameworkElement() { DataContext = cellInfo.Item };
            BindingOperations.SetBinding(element, TagProperty, column.Binding);

            return element.Tag.ToString();
        }
    }
}
