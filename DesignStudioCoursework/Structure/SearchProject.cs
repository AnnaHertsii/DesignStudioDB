using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class SearchProject
    {
        public void ShowProjectsByOption(DataGrid dataGrid_Project, ComboBox SearchProjectCombo, TextBox SearchProjectBox)
        {
            if (SearchProjectCombo.SelectedIndex == 0)
            {
                ShowProjectsByName(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 1)
            {
                ShowProjectsByPrice(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 2)
            {
                ShowProjectsByEmployee(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 3)
            {
                ShowProjectsByOrder(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 4)
            {
                ShowProjectsByClient(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 5)
            {
                ShowProjectsByInterior(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 6)
            {
                ShowProjectsByStyle(dataGrid_Project, SearchProjectBox);
            }
            else if (SearchProjectCombo.SelectedIndex == 7)
            {
                ShowProjectsByStatus(dataGrid_Project, SearchProjectBox);
            }
        }

        public void ShowProjectsByName(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where project.Project_name.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByPrice(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where project.Project_price.ToString().Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByEmployee(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where employee.Name.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByOrder(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where order.Description.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByClient(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where customer.Name.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByInterior(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where interior.Interior_type1.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByStyle(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where style.Style_name.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowProjectsByStatus(DataGrid dataGrid_Project, TextBox SearchProjectBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var projects = from project in db.Design_Project
                               join employee in db.Employee on project.Employee_Ref equals employee.Employee_ID
                               join order in db.Order on project.Order_Ref equals order.Order_ID
                               join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                               join interior in db.Interior_Type on project.Interior_type_Ref equals interior.Interior_type_ID
                               join style in db.Style on project.Style_Ref equals style.Style_ID
                               join status in db.Status on project.Project_status_Ref equals status.Status_ID
                               where status.Status1.Contains(SearchProjectBox.Text)
                               select new
                               {
                                   Name = project.Project_name,
                                   Price = project.Project_price,
                                   Employee = employee.Name,
                                   Order = order.Description,
                                   Customer = customer.Name,
                                   Interior = interior.Interior_type1,
                                   Style = style.Style_name,
                                   Status = status.Status1
                               };
                dataGrid_Project.ItemsSource = projects.ToList();
            }
        }
    }
}
