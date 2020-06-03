using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DesignStudioCoursework.Structure
{
    class DisplayItem
    {
        public void ShowItemsForTask(DataGrid datagrid_Item, int task_id)
        {
            string connString = @"Data Source=DESKTOP-O22ROGE;Initial Catalog=DesignStudio;Integrated Security=True";
            string query = "";
            if (task_id != 0)
            {
                query = string.Format("SELECT Item_name AS Name, Color,  Item_amount AS Amount, Item_type AS Type FROM Item JOIN[Item Type] ON Item.Item_type_Ref = Item_type_ID " +
    "JOIN[Item Color] ON Item.Item_ID = [Item Color].Item_Ref JOIN Color ON[Item Color].Color_Ref = Color.Color_ID JOIN[Task Item] ON Item.Item_ID = [Task Item].Item_Ref JOIN Task ON[Task Item].Task_Ref = Task.Task_ID WHERE Task_ID = '{0}'", task_id);
            }
            else
            {
                query = "SELECT Item_name AS Name, Color,  Item_amount AS Amount, Item_type AS Type, Task_name AS Task FROM Item JOIN[Item Type] ON Item.Item_type_Ref = Item_type_ID " +
"JOIN[Item Color] ON Item.Item_ID = [Item Color].Item_Ref JOIN Color ON[Item Color].Color_Ref = Color.Color_ID JOIN[Task Item] ON Item.Item_ID = [Task Item].Item_Ref JOIN Task ON[Task Item].Task_Ref = Task.Task_ID";
            }
            SqlConnection conn = new SqlConnection(connString);
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable("Item");
            da.Fill(dt);
            datagrid_Item.ItemsSource = dt.DefaultView;
        }
    }
}
