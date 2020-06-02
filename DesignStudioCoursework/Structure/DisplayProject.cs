using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayProject
    {
        public void ShowProjects(DataGrid datagrid_Project)
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
                datagrid_Project.ItemsSource = projects.ToList();
            }
        }

        public void ShowNotOpenedProjects(DataGrid datagrid_Project)
        {
            using (var db = new DesignStudioEntities())
            {
                var subselect = (from project in db.Design_Project select project.Order_Ref).ToList();
                var result = from order in db.Order
                             join customer in db.Customer on order.Customer_Ref equals customer.Customer_ID
                             join employee in db.Employee on order.Employee_Ref equals employee.Employee_ID
                             join position in db.Position on employee.Position_Ref equals position.Position_ID
                             where !subselect.Contains(order.Order_ID)
                             select
                             new
                             {
                                 order.Description,
                                 Start = order.Start_date,
                                 End = order.End_date,
                                 Price = order.Total_price,
                                 Customer = customer.Name,
                                 Employee = employee.Name,
                                 Position = position.Position_name
                             };                 
                datagrid_Project.ItemsSource = result.ToList();
            }
        }
    }
}
