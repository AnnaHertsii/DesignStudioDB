using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class SearchTask
    {
        public void ShowTasksByOption(DataGrid dataGrid_Task, ComboBox SearchTaskCombo, TextBox SearchTaskBox, DatePicker MyDate)
        {
            if (SearchTaskCombo.SelectedIndex == 0)
            {
                ShowTasksByProject(dataGrid_Task, SearchTaskBox);
            }
            else if (SearchTaskCombo.SelectedIndex == 1)
            {
                ShowTasksByName(dataGrid_Task, SearchTaskBox);
            }
            else if (SearchTaskCombo.SelectedIndex == 2)
            {
                ShowTasksByDescription(dataGrid_Task, SearchTaskBox);
            }
            else if (SearchTaskCombo.SelectedIndex == 3)
            {
                ShowTasksByStartDate(dataGrid_Task, MyDate);
            }
            else if (SearchTaskCombo.SelectedIndex == 4)
            {
                ShowTasksByEndDate(dataGrid_Task, MyDate);
            }
            else if (SearchTaskCombo.SelectedIndex == 5)
            {
                ShowTasksByEmployee(dataGrid_Task, SearchTaskBox);
            }
            else if (SearchTaskCombo.SelectedIndex == 6)
            {
                ShowTasksByStatus(dataGrid_Task, SearchTaskBox);
            }

        }

        private void ShowTasksByProject(DataGrid dataGrid_Task, TextBox SearchTaskBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where project.Project_name.Contains(SearchTaskBox.Text)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByName(DataGrid dataGrid_Task, TextBox SearchTaskBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where task.Task_name.Contains(SearchTaskBox.Text)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByDescription(DataGrid dataGrid_Task, TextBox SearchTaskBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where task.Description.Contains(SearchTaskBox.Text)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByStartDate(DataGrid dataGrid_Task, DatePicker MyDate)
        {
            string formattedstart = null;
            DateTime? startDate = MyDate.SelectedDate;
            if (startDate.HasValue)
            {
                formattedstart = startDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where task.Start_date.ToString().Contains(formattedstart)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByEndDate(DataGrid dataGrid_Task, DatePicker MyDate)
        {
            string formattedend = null;
            DateTime? startDate = MyDate.SelectedDate;
            if (startDate.HasValue)
            {
                formattedend = startDate.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where task.End_date.ToString().Contains(formattedend)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByEmployee(DataGrid dataGrid_Task, TextBox SearchTaskBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where employee.Name.Contains(SearchTaskBox.Text)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }

        private void ShowTasksByStatus(DataGrid dataGrid_Task, TextBox SearchTaskBox)
        {
            using (var db = new DesignStudioEntities())
            {
                var tasks = from task in db.Task
                            join employee in db.Employee on task.Employee_Ref equals employee.Employee_ID
                            join project in db.Design_Project on task.Project_Ref equals project.Project_ID
                            join status in db.Status on task.Task_status_Ref equals status.Status_ID
                            where status.Status1.Contains(SearchTaskBox.Text)
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
                dataGrid_Task.ItemsSource = tasks.ToList();
            }
        }
    }
}
