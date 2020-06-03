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

namespace DesignStudioCoursework.Management.TaskManagement
{
    public partial class NewItemPage : Page
    {
        private Action goBack;

        public NewItemPage(Action goBack)
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

        private void task_Click(object sender, RoutedEventArgs e)
        {
            TasksWindow projects = new TasksWindow(task);
            projects.Show();
        }

        private void AddItemButton_Click(object sender, RoutedEventArgs e)
        {
            AddItem();
        }

        public void AddItem()
        {
            try
            {
                int item_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                int color_id = 7;
                int itemtype_id = itemtypecombo.SelectedIndex + 1;
                int amount1 = 0;
                if (amount.Text != "")
                {
                    amount1 = Int32.Parse(amount.Text);
                }

                string strSQL = string.Format("INSERT INTO [Item](Item_ID, Item_name, Item_amount, Item_type_Ref) VALUES ('{0}', '{1}', '{2}', '{3}')",
                    item_id, name.Text, amount1, itemtype_id);

                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Новий предмет додано до задачі!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int getTaskID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Task_ID FROM [Task] WHERE Task_name = '{0}'", task.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        } 

        public int MaxID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = "SELECT MAX(Item_ID) FROM [Item]";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
