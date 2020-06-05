using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class ProjectLifeCycle
    {
        public void ShowProjectsByOption(DataGrid dataGrid_Project, ComboBox SearchProjectCombo)
        {
            if (SearchProjectCombo.SelectedIndex == 0)
            {
                ShowFutureProjects(dataGrid_Project);
            }
            else if (SearchProjectCombo.SelectedIndex == 1)
            {
                ShowCurrentProjects(dataGrid_Project);
            }
            else if (SearchProjectCombo.SelectedIndex == 2)
            {
                ShowDoneProjects(dataGrid_Project);
            }          
        }

        public void ShowFutureProjects(DataGrid dataGrid_Project)
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
                               where order.Start_date > DateTime.Now
                               select new
                               {
                                   Start = order.Start_date,
                                   End = order.End_date,
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

        public void ShowCurrentProjects(DataGrid dataGrid_Project)
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
                               where ((order.Start_date < DateTime.Now) && (order.End_date > DateTime.Now))
                               select new
                               {
                                   Start = order.Start_date,
                                   End = order.End_date,
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

        public void ShowDoneProjects(DataGrid dataGrid_Project)
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
                               where order.End_date < DateTime.Now
                               select new
                               {
                                   Start = order.Start_date,
                                   End = order.End_date,
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
