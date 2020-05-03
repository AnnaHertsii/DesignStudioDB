using System;
using System.Collections.Generic;
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

namespace DesignStudioCoursework.NewEmployee
{
    public partial class NewEmployeePage : Page
    {
        private Action goBack;

        public NewEmployeePage(Action goBack)
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

        private void ComboBox_Selected(object sender, RoutedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            String employee = selectedItem.Content.ToString();

            BitmapImage image = null;
            if (employee == "Проектувальник")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/projecter.png", UriKind.Relative));
            }
            else if (employee == "Художник")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/painter.png", UriKind.Relative));
            }
            else if (employee == "Дизайнер")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/designer.png", UriKind.Relative));
            }
            else if (employee == "Менеджер")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/manager.png", UriKind.Relative));
            }
            else if (employee == "Секретар")
            {
                image = new BitmapImage(new Uri("/DesignStudioCoursework;component/Resources/secreter.png", UriKind.Relative));
            }
            employeeType.Source = image;
        }
    }
}
