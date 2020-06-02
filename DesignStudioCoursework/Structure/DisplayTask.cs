using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayTask
    {
        public void ShowTasks(DataGrid datagrid_Project)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                               join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                               join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                               join status in db.Status on task.Task_status_Ref equals status.Status_ID
                               select new
                               {
                                   Name = task.Task_name,
                                   task.Description,
                                   Start = task.Start_date,
                                   End = task.End_date,
                                   Employee = employee.Name,
                                   Project = project.Project_name,
                                   Status = status.Status1
                               };
                datagrid_Project.ItemsSource = tasks.ToList();
            }
        }
    }
}
