using DesignStudioCoursework;
using DesignStudioCoursework.NewProject;
using System;
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

namespace DesignStudioCoursework
{
    public partial class NewProjectPage : Page
    {
        public List<Style> Styles { get; set; }
        String interiorType;
        private Action goBack;

        public NewProjectPage(Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            BindComboStyle();
        }

        public NewProjectPage(String val, Action goBack)
        {
            this.goBack = goBack;
            InitializeComponent();
            interiorType = val;
            this.Loaded += new RoutedEventHandler(Page2_Loaded);
            BindComboStyle();
        }

        void Page2_Loaded(object sender, RoutedEventArgs e)
        {
           interior.Text = interiorType;
           BitmapImage image = null;
           if (interiorType == "Кухня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/kitchen.jpg", UriKind.Relative));               
            }
            else if (interiorType == "Спальня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/bed2.png", UriKind.Relative));
            }
            else if (interiorType == "Вітальня")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/living.jpg", UriKind.Relative));
            }
            else if (interiorType == "Санвузол")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/toilet.jpg", UriKind.Relative));
            }
            else if (interiorType == "Кабінет")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/work.jpg", UriKind.Relative));
            }
            else if (interiorType == "Дитяча")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/child.jpg", UriKind.Relative));
            }
            else if (interiorType == "Гардероб")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/wardrobe.jpg", UriKind.Relative));
            }
            else if (interiorType == "Коридор")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/corridor.png", UriKind.Relative));
            }
            projectImage.Source = image;
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            goBack();
        }

        private void interior_Click(object sender, RoutedEventArgs e)
        {
            InteriorTypePage interior = new InteriorTypePage(goBack);
            this.NavigationService.Navigate(interior);
        }

        private void order_Click(object sender, RoutedEventArgs e)
        {
            OrdersWindow orders = new OrdersWindow(order);
            orders.Show();
        }

        private void employee_Click(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employees = new EmployeesWindow(employee);
            employees.Show();
        }

        private void BindComboStyle()
        {
            DesignStudioEntities db = new DesignStudioEntities();
            var items = db.Style.ToList();
            Styles = items;
            DataContext = Styles;
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (interior.Text == "")
                interior_error.Visibility = Visibility.Visible;
            else
            {
                interior_error.Visibility = Visibility.Hidden;
            }

            if (order.Text == "")
                order_error.Visibility = Visibility.Visible;
            else
            {
                order_error.Visibility = Visibility.Hidden;
            }

            if (employee.Text == "")
                employee_error.Visibility = Visibility.Visible;
            else
            {
                employee_error.Visibility = Visibility.Hidden;
            }

            if (name.Text.Length > 50)
                name_error.Visibility = Visibility.Visible;
            else
            {
                name_error.Visibility = Visibility.Hidden;
            }

            bool isDigit = true;
            foreach (char c in price.Text)
            {
                if (c < '0' || c > '9')
                    isDigit = false;
            }
            if (isDigit == false)
            {
                price_error.Visibility = Visibility.Visible;
            }
            else
            {
                price_error.Visibility = Visibility.Hidden;
            }

            if (style.SelectedIndex == -1)
                style_error.Visibility = Visibility.Visible;
            else
            {
                style_error.Visibility = Visibility.Hidden;
            }

            if ((interior.Text != "") && (order.Text != "") && (employee.Text != "") && (name.Text.Length < 50) && (isDigit != false) && (style.SelectedIndex != -1))
            {
                interior_error.Visibility = Visibility.Hidden;
                order_error.Visibility = Visibility.Hidden;
                employee_error.Visibility = Visibility.Hidden;
                name_error.Visibility = Visibility.Hidden;
                price_error.Visibility = Visibility.Hidden;
                style_error.Visibility = Visibility.Hidden;
                AddProject();
            }
        }

        public void AddProject()
        {
            try
            {
                int Project_id = MaxID() + 1;
                string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand();
                String Name = name.Text;
                int Order_id = getOrderID();
                int Interior_id = getInteriorID();
                int Employee_id = getEmployeeID();
                int Style_id = style.SelectedIndex + 1;
                int Status_id = status.SelectedIndex + 1;
                int totalprice = 0;
                if (price.Text != "")
                {
                    totalprice = Int32.Parse(price.Text);
                }

                string strSQL = string.Format("INSERT INTO [Design Project](Project_ID, Project_name, Project_price, Employee_Ref, Order_Ref, Interior_type_Ref, Style_Ref, Project_status_Ref) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}')",
                    Project_id, Name, totalprice, Employee_id, Order_id, Interior_id, Style_id, Status_id);
                    
                SqlCommand myCommand = new SqlCommand(strSQL, connection);
                myCommand.ExecuteNonQuery();

                MessageBox.Show("Нове замовлення успішно оформлено!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public int getOrderID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Order_ID FROM [Order] WHERE Description = '{0}'", order.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int getInteriorID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Interior_type_ID FROM [Interior Type] WHERE Interior_type = '{0}'", interior.Text);
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }

        public int getEmployeeID()
        {
            string connectionString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            string strSQL = string.Format("SELECT TOP 1 Employee_ID FROM [Employee] WHERE Name = '{0}'", employee.Text);
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
            string strSQL = "SELECT MAX(Project_ID) FROM [Design Project]";
            SqlCommand myCommand = new SqlCommand(strSQL, connection);
            SqlDataReader reader = myCommand.ExecuteReader();
            string st = null;
            if (reader.Read())
                st = reader[0].ToString();
            return Int32.Parse(st);
        }
    }
}
